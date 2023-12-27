using System;

namespace Octokit.Copilot
{
    public class CopilotSeat
    {
        public CopilotSeat() {}

        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }

        public CopilotAssignee Assignee { get; set; }
    }
}
