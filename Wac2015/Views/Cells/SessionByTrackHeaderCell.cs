using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
    public class SessionByTrackHeaderCell : ViewCell
    {
        public string Key = "Location";
        public SessionByTrackHeaderCell()
        {
            Height = Device.OnPlatform(25, 25, 40);
            var day = new Label
            {
                Font = Font.SystemFontOfSize(NamedSize.Small, FontAttributes.Bold),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center
            };

            day.SetBinding(Label.TextProperty, Key);

			if (Device.OS == TargetPlatform.WinPhone) {
				View = new StackLayout {
					Spacing = 5,
					Padding = 5,
					WidthRequest = 600,
					VerticalOptions = LayoutOptions.Center,
					HorizontalOptions = LayoutOptions.StartAndExpand,
					Orientation = StackOrientation.Horizontal,
					Children = { day },
					HeightRequest = Height,
					BackgroundColor = App.XamBlue,
				};
			}
			/*else
			if (Device.OS == TargetPlatform.Android) 
			{
				View = new Frame
				{
						HasShadow = false,
						OutlineColor = App.XamBlue,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					HeightRequest = Height,
					BackgroundColor = App.XamBlue,
					Padding = 5,
					Content = new StackLayout
					{
						Spacing = 5,
						Orientation = StackOrientation.Horizontal,
						Children = {day, month}
					}
				};
			}*/
            else
            {
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
                        Children = {day }
                    }
                };
            }
        }
    }
}