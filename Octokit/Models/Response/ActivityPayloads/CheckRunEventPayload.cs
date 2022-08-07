using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunEventPayload : ActivityWithActionPayload
    {
        public CheckRun CheckRun { get; protected set; }
        public CheckRunRequestedAction RequestedAction { get; protected set; }
    }
}
