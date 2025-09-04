#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    public class AuditOrganizationsClient : ApiClient, IAuditOrganizationsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Issue Events API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public AuditOrganizationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        [ManualRoute("GET", "/organizations/{org}/audit-log?phrase={phrase}")]
        public async Task<DateTime?> GetUserLastActivityDate(string organization, AuditLogPhraseOptions auditLogPhraseOptions)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(auditLogPhraseOptions, nameof(auditLogPhraseOptions));
            Ensure.ArgumentNotNullOrEmptyString(auditLogPhraseOptions.Repository, nameof(AuditLogPhraseOptions.Repository));
            Ensure.ArgumentNotNullOrEmptyString(auditLogPhraseOptions.User, nameof(AuditLogPhraseOptions.User));

            var options = new ApiOptions()
            {
                PageSize = 1
            };
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            Pagination.Setup(parameters, options);

            var phrase = auditLogPhraseOptions.BuildPhrase(organization);
            var auditLogs = await ApiConnection.Get<List<AuditLogEvent>>(ApiUrls.AuditLog(organization, phrase), parameters);

            if (!auditLogs.Any())
            {
                return null;
            }

            var auditLog = auditLogs.Single();

            var dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(auditLog.CreatedAt);
            var dateTime = dateTimeOffSet.DateTime;

            return dateTime;
        }

        [ManualRoute("GET", "/organizations/{org}/audit-log?phrase={phrase}")]
        public async Task<RepositoryVisibilityChangeEvent?> GetRepositoryVisibilityChangeLastEvent(string organization, AuditLogPhraseOptions auditLogPhraseOptions)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(auditLogPhraseOptions, nameof(auditLogPhraseOptions));
            Ensure.ArgumentNotNullOrEmptyString(auditLogPhraseOptions.Repository, nameof(AuditLogPhraseOptions.Repository));
            if (!string.IsNullOrWhiteSpace(auditLogPhraseOptions.User))
            {
                throw new ArgumentException("User is not supported", nameof(auditLogPhraseOptions.User));
            }

            var options = new ApiOptions()
            {
                PageSize = 100
            };

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            Pagination.Setup(parameters, options);

            var phrase = auditLogPhraseOptions.BuildPhrase(organization, "repo.access");
            var auditLogs = await ApiConnection.Get<List<AuditLogEvent>>(ApiUrls.AuditLog(organization, phrase), parameters);

            if (!auditLogs.Any())
            {
                return null;
            }

            RepositoryVisibilityChangeEvent? repositoryVisibilityChangeEvent = null;

            foreach (var auditLog in auditLogs)
            {
                repositoryVisibilityChangeEvent = GetRepositoryVisibilityChangeEvent(auditLog);
                if (repositoryVisibilityChangeEvent != null)
                {
                    break;
                }
            }

            return repositoryVisibilityChangeEvent;
        }

        [ManualRoute("GET", "/organizations/{org}/audit-log?phrase={phrase}")]
        public async Task<ForkRepositoryCreatedEvent?> GetRepositoryCreatedByForkLastEvent(string organization, AuditLogPhraseOptions auditLogPhraseOptions)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(auditLogPhraseOptions, nameof(auditLogPhraseOptions));
            Ensure.ArgumentNotNullOrEmptyString(auditLogPhraseOptions.Repository, nameof(AuditLogPhraseOptions.Repository));
            if (!string.IsNullOrWhiteSpace(auditLogPhraseOptions.User))
            {
                throw new ArgumentException("User is not supported", nameof(auditLogPhraseOptions.User));
            }

            var options = new ApiOptions()
            {
                PageSize = 100
            };

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            Pagination.Setup(parameters, options);

            var phrase = auditLogPhraseOptions.BuildPhrase(organization, "repo.create");
            var auditLogs = await ApiConnection.Get<List<AuditLogEvent>>(ApiUrls.AuditLog(organization, phrase), parameters);

            if (!auditLogs.Any())
            {
                return null;
            }

            ForkRepositoryCreatedEvent? forkRepositoryCreatedEvent = null;

            foreach (var auditLog in auditLogs)
            {
                forkRepositoryCreatedEvent = GetRepositoryCreatedByForkEvent(auditLog);
                if (forkRepositoryCreatedEvent != null)
                {
                    break;
                }
            }

            return forkRepositoryCreatedEvent;
        }

        private static ForkRepositoryCreatedEvent? GetRepositoryCreatedByForkEvent(AuditLogEvent auditLog)
        {
            if (auditLog.From != "tree#fork")
            {
                return null;
            }

            var dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(auditLog.CreatedAt);
            var created = dateTimeOffSet.DateTime;

            if (auditLog.Data is not JsonObject forkRepoData)
            {
                return null;
            }

            var visibility = GetVisibilityChange(forkRepoData, "visibility");
            GetDataItem<string>(forkRepoData, "fork_parent", out var forkPatentFullRepoName);
            GetDataItem<bool?>(forkRepoData, "public_repo", out var isPublic);

            return new ForkRepositoryCreatedEvent(
                auditLog.Actor,
                auditLog.ActorId,
                created,
                auditLog.Repo,
                auditLog.Org,
                forkPatentFullRepoName,
                isPublic,
                visibility
            );
        }

        private static RepositoryVisibilityChangeEvent? GetRepositoryVisibilityChangeEvent(AuditLogEvent auditLog)
        {
            if (auditLog.From != "edit_repositories#set_visibility")
            {
                return null;
            }

            var dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(auditLog.CreatedAt);
            var created = dateTimeOffSet.DateTime;
            var actor = auditLog.Actor;
            var actorId = auditLog.ActorId;

            if (auditLog.Data is not JsonObject visibilityChangeData)
            {
                return null;
            }

            var fromVisibility = GetVisibilityChange(visibilityChangeData, "previous_visibility");
            var toVisibility = GetVisibilityChange(visibilityChangeData, "visibility");

            if (fromVisibility == null || toVisibility == null)
            {
                return null;
            }

            return new RepositoryVisibilityChangeEvent(actor, actorId, created, fromVisibility.Value, toVisibility.Value);
        }

        private static RepositoryVisibility? GetVisibilityChange(JsonObject data, string visibilityKey)
        {
            if (!GetDataItem<string>(data, visibilityKey, out var visibility))
            {
                return null;
            }

            if (Enum.TryParse<RepositoryVisibility>(visibility, true, out var repositoryVisibility))
            {
                return repositoryVisibility;
            }

            return null;
        }

        private static bool GetDataItem<T>(JsonObject data, string key, out T? result)
        {
            result = default;
            if (!data.TryGetValue(key, out var value))
            {
                return false;
            }

            if (value is not T finalValue)
            {
                return false;
            }

            result = finalValue;
            return true;
        }
    }
}
