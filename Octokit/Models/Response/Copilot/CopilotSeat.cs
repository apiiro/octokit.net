using System;

namespace Octokit.Copilot
{
    public class CopilotSeat
    {
        public CopilotSeat() {}

        public CopilotSeat(DateTime createdAt, DateTime updatedAt, User assignee)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Assignee = assignee;
        }

        public DateTime CreatedAt { get; private set; }
        
        public DateTime UpdatedAt { get; private set; }

        public User Assignee { get; private set; }
    }
}
