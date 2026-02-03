using System;
using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Represents a Dependabot alert for a repository vulnerability.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependabotAlert
    {
        public DependabotAlert() { }

        public int Number { get; protected set; }
        public string State { get; protected set; }
        public DependabotAlertDependency Dependency { get; protected set; }
        public DependabotSecurityAdvisory SecurityAdvisory { get; protected set; }
        public DependabotSecurityVulnerability SecurityVulnerability { get; protected set; }
        public string HtmlUrl { get; protected set; }
        public DateTimeOffset? CreatedAt { get; protected set; }
        public DateTimeOffset? UpdatedAt { get; protected set; }
        public DateTimeOffset? DismissedAt { get; protected set; }
        public DateTimeOffset? FixedAt { get; protected set; }
        public DateTimeOffset? AutoDismissedAt { get; protected set; }
        public User DismissedBy { get; protected set; }
        public string DismissedReason { get; protected set; }
        public string DismissedComment { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return $"Alert #{Number}: {State}"; }
        }
    }

    /// <summary>
    /// Represents the dependency affected by a Dependabot alert.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependabotAlertDependency
    {
        public DependabotAlertDependency() { }

        public DependabotPackage Package { get; protected set; }
        public string ManifestPath { get; protected set; }
        public string Scope { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return $"{Package?.Name} ({ManifestPath})"; }
        }
    }

    /// <summary>
    /// Represents a package in a Dependabot alert.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependabotPackage
    {
        public DependabotPackage() { }

        public string Ecosystem { get; protected set; }
        public string Name { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return $"{Ecosystem}: {Name}"; }
        }
    }

    /// <summary>
    /// Represents security advisory information for a Dependabot alert.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependabotSecurityAdvisory
    {
        public DependabotSecurityAdvisory() { }

        public string GhsaId { get; protected set; }
        public string CveId { get; protected set; }
        public string Summary { get; protected set; }
        public string Description { get; protected set; }
        public string Severity { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return $"{GhsaId ?? CveId}: {Severity}"; }
        }
    }

    /// <summary>
    /// Represents security vulnerability information for a Dependabot alert.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependabotSecurityVulnerability
    {
        public DependabotSecurityVulnerability() { }

        public string Severity { get; protected set; }
        public string VulnerableVersionRange { get; protected set; }
        public DependabotFirstPatchedVersion FirstPatchedVersion { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return $"{Severity}: {VulnerableVersionRange}"; }
        }
    }

    /// <summary>
    /// Represents the first patched version for a vulnerability.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DependabotFirstPatchedVersion
    {
        public DependabotFirstPatchedVersion() { }

        public string Identifier { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return Identifier; }
        }
    }
}
