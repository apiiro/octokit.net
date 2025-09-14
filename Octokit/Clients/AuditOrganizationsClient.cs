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

        [ManualRoute("GET", "/organizations/{org}")]
        public async Task<DateTime?> GetLastActivityDate(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            return await GetLastActivityDateImpl(organization);
        }

        [ManualRoute("GET", "/organizations/{org}/audit-log?phrase={phrase}")]
        public async Task<DateTime?> GetUserLastActivityForRepositoryDate(string organization, AuditLogPhraseOptions auditLogPhraseOptions)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(auditLogPhraseOptions, nameof(auditLogPhraseOptions));
            Ensure.ArgumentNotNullOrEmptyString(auditLogPhraseOptions.Repository, nameof(AuditLogPhraseOptions.Repository));
            Ensure.ArgumentNotNullOrEmptyString(auditLogPhraseOptions.User, nameof(AuditLogPhraseOptions.User));

            return await GetLastActivityDateImpl(organization, auditLogPhraseOptions);
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

            var options = new ApiOptions
            {
                PageSize = 10
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
        public async Task<RepositoryCreatedEvent?> GetRepositoryCreatedLastEvent(string organization, AuditLogPhraseOptions auditLogPhraseOptions)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(auditLogPhraseOptions, nameof(auditLogPhraseOptions));
            Ensure.ArgumentNotNullOrEmptyString(auditLogPhraseOptions.Repository, nameof(AuditLogPhraseOptions.Repository));
            if (!string.IsNullOrWhiteSpace(auditLogPhraseOptions.User))
            {
                throw new ArgumentException("User is not supported", nameof(auditLogPhraseOptions.User));
            }

            var options = new ApiOptions
            {
                PageSize = 1
            };

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            Pagination.Setup(parameters, options);

            var phrase = auditLogPhraseOptions.BuildPhrase(organization, "repo.create");
            var auditLogs = await ApiConnection.Get<List<AuditLogEvent>>(ApiUrls.AuditLog(organization, phrase), parameters);

            if (!auditLogs.Any())
            {
                return null;
            }

            var auditLog = auditLogs.Single();
            var repositoryCreatedEvent = GetRepositoryCreatedEvent(auditLog);

            return repositoryCreatedEvent;
        }

        private async Task<DateTime?> GetLastActivityDateImpl(string organization, AuditLogPhraseOptions? auditLogPhraseOptions = null)
        {
            var options = new ApiOptions
            {
                PageSize = 1
            };
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            Pagination.Setup(parameters, options);

            var phrase = auditLogPhraseOptions?.BuildPhrase(organization);
            var auditLogs = await ApiConnection.Get<List<AuditLogEvent>>(ApiUrls.AuditLog(organization, phrase), parameters);

            if (!auditLogs.Any())
            {
                return null;
            }

            var auditLog = auditLogs.Single();

            var dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(auditLog.CreatedAt);
            var dateTime = dateTimeOffSet.UtcDateTime;

            return dateTime;
        }

        private static RepositoryCreatedEvent GetRepositoryCreatedEvent(AuditLogEvent auditLog)
        {
            var dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(auditLog.CreatedAt);
            var created = dateTimeOffSet.UtcDateTime;

            var forkRepoData = auditLog.Data as JsonObject; // This data is returned from OnPrem instances

            var visibility = GetRepositoryVisibility(auditLog.Visibility) ?? GetVisibilityChange(forkRepoData, "visibility");
            var isPublic = auditLog.PublicRepo ?? GetDataItem<bool?>(forkRepoData, "public_repo");

            return new RepositoryCreatedEvent(
                auditLog.Actor,
                auditLog.ActorId,
                created,
                auditLog.Repo,
                auditLog.Org,
                isPublic,
                visibility
            );
        }

        private static RepositoryVisibilityChangeEvent? GetRepositoryVisibilityChangeEvent(AuditLogEvent auditLog)
        {
            var dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(auditLog.CreatedAt);
            var created = dateTimeOffSet.UtcDateTime;
            var actor = auditLog.Actor;
            var actorId = auditLog.ActorId;

            var visibilityChangeData = auditLog.Data as JsonObject; // This data is returned from OnPrem instances

            /*
             * Audit logs responses are not returned in the same schema format from SAAS and On Prem instances
             * AuditLog's PreviousVisibility and Visibility values returned from SAAS instances but not from OnPrem instances
             * These values are found in on prem responses, in a separate json object
             */
            var fromVisibility = GetRepositoryVisibility(auditLog.PreviousVisibility) ?? GetVisibilityChange(visibilityChangeData, "previous_visibility");
            var toVisibility = GetRepositoryVisibility(auditLog.Visibility) ?? GetVisibilityChange(visibilityChangeData, "visibility");

            if (fromVisibility == null || toVisibility == null)
            {
                return null;
            }

            return new RepositoryVisibilityChangeEvent(actor, actorId, created, fromVisibility.Value, toVisibility.Value);
        }

        private static RepositoryVisibility? GetVisibilityChange(JsonObject? data, string visibilityKey)
        {
            var visibility = GetDataItem<string>(data, visibilityKey);
            return string.IsNullOrEmpty(visibility) ? null : GetRepositoryVisibility(visibility);
        }

        private static RepositoryVisibility? GetRepositoryVisibility(string? visibility)
        {
            if (Enum.TryParse<RepositoryVisibility>(visibility, true, out var repositoryVisibility))
            {
                return repositoryVisibility;
            }

            return null;
        }

        private static T? GetDataItem<T>(JsonObject? data, string key)
        {
            if (data == null || !data.TryGetValue(key, out var value))
            {
                return default;
            }

            return value is T finalValue ? finalValue : default;
        }
    }
}
