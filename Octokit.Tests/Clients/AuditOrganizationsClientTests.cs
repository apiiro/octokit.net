using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class AuditOrganizationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new AuditOrganizationsClient(null));
            }
        }

        public class TheAuditLogQueryMethods
        {
            private static AuditOrganizationsClient CreateClientReturningNullResponse()
            {
                var connection = Substitute.For<IApiConnection>();
                connection.Get<List<AuditLogEvent>>(Arg.Any<Uri>(), Arg.Any<IDictionary<string, string>>())
                    .Returns(Task.FromResult<List<AuditLogEvent>>(null));
                return new AuditOrganizationsClient(connection);
            }

            [Fact]
            public async Task GetRepositoryVisibilityChangeLastEventReturnsNullWhenResponseIsNull()
            {
                var client = CreateClientReturningNullResponse();

                var result = await client.GetRepositoryVisibilityChangeLastEvent("org", new AuditLogPhraseOptions { Repository = "repo" });

                Assert.Null(result);
            }

            [Fact]
            public async Task GetRepositoryCreatedLastEventReturnsNullWhenResponseIsNull()
            {
                var client = CreateClientReturningNullResponse();

                var result = await client.GetRepositoryCreatedLastEvent("org", new AuditLogPhraseOptions { Repository = "repo" });

                Assert.Null(result);
            }

            [Fact]
            public async Task GetUserLastActivityForRepositoryDateReturnsNullWhenResponseIsNull()
            {
                var client = CreateClientReturningNullResponse();

                var result = await client.GetUserLastActivityForRepositoryDate("org", new AuditLogPhraseOptions { Repository = "repo", User = "user" });

                Assert.Null(result);
            }
        }
    }
}
