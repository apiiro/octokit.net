#nullable enable
using System;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IAuditOrganizationsClient
    {
        /// <summary>
        /// Gets user last activity date for a repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="organization">The organization</param>
        /// <param name="auditLogPhraseOptions">The query phrase options</param>
        Task<DateTime?> GetUserLastActivityDate(string organization, AuditLogPhraseOptions auditLogPhraseOptions);
        
        /// <summary>
        /// Gets last visibility change event for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="organization">The organization</param>
        /// <param name="auditLogPhraseOptions">The query phrase options</param>
        Task<RepositoryVisibilityChangeEvent?> GetRepositoryVisibilityChangeLastEvent(string organization, AuditLogPhraseOptions auditLogPhraseOptions);

        /// <summary>
        /// Gets last visibility change event for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="organization">The organization</param>
        /// <param name="auditLogPhraseOptions">The query phrase options</param>
        Task<ForkRepositoryCreatedEvent?> GetRepositoryCreatedByForkLastEvent(string organization, AuditLogPhraseOptions auditLogPhraseOptions);
    }
}
