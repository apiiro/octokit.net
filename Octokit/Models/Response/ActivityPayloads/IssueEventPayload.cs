using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueEventPayload : ActivityWithInstallationIdPayload
    {
        public Issue Issue { get; protected set; }
    }
}
