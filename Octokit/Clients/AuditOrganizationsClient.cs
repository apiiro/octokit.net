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
        public async Task<RepositoryVisibilityChange?> GetLastRepositoryVisibilityChange(string organization, AuditLogPhraseOptions auditLogPhraseOptions)
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

            return new RepositoryVisibilityChange(actor, actorId, created, fromVisibility.Value, toVisibility.Value);

            RepositoryVisibility? GetVisibilityChange(JsonObject visibilityChangeDataJsonObject, string visibilityKey)
            {
                if (!visibilityChangeDataJsonObject.TryGetValue(visibilityKey, out var visibility))
                {
                    return null;
                }

                if (Enum.TryParse<RepositoryVisibility>(visibility.ToString(), true, out var repositoryVisibility))
                {
                    return repositoryVisibility;
                }

                return null;
            }
        }
    }
}
