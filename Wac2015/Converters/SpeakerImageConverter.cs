using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Helpers;
using Wac2015.Models;
using Xamarin.Forms;

namespace Wac2015.Converters
{
    public class SpeakerImageConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var speaker = value as string;


            var imagesource = ImageSource.FromFile("images/speakers/" + speaker + ".png");
            return imagesource;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /*public class SpeakerImageConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var speaker = value as Speaker;


            if (speaker == null)
                return ImageSource.FromFile(Utils.GetFile("missingprofile.png"));

            var imagePath = "WAC2015." + speaker.Id + ".png";  //Utils.GetImagePath(speaker.Id, speaker.HeadshotUrl);
            return ImageSource.FromResource(imagePath);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }*/
}
