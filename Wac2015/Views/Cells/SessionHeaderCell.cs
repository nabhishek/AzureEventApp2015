using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
    public class SessionHeaderCell : ViewCell
    {
        public string Key = "Key";
        public SessionHeaderCell()
        {
            Height = Device.OnPlatform(25, 25, 40);
            var day = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Small, FontAttributes.Bold),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center
            };

            day.SetBinding(Label.TextProperty, Key);


            View = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = Height,
                BackgroundColor = App.XamBlue,
                Padding = 5,
                Content = new StackLayout
                {
                    Spacing = 5,
                    Orientation = StackOrientation.Horizontal,
                    Children = { day }
                }
            };

        }
    }
}