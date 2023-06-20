namespace Octokit
{
    public class AuditLogClient : ApiClient, IAuditLogClient
    {
        public AuditLogClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Organizations = new AuditOrganizationsClient(apiConnection);
        }

        /// <summary>
        /// Client for the Organization audit log
        /// </summary>
        public IAuditOrganizationsClient Organizations { get; }
    }
}
