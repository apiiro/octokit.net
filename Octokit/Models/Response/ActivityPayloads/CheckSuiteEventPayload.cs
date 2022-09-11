using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuiteEventPayload : ActivityWithInstallationIdPayload
    {
        public CheckSuite CheckSuite { get; protected set; }
    }
}
