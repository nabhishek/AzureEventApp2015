using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Wac2015.Helpers;
using Wac2015.Models;
using Wac2015.Views.Cells;
using Xamarin.Forms;

namespace Wac2015.Views
{
    public class MenuTableView : TableView
    {
    }
    public class MenuPage : ContentPage
    {

        ObservableCollection<Models.MenuItem> Items;
        public ObservableCollection<MenuModel> SessionMenu { get; set; }
        private readonly MasterDetailPage _master;
        private NavigationPage _about;
        private NavigationPage _venue;
        private NavigationPage _sessions;
        private NavigationPage _sessionsByTime;
        private NavigationPage _sessionsByTrack;
        private NavigationPage _speakers;
        private NavigationPage _mySessions;
        private NavigationPage _contactUs;
        private NavigationPage _newsPage;

        private NavigationPage _sponsors;
        private NavigationPage _location;
        private NavigationPage _home;
        private NavigationPage _nowSessions;

        public MenuPage(MasterDetailPage m)
        {
            SessionMenu = new ObservableCollection<MenuModel>();
            SessionMenu.Add(new MenuModel() { Name = "All by Day", Description = "Show all sessions by day", MenuType = MenuTypes.SessionAllByDay, IconPath = "../Assets/Images/AllByDay.png" });
            SessionMenu.Add(new MenuModel() { Name = "My Agenda", Description = "View my agenda", MenuType = MenuTypes.SessionMyAgenda, IconPath = "../Assets/Images/MyAgenda.png" });
            SessionMenu.Add(new MenuModel() { Name = "By Speaker", Description = "Filter by speaker", MenuType = MenuTypes.SessionBySpeaker, IconPath = "../Assets/Images/BySpeaker.png" });
            SessionMenu.Add(new MenuModel() { Name = "By Time", Description = "Filter by time slot", MenuType = MenuTypes.SessionByTime, IconPath = "../Assets/Images/ByTime.png" });
            SessionMenu.Add(new MenuModel() { Name = "By Track", Description = "Filter by track", MenuType = MenuTypes.SessionByTrack, IconPath = "../Assets/Images/ByTrack.png" });
            SessionMenu.Add(new MenuModel() { Name = "News", Description = "Azure News", MenuType = MenuTypes.News, IconPath = "../Assets/Images/ByTrack.png" });
            SessionMenu.Add(new MenuModel() { Name = "Contact Us", Description = "Contact us", MenuType = MenuTypes.ContactUs, IconPath = "../Assets/Images/ByTrack.png" });
            SessionMenu.Add(new MenuModel() { Name = "About Us", Description = "About us", MenuType = MenuTypes.AboutUs, IconPath = "../Assets/Images/ByTrack.png" });

            _master = m;
            BackgroundColor = App.XamDarkBlue;
            Title = "menu";
            Icon = Device.OnPlatform(Utils.GetFile("slideout.png"),Utils.GetFile("hamburgerIcon.png"),Utils.GetFile("slideout.png"));

            ListView listView = null;
            MenuTableView tableView = null;

            if (Device.OS == TargetPlatform.Android)
            {
                listView = new ListView
                {
                    RowHeight = Device.OnPlatform(60, 60, 70),
                    BackgroundColor = App.XamDarkBlue,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    ItemTemplate = new DataTemplate(typeof(MenuCell)),
                    ItemsSource = SessionMenu
                };

                listView.ItemSelected += (sender, e) =>
                {

                    var item = e.SelectedItem as Models.MenuModel;
                    if (item == null)
                        return;

                    Selected(item.MenuType);

                    listView.SelectedItem = null;//clear out
                };

            }
            else
            {
                var section = new TableSection();
                foreach (var item in SessionMenu)
                    section.Add(new MenuCell { Text = item.Name, MenuOption = item.MenuType, Host = this });

                var root = new TableRoot { section };

                tableView = new MenuTableView
                {
                    Root = root,
                    RowHeight = Device.OnPlatform(50, 60, 70),
                    Intent = TableIntent.Menu,
                    BackgroundColor = App.XamDarkBlue,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };
            }

            var evolveLogo = new Frame
            {
                HasShadow = false,
                BackgroundColor = Color.Transparent,
                Padding = new Thickness(Device.OnPlatform(25, 25, 40), Device.OnPlatform(0, 0, 20), 0, 0),
                HorizontalOptions = LayoutOptions.Start,
                Content =
                    new Image
                    {
                        Source = new FileImageSource
                        {
                            File = Utils.GetFile("azurelogo_100.png")
                        },
                        Aspect = Aspect.AspectFit,
                        HorizontalOptions = LayoutOptions.Start,
                        HeightRequest = Device.OnPlatform(60, 75, 75),
                    }
            };

            var stack = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(20, 20, 20), 0, 0),
                Spacing = 0,
                BackgroundColor = App.XamDarkBlue,
                Children = { evolveLogo }
            };


            if (Device.OS == TargetPlatform.Android)
            {
                stack.Children.Add(listView);
            }
            else
            {
                stack.Children.Add(tableView);
            }

            if (Device.OS == TargetPlatform.WinPhone)
                this.BackgroundColor = App.XamDarkBlue;

            Content = stack;
        }

        public async void Selected(MenuTypes item)
        {
            _master.IsPresented = false; // close the slide-out
            switch (item)
            {
                case MenuTypes.SessionAllByDay:
                    _master.Detail = _sessions ?? (_sessions = new NavigationPage(new SessionsPage())
                    {
                        BarTextColor = App.XamBlue
                    });
                    break;
                case MenuTypes.SessionByTime:
                    _master.Detail = _sessionsByTime ?? (_sessionsByTime = new NavigationPage(new SessionsByTime())
                    {
                        BarTextColor = App.XamBlue
                    });
                    break;
                case MenuTypes.SessionByTrack:
                    _master.Detail = _sessionsByTrack ?? (_sessionsByTrack = new NavigationPage(new SessionsByTrack())
                    {
                        BarTextColor = App.XamBlue
                    });
                    break;
                case MenuTypes.SessionBySpeaker:
                    _master.Detail = _speakers ?? (_speakers = new NavigationPage(new SpeakersPage())
                    {
                        BarTextColor = App.XamBlue
                    });
                    break;
                case MenuTypes.SessionMyAgenda:
                    _master.Detail = new NavigationPage(new MyAgendaPage())
                    {
                        BarTextColor = App.XamBlue
                    };
                    break;
                case MenuTypes.ContactUs:
                    _master.Detail = _contactUs ?? (_contactUs = new NavigationPage(new ContactUsPage())
                    {
                        BarTextColor = App.XamBlue
                    });
                    break;
                case MenuTypes.News:
                    _master.Detail = _newsPage ?? (_newsPage = new NavigationPage(new NewsPage())
                     {
                         BarTextColor = App.XamBlue
                     });
                    break;
                case MenuTypes.AboutUs:
                    _master.Detail = _about ?? (_about = new NavigationPage(new AboutPage())
                    {
                        BarTextColor = App.XamBlue
                    });
                    break;
            }
        }
    }
}
