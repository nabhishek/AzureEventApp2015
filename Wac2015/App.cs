using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LinqToTwitter;
using Wac2015.Helpers;
using Wac2015.Models;
using Wac2015.ViewModels;
using Wac2015.Views;
using Wac2015.Views.Cells;
using Xamarin.Forms;

namespace Wac2015
{
    public class App : Application
    {
        public static readonly Color XamGreen = Color.FromHex("77D065");
        public static readonly Color XamPurple = Color.FromHex("B455B6");
        public static readonly Color XamBlue = Color.FromHex("3498DB");
        public static readonly Color XamDarkBlue = Color.FromHex("2C3E50");
        public static readonly Color XamGray = Color.FromHex("738182");
        public static readonly Color XamLightGray = Color.FromHex("B4BCBC");
        public static readonly int SessionTitle = 22;
        private static AppStorageDatabase database;

        private static MainViewModel viewModel = null;
        public static SessionViewModel sessionViewModel = null;
        public static Speaker CurrentSpeaker;
        public static Session CurrentSession;
        public static NewsPublish CurrentNews;
        public static ObservableCollection<Session> Sessions;
        public static ObservableCollection<Speaker> Speakers;
        public static ObservableCollection<Session> SavedSessions;


        public static List<string> SavedSessionIds { get; set; }
        public static bool IsExtendedFeedbackProvided = false;
        public static int FeedbackCount = 0;
        public static string sessionData = string.Empty;
        public static string uuid = string.Empty;
        public static User UserDetails = null;
        public static List<string> FeedbackList;
        public static int DefaultSessionVersion = 16;
        public static int CurrentSessionVersion;
        public static string CurrentSessionData;
        public static string SpeakerId;
        public static string SessionId;
        public static int AvailableFeedbackCountDayOne = 0;
        public static int AvailableFeedbackCountDayTwo = 0;
        public static int TotalFeedbackCount = 10;
        public static bool IsDayOne { get; set; }
        public static string DayOneString = "2015-03-18T09:30:00.0000000+05:30";
        public static DateTime DayOne { get; set; }
        public static DayTypes CurrentDayType { get; set; }
        public static bool DayOneExtFeedback { get; set; }
        public static bool DayTwoExtFeedback { get; set; }
        public static int DayOneFeedbackCount = 0;
        public static int DayTwoFeedbackCount = 0;
        public static bool IsNewsPage = false;
        public static INetworkMonitor NetworkMonitor { get; set; }

        public static MenuPage iOSMenuPage;
        public static MasterDetailPage IOsMasterDetailPage;

        //public static IDataStore DataStore { get; set; }

        //public static Color NavTint
        //{
        //    get { return AppSettings.ColorMainApp; }
        //}

        //public static Color HeaderTint
        //{
        //    get { return AppSettings.ColorMainApp; }
        //}

        //public static IDataStore Initialize(IDataCache cache, INetworkMonitor networkMonitor)
        //{
        //    return DataStore = new JsonDataStore(cache, AppSettings.Log, networkMonitor);
        //}
        public App()
        {
            //var CurrentDate = DateTime.Now.Date;
            DayOne = DateTime.Parse(DayOneString);
            DayOne = DayOne.Date;
            var dayTwo = DayOne.AddDays(1).Date;
            var currentDate = DateTime.Now.Date;

            if (currentDate < DayOne)
                CurrentDayType = DayTypes.Unknown;
            else if (currentDate == DayOne)
                CurrentDayType = DayTypes.Day1;
            else if (currentDate == dayTwo)
                CurrentDayType = DayTypes.Day2;
            else
                CurrentDayType = DayTypes.Unknown;
            MainPage = GetMainPage();
        }

        public static Page GetMainPage()
        {
            var md = new MasterDetailPage();

            var master = new MenuPage(md);
            md.Master = master;
            if (IsNewsPage)
            {
                master.Selected(MenuTypes.News);
                IsNewsPage = false;
            }
            else
                master.Selected(MenuTypes.SessionAllByDay);
            IOsMasterDetailPage = md;
            return md;
        }

        protected override void OnStart()
        {
            ReadDataFromStore();
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            WriteDataToStore();
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            ReadDataFromStore();
            // Handle when your app resumes
        }

        public static MainViewModel ViewModel
        {
            get
            {
                // Delay creation of the view model until necessary
                if (viewModel == null)
                    viewModel = new MainViewModel();

                return viewModel;
            }
        }

        public static void ReadDataFromStore()
        {
            SavedSessionIds = AppStorageHelper.GetFavoriteSessions();
            uuid = AppStorageHelper.GetUserId();
            FeedbackList = AppStorageHelper.GetFeedbackList();
            CurrentSessionData = AppStorageHelper.GetCurrentSessionData();
            CurrentSessionVersion = AppStorageHelper.GetCurrentVersionNumber();

        }

        public static void WriteDataToStore()
        {
            AppStorageHelper.SaveFavoriteSessions(SavedSessionIds);
            AppStorageHelper.SaveUserId(uuid);
            AppStorageHelper.SaveFeedbackList(FeedbackList);
            
        }

        public static AppStorageDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new AppStorageDatabase();
                }
                return database;
            }
        }

        public static FeedbackManager feedbackManager { get; set; }

        public static void SetFeedbackManager(FeedbackManager feedbackItemManager)
        {
            feedbackManager = feedbackItemManager;
        }
    }
}
