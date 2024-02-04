using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Protection rule set for a <see cref="Branch"/>.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionRule
    {
        public BranchProtectionRule()
        {
        }

        public BranchProtectionRule(string type, string rulesetSourceType, long rulesetId, BranchProtectionRuleParameters parameters)
        {
            Type = type;
            RulesetSourceType = rulesetSourceType;
            RulesetId = rulesetId;
            Parameters = parameters;
        }

        public string Type { get; private set; }
        public string RulesetSourceType { get; private set; }
        public long RulesetId { get; private set; }
        public BranchProtectionRuleParameters Parameters { get; private set; }

        internal string DebuggerDisplay =>
            string.Format(CultureInfo.InvariantCulture,
                "Type: {0} RulesetSourceType {1} RulesetId: {2} Parameters: {3}",
                Type,
                RulesetSourceType,
                RulesetId,
                Parameters?.DebuggerDisplay ?? "disabled");
    }

    /// <summary>
    /// Specifies settings for status checks which must pass before branches can be merged into the protected branch
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class BranchProtectionRuleParameters
    {
        public BranchProtectionRuleParameters()
        {
        }

        public BranchProtectionRuleParameters(int requiredApprovingReviewCount)
        {
            RequiredApprovingReviewCount = requiredApprovingReviewCount;
        }

        public int RequiredApprovingReviewCount { get; private set; }

        internal string DebuggerDisplay =>
            string.Format(CultureInfo.InvariantCulture,
                "RequiredApprovingReviewCount: {0}", RequiredApprovingReviewCount);
    }
}
