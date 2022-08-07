using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestCommentPayload : ActivityWithActionPayload
    {
        public PullRequest PullRequest { get; protected set; }
        public PullRequestReviewComment Comment { get; protected set; }
    }
}
