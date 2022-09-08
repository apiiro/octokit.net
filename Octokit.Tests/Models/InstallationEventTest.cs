using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models;

public class InstallationEventTest
{
    [Fact]
    public void CanBeDeserialized()
    {
        const string json = @"{
  ""action"": ""deleted"",
  ""installation"": {
    ""id"": 29024610,
    ""account"": {
      ""login"": ""tomer-apiiro-test"",
      ""id"": 64733398,
      ""node_id"": ""MDEyOk9yZ2FuaXphdGlvbjY0NzMzMzk4"",
      ""avatar_url"": ""https://avatars.githubusercontent.com/u/64733398?v=4"",
      ""gravatar_id"": """",
      ""url"": ""https://api.github.com/users/tomer-apiiro-test"",
      ""html_url"": ""https://github.com/tomer-apiiro-test"",
      ""followers_url"": ""https://api.github.com/users/tomer-apiiro-test/followers"",
      ""following_url"": ""https://api.github.com/users/tomer-apiiro-test/following{/other_user}"",
      ""gists_url"": ""https://api.github.com/users/tomer-apiiro-test/gists{/gist_id}"",
      ""starred_url"": ""https://api.github.com/users/tomer-apiiro-test/starred{/owner}{/repo}"",
      ""subscriptions_url"": ""https://api.github.com/users/tomer-apiiro-test/subscriptions"",
      ""organizations_url"": ""https://api.github.com/users/tomer-apiiro-test/orgs"",
      ""repos_url"": ""https://api.github.com/users/tomer-apiiro-test/repos"",
      ""events_url"": ""https://api.github.com/users/tomer-apiiro-test/events{/privacy}"",
      ""received_events_url"": ""https://api.github.com/users/tomer-apiiro-test/received_events"",
      ""type"": ""Organization"",
      ""site_admin"": false
    },
    ""repository_selection"": ""all"",
    ""access_tokens_url"": ""https://api.github.com/app/installations/29024610/access_tokens"",
    ""repositories_url"": ""https://api.github.com/installation/repositories"",
    ""html_url"": ""https://github.com/organizations/tomer-apiiro-test/settings/installations/29024610"",
    ""app_id"": 111651,
    ""app_slug"": ""apiiro"",
    ""target_id"": 64733398,
    ""target_type"": ""Organization"",
    ""permissions"": {
      ""checks"": ""write"",
      ""issues"": ""read"",
      ""contents"": ""read"",
      ""metadata"": ""read"",
      ""packages"": ""read"",
      ""deployments"": ""read"",
      ""pull_requests"": ""write"",
      ""administration"": ""read"",
      ""security_events"": ""read"",
      ""vulnerability_alerts"": ""read"",
      ""secret_scanning_alerts"": ""read"",
      ""organization_user_blocking"": ""read"",
      ""organization_administration"": ""read""
    },
    ""events"": [
      ""check_run"",
      ""check_suite"",
      ""pull_request""
    ],
    ""created_at"": ""2022-09-08T13:16:41.000Z"",
    ""updated_at"": ""2022-09-08T13:16:41.000Z"",
    ""single_file_name"": null,
    ""has_multiple_single_files"": false,
    ""single_file_paths"": [

    ],
    ""suspended_by"": null,
    ""suspended_at"": null
  },
  ""repositories"": [
    {
      ""id"": 260900357,
      ""node_id"": ""MDEwOlJlcG9zaXRvcnkyNjA5MDAzNTc="",
      ""name"": ""test"",
      ""full_name"": ""tomer-apiiro-test/test"",
      ""private"": true
    },
    {
      ""id"": 277490362,
      ""node_id"": ""MDEwOlJlcG9zaXRvcnkyNzc0OTAzNjI="",
      ""name"": ""Apiiro_test"",
      ""full_name"": ""tomer-apiiro-test/Apiiro_test"",
      ""private"": true
    },
    {
      ""id"": 279815866,
      ""node_id"": ""MDEwOlJlcG9zaXRvcnkyNzk4MTU4NjY="",
      ""name"": ""lic"",
      ""full_name"": ""tomer-apiiro-test/lic"",
      ""private"": false
    },
    {
      ""id"": 294123711,
      ""node_id"": ""MDEwOlJlcG9zaXRvcnkyOTQxMjM3MTE="",
      ""name"": ""empty-repo"",
      ""full_name"": ""tomer-apiiro-test/empty-repo"",
      ""private"": true
    },
    {
      ""id"": 397634362,
      ""node_id"": ""MDEwOlJlcG9zaXRvcnkzOTc2MzQzNjI="",
      ""name"": ""dotnet-test"",
      ""full_name"": ""tomer-apiiro-test/dotnet-test"",
      ""private"": false
    },
    {
      ""id"": 418169292,
      ""node_id"": ""R_kgDOGOzBzA"",
      ""name"": ""node-test"",
      ""full_name"": ""tomer-apiiro-test/node-test"",
      ""private"": true
    }
  ],
  ""sender"": {
    ""login"": ""tomer-amir"",
    ""id"": 63953532,
    ""node_id"": ""MDQ6VXNlcjYzOTUzNTMy"",
    ""avatar_url"": ""https://avatars.githubusercontent.com/u/63953532?v=4"",
    ""gravatar_id"": """",
    ""url"": ""https://api.github.com/users/tomer-amir"",
    ""html_url"": ""https://github.com/tomer-amir"",
    ""followers_url"": ""https://api.github.com/users/tomer-amir/followers"",
    ""following_url"": ""https://api.github.com/users/tomer-amir/following{/other_user}"",
    ""gists_url"": ""https://api.github.com/users/tomer-amir/gists{/gist_id}"",
    ""starred_url"": ""https://api.github.com/users/tomer-amir/starred{/owner}{/repo}"",
    ""subscriptions_url"": ""https://api.github.com/users/tomer-amir/subscriptions"",
    ""organizations_url"": ""https://api.github.com/users/tomer-amir/orgs"",
    ""repos_url"": ""https://api.github.com/users/tomer-amir/repos"",
    ""events_url"": ""https://api.github.com/users/tomer-amir/events{/privacy}"",
    ""received_events_url"": ""https://api.github.com/users/tomer-amir/received_events"",
    ""type"": ""User"",
    ""site_admin"": false
  }
}";
        var serializer = new SimpleJsonSerializer();

        var installationEvent = serializer.Deserialize<InstallationEventPayload>(json);

        Assert.Equal("deleted", installationEvent.Action);
        Assert.Equal("https://github.com/tomer-apiiro-test", installationEvent.Installation.Account?.HtmlUrl);
        Assert.Equal(6, installationEvent.Repositories.Count);
    }
}
