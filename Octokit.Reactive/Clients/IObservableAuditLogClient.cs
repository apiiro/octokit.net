namespace Octokit.Reactive;

public interface IObservableAuditLogClient
{
    public IObservableAuditOrganizationsClient Organizations { get; }
}