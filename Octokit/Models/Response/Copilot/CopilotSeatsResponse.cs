using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit.Copilot
{
    /// <summary>
    /// Represents a Copilot seats response on GitHub.
    /// </summary>
    [ExcludeFromCtorWithAllPropertiesConventionTest(nameof(Type))]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
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
        
        internal string DebuggerDisplay =>
            string.Format(CultureInfo.InvariantCulture,
                "TotalSeats: {0}", TotalSeats);
    }
}
