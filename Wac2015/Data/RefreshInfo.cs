using System;
using System.Runtime.Serialization;

namespace XamarinEvolve.Mobile.Data
{
    [DataContract]
    public class RefreshInfo
    {
        public RefreshInfo(RefreshType refreshType, Guid id)
        {
            RefreshType = refreshType;
            Id = id;
        }

        [DataMember]
        public RefreshType RefreshType { get; private set; }

        [DataMember]
        public Guid Id { get; private set; }
    }
}