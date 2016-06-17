using Wac2015.Helpers;
using Wac2015.Models;
using Wac2015.ViewModels;
using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
    public class SessionCell : ViewCell
    {
        public bool DisplayTime = false;

        public SessionCell(bool displayTime = false)
        {
            this.DisplayTime = displayTime;
            //Label timeLabel = null;
            Grid footer = null;
            var title = new Label
            {
                VerticalOptions = LayoutOptions.End,
                Font = Device.OnPlatform(
                    Font.SystemFontOfSize(NamedSize.Small),
                    Font.SystemFontOfSize(NamedSize.Large),
                    Font.SystemFontOfSize(NamedSize.Medium)),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Device.OnPlatform(Color.White, Color.White, Color.White)
            };

            var titleContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Content = title
            };

            title.SetBinding<Session>(Label.TextProperty, s => s.Title);

            var track = new Label
            {
                VerticalOptions = LayoutOptions.End,
                Font = Device.OnPlatform(Font.SystemFontOfSize(NamedSize.Small),
                    Font.SystemFontOfSize(NamedSize.Small),
                    Font.SystemFontOfSize(NamedSize.Small)),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Device.OnPlatform(App.XamBlue, App.XamBlue, Color.White)
            };
            track.SetBinding<Session>(Label.TextProperty, s => s.TrackName);

            if (DisplayTime)
            {
                var timeLabel = new Label
                {
                    VerticalOptions = LayoutOptions.End,
                    Font = Device.OnPlatform(Font.SystemFontOfSize(NamedSize.Small),
                        Font.SystemFontOfSize(NamedSize.Small),
                        Font.SystemFontOfSize(NamedSize.Small)),
                    LineBreakMode = LineBreakMode.WordWrap,
                    TextColor = Device.OnPlatform(App.XamBlue, App.XamBlue, Color.White)
                };
                timeLabel.SetBinding<Session>(Label.TextProperty, s => s.DateTimeDisplay);

                footer = new Grid
                {
                    Padding = new Thickness(Device.OnPlatform(5, 5, 5), 5),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand

                };

                footer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                footer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                footer.Children.Add(track);
                footer.Children.Add(timeLabel);

                Grid.SetColumn(track, 0);
                Grid.SetColumn(timeLabel, 1);
            }

            var gridContent = new Grid
            {
                Padding = new Thickness(Device.OnPlatform(5, 5, 5), 5),
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent
            };


            gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            gridContent.Children.Add(titleContent);
            if(DisplayTime)
                gridContent.Children.Add(footer);
            else
                gridContent.Children.Add(track);

            Grid.SetRow(titleContent, 0);

            if(DisplayTime)
                Grid.SetRow(footer, 1);
            else
                Grid.SetRow(track, 1);
            View = gridContent;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            //Height = Device.OnPlatform(100, 50, 80);
        }
    }
}