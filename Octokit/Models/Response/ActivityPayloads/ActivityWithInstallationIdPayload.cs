using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ActivityWithInstallationIdPayload : ActivityWithActionPayload
    {
        public InstallationId Installation { get; set; }
    }
}
