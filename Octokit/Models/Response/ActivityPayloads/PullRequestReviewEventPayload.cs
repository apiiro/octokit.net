using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewEventPayload : ActivityWithActionPayload
    {
        public PullRequest PullRequest { get; protected set; }
        public PullRequestReview Review { get; protected set; }
    }
}
