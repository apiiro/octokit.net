using System;

namespace Octokit
{
    public class ForkRepositoryCreatedEvent
    {
        public ForkRepositoryCreatedEvent()
        {
        }

        public ForkRepositoryCreatedEvent(
            string actor,
            long? actorId,
            DateTime created,
            string fullRepoName,
            string organizationName,
            string forkPatentFullRepoName,
            bool? isPublic,
            RepositoryVisibility? visibility
        )
        {
            Actor = actor;
            ActorId = actorId;
            Created = created;
            FullRepoName = fullRepoName;
            OrganizationName = organizationName;
            RepoName = fullRepoName[fullRepoName.IndexOf(organizationName, StringComparison.InvariantCultureIgnoreCase)..];
            ForkPatentFullRepoName = forkPatentFullRepoName;
            IsPublic = isPublic;
            Visibility = visibility;
        }

        public string Actor { get; private set; }
        public long? ActorId { get; private set; }
        public DateTime Created { get; private set; }
        public string FullRepoName { get; private set; }
        public string RepoName { get; private set; }
        public string OrganizationName { get; private set; }
        public string ForkPatentFullRepoName { get; private set; }
        public bool? IsPublic { get; private set; }
        public RepositoryVisibility? Visibility { get; private set; }

        public override string ToString()
        {
            return $"Actor: {Actor}, ActorId: {ActorId}, Created: {Created}, FullRepoName: {FullRepoName}, RepoName: {RepoName}, " +
                   $"OrganizationName: {OrganizationName}, ForkPatentFullRepoName: {ForkPatentFullRepoName}, IsPublic: {IsPublic}, Visibility: {Visibility}";
        }
    }
}
