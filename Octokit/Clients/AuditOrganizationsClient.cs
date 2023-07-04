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
            if (auditLog == null) return null;
                    
            var dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(auditLog.CreatedAt);
            var dateTime = dateTimeOffSet.DateTime;
                    
            return dateTime;

        }
    }
}
