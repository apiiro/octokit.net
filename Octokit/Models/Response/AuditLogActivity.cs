using System.Text.Json.Serialization;

namespace Octokit
{
    public class AuditLogActivity
    {
        [JsonPropertyName("created_at")]
        public long CreatedAt;
    }
}
