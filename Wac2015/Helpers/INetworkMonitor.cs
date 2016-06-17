using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wac2015.Helpers
{
    public interface INetworkMonitor
    {
        bool IsAvailable();
    }
}
