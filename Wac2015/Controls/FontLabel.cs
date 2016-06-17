using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Wac2015.Controls
{
    public enum FontLabelType
    {
        Thin = 0,
        ThinItalic = 1,
        Light = 2,
        LightItalic = 3,
        Regular = 4,
        Italic = 5,
        Medium = 6,
        MediumItalic = 7,
        Bold = 8,
        BoldItalic = 9,
        Black = 10,
        BlackItalic = 11,
        Condensed = 12,
        CondensedItalic = 13,
        CondensedBold = 14,
        CondensedBoldItalic = 15
    }

    public class FontLabel : Label
    {
        public static readonly BindableProperty FontLabelTypeProperty =
          BindableProperty.Create<FontLabel, FontLabelType>(
            p => p.FontLabelType, FontLabelType.Thin);

        public FontLabelType FontLabelType
        {
            get { return (FontLabelType)GetValue(FontLabelTypeProperty); }
            set { SetValue(FontLabelTypeProperty, value); }
        }
    }
}
