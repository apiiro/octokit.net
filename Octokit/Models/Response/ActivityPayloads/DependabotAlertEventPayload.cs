using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Represents the payload for a dependabot_alert webhook event.
    /// See: https://docs.github.com/en/webhooks/webhook-events-and-payloads#dependabot_alert
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependabotAlertEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public DependabotAlert Alert { get; protected set; }
    }
}
