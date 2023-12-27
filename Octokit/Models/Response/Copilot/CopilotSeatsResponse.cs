using System.Collections.Generic;

namespace Octokit.Copilot
{
    /// <summary>
    /// Represents a Copilot seats response on GitHub.
    /// </summary>
    public class CopilotSeatsResponse
    {
        public CopilotSeatsResponse()
        {
        }

        public CopilotSeatsResponse(int totalSeats, IReadOnlyList<CopilotSeat> seats)
        {
            TotalSeats = totalSeats;
            Seats = seats;
        }

        public int TotalSeats { get; private set;}

        public IReadOnlyList<CopilotSeat> Seats { get; private set;}
    }
}
