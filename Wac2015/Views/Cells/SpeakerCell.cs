using System;
using Wac2015.Controls;
using Wac2015.Converters;
using Wac2015.Helpers;
using Wac2015.Models;
using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
    public class SpeakerCell : ViewCell
    {
        public SpeakerCell()
        {
            var nameLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
					Font.SystemFontOfSize(NamedSize.Large),
					Font.SystemFontOfSize(NamedSize.Large)),
                    Text = "Name"
            };


			//first.SetBinding<Speaker>(Label.TextProperty, s => s.Name);

            var name = new Label
            {
				VerticalOptions = LayoutOptions.Center,
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
					Font.SystemFontOfSize(22),
					Font.SystemFontOfSize(NamedSize.Large)),
				LineBreakMode = LineBreakMode.NoWrap,
                TextColor = Device.OnPlatform(App.XamBlue, Color.White, Color.White)
            };
            name.SetBinding<Speaker>(Label.TextProperty, s => s.Name);


            var companyLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                    Font.SystemFontOfSize(NamedSize.Large),
                    Font.SystemFontOfSize(NamedSize.Large)),
                Text = "Position"
            };

			var company = new Label
			{
				VerticalOptions = LayoutOptions.Center,
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                    Font.SystemFontOfSize(NamedSize.Large),
                    Font.SystemFontOfSize(NamedSize.Large)),
				TextColor = App.XamGray,
				LineBreakMode = LineBreakMode.WordWrap
			};
			company.SetBinding<Speaker>(Label.TextProperty, s => s.TagLine);
            CircleImage photo = new CircleImage();
            //try
            //{
                int photoSize = Device.OnPlatform(80, 80, 80);
                 photo = new CircleImage
                {
                    WidthRequest = photoSize,
                    HeightRequest = photoSize,
                    Aspect = Aspect.AspectFill,
                    HorizontalOptions = LayoutOptions.Center,
                    

                };
                 photo.SetBinding<Speaker>(Image.SourceProperty, s => s.HeadshotLink,
                 converter: new CachableImageSourceFromLinkConverter(TimeSpan.FromDays(3), Utils.GetFile("missingprofile.png")));
                 //photo.SetBinding<Speaker>(Image.SourceProperty, s => s.Id,
                 //     BindingMode.Default, converter: new SpeakerImageConveter());
            //}
            //catch (Exception ex)
            //{
            //    var msg = ex.Message;
            //}

            var stackright = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 5,
                Children = { name, company }
            };

            //var stackname = new StackLayout
            //{
            //    Orientation = StackOrientation.Horizontal,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    Spacing = 5,
            //    Children = {photo, stackright}
            //};


			var stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(Device.OnPlatform(5, 10, 10), Device.OnPlatform(5, 10, 20)),
				Spacing = 5,
				Children = {photo, stackright}
            };

			View = stack;
        }
    }
}