using System;
using System.Reactive;

namespace Octokit.Reactive;

public interface IObservableAuditOrganizationsClient
{
    IObservable<DateTime?> GetUserLastActivityDate(string organization, string repository, string user);
}