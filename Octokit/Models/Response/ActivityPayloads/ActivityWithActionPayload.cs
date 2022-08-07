using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ActivityWithActionPayload : ActivityPayload
    {
        public string Action { get; protected set; }
    }
}
