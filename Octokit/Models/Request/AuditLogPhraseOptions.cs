using System;
using System.Text;

namespace Octokit
{
    public class AuditLogPhraseOptions
    {
        public string User { get; set; }
        public string Repository { get; set; }
        public DateTime? Created { get; set; }

        public string BuildPhrase(string organization)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(User))
            {
                sb.Append($"actor:{User} ");
            }
            
            if (!string.IsNullOrWhiteSpace(Repository))
            {
                sb.Append($"repo:{organization}/{Repository} ");
            }

            if (Created.HasValue)
            {
                sb.Append($"created:>={Created.Value:yyyy-MM-dd}");
            }

            return sb.ToString();
        }
    }
}
