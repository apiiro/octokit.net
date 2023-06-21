namespace Octokit.Reactive;

public class ObservableAuditLogClient : IObservableAuditLogClient
{
    
    public ObservableAuditLogClient(IGitHubClient client)
    {
        Ensure.ArgumentNotNull(client, nameof(client));
        
        Organizations = new ObservableAuditOrganizationsClient(client);
    }
    public IObservableAuditOrganizationsClient Organizations { get; }
}