using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Wac2015.Controls;
using Xamarin.Forms;

namespace Wac2015.Views
{
    public class AboutPage : ContentPage
    {
        public AboutPage()
        {
            Title = "About us";
            BackgroundColor = Color.FromHex("2b1e6d");
            
            var aboutUsText = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = Device.OnPlatform("This is a conference companion application, that helps you get the conference details.\n\n" +
                       //"Get all the live updates regarding the Azure Conference 2015, Pune. " +
                       "Using this application you can navigate through all the sessions and plan your own agenda. " +
                       "This also lets you provide feedback for each session. " +
                       "Go green, go paperless! Use this conference buddy application." +
                       "\n\n\nDeveloped by:\n\n" + 
                       "Anubhav Ranjan \t\t@anubhavr05\n" +
                       "Prashant C \t\t\t@prshntvc\n" +
                       "Abhishek Narain \t\t@narainabhishek\n" + 
                       "Saurabh Kirtani \t\t@saurabhkirtani",
                       "This is a conference companion application, that helps you get the conference details.\n" +
                       "Get all the live updates regarding the Azure Conference 2015, Pune. " +
                       "Using this application you can navigate through all the sessions and plan your own agenda. " +
                       "This also lets you provide feedback for each session. " +
                       "Go green, go paperless! Use this conference buddy application." +
                       "\n\nDeveloped by:\n", "")
            };

            var link1 = new LinkLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Anubhav Ranjan \t\t$$@anubhavr05|https://twitter.com/anubhavr05$$ "
            };

            var link2 = new LinkLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Abhishek Narain \t\t$$@narainabhishek|https://twitter.com/narainabhishek$$ "
            };

            var link3 = new LinkLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Saurabh Kirtani \t\t\t$$@saurabhkirtani|https://twitter.com/saurabhkirtani$$ "
            };

            var image = new Image
            {
                Source = "splash.png"
            };

            var mainStack = new StackLayout
            {
                Padding = new Thickness(10, 10),
                Children = {
					aboutUsText, link1, link2, link3, image
				}
            };

            var scrollViewer = new ScrollView
            {
                Content = mainStack
            };

            Content = scrollViewer;

            //Content = new StackLayout
            //{
            //    Padding = new Thickness(10,10),
            //    Children = {
            //        aboutUsText, image
            //    }
            //};
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
