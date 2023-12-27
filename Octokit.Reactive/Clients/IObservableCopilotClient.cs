using System;
using Octokit.Copilot;

namespace Octokit.Reactive;

public interface IObservableCopilotClient
{
    IObservable<CopilotSeatsResponse> GetAllSeats(string org);
}
