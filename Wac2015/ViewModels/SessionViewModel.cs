using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Wac2015.Helpers;
using Wac2015.Models;
using Xamarin;
using Xamarin.Forms;

namespace Wac2015.ViewModels
{
    public class SessionViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Speaker> Speakers { get; private set; }
        public Session Session { get; private set; }

        private string _favoriteImage;
        private bool _isFavorite;
        public bool IsFavorite
        { get { return _isFavorite; } set { _isFavorite = value; NotifyPropertyChanged("IsFavorite"); NotifyPropertyChanged("IsUnFavorite"); } }

        public bool IsUnFavorite
        { get { return !_isFavorite; } set { _isFavorite = value; NotifyPropertyChanged("IsFavorite"); NotifyPropertyChanged("IsUnFavorite"); } }

        public string FavoriteImage
        {
            get { return _favoriteImage; }
            set { _favoriteImage = value; NotifyPropertyChanged("FavoriteImage"); }
        }

        private string _uuid;
        public string Uuid
        {
            get { return App.uuid; }
            set { _uuid = value; NotifyPropertyChanged("Uuid"); NotifyPropertyChanged("UuidBasedVisibility"); }
        }

        //public Visibility UuidBasedVisibility
        //{
        //    get { return String.IsNullOrEmpty(Uuid) ? Visibility.Collapsed : Visibility.Visible; }
        //}
        public bool ButtonEnabled
        {
            get
            {
                if (((CurrentDay == Session.Begins.Date && CurrentDay == DayOne) ||
                    (CurrentDay == Session.Begins.Date && CurrentDay == DayTwo)) &&
                    FeedbackCount < App.TotalFeedbackCount)
                {
                    return true;
                }
                return false;
                //if (Session.Begins.Date != DayOne && Session.Begins.Date != DayTwo)
                //    return false;
                //if (Session.Begins.Date == DayOne || Session.Begins.Date == DayTwo)
                //{
                //    if (FeedbackCount < App.TotalFeedbackCount)
                //        return true;
                //    return false;
                //}
                //return false;
            }
        }
        private int _feedbackCount;
        public int FeedbackCount
        {
            get
            {
                return _feedbackCount;
            }
            set
            {
                _feedbackCount = value;
                NotifyPropertyChanged("FeedbackCount");
            }
        }

        public string ToolTipText
        {
            get
            {
                return String.Format("You have given {0} out of {1}. You can only provide max of {1} feedbacks in a day. Please provide your valuable feedback!", FeedbackCount, TotalFeedbackCount);
            }
        }

        public int TotalFeedbackCount
        {
            get
            {
                return App.TotalFeedbackCount;
            }
        }

        private DateTime CurrentDay;
        private DateTime DayOne;
        private DateTime DayTwo;

        ICommand tapCommand;
        int taps = 0;
        public SessionViewModel()
        {
            //if (!Xamarin.Insights.IsInitialized)
            //{
            //    Xamarin.Insights.Initialize("cb9dddf47d18b81b88181a4f106fcb7565048148");
            //    Insights.ForceDataTransmission = true;
            //    if (!string.IsNullOrEmpty(App.uuid))
            //    {

            //        var manyInfos = new Dictionary<string, string> {
            //        { Xamarin.Insights.Traits.GuestIdentifier, App.uuid },
            //        { "CurrentCulture", CultureInfo.CurrentCulture.Name } 
            //    };

            //        Xamarin.Insights.Identify(App.uuid, manyInfos);
            //    }
            //}

            tapCommand = new Command(OnTapped);
            LoadData();
            if (App.SavedSessionIds.Contains(Session.Id))
                IsFavorite = true;
            //TODO: Abhishek to update code here
            //CurrentDay = DateTime.Now.Date;
            CurrentDay = DateTime.UtcNow.AddHours(5).AddMinutes(30).Date;//.AddDays(11).Date;
            DayOne = DateTime.Parse(App.DayOneString).Date;
            DayTwo = DayOne.AddDays(1);
            if (CurrentDay == Session.Begins.Date && CurrentDay == DayOne)
            {
                FeedbackCount = App.AvailableFeedbackCountDayOne;
                App.IsDayOne = true;
            }
            else if (CurrentDay == Session.Begins.Date && CurrentDay == DayTwo)
            {
                FeedbackCount = App.AvailableFeedbackCountDayTwo;
                App.IsDayOne = false;
            }
            else
                _feedbackCount = 0;
        }

        public void LoadData()
        {
            if (App.CurrentSession != null)
            {
                Session = App.CurrentSession;
                Speakers = Session.Speakers.ToObservableCollection();
                var result = (from s in App.ViewModel.SavedSessions.ToList()
                              where s.Id == this.Session.Id
                              select s).FirstOrDefault();
                if (result != null)
                {
                    this.IsFavorite = true;
                    this.Session.IsFavorite = true;
                    this.FavoriteImage = "/Assets/heart2.png";
                }
                else
                {
                    this.IsFavorite = false;
                    this.Session.IsFavorite = false;
                    this.FavoriteImage = "/Assets/heart2.empty.png";
                }
            }
        }




        public ICommand TapCommand
        {
            get { return tapCommand; }
        }
        void OnTapped(object s)
        {
            if (IsFavorite)
            {
                IsFavorite = false;
                App.SavedSessionIds.Remove(Session.Id);
                Insights.Track("Unfavorite Session", new Dictionary<string, string>{
                                {"UserID", App.uuid},
                                {"SessionId", Session.Id}
                            });
            }
            else
            {
                App.SavedSessionIds.Add(Session.Id);
                IsFavorite = true;
                Insights.Track("Favorite Session", new Dictionary<string, string>{
                                {"UserID", App.uuid},
                                {"SessionId", Session.Id}
                            });
            }
            AppStorageHelper.SaveFavoriteSessions(App.SavedSessionIds);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
