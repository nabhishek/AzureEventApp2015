using System.Runtime.Serialization;

namespace XamarinEvolve.Mobile.Data
{
    [DataContract]
    public enum RefreshType
    {
        [EnumMember]
        Event,

        [EnumMember]
        Session,

        [EnumMember]
        Speaker,

        [EnumMember]
        Sponsor,

        [EnumMember]
        Ticket
    }
}