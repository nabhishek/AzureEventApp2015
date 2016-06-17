using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
  public class SponsorHeaderCell : ViewCell
  {
    public SponsorHeaderCell()
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

      title.SetBinding(Label.TextProperty, "Key.Name");
      //title.SetBinding(Label.BackgroundColorProperty, "Key.Color");

      if (Device.OS == TargetPlatform.WinPhone)
      {
        var stack = new StackLayout
        {
          Spacing = 5,
          Padding = 5,
          WidthRequest = 600,
          VerticalOptions = LayoutOptions.Center,
          HorizontalOptions = LayoutOptions.StartAndExpand,
          Orientation = StackOrientation.Horizontal,
          Children = { title },
          HeightRequest = Height,
          BackgroundColor = App.XamBlue
        };
        //stack.SetBinding(Label.BackgroundColorProperty, "Key.Color");
        View = stack;
      }
		/*else if(Device.OS == TargetPlatform.Android)
		{
			var frame = new Frame
			{
				HasShadow = false,
				OutlineColor = App.XamBlue,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = Height,
				BackgroundColor = App.XamBlue,
				Padding = new Thickness(10, 5),
				Content = title
			};

			//frame.SetBinding(Frame.BackgroundColorProperty, "Key.Color");
			//frame.SetBinding(Frame.OutlineColorProperty, "Key.Color");
			View = frame;
		}*/
      else
      {
		var frame = new ContentView
        {
          HorizontalOptions = LayoutOptions.FillAndExpand,
          HeightRequest = Height,
          BackgroundColor = App.XamBlue,
          Padding = 5,
          Content = title
        };

        //frame.SetBinding(Label.BackgroundColorProperty, "Key.Color");
        View = frame;
      }
    }
  }
}