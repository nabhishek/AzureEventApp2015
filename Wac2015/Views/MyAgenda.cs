using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Controls;
using Wac2015.Models;
using Wac2015.ViewModels;
using Wac2015.Views.Cells;
using Xamarin.Forms;

namespace Wac2015.Views
{
    public class MyAgendaPage : ContentPage
    {
        private FontLabel title;
        private StackLayout titleStack;
        public MyAgendaPage()
        {
            Title = "My Agenda";
            BackgroundImage = "background.png";
            NavigationPage.SetHasNavigationBar(this, true);

            //if (App.ViewModel.TimeBasedMyAgendaCollection == null || !App.ViewModel.TimeBasedMyAgendaCollection.Any())
            //{

            //    var title = new FontLabel
            //    {
            //        Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Medium),
            //          Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
            //        FontLabelType = FontLabelType.Light,
            //        LineBreakMode = LineBreakMode.WordWrap,
            //        TextColor = Color.White,
            //        Text = "It seems you have not marked any session as Favorite.",
            //        FontAttributes = FontAttributes.Bold
            //    };

            //    var mainStack = new StackLayout
            //    {
            //        HorizontalOptions = LayoutOptions.FillAndExpand,
            //        VerticalOptions = LayoutOptions.FillAndExpand,
            //        Padding = new Thickness(10),
            //        Children = { title }
            //    };


            //    BindingContext = App.ViewModel;

            //    Content = mainStack;
            //}

            //else
            //{
            //    var mySessions = CreateList(true);
            //    mySessions.SetBinding<MainViewModel>(ListView.ItemsSourceProperty, s => s.TimeBasedMyAgendaCollection);

            //    var mainStack = new StackLayout
            //    {
            //        HorizontalOptions = LayoutOptions.FillAndExpand,
            //        VerticalOptions = LayoutOptions.FillAndExpand,
                    
            //        Children = { mySessions }
            //    };


            //    BindingContext = App.ViewModel;

            //    Content = mainStack;
            //}

            title = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Medium),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Color.White,
                Text = "It seems you have not marked any session as Favorite.",
                FontAttributes = FontAttributes.Bold,
                //IsVisible = false
            };

            titleStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10),
                Children = { title }
            };

            var mySessions = CreateList(true);
            mySessions.SetBinding<MainViewModel>(ListView.ItemsSourceProperty, s => s.TimeBasedMyAgendaCollection);

            var mainStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,

                Children = { titleStack, mySessions }
            };


            BindingContext = App.ViewModel;

            Content = mainStack;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.ViewModel.UpdateSavedSessions();
            if (App.ViewModel.TimeBasedMyAgendaCollection.Any())
                titleStack.IsVisible = false;
            else
                titleStack.IsVisible = true;
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
