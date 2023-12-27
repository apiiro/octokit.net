using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Copilot;

namespace Octokit
{
    public interface ICopilotClient
    {
        Task<CopilotSeatsResponse> GetAllSeats(string org);
    }
}
