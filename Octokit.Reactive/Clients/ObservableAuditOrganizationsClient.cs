using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive;

public class ObservableAuditOrganizationsClient : IObservableAuditOrganizationsClient
{
    readonly IAuditOrganizationsClient _client;
    public ObservableAuditOrganizationsClient(IGitHubClient client)
    {
        _client = client.AuditLog.Organizations;
    }

    public IObservable<DateTime?> GetUserLastActivityDate(string organization, AuditLogPhraseOptions phraseOptions)
    {
        return _client.GetUserLastActivityForRepositoryDate(organization, phraseOptions).ToObservable();
    }
}