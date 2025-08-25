using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryVisibilityChange
    {
        public RepositoryVisibilityChange() { }

        public RepositoryVisibilityChange(string user, DateTime created, RepositoryVisibility fromVisibility, RepositoryVisibility toVisibility)
        {
            User = user;
            Created = created;
            FromVisibility = fromVisibility;
            ToVisibility = toVisibility;
        }
        
        public string User { get; private set; }
        public DateTime Created { get; private set; }
        public RepositoryVisibility FromVisibility { get; private set; }
        public RepositoryVisibility ToVisibility { get; private set; }
        
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "User: {0}, Created: {1}, FromVisibility: {2}, ToVisibility:{3}", User, Created, FromVisibility, ToVisibility);
            }
        }
    }
}
