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
    public class SpeakerPage : TabbedPage
    {
        private readonly SpeakerViewModel _viewModel;
        public SpeakerPage()
        {
            _viewModel = new SpeakerViewModel();
            Title = "Speaker Details";

            var listView = new CustomListView
            {
                RowHeight = Device.OnPlatform(70, 70, 125),
                ItemTemplate = new DataTemplate(typeof(SpeakerDetailsCell)),
                HasUnevenRows = true,
                IsGroupingEnabled = false,
                BackgroundColor = Color.Transparent
                //GroupShortNameBinding = new Binding("Key")//Remove for now as it causes crash on iOS if you scroll too fast:(
            };

            listView.SetBinding<SpeakerViewModel>(ListView.ItemsSourceProperty, x => x.Speakers);

            listView.ItemSelected += async (sender, e) =>
            {
                listView.SelectedItem = null;
            };


            var speakersStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { listView },
                Padding = new Thickness(5, 20)
            };

            var speakersPage = new ContentPage
            {
                Content = speakersStack,
                Title = "Speakers",
                BackgroundImage = "background.png"
            };

            Children.Add(speakersPage);
            

            // Sessions List

            var sessions = CreateList(false);
            sessions.SetBinding<SpeakerViewModel>(ListView.ItemsSourceProperty, s => s.Speaker.Sessions);

            var sessionsStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { sessions }
            };

            var sessionsPage = new ContentPage
            {
                Title = "Sessions",
                Content = sessionsStack,
                BackgroundImage = "background.png"
            };

            Children.Add(sessionsPage);

            BindingContext = _viewModel;
        }

        private View CreateList(bool hasTitle = false)
        {
            var listView = new CustomListView
            {
                RowHeight = Device.OnPlatform(65, 50, 80),
                HasUnevenRows = true,
                BackgroundColor = Color.Transparent
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
    }
}
