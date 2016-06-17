using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using Xamarin.Forms;

namespace Wac2015.Models
{
    public class AppStorageDatabase
    {
        static object locker = new object();
        SQLiteConnection database;

        public AppStorageDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            // create the tables
            database.CreateTable<AppStorage>();
        }

        public IEnumerable<AppStorage> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<AppStorage>() select i).ToList();
            }
        }

        public AppStorage GetItem(string id)
        {
            try
            {
                lock (locker)
                {
                    return database.Table<AppStorage>().FirstOrDefault(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }

        public int SaveItem(AppStorage item)
        {
            try
            {
                int result = -1;
                lock (locker)
                {
                    result = database.InsertOrReplace(item);
                    //if (!String.IsNullOrEmpty(item.Id))
                    //{
                    //    result = database.Update(item);
                    //}
                    //else
                    //{
                    //    result = database.Insert(item);
                    //}
                }
                var items = GetItems();
                return result;
            }
            catch (Exception ex)
            {
                
            }
            return 0;
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<AppStorage>(id);
            }
        }
    }

    
}
