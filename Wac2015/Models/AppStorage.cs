using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace Wac2015.Models
{
    public class AppStorage
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Data { get; set; }
    }
}
