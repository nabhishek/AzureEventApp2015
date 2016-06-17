using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
  public class SpeakerSessionCell : ViewCell
  {
		public SpeakerSessionCell()
		{
      Height = Height = Device.OnPlatform(66, 66, 100); ;
			var title = new Label
			{
				VerticalOptions = LayoutOptions.Center,
				Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Medium),
					Font.SystemFontOfSize(NamedSize.Medium),
					Font.SystemFontOfSize(NamedSize.Medium)),
				LineBreakMode = LineBreakMode.TailTruncation
			};
			title.SetBinding<SessionState>(Label.TextProperty, s => s.Title);

			var date = new Label
			{
				VerticalOptions = LayoutOptions.Center,
				Font = Device.OnPlatform(Font.OfSize("HelveticaNeue", NamedSize.Small),
					Font.SystemFontOfSize(NamedSize.Small),
					Font.SystemFontOfSize(NamedSize.Small)),
				LineBreakMode = LineBreakMode.NoWrap
			};
			date.SetBinding(Label.TextProperty, new Binding(".",  converter : new FullSessionDateTimeConverter()));

			int photoSize = Device.OnPlatform(30, 30, 40);
			var photo = new Image
			{
				WidthRequest = photoSize,
				HeightRequest = photoSize,
				Aspect = Aspect.AspectFit,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Source = Utils.GetFile("presentation.png")
			};

			var stack = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				Padding = new Thickness(Device.OnPlatform(5, 10, 10), Device.OnPlatform(10, 10, 20)),
				Spacing = 10,
				Children = 
				{
					photo,
					new StackLayout
					{
						Children = { title, date}
					}
				}
			};

			View = stack;
		}
  }
}
