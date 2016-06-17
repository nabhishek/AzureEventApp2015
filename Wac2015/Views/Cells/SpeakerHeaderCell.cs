using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
    public class SpeakerHeaderCell : ViewCell
    {

        public SpeakerHeaderCell()
        {
            Height = Device.OnPlatform(25, 25, 50);
            var title = new Label
            {
                Font = Font.SystemFontOfSize(Device.OnPlatform(NamedSize.Small, NamedSize.Small, NamedSize.Large),
                    Device.OnPlatform(FontAttributes.Bold, FontAttributes.Bold, FontAttributes.None)),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = App.XamBlue,
            };

			title.SetBinding(Label.TextProperty, "Key");

			if (Device.OS == TargetPlatform.WinPhone)
            {
                title.WidthRequest = Height;
                title.VerticalOptions = LayoutOptions.CenterAndExpand;
                title.HorizontalOptions = LayoutOptions.CenterAndExpand;
                View = title;
            }
			/*else if(Device.OS == TargetPlatform.Android)
			{
				var frame = new Frame
				{		

					HeightRequest = Height,
					BackgroundColor = App.XamBlue,
					Padding = new Thickness(10, 5),
					Content=title,
					IsClippedToBounds = true,
					OutlineColor = App.XamBlue,
					HasShadow = false
				};

				View = frame;
			}*/
            else
            {
				View = new ContentView
				{		

                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = Height,
                    BackgroundColor = App.XamBlue,
                    Padding = 5,
                    Content = title
                };
            }
        }
    }
}