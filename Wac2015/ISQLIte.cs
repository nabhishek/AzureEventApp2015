using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;

namespace Wac2015
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
