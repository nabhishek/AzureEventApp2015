using System;
using System.Collections.Generic;
using System.Globalization;
using Wac2015.Controls;
using Wac2015.Models;
using Wac2015.ViewModels;
using Wac2015.Views.Cells;
using Xamarin;
using Xamarin.Forms;

namespace Wac2015.Views
{
    public class SessionPage : TabbedPage
    {
        private readonly SessionViewModel _viewModel;
        private Button feedbackButton;
        private ITrackHandle handle;
        public SessionPage()
        {
            _viewModel = new SessionViewModel();

            //if (!Xamarin.Insights.IsInitialized)
            //{
            //    Xamarin.Insights.Initialize("cb9dddf47d18b81b88181a4f106fcb7565048148");
            //    Insights.ForceDataTransmission = true;
            //    if (!string.IsNullOrEmpty(App.uuid))
            //    {

            //        var manyInfos = new Dictionary<string, string> {
            //        { Xamarin.Insights.Traits.GuestIdentifier, App.uuid },
            //        { "CurrentCulture", CultureInfo.CurrentCulture.Name } 
            //    };

            //        Xamarin.Insights.Identify(App.uuid, manyInfos);
            //    }
            //}
            IDictionary<string, string> info = new Dictionary<string, string>();
            if (App.CurrentSession != null)
                info = new Dictionary<string, string> { { "SessionId", App.CurrentSession.Id } };
            handle = Insights.TrackTime("SessionDetailsTime", info);
            //_viewModel.Error += (sender, args) => DisplayAlert("Sorry about the mess.", args.ErrorMessage, "OK");

            Title = "Session Details";

            //var share = new ToolbarItem
            //{
            //    Icon = Utils.GetFile("ic_action_social_share.png"),
            //    Name = "Share",
            //    Command = new Command(() => DependencyService.Get<IShare>().ShareText("I can't wait to check out " + _viewModel.Session.Title + " at #XamarinEvolve"))
            //};

            //ToolbarItems.Add(share);

            var titleLabel = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Title"
            };

            var favoriteImageSize = Device.OnPlatform(50, 50, 50);
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "TapCommand");

            var favoriteImage = new Image
            {
                WidthRequest = favoriteImageSize,
                HeightRequest = 20,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Source = ImageSource.FromFile("heart2.png")
            };
            favoriteImage.SetBinding<SessionViewModel>(IsVisibleProperty, s => s.IsFavorite);
            favoriteImage.GestureRecognizers.Add(tapGestureRecognizer);

            var unfavoriteImage = new Image
            {
                WidthRequest = favoriteImageSize,
                HeightRequest = 20,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Source = ImageSource.FromFile("heart2empty.png")
            };
            unfavoriteImage.SetBinding<SessionViewModel>(IsVisibleProperty, s => s.IsUnFavorite);
            unfavoriteImage.GestureRecognizers.Add(tapGestureRecognizer);

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Absolute) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            grid.Children.Add(titleLabel);
            grid.Children.Add(favoriteImage);
            grid.Children.Add(unfavoriteImage);

            Grid.SetColumn(titleLabel, 0);
            Grid.SetColumn(favoriteImage, 1);
            Grid.SetColumn(unfavoriteImage, 1);

            feedbackButton = new Button
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                TextColor = Device.OnPlatform(Color.White, Color.White, Color.White),
                Text = "Add Feedback",
                BorderColor = Color.White,
                BorderRadius = 5
            };

            feedbackButton.Clicked += async (sender, args) =>
            {
                if ((App.DayOneFeedbackCount == 3 && !App.DayOneExtFeedback)
                    || (App.DayTwoFeedbackCount == 3 && !App.DayTwoExtFeedback))
                {
                    var feedbackPage = new ExtendedSessionFeedback();
                    await Navigation.PushAsync(feedbackPage);
                }
                else
                {
                    var feedbackPage = new SessionFeedbackPage();
                    await Navigation.PushAsync(feedbackPage);
                }

            };

            var title = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Color.White
            };

            title.SetBinding<SessionViewModel>(Label.TextProperty, s => s.Session.Title);

            var timeLabel = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Time"
            };

            var time = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Color.White
            };

            time.SetBinding<SessionViewModel>(Label.TextProperty, s => s.Session.DateTimeDisplay);

            var locationLabel = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Track Name"
            };

            var location = new Label
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Color.White
            };

            location.SetBinding<SessionViewModel>(Label.TextProperty, s => s.Session.TrackName);

            var descriptionLabel = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Description"
            };

            var description = new Label
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(NamedSize.Large), Font.SystemFontOfSize(22)),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = Color.White
            };

            description.SetBinding<SessionViewModel>(Label.TextProperty, s => s.Session.Abstract);

            var sessionStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { grid, feedbackButton, title, timeLabel, time, locationLabel, location, descriptionLabel, description },
                Padding = new Thickness(5, 20),
                //BackgroundColor = Color.Transparent
            };

            var scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.Fill,
                Orientation = ScrollOrientation.Vertical,
                Content = sessionStack,
                //BackgroundColor = Color.Transparent
            };

            var sessionPage = new ContentPage
            {
                Title = "By Track",
                Content = scrollView,
                BackgroundImage = "background.png"
            };
            BackgroundImage = "background.png";

            var listView = new CustomListView
            {
                RowHeight = Device.OnPlatform(400, 70, 125),
                ItemTemplate = new DataTemplate(typeof(SpeakerDetailsCelliOS)),
                HasUnevenRows = true,
                IsGroupingEnabled = false,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.FillAndExpand
                
                //GroupShortNameBinding = new Binding("Key")//Remove for now as it causes crash on iOS if you scroll too fast:(
            };

            listView.SetBinding<SessionViewModel>(ListView.ItemsSourceProperty, x => x.Speakers);
            listView.ItemSelected += async (sender, e) =>
            {
                listView.SelectedItem = null;
            };

            var speakersStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { listView },
                Padding = new Thickness(5, 20),
                //BackgroundColor = Color.Transparent
            };

            var speakersPage = new ContentPage
            {
                Content = speakersStack,
                Title = "Speakers",
                BackgroundImage = "background.png"
            };


            Children.Add(sessionPage);
            Children.Add(speakersPage);


            //int speakerPhotoSize = Device.OnPlatform(80, 80, 100);
            //speakerPhoto = new CircleImage
            //{
            //    WidthRequest = speakerPhotoSize,
            //    HeightRequest = speakerPhotoSize,
            //    Aspect = Aspect.AspectFill,
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center,
            //    Source = Utils.GetFile("missing.png")
            //};
            //speakerPhoto.SetBinding<SessionViewModel>(Image.SourceProperty, s => s.Speakers,
            //    converter: new MainSpeakerImageConveter(_viewModel));

            //    Color color = App.XamDarkBlue;
            //    var favorite = new Button
            //    {
            //        Text = " ADDED TO FAVORITES ", //to do put back in binding
            //        HeightRequest = Device.OnPlatform<double>(25, 30, 70),
            //        WidthRequest = Device.OnPlatform(150, 150, 220),
            //        BackgroundColor = Color.Transparent,
            //        TextColor = color,
            //        Font = Font.SystemFontOfSize(NamedSize.Micro),
            //        BorderWidth = 1,
            //        BorderRadius = 5,
            //        BorderColor = color,
            //        HorizontalOptions = LayoutOptions.End,
            //        IsVisible = false
            //    };

            //    favorite.SetBinding<SessionViewModel>(IsVisibleProperty, x => x.IsBusy,
            //        converter: new ReverseBooleanConverter());

            //    favorite.SetBinding<SessionViewModel>(Button.CommandProperty, s => s.IsFavoriteSession,
            //        converter: new FavoriteSessionConverter(favorite, _viewModel, Navigation));

            //    double relativeHeight = bannerImageHeight + (speakerPhotoSize * .75);
            //    var relativeLayout = new RelativeLayout
            //    {
            //        HeightRequest = relativeHeight,
            //        VerticalOptions = LayoutOptions.Start
            //    };

            //    relativeLayout.Children.Add(bannerImageStack, null, null, Constraint.RelativeToParent(v => v.Width));
            //    relativeLayout.Children.Add(title,
            //        Constraint.Constant(10),
            //Constraint.RelativeToParent(parent => 5), Constraint.RelativeToParent(parent => (parent.Width - 20)), Constraint.Constant(bannerImageHeight * .6));


            //    relativeLayout.Children.Add(by,
            //Constraint.Constant(10),
            //        Constraint.Constant(bannerImageHeight * .68),
            //Constraint.RelativeToParent(parent => (parent.Width - 20)));

            //    relativeLayout.Children.Add(speakerPhoto,
            //        // ReSharper disable once PossibleLossOfFraction
            //        Constraint.Constant(speakerPhotoSize / 4),
            //        Constraint.Constant(bannerImageHeight - speakerPhotoSize / 4));

            //    double addHeightForFravorite = Device.OnPlatform(favorite.HeightRequest / 2, favorite.HeightRequest / 2, 0);
            //    relativeLayout.Children.Add(favorite, Constraint.RelativeToParent(v =>
            //    {
            //        // ReSharper disable once CompareOfFloatsByEqualityOperator
            //        var width = Device.OnPlatform(150, 150, 200);
            //        return v.Width - 35 - width;
            //    }), Constraint.Constant(bannerImageHeight + addHeightForFravorite));


            //    var whenWhere = new Label
            //    {
            //        Text = "WHEN AND WHERE",
            //        TextColor = App.XamLightGray,
            //        Font = Font.SystemFontOfSize(NamedSize.Small, FontAttributes.Bold)
            //    };

            //    var timeLabel = new Label
            //    {
            //        Font = Device.OnPlatform(Font.SystemFontOfSize(NamedSize.Small),
            //            Font.SystemFontOfSize(NamedSize.Medium),
            //            Font.SystemFontOfSize(NamedSize.Medium)),
            //        TextColor = App.XamGray
            //    };

            //    timeLabel.SetBinding<SessionViewModel>(Label.TextProperty, s => s.Session,
            //        converter: new FullSessionDateTimeConverter());

            //    var location = new Label
            //    {
            //        Font = Device.OnPlatform(Font.SystemFontOfSize(NamedSize.Small),
            //          Font.SystemFontOfSize(NamedSize.Medium),
            //                  Font.SystemFontOfSize(NamedSize.Medium)),
            //        TextColor = App.XamGray,
            //    };
            //    location.SetBinding<SessionViewModel>(Label.TextProperty, s => s.Room, converter: new RoomTitleConverter());


            //    var details = new Label
            //    {
            //        Text = "DETAILS",
            //        TextColor = App.XamLightGray,
            //        Font = Font.SystemFontOfSize(NamedSize.Small, FontAttributes.Bold)
            //    };

            //    var description = new HTMLLabel
            //    {
            //        Font = Device.OnPlatform(Font.SystemFontOfSize(NamedSize.Small),
            //          Font.SystemFontOfSize(NamedSize.Small),
            //          Font.SystemFontOfSize(NamedSize.Small)),
            //        TextColor = App.XamGray
            //    };
            //    description.SetBinding<SessionViewModel>(Label.TextProperty, s => s.Description, converter: new DetailsNotAvailableConverter("Details are not available at this time."));

            /*var descriptionScroll = new ContentView
            {
        VerticalOptions = LayoutOptions.FillAndExpand,
        HorizontalOptions = LayoutOptions.FillAndExpand,
                Content = new ScrollView
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Content = description
                }
            };*/

            //var alarmButton = new Button
            //{
            //    TextColor = Color.White,
            //    BorderRadius = 5,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    HeightRequest = Device.OnPlatform<double>(35, 35, 70),
            //    Font = Font.SystemFontOfSize(NamedSize.Small),
            //    Command = _viewModel.ToggleCalendarCommand,
            //    IsVisible = false
            //};


            //alarmButton.SetBinding<SessionViewModel>(BackgroundColorProperty, s => s.CalendarColor);
            //alarmButton.SetBinding<SessionViewModel>(IsVisibleProperty, s => s.CalendarVisible);
            //alarmButton.SetBinding<SessionViewModel>(Button.TextProperty, s => s.CalendarText);

            //var feedback = new Button
            //{
            //    Text = "Provide Feedback", //to do put back in binding
            //    TextColor = Color.White,
            //    BackgroundColor = App.XamBlue,
            //    BorderRadius = 5,
            //    BorderColor = App.XamBlue,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    HeightRequest = Device.OnPlatform<double>(35, 35, 70),
            //    Font = Font.SystemFontOfSize(NamedSize.Small),
            //    IsVisible = false
            //};

            //feedback.SetBinding<SessionViewModel>(Button.CommandProperty, s => s.FeedbackConditionsChanged, converter: new SessionFeedbackConverter(feedback, _viewModel, Navigation));

            //var gridContent = new Grid
            //{
            //    Padding = 10
            //};

            //gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //gridContent.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //gridContent.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //gridContent.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            //gridContent.Children.Add(whenWhere);
            //gridContent.Children.Add(timeLabel);
            //gridContent.Children.Add(location);
            //gridContent.Children.Add(details);
            //gridContent.Children.Add(description);
            //gridContent.Children.Add(alarmButton);
            //gridContent.Children.Add(feedback);


            //Grid.SetRow(whenWhere, 0);
            //Grid.SetColumnSpan(whenWhere, 2);

            //Grid.SetRow(timeLabel, 1);
            //Grid.SetColumnSpan(timeLabel, 2);

            //Grid.SetRow(location, 2);
            //Grid.SetColumnSpan(location, 2);

            //Grid.SetRow(details, 3);
            //Grid.SetColumnSpan(details, 2);

            //Grid.SetRow(description, 4);
            //Grid.SetColumnSpan(description, 2);

            //Grid.SetRow(alarmButton, 5);
            //Grid.SetColumnSpan(alarmButton, 2);
            //Grid.SetRow(feedback, 5);
            //Grid.SetColumnSpan(feedback, 2);

            //var gridMain = new Grid();

            //gridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(relativeHeight) });
            //gridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //gridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            //gridMain.Children.Add(relativeLayout);
            //gridMain.Children.Add(gridContent);
            //Grid.SetRow(relativeLayout, 0);
            //Grid.SetRow(gridContent, 1);

            //this.SetBinding(IsBusyProperty, "IsBusy");

            //Content = new ScrollView { Content = gridMain };
            BindingContext = _viewModel;


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (App.CurrentDayType == DayTypes.Day1)
            {
                if (!App.FeedbackList.Contains(App.CurrentSession.Id))
                {
                    feedbackButton.IsEnabled = true;
                }
            }
            else if (App.CurrentDayType == DayTypes.Day2)
            {
                if (!App.FeedbackList.Contains(App.CurrentSession.Id))
                {
                    feedbackButton.IsEnabled = true;
                }
            }
            else
            {
                feedbackButton.IsEnabled = false;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
            handle.Stop();
        }
    }
}
