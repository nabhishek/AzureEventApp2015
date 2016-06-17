using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Wac2015.Views
{
    public class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            Content = new WebView {Source = "https://www.microsoft.com/en-sg/MicrosoftDeveloperDay"};

        
        }
    }
}
