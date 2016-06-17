using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Wac2015.Helpers
{
    public static class Utils
    {
        public static readonly int SessionFontLarge = 20;
        public static readonly int SessionFontMedium = 18;
        public static readonly int SessionFontSmall = 16;

        public static readonly int SpeakerFontLarge = 22;
        public static readonly int SpeakerFontMedium = 20;
        public static readonly int SpeakerFontSmall = 18;
        public static bool IsUSA
        {
            get
            {
                return CultureInfo.CurrentCulture.Name == "en-US";
            }
        }

        public static string GetFile(string file)
        {
            return Device.OnPlatform<string>(file, file, "Assets/" + file);
        }

        public static string StripHTML(string caption)
        {

            if (string.IsNullOrWhiteSpace(caption))
                return string.Empty;

            //get rid of HTML tags
            caption = Regex.Replace(caption, "<[^>]*>", string.Empty);

            //get rid of multiple blank lines
            caption = Regex.Replace(caption, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

            return caption;

        }

        public static string ReadFile(string filePath)
        {

            var assembly = typeof(Utils).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(filePath);
            string text = "";
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }

        public static string GetImagePath(string speakerId, string url)
        {
            return String.IsNullOrEmpty(url)
                ? GetFile("missingprofile.png")
                : GetFile(String.Format("speakers/{0}.png", speakerId));
        }

        public static string GetLocationOrder(string location)
        {
            switch (location)
            {
                case "Keynote":
                    return "1. " + location;
                    break;
                case "OPEN":
                    return "2. " + location;
                    break;
                case "BROAD(Develop)":
                    return "3. " + location;
                    break;
                case "FLEXIBLE(Deploy&Manage)":
                    return "4. " + location;
                    break;
                case "Start-up Conclave":
                    return "5. " + location;
                    break;
                case "MIC Champs Connect":
                    return "6. " + location;
                    break;
                case "HOL":
                    return "7. " + location;
                    break;
                default:
                    return "10";
            }
        }

        public static int GetPickerValue(int value)
        {
            switch (value)
            {
                case 0:
                    return 4;
                case 1:
                    return 3;
                case 2:
                    return 2;
                case 3:
                    return 1;
                default:
                    return -1;
            }
        }

        public static async Task<String> GetDataFromUrl(string url)
        {
            string responseString = string.Empty;
            try
            {
                var httpClient = new HttpClient(new HttpClientHandler());
                httpClient.Timeout = TimeSpan.FromMilliseconds(5000);
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseString = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                
            }
            return responseString;
        }
    }

    
}
