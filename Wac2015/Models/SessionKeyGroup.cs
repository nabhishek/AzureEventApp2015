using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Helpers;

namespace Wac2015.Models
{
    public class SessionKeyGroup<T> : List<T>
    {
        public delegate string GetKeyDelegate(T item);

        public delegate DateTime GetDateKeyDelegate(T item);

        public delegate string GetLocationDelegate(T item);

        public delegate string GetTrackDelegate(T item);
        public string Location { get; set; }

        public string TrackName { get; set; }
        //public string Key { get; private set; }

        public string Key
        {
            get { return DateKey.ToString("ddd MMM dd H:mm"); }
        }

        public DateTime DateKey { get; set; }

        public string DisplayDate
        {
            get { return DateKey.ToString("dd-MM-yyyy"); }
        }

        public string DisplayTime
        {
            get { return DateKey.ToString("hh:mm tt"); }
        }
        public SessionKeyGroup(DateTime key)
        {
            DateKey = key;
        }

        public SessionKeyGroup(string key)
        {
            Location = key;
            TrackName = key;
        }
        public static ObservableCollection<SessionKeyGroup<T>> CreateDateGroups(IEnumerable<T> items, CultureInfo ci, GetDateKeyDelegate getKey, bool sort)
        {
            var list = new ObservableCollection<SessionKeyGroup<T>>();

            foreach (var item in items)
            {
                var itemKey = getKey(item);

                var itemGroup = list.FirstOrDefault(li => li.DateKey == itemKey);
                var itemGroupIndex = itemGroup != null ? list.IndexOf(itemGroup) : -1;

                if (itemGroupIndex == -1)
                {
                    list.Add(new SessionKeyGroup<T>(itemKey));
                    itemGroupIndex = list.Count - 1;
                }
                if (itemGroupIndex >= 0 && itemGroupIndex < list.Count)
                {
                    list[itemGroupIndex].Add(item);
                }
            }
            if (sort)
            {
                string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };



                foreach (var group in list)
                {
                    //group.ToList().Sort((c0, c1) => ci.CompareInfo.Compare(getKey(c0), getKey(c1)));               

                }



            }

            return list;
        }

        public static ObservableCollection<SessionKeyGroup<T>> CreateLocationGroups(IEnumerable<T> items, CultureInfo ci, GetLocationDelegate getKey, bool sort)
        {
            var list = new ObservableCollection<SessionKeyGroup<T>>();

            foreach (var item in items)
            {
                var itemKey = getKey(item);

                var itemGroup = list.FirstOrDefault(li => li.Location == itemKey);
                var itemGroupIndex = itemGroup != null ? list.IndexOf(itemGroup) : -1;

                if (itemGroupIndex == -1)
                {
                    list.Add(new SessionKeyGroup<T>(itemKey));
                    itemGroupIndex = list.Count - 1;
                }
                if (itemGroupIndex >= 0 && itemGroupIndex < list.Count)
                {
                    list[itemGroupIndex].Add(item);
                }
            }
            if (sort)
            {
                string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };



                foreach (var group in list)
                {
                    //group.ToList().Sort((c0, c1) => ci.CompareInfo.Compare(getKey(c0), getKey(c1)));               

                }



            }

            return list;
        }

        public static ObservableCollection<SessionKeyGroup<T>> CreateTrackGroups(IEnumerable<T> items, CultureInfo ci, GetTrackDelegate getKey, bool sort)
        {
            var list = new ObservableCollection<SessionKeyGroup<T>>();

            foreach (var item in items)
            {
                var itemKey = getKey(item);

                var itemGroup = list.FirstOrDefault(li => li.TrackName == itemKey);
                var itemGroupIndex = itemGroup != null ? list.IndexOf(itemGroup) : -1;

                if (itemGroupIndex == -1)
                {
                    list.Add(new SessionKeyGroup<T>(itemKey));
                    itemGroupIndex = list.Count - 1;
                }
                if (itemGroupIndex >= 0 && itemGroupIndex < list.Count)
                {
                    list[itemGroupIndex].Add(item);
                }
            }
            if (sort)
            {
                string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };


                foreach (var group in list)
                {
                    group.ToList().Sort((c0, c1) => ci.CompareInfo.Compare(getKey(c0), getKey(c1)));

                }



            }
            list = list.OrderBy(x => x.TrackName).ToObservableCollection();
            return list;
        }






        public static IEnumerable<Session> GetMovieList(List<Session> items)
        {
            var sessionList = new List<Session>();

            sessionList = items;
            return sessionList;
        }


        public static List<Group<Session>> GetMovieGroups(List<Session> items)
        {
            IEnumerable<Session> movieList = GetMovieList(items);
            return GetItemGroups(movieList, s => s.Title);
        }

        public static List<Group<Session>> GetSessionGroups(List<Session> items)
        {
            IEnumerable<Session> movieList = GetMovieList(items);
            return GetItemGroups(movieList, s => s.Title);
        }


        public static List<Group<T>> GetItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {
            IEnumerable<Group<T>> groupList = from item in itemList
                                              group item by getKeyFunc(item) into g
                                              orderby g.Key
                                              select new Group<T>(g.Key, g);

            return groupList.ToList();
        }


        public class Group<T> : List<T>
        {
            public Group(string name, IEnumerable<T> items)
                : base(items)
            {
                this.Title = name;
            }

            public string Title
            {
                get;
                set;
            }
        }

    }
}
