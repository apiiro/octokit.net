using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ActivityPayload
    {
        public ActivityPayload() { }


        public ActivityPayload(Repository repository, User sender)
        {
            Repository = repository;
            Sender = sender;
        }

        public Repository Repository { get; protected set; }
        public User Sender { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return Repository?.FullName; }
        }
    }
}
