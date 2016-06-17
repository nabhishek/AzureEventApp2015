using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Wac2015.Controls;
using Xamarin.Forms;

namespace Wac2015.Views
{
    public class NewsDetailPage : ContentPage
    {
        public NewsDetailPage()
        {
            Title = "News Details";

            var isLinkPresent = false;
            var newsitem = App.CurrentNews;

            if (newsitem.NewsDetails.Contains("[New_Line]"))
                newsitem.NewsDetails = newsitem.NewsDetails.Replace("[New_Line]", "\n");

            //if (newsitem.NewsDetails.Contains("$$"))
            //{
            //    if (newsitem.NewsDetails.EndsWith("$$"))
            //        newsitem.NewsDetails += ".";
            //    var splits = newsitem.NewsDetails.Split(new string[1] { "$$" }, StringSplitOptions.None);
            //    if (splits.Length == 3)
            //    {
            //        newsitem.NewsDetailsPartOne = splits[0];
            //        var linkSplit = splits[1].Split('|');
            //        if (linkSplit.Length == 2)
            //        {
            //            newsitem.LinkText = linkSplit[0];
            //            newsitem.LinkUrl = linkSplit[1];
            //            isLinkPresent = true;
            //        }
            //        newsitem.NewsDetailsPartTwo = splits[2];
            //    }
            //}


            if (!isLinkPresent)
            {
                //var titleLabel = new FontLabel
                //{
                //    Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Medium),
                //      Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                //    FontLabelType = FontLabelType.Light,
                //    LineBreakMode = LineBreakMode.WordWrap,
                //    TextColor = App.XamBlue,
                //    Text = App.CurrentNews.NewsHeader,
                //    FontAttributes = FontAttributes.Bold
                //};



                //var description = new LinkLabel
                //{
                //    Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                //      Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                //    LineBreakMode = LineBreakMode.WordWrap,
                //    TextColor = Color.White,
                //    Text = App.CurrentNews.NewsDetails
                //};

                //var mainStack = new StackLayout
                //{
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    VerticalOptions = LayoutOptions.FillAndExpand,
                //    Children = { titleLabel, description },
                //    Padding = new Thickness(10, 10)
                //};

                //var scrollView = new ScrollView
                //{
                //    VerticalOptions = LayoutOptions.Fill,
                //    Orientation = ScrollOrientation.Vertical,
                //    Content = mainStack,
                //};


                //Content = scrollView;

            }
            else
            {
                //var titleLabel = new FontLabel
                //{
                //    Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Medium),
                //      Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                //    FontLabelType = FontLabelType.Light,
                //    LineBreakMode = LineBreakMode.WordWrap,
                //    TextColor = App.XamBlue,
                //    Text = App.CurrentNews.NewsHeader,
                //    FontAttributes = FontAttributes.Bold
                //};

                //var description = new LinkLabel
                //{
                //    Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                //      Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                //    LineBreakMode = LineBreakMode.WordWrap,
                //    TextColor = Color.White,
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    VerticalOptions = LayoutOptions.StartAndExpand,
                //    Text = App.CurrentNews.NewsDetails
                //};

                //var description1 = new Label
                //{
                //    Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                //      Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                //    LineBreakMode = LineBreakMode.NoWrap,
                //    TextColor = Color.White,
                //    HorizontalOptions = LayoutOptions.StartAndExpand,
                //    VerticalOptions = LayoutOptions.StartAndExpand,
                    
                //    Text = App.CurrentNews.NewsDetailsPartOne
                //};


                //var linkText = new Label
                //{
                //    Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                //      Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                //    LineBreakMode = LineBreakMode.WordWrap,
                //    TextColor = App.XamBlue,
                //    VerticalOptions = LayoutOptions.StartAndExpand,
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    Text = App.CurrentNews.LinkText
                //};

                //var tap = new TapGestureRecognizer((view, obj) =>
                //{
                //    Device.OpenUri(new Uri(App.CurrentNews.LinkUrl));
                //});

                //linkText.GestureRecognizers.Add(tap);

                //var description2 = new Label
                //{
                //    Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                //      Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                //    LineBreakMode = LineBreakMode.WordWrap,
                //    TextColor = Color.White,
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    VerticalOptions = LayoutOptions.StartAndExpand,
                //    Text = App.CurrentNews.NewsDetailsPartTwo
                //};

                //var descriptionStack = new StackLayout
                //{
                //    Orientation = StackOrientation.Horizontal,
                //    VerticalOptions = LayoutOptions.StartAndExpand,
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    Children = { description1, linkText, description2 }
                //};

                //var finalString = new LinkLabel
                //{
                //    Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                //      Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                //    LineBreakMode = LineBreakMode.WordWrap,
                //    TextColor = Color.White,
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    VerticalOptions = LayoutOptions.StartAndExpand,
                //    Text = App.CurrentNews.NewsDetails
                //};


                //var mainStack = new StackLayout
                //{
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    VerticalOptions = LayoutOptions.FillAndExpand,
                //    Children = { titleLabel, description },
                //    Padding = new Thickness(10, 10)
                //};

                //var scrollView = new ScrollView
                //{
                //    VerticalOptions = LayoutOptions.Fill,
                //    Orientation = ScrollOrientation.Vertical,
                //    Content = mainStack,
                //};


                //Content = scrollView;
            
            }

            var titleLabel = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Medium),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = App.CurrentNews.NewsHeader,
                FontAttributes = FontAttributes.Bold
            };



            var description = new LinkLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Color.White,
                Text = App.CurrentNews.NewsDetails
            };

            var mainStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { titleLabel, description },
                Padding = new Thickness(10, 10)
            };

            var scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.Fill,
                Orientation = ScrollOrientation.Vertical,
                Content = mainStack,
            };


            Content = scrollView;

            BackgroundImage = "background.png";

            //BindingContext = App.ViewModel;
        }
    }
}
