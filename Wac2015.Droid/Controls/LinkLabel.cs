using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Text.Util;
using Android.Views;
using Android.Widget;
using Wac2015.Controls;
using Wac2015.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LinkLabel), typeof(LinkLabelRenderer))]
namespace Wac2015.Droid.Controls
{
    public class LinkLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var text = Control.Text;
            string partOne = "", partTwo = "", linkText = "", linkUrl = "";
            var isLinkPresent = false;
            if (text.Contains("$$"))
            {
                if (text.EndsWith("$$"))
                    text += ".";
                var splits = text.Split(new string[1] { "$$" }, StringSplitOptions.None);
                if (splits.Length == 3)
                {
                    partOne = splits[0];
                    var linkSplit = splits[1].Split('|');
                    if (linkSplit.Length == 2)
                    {
                        linkText = linkSplit[0];
                        linkUrl = linkSplit[1];
                        isLinkPresent = true;
                    }
                    partTwo = splits[2];
                }
            }

            //var url = App.CurrentNews.LinkUrl;
            //var link = App.CurrentNews.LinkText;
            //partOne = App.CurrentNews.NewsDetailsPartOne;
            //partTwo = App.CurrentNews.NewsDetailsPartTwo;

            if (isLinkPresent)
            {
                var finalText = String.Format("<a href=\"{0}\">{1}</a>", linkUrl, linkText);
                var finalString = String.Concat(partOne, finalText, partTwo);
                Control.Text = finalString;
                var text1 = Html.FromHtml(finalString);
                //Control.AutoLinkMask = MatchOptions.All;
                Control.LinksClickable = true;
                Control.SetText(text1, TextView.BufferType.Spannable);
                Control.MovementMethod = LinkMovementMethod.Instance;
                //var converted = Linkify.AddLinks(Control, MatchOptions.WebUrls);
            }
            else
            {
                var converted = Linkify.AddLinks(Control, MatchOptions.WebUrls);
            }




        }
    }
}