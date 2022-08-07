using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestEventPayload : ActivityWithActionPayload
    {
        public int Number { get; protected set; }

        public PullRequest PullRequest { get; protected set; }
    }
}
