using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit.Copilot
{
    [ExcludeFromCtorWithAllPropertiesConventionTest(nameof(Type))]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
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
        
        internal string DebuggerDisplay =>
            string.Format(CultureInfo.InvariantCulture,
                "User: Id: {0} Login: {1}", Assignee.Id, Assignee.Login);
    }
}
