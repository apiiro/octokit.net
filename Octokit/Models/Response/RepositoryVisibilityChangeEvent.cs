using System;

namespace Octokit
{
    public class RepositoryVisibilityChangeEvent
    {
        public RepositoryVisibilityChangeEvent()
        {
        }

        public RepositoryVisibilityChangeEvent(string actor, long? actorId, DateTime created, RepositoryVisibility fromVisibility, RepositoryVisibility toVisibility)
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

        public override string ToString()
        {
            return $"Actor: {Actor}, ActorId: {ActorId}, Created: {Created}, FromVisibility: {FromVisibility}, ToVisibility: {ToVisibility}";
        }
    }
}
