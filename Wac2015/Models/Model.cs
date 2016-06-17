using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Views.Cells;

namespace Wac2015.Models
{
    public class TwitterModel
    {
        public string TwitterHandle { get; set; }
        public string Message { get; set; }
    }

    public class ResourcesModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public enum ResourceType
    {
        About,
        Location,

    }

    
    public class MenuModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MenuTypes MenuType { get; set; }
        public string IconPath { get; set; }
    }
    public class TwitAuthenticateResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }

    public enum MenuTypes
    {
        SessionAllByDay,
        SessionMyAgenda,
        SessionByTime,
        SessionByTrack,
        SessionBySpeaker,
        ContactUs,
        News,
        Resources,
        AboutUs
    }

    public enum DayTypes
    {
        Day1,
        Day2,
        Unknown
    }
}
