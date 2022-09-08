using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InstallationEventPayload
    {
        public string Action { get; set; }

        public User Sender { get; set; }

        public IReadOnlyCollection<Repository> Repositories { get; set; }

        public Installation Installation { get; set; }
    }
}
