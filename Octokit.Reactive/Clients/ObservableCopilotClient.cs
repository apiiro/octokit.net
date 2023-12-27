using System;
using System.Reactive.Threading.Tasks;
using Octokit.Copilot;

namespace Octokit.Reactive;

public class ObservableCopilotClient : IObservableCopilotClient
{
    private readonly ICopilotClient _client;
    public ObservableCopilotClient(IGitHubClient client)
    {
        _client = client.Organization.Copilot;
    }
    
    public IObservable<CopilotSeatsResponse> GetAllSeats(string org)
    {
        return _client.GetAllSeats(org).ToObservable();
    }
}
