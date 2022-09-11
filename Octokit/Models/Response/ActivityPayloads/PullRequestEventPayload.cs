using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestEventPayload : ActivityWithInstallationIdPayload
    {
        public int Number { get; protected set; }

        public PullRequest PullRequest { get; protected set; }
    }
}
