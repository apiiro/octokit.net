using System.Collections.Generic;

namespace Octokit.Copilot
{
    /// <summary>
    /// Represents a Copilot seats response on GitHub.
    /// </summary>
    public class CopilotSeatsResponse
    {
        public CopilotSeatsResponse() {}

        public int TotalSeats { get; set; }

        public IReadOnlyList<CopilotSeat> Seats { get; set; }
    }
}
