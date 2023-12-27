using System;
using System.Reactive.Threading.Tasks;
using Octokit.Copilot;

namespace Octokit.Reactive;

public class ObservableICopilotClient : IObservableICopilotClient
{
    private readonly ICopilotClient _client;
    public ObservableICopilotClient(IGitHubClient client)
    {
        _client = client.Organization.Copilot;
    }
    
    public IObservable<CopilotSeatsResponse> GetAllSeats(string org)
    {
        return _client.GetAllSeats(org).ToObservable();
    }
}
