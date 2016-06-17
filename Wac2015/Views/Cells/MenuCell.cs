using Wac2015.Controls;
using Wac2015.Models;
using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
    public enum MenuOption
    {
        SessionAllByDay,
        SessionMyAgenda,
        SessionByTime,
        SessionByTrack,
        SessionBySpeaker
        
    }

    public class MenuCell : ViewCell
    {
        public string Text
        {
            get { return _label.Text; }
            set { _label.Text = value; }
        }

        public MenuTypes MenuOption { get; set; }

        readonly FontLabel _label;

        public MenuPage Host { get; set; }
        public bool IsWindowsPhone
        {
            get { return Device.OS == TargetPlatform.WinPhone; }
        }

        public MenuCell()
        {


            _label = new FontLabel
            {
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.White,
                FontLabelType = FontLabelType.Light,
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Thin", NamedSize.Small),
                    Font.SystemFontOfSize(18),
                    Font.SystemFontOfSize(40))
            };

            if (Device.OS == TargetPlatform.Android)
                _label.SetBinding(Label.TextProperty, "Name");


            var layout = new StackLayout
            {
                BackgroundColor = App.XamDarkBlue,
                Padding = new Thickness(25, 5, 10, 5),
                Spacing = 10,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { /*icon,*/ _label }
            };

            if (IsWindowsPhone)
            {
                layout.Padding = new Thickness(25, 10, 10, 10);
            }
            View = layout;
        }

        protected override void OnTapped()
        {
            base.OnTapped();
            //we are using lsitview taps
            if (Device.OS == TargetPlatform.Android)
                return;

            Host.Selected(MenuOption);
        }
    }
}

