using LinqToTwitter;
using Xamarin.Forms;

namespace Wac2015.Views.Cells
{

  public class EventCell : ViewCell
  {

    public EventCell()
    {

      var size = Device.OnPlatform(50, 50, 60);
      Height = Device.OnPlatform(120, 120, 160);
      var pin = new Image
      {
        WidthRequest = size,
        HeightRequest = size,
        Aspect = Aspect.AspectFill,
        HorizontalOptions = LayoutOptions.Center,
        VerticalOptions = LayoutOptions.Center,
      };
      pin.SetBinding<Location>(Image.SourceProperty, s => s.MainEvent, converter: new EventPinConverter());


      var name = new Label
      {
        VerticalOptions = LayoutOptions.FillAndExpand,
        Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", 18),
        Font.SystemFontOfSize(NamedSize.Large),
        Font.SystemFontOfSize(NamedSize.Large)),
        LineBreakMode = LineBreakMode.TailTruncation
      };

      name.SetBinding(Label.TextProperty, "EventName");

      var date = new Label
      {
        Font = Device.OnPlatform(Font.OfSize("HelveticaNeue", 14),
        Font.SystemFontOfSize(NamedSize.Medium),
        Font.SystemFontOfSize(NamedSize.Medium)),
        LineBreakMode = LineBreakMode.TailTruncation
      };

      date.SetBinding<Location>(Label.TextProperty, s => s.DisplayDate);

      var venue = new Label
      {
        Font = Device.OnPlatform(Font.SystemFontOfSize(NamedSize.Small),
        Font.SystemFontOfSize(NamedSize.Small),
        Font.SystemFontOfSize(NamedSize.Small)),
        LineBreakMode = LineBreakMode.TailTruncation
      };

      venue.SetBinding<Location>(Label.TextProperty, s => s.Name);

      var address = new Label
      {
		Font = Device.OnPlatform(Font.SystemFontOfSize(NamedSize.Micro),
        Font.SystemFontOfSize(NamedSize.Small),
        Font.SystemFontOfSize(NamedSize.Small)),
        LineBreakMode = LineBreakMode.TailTruncation
      };
      
      address.SetBinding<Location>(Label.TextProperty, s => s.Address);

      var gridContent = new Grid
      {
        Padding = new Thickness(15, 10, 10, 10),
        VerticalOptions = LayoutOptions.FillAndExpand
      };


      gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
      gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
      gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
      gridContent.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
      gridContent.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.12, GridUnitType.Star) });
      gridContent.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
      gridContent.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(.88, GridUnitType.Star) });

      gridContent.Children.Add(pin);
      gridContent.Children.Add(name);
      gridContent.Children.Add(date);
      gridContent.Children.Add(venue);
      gridContent.Children.Add(address);

      Grid.SetRowSpan(pin, 4);
      Grid.SetColumn(name, 2);
      Grid.SetColumn(date, 2);
      Grid.SetRow(date, 1);
      Grid.SetColumn(venue, 2);
      Grid.SetRow(venue, 2);
      Grid.SetColumn(address, 2);
      Grid.SetRow(address, 3);
      View = gridContent;

    }
  }
}
