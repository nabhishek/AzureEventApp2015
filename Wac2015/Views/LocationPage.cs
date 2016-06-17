using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Wac2015.Views
{
    public class LocationPage : ContentPage
    {
        public LocationPage()
        {
            var l = new Label
            {
                Text = "Developer Day\n\nAddress:\nUniversity Culture Centre Hall\n50 Kent Ridge Crescent\nNational University of Singapore\nSingapore 119279",
            };

            var openLocation = new Button
            {
                Text = "Locate"
            };
            openLocation.Clicked += (sender, e) => {

                if (Device.OS == TargetPlatform.iOS)
                {
                    //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                    Device.OpenUri(new Uri("http://maps.apple.com/?q=University+Cultural+Centre+Hall&sll=1.3017,103.7721&z=16&t=s"));
                }
                else if (Device.OS == TargetPlatform.Android)
                {
                    // opens the Maps app directly
                    Device.OpenUri(new Uri("geo:1.3017,103.7721?q=University+Cultural+Centre+Hall"));

                }
                else if (Device.OS == TargetPlatform.Windows)
                {
                    Device.OpenUri(new Uri("bingmaps:?where=394 Pacific Ave San Francisco CA"));
                }
            };

            var openDirections = new Button
            {
                Text = "Navigate"
            };
            openDirections.Clicked += (sender, e) => {

                if (Device.OS == TargetPlatform.iOS)
                {
                    //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                    Device.OpenUri(new Uri("http://maps.apple.com/?daddr=University+Cultural+Centre+Hall&sll=1.3017,103.7721"));

                }
                else if (Device.OS == TargetPlatform.Android)
                {
                    // opens the 'task chooser' so the user can pick Maps, Chrome or other mapping app
                    Device.OpenUri(new Uri("gogle.navigation:1.3017,103.7721?q=University+Cultural+Centre+Hall"));
                    //Device.OpenUri(new Uri("http://maps.google.com/?daddr=San+Francisco,+CA&saddr=Mountain+View"));
                }
                else if (Device.OS == TargetPlatform.Windows)
                {
                    Device.OpenUri(new Uri("bingmaps:?rtp=adr.394 Pacific Ave San Francisco CA~adr.One Microsoft Way Redmond WA 98052"));
                }
            };
            Content = new StackLayout
            {
                Padding = new Thickness(15, 20, 15, 10),
                HorizontalOptions = LayoutOptions.Fill,
                Children = {
                    l,
                    openLocation,
                    openDirections
                }
            };
        }
    }
    
}
