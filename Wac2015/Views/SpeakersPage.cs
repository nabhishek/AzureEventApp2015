using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Controls;
using Wac2015.Helpers;
using Wac2015.Models;
using Wac2015.ViewModels;
using Wac2015.Views.Cells;
using Xamarin.Forms;

namespace Wac2015.Views
{
    public class SpeakersPage : ContentPage
    {

        public SpeakersPage()
        {
            Title = "Speakers List";
            Icon = Device.OnPlatform(Utils.GetFile("slideout.png"),Utils.GetFile("slideout.png"),Utils.GetFile("slideout.png"));
            BackgroundImage = "background.png";
            NavigationPage.SetHasNavigationBar(this, true);

            BindingContext = App.ViewModel;
            //_viewModel.Error += (sender, args) => DisplayAlert("Sorry about the mess.", args.ErrorMessage, "OK");

            var listView = new CustomListView
            {
                RowHeight = Device.OnPlatform(90, 70, 125),
                ItemTemplate = new DataTemplate(typeof(SpeakerCell)),
                HasUnevenRows = true,
                IsGroupingEnabled = false,
                GroupDisplayBinding = new Binding("Speakers"),
                GroupHeaderTemplate = new DataTemplate(typeof(SpeakerHeaderCell)),
                BackgroundColor = Color.Transparent
                //GroupShortNameBinding = new Binding("Key")//Remove for now as it causes crash on iOS if you scroll too fast:(
            };

            listView.SetBinding<MainViewModel>(ListView.ItemsSourceProperty, x => x.Speakers);

            listView.ItemSelected += async (sender, e) =>
            {
                if (listView.SelectedItem == null)
                    return;

                var speaker = (Speaker)e.SelectedItem;
                App.CurrentSpeaker = speaker;
                var speakerPage = new SpeakerPage();
                await Navigation.PushAsync(speakerPage);

                listView.SelectedItem = null;
            };

            var mainStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { listView }
            };

            //mainStack.SetBinding<SpeakersViewModel>(IsBusyProperty, x => x.IsBusy);

            //if (Device.OS == TargetPlatform.WinPhone)
            //{
            //    mainStack.Padding = new Thickness(20, 10, 20, 0);
            //    mainStack.Children.Insert(0,
            //        new Label
            //        {
            //            Text = "SPEAKERS",
            //            Font = Font.SystemFontOfSize(NamedSize.Medium)
            //        }
            //        );
            //}
            Content = mainStack;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (_viewModel.IsInitialized)
            //    return;

            //_viewModel.IsInitialized = true;
            //_viewModel.LoadSpeakersCommand.Execute(null);
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
