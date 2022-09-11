using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueEventPayload : ActivityWithActionPayload
    {
        public Issue Issue { get; protected set; }
    }
}
