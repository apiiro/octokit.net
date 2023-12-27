using System;
using Octokit.Copilot;

namespace Octokit.Reactive;

public interface IObservableICopilotClient
{
    IObservable<CopilotSeatsResponse> GetAllSeats(string org);
}
