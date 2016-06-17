using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Models;

namespace Wac2015.Helpers
{
    public class AppStorageHelper
    {
        public static readonly string FavoriteSessionIds = "FavoriteSessionIds";
        public static readonly string RegistrationId = "RegistrationId";
        public static readonly string VersionNumber = "VersionNumber";
        public static readonly string SessionData = "SessionData";
        public static readonly string FeedbacksGiven = "SessionFeedback";
        public static readonly string DayOneFeedbackCount = "DayOneFeedbackCount";
        public static readonly string DayTwoFeedbackCount = "DayTwoFeedbackCount";
        public static readonly string DayOneExtFeedback = "DayOneExtFeedback";
        public static readonly string DayTwoExtFeedback = "DayTwoExtFeedback";
        public static List<string> GetFavoriteSessions()
        {
            try
            {
                var favoriteSessions = App.Database.GetItem(FavoriteSessionIds);
                if (favoriteSessions == null)
                    return new List<string>();
                var sessionIds = favoriteSessions.Data;
                var ids = sessionIds.Split(',');
                return ids.ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return new List<string>();
        }

        public static int SaveFavoriteSessions(List<string> sessionsIds)
        {
            try
            {
                var sessions = String.Join(",", sessionsIds);
                var item = new AppStorage();
                item.Id = FavoriteSessionIds;
                item.Data = sessions;
                return App.Database.SaveItem(item);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return 0;
        }

        public static int SaveUserId(string id)
        {
            try
            {
                var item = new AppStorage();
                item.Id = RegistrationId;
                item.Data = id;
                return App.Database.SaveItem(item);
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public static string GetUserId()
        {
            try
            {
                var item = App.Database.GetItem(RegistrationId);
                if (item != null)
                    return item.Data;
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }

        public static int SaveGenericData(string id, string data)
        {
            try
            {
                var item = new AppStorage();
                item.Id = id;
                item.Data = data;
                return App.Database.SaveItem(item);
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public static string GetGenericData(string id)
        {
            try
            {
                var item = App.Database.GetItem(id);
                if (item != null)
                    return item.Data;
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }

        public static List<string> GetGenericList(string id)
        {
            try
            {
                var item = App.Database.GetItem(id);
                if (item == null)
                    return new List<string>();
                var data = item.Data;
                var result = data.Split(',');
                return result.ToList();
            }
            catch (Exception ex)
            {
            }
            return new List<string>();
        }
        public static int SaveGenericList(string id, List<string> data)
        {
            try
            {
                var stringData = String.Join(",", data);
                var item = new AppStorage();
                item.Id = id;
                item.Data = stringData;
                return App.Database.SaveItem(item);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return 0;
        }

        public static int SaveFeedbackList(List<string> feedbacks)
        {
            return SaveGenericList(FavoriteSessionIds, feedbacks);
        }

        public static List<string> GetFeedbackList()
        {
            return GetGenericList(FavoriteSessionIds);
        }

        public static int SaveCurrentVersionNumber(string value)
        {
            return SaveGenericData(VersionNumber, value);
        }

        public static int GetCurrentVersionNumber()
        {
            int result = App.DefaultSessionVersion;
            var version = GetGenericData(VersionNumber);
            if (!String.IsNullOrEmpty(version) && !int.TryParse(version, out result))
                result = App.DefaultSessionVersion;
            return result;
        }

        public static int SaveCurrentSessionData(string value)
        {
            return SaveGenericData(SessionData, value);
        }

        public static string GetCurrentSessionData()
        {
            return GetGenericData(SessionData);
        }


        #region FeedbackData

        public static int GetDayOneFeedbackCount()
        {
            int result = 0;
            var count = GetGenericData(DayOneFeedbackCount);
            if (!String.IsNullOrEmpty(count) && !int.TryParse(count, out result))
                result = 0;
            return result;
        }

        public static int GetDayTwoFeedbackCount()
        {
            int result = 0;
            var count = GetGenericData(DayTwoFeedbackCount);
            if (!String.IsNullOrEmpty(count) && !int.TryParse(count, out result))
                result = 0;
            return result;
        }

        public static bool GetDayOneExtFeedback()
        {
            int tmp = 0;
            var count = GetGenericData(DayOneExtFeedback);
            if (!String.IsNullOrEmpty(count) && !int.TryParse(count, out tmp))
                return false;
            if (tmp == 1)
                return true;
            return false;
        }

        public static bool GetDayTwoExtFeedback()
        {
            int tmp = 0;
            var count = GetGenericData(DayTwoExtFeedback);
            if (!String.IsNullOrEmpty(count) && !int.TryParse(count, out tmp))
                return false;
            if (tmp == 1)
                return true;
            return false;
        }

        public static int SaveDayOneFeedbackCount(int dayOneFeedbackCount)
        {
            return SaveGenericData(DayOneFeedbackCount, dayOneFeedbackCount.ToString());
        }

        public static int SaveDayTwoFeedbackCount(int dayTwoFeedbackCount)
        {
            return SaveGenericData(DayTwoFeedbackCount, dayTwoFeedbackCount.ToString());
        }
        public static int SaveDayOneExtFeedback(int dayOneExtFeedback)
        {
            return SaveGenericData(DayOneExtFeedback, dayOneExtFeedback.ToString());
        }

        public static int SaveDayTwoExtFeedback(int dayTwoExtFeedback)
        {
            return SaveGenericData(DayTwoFeedbackCount, dayTwoExtFeedback.ToString());
        }

        #endregion
    }
}
