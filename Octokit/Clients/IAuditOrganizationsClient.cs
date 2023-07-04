using System;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IAuditOrganizationsClient
    {
        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="organization">The organization</param>
        /// <param name="auditLogPhraseOptions">The query phrase options</param>
        Task<DateTime?> GetUserLastActivityDate(string organization, AuditLogPhraseOptions auditLogPhraseOptions);        
    }
}
