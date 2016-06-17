using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wac2015.Controls;
using Wac2015.Helpers;
using Wac2015.Models;
using Wac2015.ViewModels;
using Xamarin;
using Xamarin.Forms;

namespace Wac2015.Views
{
    public class ContactUsPage : ContentPage
    {
        private Entry registrationEntry;
        private long uuid = 0;
        private bool IsBackButtonPressed = false;
        private ITrackHandle handle;

        public ContactUsPage()
        {
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
            handle = Insights.TrackTime("ContactUsTime");
            var registrationIdLabel = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Registration Id *"
            };

            registrationEntry = new Entry
            {
                Placeholder = "XXXXXXXX",
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                Keyboard = Keyboard.Numeric,

            };

            registrationEntry.Unfocused += async (sender, args) =>
            {
                if (!String.IsNullOrEmpty(registrationEntry.Text) && !long.TryParse(registrationEntry.Text, out uuid))
                {
                    registrationEntry.Text = "";
                    await DisplayAlert("Invalid Registration Id!", "Please check your Tag for Registration Id.", "OK");
                    registrationEntry.Focus();
                }

            };

            var changeLink = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(14), Font.SystemFontOfSize(22)),
                FontAttributes = FontAttributes.Italic,
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                VerticalOptions = LayoutOptions.End,
                TextColor = App.XamDarkBlue,
                Text = "Edit"
            };

            var tap = new TapGestureRecognizer((view, obj) =>
            {
                registrationEntry.IsEnabled = true;
                registrationEntry.Focus();
                changeLink.IsVisible = false;
            });

            changeLink.GestureRecognizers.Add(tap);

            var registrationStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            if (!String.IsNullOrEmpty(App.uuid))
            {
                registrationEntry.Text = App.uuid;
                registrationEntry.IsEnabled = false;
                registrationStack.Children.Add(registrationEntry);
                registrationStack.Children.Add(changeLink);
            }
            else
            {
                registrationStack.Children.Add(registrationEntry);
                registrationEntry.IsEnabled = true;
            }

            var questionOne = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "As a result of attending this event, my propensity to recommend Microsoft Cloud Services has *"
            };

            var answerOnePicker = new Picker
            {
                Title = "Select",
            };
            answerOnePicker.Items.Add("Increased");
            answerOnePicker.Items.Add("Decreased");
            answerOnePicker.Items.Add("Stayed the same");

            var questionTwo = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Which of the following best describes your role in deciding on investing in the Microsoft Cloud Solution? *"
            };

            var answerTwoPicker = new Picker
            {
                Title = "Select",
            };
            answerTwoPicker.Items.Add("I am a Technical Decision Maker");
            answerTwoPicker.Items.Add("I am a Business Decision Maker");
            answerTwoPicker.Items.Add("I influence the above decision");



            var questionTwoText = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "My role is: "
            };

            var answerTwoText = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsEnabled = false
            };

            answerTwoPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (answerTwoPicker.SelectedIndex == 2)
                {
                    answerTwoText.IsEnabled = true;
                    answerTwoText.Focus();
                }
                else
                {
                    answerTwoText.IsEnabled = false;
                    answerTwoText.Text = "";
                }
            };

            var questionTwoStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { questionTwoText, answerTwoText }
            };

            var questionThree = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                    Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Approximately how many PCs do you have in your organization? (Please mention nos.) *"
            };

            var answerThree = new Entry
            {
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                Keyboard = Keyboard.Numeric,
            };

            //var answerThreePicker = new Picker
            //{
            //    Title = "Select",
            //};
            //answerThreePicker.Items.Add("< 10");
            //answerThreePicker.Items.Add("11 - 50");
            //answerThreePicker.Items.Add("51 - 100");
            //answerThreePicker.Items.Add("> 100");

            var questionFour = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                    Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "When do you intent to deploy Microsoft Cloud Solution? *"
            };

            var answerFourPicker = new Picker
            {
                Title = "Select",
            };
            answerFourPicker.Items.Add("0 - 3 months");
            answerFourPicker.Items.Add("3 - 6 months");
            answerFourPicker.Items.Add("6 months - 1 year");
            answerFourPicker.Items.Add("More than 1 year");

            var questionFive = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                    Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Do you have a planned budget for implementing Microsoft Cloud Solution offerings interested in? *"
            };

            var answerFivePicker = new Picker
            {
                Title = "Select",
            };
            answerFivePicker.Items.Add("Yes");
            answerFivePicker.Items.Add("No");


            var questionFiveText = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                  Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Estimated budget (optional)"
            };

            var answerFiveText = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsEnabled = false
            };

            answerFivePicker.SelectedIndexChanged += (sender, args) =>
            {
                if (answerFivePicker.SelectedIndex == 0)
                {
                    answerFiveText.IsEnabled = true;
                    answerFiveText.Focus();
                }
                else
                {
                    answerFiveText.IsEnabled = false;
                    answerFiveText.Text = "";
                }
            };

            var questionFiveStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { questionFiveText, answerFiveText }
            };

            var questionSix = new FontLabel
            {
                Font = Device.OnPlatform(Font.OfSize("HelveticaNeue-Light", NamedSize.Small),
                    Font.SystemFontOfSize(18), Font.SystemFontOfSize(22)),
                FontLabelType = FontLabelType.Light,
                LineBreakMode = LineBreakMode.WordWrap,
                TextColor = App.XamBlue,
                Text = "Comments:"
            };

            var answerSix = new Editor
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            var buttonReset = new Button
            {
                Text = "Reset"
            };

            var buttonSubmit = new Button
            {
                Text = "Submit"
            };

            buttonReset.Clicked += (sender, args) =>
            {
                answerOnePicker.SelectedIndex = answerTwoPicker.SelectedIndex = answerFourPicker.SelectedIndex = answerFivePicker.SelectedIndex = -1;
                answerTwoText.Text = answerSix.Text = answerThree.Text = "";
            };

            var stackButtons = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { buttonReset, buttonSubmit },
            };

            var spinner = new ActivityIndicator
            {
                IsRunning = false,
                IsVisible = false,
                Color = Device.OnPlatform(App.XamBlue, Color.White, Color.White)
            };
            //spinner.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            //spinner.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");

            var isSubmitting = false;

            buttonSubmit.Clicked += async (sender, args) =>
            {
                if (!App.NetworkMonitor.IsAvailable())
                {
                    await DisplayAlert("No Internet Connectivity!", "Your Phone is not connected to the Internet. Please connect and try again.", "OK");
                }
                else if (isSubmitting == false)
                {

                    isSubmitting = true;
                    var answerOne = Utils.GetPickerValue(answerOnePicker.SelectedIndex);
                    var answerTwo = Utils.GetPickerValue(answerTwoPicker.SelectedIndex);
                    var answerFour = Utils.GetPickerValue(answerFourPicker.SelectedIndex);
                    var answerFive = Utils.GetPickerValue(answerFivePicker.SelectedIndex);
                    var answerTwoTextReq = false;
                    var answerFiveTextReq = false;
                    if (answerTwo == 2 && String.IsNullOrEmpty(answerTwoText.Text))
                    {
                        answerTwoTextReq = true;
                        answerTwoText.Focus();
                    }
                    if (answerFive == 0 && String.IsNullOrEmpty(answerFiveText.Text))
                    {
                        answerFiveTextReq = true;
                        answerFiveText.Focus();
                    }
                    if (!String.IsNullOrEmpty(registrationEntry.Text) &&
                        !long.TryParse(registrationEntry.Text, out uuid))
                    {
                        registrationEntry.Text = "";
                        await DisplayAlert("Invalid Registration Id!", "Please check your Tag for Registration Id.", "OK");
                        registrationEntry.Focus();
                    }
                    else if (uuid == 0 || answerOne == -1 || answerTwo == -1 ||
                        answerFour == -1 || answerFive == -1 || String.IsNullOrEmpty(answerThree.Text) || answerTwoTextReq || answerFiveTextReq)
                        await
                            DisplayAlert("Mandatory inputs missing!",
                                "Please provide your response for the mandatory(*) fields.", "OK");
                    else
                    {
                        try
                        {
                            spinner.IsRunning = true;
                            spinner.IsVisible = true;

                            var contactUs = new ContactUs();
                            contactUs.AnswerOne = answerOne;
                            contactUs.AnswerTwo = answerTwo;
                            contactUs.AnswerTwoText = answerTwoText.Text;
                            contactUs.AnswerThree = answerThree.Text;
                            contactUs.AnswerFour = answerFour;
                            contactUs.AnswerFive = answerFive;
                            contactUs.AnswerFiveText = answerFiveText.Text;
                            contactUs.AnswerSix = answerSix.Text;
                            contactUs.PlatformId = Device.OnPlatform(3, 2, 1);
                            contactUs.UserId = uuid;
                            App.uuid = uuid.ToString();
                            Insights.Track("ContactUs Data", new Dictionary<string, string>
                                {
                                    {"UserID", uuid.ToString()},
                                    {"Data", JsonConvert.SerializeObject(contactUs)}
                                });
                            //await DisplayAlert("Submitting your data!", "Please wait while we submit your feedback.", "OK");
                            var res = await App.feedbackManager.SaveContactUsTaskAsync(contactUs);
                            if (!res)
                            {
                                await
                                    DisplayAlert("No Internet Connectivity!",
                                        "Your Phone is not connected to the Internet. Please connect and try again.",
                                        "OK");
                            }
                            else
                            {
                                await DisplayAlert("Thank You!", "Thanks for providing your valuable feedback.", "OK");
                                //await this.Navigation.PopAsync();
                                answerOnePicker.SelectedIndex =
                                    answerTwoPicker.SelectedIndex =
                                        answerFourPicker.SelectedIndex = answerFivePicker.SelectedIndex = -1;
                                answerTwoText.Text = answerSix.Text = answerThree.Text = "";
                                handle.Stop();
                            }
                        }
                        catch (Exception ex)
                        {
                            Insights.Report(ex);
                        }
                        isSubmitting = false;
                        spinner.IsVisible = false;
                        spinner.IsRunning = false;
                        App.IOsMasterDetailPage.IsPresented = true;
                    }
                }
            };

            var contactStack = new StackLayout
            {
                Children = { registrationIdLabel, registrationStack, questionOne, answerOnePicker, questionTwo, answerTwoPicker, questionTwoStack, questionThree, answerThree, questionFour, answerFourPicker, questionFive, answerFivePicker, questionFiveStack, questionSix, answerSix, spinner, stackButtons },
            };

            var scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.Fill,
                Orientation = ScrollOrientation.Vertical,
                Content = contactStack,

            };

            Content = scrollView;
            Title = "Contact Us";
            Padding = new Thickness(10, 0);
            BackgroundColor = Color.Black;
            BindingContext = App.ViewModel;

        }

        private static long back_pressed;
        

        protected override bool OnBackButtonPressed()
        {
            var current = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (back_pressed + 3000 > current)
            {
                base.OnBackButtonPressed();
                return false;
            }
            else
            {
                DependencyService.Get<IPlatforms>().MakeToast("Press again to exit!");
                back_pressed = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            }

            return true;
        }
    }
}
