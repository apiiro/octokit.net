using System;
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
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            var options = new ApiOptions()
            {
                PageCount = 1,
                PageSize = 1
            };
            var auditLogs = await ApiConnection.GetAll<AuditLogActivity>(ApiUrls.AuditLog(organization, repository, user), options);
            
            if (!auditLogs.Any()) return null;
            
            var auditLog = auditLogs.Single();
                    
            var dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(auditLog.CreatedAt);
            var dateTime = dateTimeOffSet.DateTime;
                    
            return dateTime;

        }
    }
}
