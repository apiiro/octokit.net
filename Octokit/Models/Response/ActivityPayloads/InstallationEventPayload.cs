using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InstallationEventPayload : ActivityWithActionPayload
    {
        public IReadOnlyCollection<Repository> Repositories { get; set; }
    }
}
