using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryVisibilityChange
    {
        public RepositoryVisibilityChange() { }

        public RepositoryVisibilityChange(string actor, long? actorId, DateTime created, RepositoryVisibility fromVisibility, RepositoryVisibility toVisibility)
        {
            Actor = actor;
            ActorId = actorId;
            Created = created;
            FromVisibility = fromVisibility;
            ToVisibility = toVisibility;
        }
        
        public string Actor { get; private set; }
        public long? ActorId { get; private set; }
        public DateTime Created { get; private set; }
        public RepositoryVisibility FromVisibility { get; private set; }
        public RepositoryVisibility ToVisibility { get; private set; }
        
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Actor: {0}, ActorId: {2}, Created: {3}, FromVisibility: {4}, ToVisibility:{5}", 
                    Actor, ActorId, Created, FromVisibility, ToVisibility);
            }
        }
    }
}
