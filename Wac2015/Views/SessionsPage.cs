using System;
using Wac2015.Controls;
using Wac2015.Models;
using Wac2015.ViewModels;
using Wac2015.Views.Cells;
using Xamarin.Forms;

namespace Wac2015.Views
{
    public class SessionsPage : TabbedPage
    {
        public SessionsPage()
        {
            Title = "Sessions";
            
            NavigationPage.SetHasNavigationBar(this, true);

            var dayOneSessions = CreateList(true);
            dayOneSessions.SetBinding<MainViewModel>(ListView.ItemsSourceProperty, s => s.DayOneCollection);

            var dayOneSessionsStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { dayOneSessions }
            };

            var dayOnePage = new ContentPage
            {
                Title = "day 1",
                Content = dayOneSessionsStack,
                BackgroundImage = "background.png"
            };

            var dayTwoSessions = CreateList(true);
            dayTwoSessions.SetBinding<MainViewModel>(ListView.ItemsSourceProperty, s => s.DayTwoCollection);

            var dayTwoSessionsStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { dayTwoSessions }
            };

            var dayTwoPage = new ContentPage
            {
                Title = "day 2",
                Content = dayTwoSessionsStack,
                BackgroundImage = "background.png"
            };

            Children.Add(dayOnePage);
            Children.Add(dayTwoPage);
            
            BindingContext = App.ViewModel;
            //_viewModel.Error += (sender, args) => DisplayAlert("Sorry about the mess.", args.ErrorMessage, "OK");
        }

        private View CreateList(bool hasTitle = false)
        {
            var listView = new CustomListView
            {
                RowHeight = Device.OnPlatform(75, 50, 80),
                HasUnevenRows = true,
                BackgroundColor = Color.Transparent,
            };

            if (hasTitle)
            {
                listView.IsGroupingEnabled = true;
                listView.GroupDisplayBinding = new Binding("Key");
                listView.GroupHeaderTemplate = new DataTemplate(typeof(SessionHeaderCell));
                //listView.GroupShortNameBinding = new Binding("Key.DayShort");
                
            }

            var cell = new DataTemplate(() => new SessionCell(false));//(App.ViewModel));
            listView.ItemTemplate = cell;

            listView.ItemSelected += async (sender, e) =>
            {
                if (listView.SelectedItem == null)
                    return;
                var session = (Session)e.SelectedItem;
                App.CurrentSession = session;
                var sessionPage = new SessionPage();
                await Navigation.PushAsync(sessionPage);

                listView.SelectedItem = null;
            };

            return listView;
        }

        private static long back_pressed;

        protected override bool OnBackButtonPressed()
        {
            var current = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (back_pressed + 3000 > current)
            {
                base.OnBackButtonPressed();
                return false;
            }
            else
            {
                DependencyService.Get<IPlatforms>().MakeToast("Press again to exit!");
                back_pressed = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            }

            return true;
        }
    }
}
