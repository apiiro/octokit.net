using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class DeployKey
    {
        public DeployKey() { }

        public DeployKey(int id, string key, string url, string title, bool verified, DateTimeOffset createdAt, bool readOnly, string addedBy, DateTimeOffset? lastUsed)
        {
            Id = id;
            Key = key;
            Url = url;
            Title = title;
            Verified = verified;
            CreatedAt = createdAt;
            ReadOnly = readOnly;
            AddedBy = addedBy;
            LastUsed = lastUsed;
        }

        public int Id { get; protected set; }
        public string Key { get; protected set; }
        public string Url { get; protected set; }
        public string Title { get; protected set; }
        public bool Verified { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public bool ReadOnly { get; protected set; }
        public string AddedBy { get; protected set; }
        public DateTimeOffset? LastUsed { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Deploy Key: Id: {0} Key: {1} Url: {2} Title: {3} Verified: {4} CreatedAt: {5} ReadOnly: {6} AddedBy: {7}, LastUsed: {8}", Id, Key, Url, Title, Verified, CreatedAt, ReadOnly, AddedBy, LastUsed);
            }
        }
    }
}
