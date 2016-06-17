using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Controls;
using Wac2015.Converters;
using Wac2015.Helpers;
using Wac2015.Models;
using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
    public class SpeakerDetailsCelliOS : ViewCell
    {
        public SpeakerDetailsCelliOS()
        {
            var nameLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                    Font.SystemFontOfSize(NamedSize.Large),
                    Font.SystemFontOfSize(NamedSize.Large)),
                Text = "Name",
                TextColor = App.XamBlue
            };


            //first.SetBinding<Speaker>(Label.TextProperty, s => s.Name);

            var name = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                    Font.SystemFontOfSize(22),
                    Font.SystemFontOfSize(NamedSize.Large)),
                LineBreakMode = LineBreakMode.NoWrap,
                TextColor = Color.White
            };
            name.SetBinding<Speaker>(Label.TextProperty, s => s.Name);


            var companyLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                    Font.SystemFontOfSize(NamedSize.Large),
                    Font.SystemFontOfSize(NamedSize.Large)),
                Text = "Position",
                TextColor = App.XamBlue
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
            try
            {
                int photoSize = Device.OnPlatform(80, 100, 80);
                photo = new CircleImage
                {
                    WidthRequest = photoSize,
                    HeightRequest = photoSize,
                    Aspect = Aspect.AspectFill,
                    HorizontalOptions = LayoutOptions.Center,
                    //Source = ImageSource.FromFile("missingprofile.png")

                };
                photo.SetBinding<Speaker>(Image.SourceProperty, s => s.HeadshotLink,
                 converter: new CachableImageSourceFromLinkConverter(TimeSpan.FromDays(3), Utils.GetFile("missingprofile.png")));
                //photo.SetBinding<Speaker>(Image.SourceProperty, s => s,
                //    BindingMode.Default,
                //    converter: new SpeakerImageConveter());
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            var stackright = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 5,
                Children = { nameLabel, name, companyLabel, company }
            };

            var stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(Device.OnPlatform(0, 10, 10), Device.OnPlatform(0, 10, 20)),
                Spacing = 5,
                BackgroundColor = Color.Transparent,
                Children = { photo, stackright }
            };

            var aboutSpeaker = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                    Font.SystemFontOfSize(20),
                    Font.SystemFontOfSize(NamedSize.Large)),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Color.White
            };
            aboutSpeaker.SetBinding<Speaker>(Label.TextProperty, s => s.Bio);

            var scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.Fill,
                Orientation = ScrollOrientation.Vertical,
                Content = aboutSpeaker,
                HeightRequest = 300
            };

            var resultStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 15,
                Children = { stack, scrollView },
                BackgroundColor = Color.Transparent
            };

            View = resultStack;
        }
    }
}
