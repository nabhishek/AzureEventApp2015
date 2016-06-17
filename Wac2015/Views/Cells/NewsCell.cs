using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Models;
using Xamarin.Forms;

namespace Wac2015.Views.Cells
{
    public class NewsCell: ViewCell
    {
        public NewsCell()
        {
            
            var title = new Label
            {
                VerticalOptions = LayoutOptions.End,
                Font = Device.OnPlatform(
                    Font.SystemFontOfSize(NamedSize.Small),
                    Font.SystemFontOfSize(NamedSize.Large),
                    Font.SystemFontOfSize(NamedSize.Medium)),
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
            };
            
            var titleContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Content = title,
                Padding = new Thickness(10)
            };

            title.SetBinding<NewsPublish>(Label.TextProperty, s => s.NewsHeader);
            
            View = titleContent;
        }
    }
}
