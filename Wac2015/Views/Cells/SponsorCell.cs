using System;
using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
  public class SponsorCell : ViewCell
  {
    public SponsorCell()
    {
      var name = new Label
      {
        HorizontalOptions = LayoutOptions.FillAndExpand,
        XAlign = TextAlignment.Center,
        Font = Font.SystemFontOfSize(NamedSize.Micro),
        TextColor = AppSettings.ColorMainApp
      };
      name.SetBinding<SponsorState>(Label.TextProperty, s => s.Name);

 

      int sponsorLogoSize = Device.OnPlatform(100, 100, 120);
      Height = sponsorLogoSize + 40;
      var sponsorLogo = new Image
            {
              HeightRequest = sponsorLogoSize,
              Aspect = Aspect.AspectFit,
              VerticalOptions = LayoutOptions.Center,
              HorizontalOptions = LayoutOptions.FillAndExpand
            };
      sponsorLogo.SetBinding<SponsorState>(Image.SourceProperty, s => s.Links,
        converter: new CachableImageSourceFromLinkConverter(LinkRelations.SponsorLogo, TimeSpan.FromDays(7)));

      var gridContent = new StackLayout
      {
        Padding = new Thickness(15),
        VerticalOptions = LayoutOptions.FillAndExpand,
        HorizontalOptions = LayoutOptions.FillAndExpand
      };

      if (Device.OS == TargetPlatform.WinPhone)
        gridContent.BackgroundColor = Color.FromHex("f1f1f1");

      
      gridContent.Children.Add(sponsorLogo);
      //gridContent.Children.Add(name);

      View = gridContent;
    }
  }
}