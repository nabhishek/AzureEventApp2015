using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Wac2015.Controls;
using Wac2015.Models;
using Wac2015.ViewModels;
using Wac2015.Views.Cells;
using Xamarin.Forms;

namespace Wac2015.Views
{
    public class SessionsByTrack : TabbedPage
    {
        public SessionsByTrack()
        {
            Title = "Sessions";

            NavigationPage.SetHasNavigationBar(this, true);

            var dayOneSessions = CreateList(true);
            dayOneSessions.SetBinding<MainViewModel>(ListView.ItemsSourceProperty, s => s.TrackBasedCollection);

            var dayOneSessionsLoading = new ActivityIndicator { IsVisible = false };
            if (Device.OS == TargetPlatform.iOS)
            {
                dayOneSessionsLoading.SetBinding(IsVisibleProperty, "IsBusy");
                dayOneSessionsLoading.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            }
            var dayOneSessionsStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { dayOneSessionsLoading, dayOneSessions }
            };

            
            

            var dayOnePage = new ContentPage
            {
                Title = "By Track",
                Content = dayOneSessionsStack,
                BackgroundImage = "background.png"
            };

            Children.Add(dayOnePage);

            //Content = new ScrollView(){ Content = dayOneSessionsStack };

            this.SetBinding(IsBusyProperty, "IsBusy");

            //allPage.ToolbarItems.Add(new ToolbarItem
            //{
            //    Name = "Filter",
            //    Icon = Device.OnPlatform(null, null, Utils.GetFile("filter.png")),
            //    Command = _viewModel.FilterCommand,
            //    CommandParameter = this
            //});

            //nowPage.ToolbarItems.Add(new ToolbarItem
            //{
            //    Name = "Refresh",
            //    Icon = Utils.GetFile("refresh.png"),
            //    Command = _viewModel.UpdateNowSessionsCommand
            //});

            BindingContext = App.ViewModel;
            //_viewModel.Error += (sender, args) => DisplayAlert("Sorry about the mess.", args.ErrorMessage, "OK");
        }

        private View CreateList(bool hasTitle = false)
        {
            var listView = new CustomListView
            {
                RowHeight = Device.OnPlatform(75, 50, 80),
                HasUnevenRows = true,
                BackgroundColor = Color.Transparent
            };

            if (hasTitle)
            {
                listView.IsGroupingEnabled = true;
                listView.GroupDisplayBinding = new Binding("Location");
                listView.GroupHeaderTemplate = new DataTemplate(typeof(SessionByTrackHeaderCell));
                //listView.GroupShortNameBinding = new Binding("Key.DayShort");
            }

            // see the SessionCell implementation for how the variable row height is calculated
            //listView.ItemTemplate = new DataTemplate (typeof (SessionCell));

            var cell = new DataTemplate(() => new SessionCell(true));//(App.ViewModel));
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
