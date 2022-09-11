using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuiteEventPayload : ActivityWithActionPayload
    {
        public CheckSuite CheckSuite { get; protected set; }
    }
}
