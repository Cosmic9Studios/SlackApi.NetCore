using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackApi.Models
{
    public class Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("thread_ts")]
        public string ThreadTs { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("source_team")]
        public List<Attachment> SourceTeam { get; set; }

        [JsonProperty("subtype")]
        public string SubType { get; set; }

        [JsonProperty("reply_count")]
        public long? ReplyCount { get; set; }

        [JsonProperty("replies")]
        public List<Reply> Replies { get; set; }

        [JsonProperty("subscribed")]
        public bool? Subscribed { get; set; }

        [JsonProperty("last_read")]
        public string LastRead { get; set; }

        [JsonProperty("unread_count")]
        public long? UnreadCount { get; set; }

        [JsonProperty("parent_user_id")]
        public string ParentUserId { get; set; }
    }
}