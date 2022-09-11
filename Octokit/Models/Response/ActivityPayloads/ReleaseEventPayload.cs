using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseEventPayload : ActivityWithActionPayload
    {
        public Release Release { get; protected set; }
    }
}
