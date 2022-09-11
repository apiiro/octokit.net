using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunEventPayload : ActivityWithInstallationIdPayload
    {
        public CheckRun CheckRun { get; protected set; }
        public CheckRunRequestedAction RequestedAction { get; protected set; }
    }
}
