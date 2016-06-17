using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wac2015.Controls;
using Wac2015.Models;
using Wac2015.ViewModels;
using Wac2015.Views.Cells;
using Xamarin.Forms;

namespace Wac2015.Views
{
    public class NewsPage : ContentPage
    {
        private bool IsBackButtonPressed = false;
        public NewsPage()
        {
            Title = "News";
            BackgroundImage = "background.png";
            NavigationPage.SetHasNavigationBar(this, true);

            var news = CreateList(true);
            news.SetBinding<MainViewModel>(ListView.ItemsSourceProperty, s => s.NewsFeeds);

            var mainStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { news }
            };


            BindingContext = App.ViewModel;

            Content = mainStack;
        }


        private View CreateList(bool hasTitle = false)
        {
            var listView = new CustomListView
            {
                RowHeight = Device.OnPlatform(40, 50, 80),
                HasUnevenRows = true,
                BackgroundColor = Color.Transparent
            };

            var cell = new DataTemplate(() => new NewsCell());//(App.ViewModel));
            listView.ItemTemplate = cell;

            listView.ItemSelected += async (sender, e) =>
            {
                if (listView.SelectedItem == null)
                    return;
                var newsItem = (NewsPublish)e.SelectedItem;
                App.CurrentNews = newsItem;
                var newsDetailPage = new NewsDetailPage();
                await Navigation.PushAsync(newsDetailPage);

                listView.SelectedItem = null;
            };

            return listView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Factory.StartNew(async () =>
            {
                await App.ViewModel.LoadNews();
            });
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
                back_pressed = DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond;
            }

            return true;
        }
    }
}
