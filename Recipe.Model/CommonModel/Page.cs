using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Recipe.Model.CommonModel
{
    public class Page
    {
        public Page()
        {
            Meta = new Meta();
            Result = new JArray();
        }

        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }

        [JsonProperty(PropertyName = "results")]
        public object Result { get; set; }
    }

    public class ConnectionPage
    {
        public ConnectionPage()
        {
            Meta = new Meta();
            Result = new JArray();
        }

        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }

        [JsonProperty(PropertyName = "results")]
        public object Result { get; set; }

        [JsonProperty(PropertyName = "total_invitation_count")]
        public long TotalInvitationCount { get; set; }

        [JsonProperty(PropertyName = "total_sent_count")]
        public long TotalInvitationSentCount { get; set; }

        [JsonProperty(PropertyName = "total_receive_count")]
        public long TotalInvitationReceiveCount { get; set; }
    }

    public class InviteSpResponse
    {
        [JsonProperty(PropertyName = "total_invitation_count")]
        public long TotalInvitationCount { get; set; }

        [JsonProperty(PropertyName = "total_sent_count")]
        public long TotalInvitationSentCount { get; set; }

        [JsonProperty(PropertyName = "total_receive_count")]
        public long TotalInvitationReceiveCount { get; set; }
    }

    public class MessageListPage
    {
        public MessageListPage()
        {
            Meta = new Meta();
            Result = new JArray();
        }

        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }

        [JsonProperty(PropertyName = "results")]
        public object Result { get; set; }

        [JsonProperty("is_muted")]
        public bool IsMuted { get; set; }

        [JsonProperty("is_blocked")]
        public bool IsBlocked { get; set; }

        [JsonProperty("channel_sid")]
        public string ChannelSid { get; set; }
    }

    // Meta class (assuming you already have it elsewhere, include if missing)
    //public class Meta
    //{
    //    public int PageNo { get; set; }
    //    public int PageSize { get; set; }
    //    public int TotalRecords { get; set; }
    //}
}
