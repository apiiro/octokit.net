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
        
        [ManualRoute("GET", "/organizations/{org}/audit-log?phrase=actor:{user}+repo:{org}/{repo}")]
        public async Task<DateTime?> GetUserLastActivityDate(string organization, string repository, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            var options = new ApiOptions()
            {
                PageSize = 1
            };
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            Pagination.Setup(parameters, options);
            var auditLogs = await ApiConnection.Get<List<AuditLogEvent>>(ApiUrls.AuditLog(organization, repository, user), parameters);

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
