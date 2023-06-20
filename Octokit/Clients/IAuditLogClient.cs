namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Activity API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/enterprise-cloud@latest/organizations/keeping-your-organization-secure/managing-security-settings-for-your-organization/reviewing-the-audit-log-for-your-organization">Organization Audit Log API documentation</a> for more information.
    /// </remarks>
    public interface IAuditLogClient
    {
        /// <summary>
        /// Client for the Audit Log Organizations API
        /// </summary>
        public IAuditOrganizationsClient Organizations { get; }
    }
}
