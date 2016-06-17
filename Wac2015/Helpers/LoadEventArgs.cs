using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wac2015.Helpers
{
    public delegate void LoadEventHandler(object sender, LoadEventArgs e);

    public class LoadEventArgs : EventArgs
    {
        public LoadEventArgs() : base() { }

        public LoadEventArgs(bool IsLoaded, string Message)
        {
            this.IsLoaded = IsLoaded;
            this.Message = Message;
        }
        public bool IsLoaded { get; set; }
        public string Message { get; set; }
    }
}
