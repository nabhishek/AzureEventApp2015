using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wac2015.Models
{
    public class Feedback2
    {

        public string Id { get; set; }

        [JsonProperty(PropertyName = "sessionrating")]
        public int SessionRating { get; set; }

        [JsonProperty(PropertyName = "speakerrating")]
        public int SpeakerRating { get; set; }
        [JsonProperty(PropertyName = "overallrating")]
        public int OverallRating { get; set; }
        [JsonProperty(PropertyName = "platformid")]
        public int PlatformId { get; set; }
        [JsonProperty(PropertyName = "uuid")]
        public long UserId { get; set; }
        [JsonProperty(PropertyName = "tocontact")]
        public bool ToContact { get; set; }
        [JsonProperty(PropertyName = "textfeedback")]
        public string TextFeedback { get; set; }

        [JsonProperty(PropertyName = "sessionid")]
        public string SessionId { get; set; }
        [JsonProperty(PropertyName = "deviceid")]
        public string DeviceId { get; set; }
    }

    public class Eventfeedback
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "uuid")]
        public long UserId { get; set; }
        [JsonProperty(PropertyName = "q1_satisfied")]
        public int OverallSatisfaction { get; set; }
        [JsonProperty(PropertyName = "q2_complete")]
        public int CompleteAzure { get; set; }
        [JsonProperty(PropertyName = "q3_recommend")]
        public int RecommendAzure { get; set; }
    }

    public class ContactUs
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "uuid")]
        public long UserId { get; set; }
        [JsonProperty(PropertyName = "q1_propensity")]
        public int AnswerOne { get; set; }
        [JsonProperty(PropertyName = "q2_role")]
        public int AnswerTwo { get; set; }
        [JsonProperty(PropertyName = "q2_roletext")]
        public string AnswerTwoText { get; set; }
        [JsonProperty(PropertyName = "q3_pcs")]
        public string AnswerThree { get; set; }
        [JsonProperty(PropertyName = "q4_deploywhen")]
        public int AnswerFour { get; set; }
        [JsonProperty(PropertyName = "q5_budget")]
        public int AnswerFive { get; set; }
        [JsonProperty(PropertyName = "q5_budgetamount")]
        public string AnswerFiveText { get; set; }
        [JsonProperty(PropertyName = "q6_comments")]
        public string AnswerSix { get; set; }
        [JsonProperty(PropertyName = "platformid")]
        public int PlatformId { get; set; }
    }

    public class NewsPublish
    {
        public string id { get; set; }
        public string NewsHeader { get; set; }
        public string NewsDetails { get; set; }
        public DateTime CreatedDt { get; set; }
        public string NewsDetailsPartOne { get; set; }
        public string NewsDetailsPartTwo { get; set; }
        public string LinkText { get; set; }
        public string LinkUrl { get; set; }
        public bool IsLinkPresent { get; set; }
        public bool HideOthers { get; set; }
    }


}
