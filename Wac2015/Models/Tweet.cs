using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wac2015.Models
{
    public class Tweet
    {
        public ulong StatusId { get; set; }
        public string ScreenName { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProfileImage { get; set; }
    }
}
