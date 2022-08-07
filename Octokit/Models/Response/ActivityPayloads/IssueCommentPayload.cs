using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueCommentPayload : ActivityWithActionPayload
    {
        // should always be "created" according to github api docs
        public Issue Issue { get; protected set; }
        public IssueComment Comment { get; protected set; }
    }
}
