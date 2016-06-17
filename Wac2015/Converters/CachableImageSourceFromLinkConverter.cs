using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Wac2015.Converters
{
    public class CachableImageSourceFromLinkConverter : IValueConverter
    {
        //private readonly string _linkRelation;
        private readonly TimeSpan _cacheFor;
        private readonly string _defaultImage;

        public CachableImageSourceFromLinkConverter(TimeSpan? cacheFor = null, string defaultImage = null)
        {
            //_linkRelation = linkRelation;
            _cacheFor = cacheFor.HasValue ? cacheFor.Value : TimeSpan.FromDays(3);
            _defaultImage = defaultImage;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var link = value as string;
            if (String.IsNullOrEmpty(link))
                return _defaultImage == null ? null : ImageSource.FromFile(_defaultImage);

            return new UriImageSource
            {
                Uri = new Uri(link),
                CachingEnabled = true,
                CacheValidity = _cacheFor
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
