using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseEventPayload : ActivityWithInstallationIdPayload
    {
        public Release Release { get; protected set; }
    }
}
