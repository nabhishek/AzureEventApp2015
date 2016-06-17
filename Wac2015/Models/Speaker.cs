using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.ViewModels;

namespace Wac2015.Models
{
    public class Speaker : ModelBase
    {
        public Speaker()
        {
            Sessions = new ObservableCollection<Session>();
        }
//#if !WINDOWS_PHONE
//        public Speaker(JsonValue json)
//            : this()
//        {
//            Id = json["id"];
//            Name = json["name"];
//            TwitterHandle = json["twitterHandle"];
//            Bio = json["bio"];
//            HeadshotUrl = json["headshotUrl"];
//        }
//#endif
        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                    return;
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string TwitterHandle { get; set; }

        public string TwitterLink
        {
            get
            {
                if (String.IsNullOrEmpty(TwitterHandle))
                    return "https://twitter.com/search?q=%23Azure4Sure";
                else
                    return "https://twitter.com/" + TwitterHandle;
            }
        }

        private string _bio;
        public string Bio
        {
            get { return _bio; }
            set
            {
                if (_bio == value)
                    return;
                _bio = value;
                NotifyPropertyChanged("Bio");
            }
        }

        private string _tagLine;
        public string TagLine
        {
            get { return _tagLine; }
            set
            {
                if (_tagLine == value)
                    return;
                _tagLine = value;
                NotifyPropertyChanged("TagLine");
            }
        }

        private string _headshotLink;

        public string HeadshotLink
        {
            get
            {
                return _headshotLink;
            }
            set
            {
                _headshotLink = value;
                NotifyPropertyChanged("HeadshotLink");
            }
        }

        private string _headshotUrl;
        public string HeadshotUrl
        {
            get
            {
                return _headshotUrl;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _headshotUrl = @"/Images/missingprofile.png";
                }
                else
                {
                    var fileName = value.Split('/');
                    if (fileName.Length == 5 && !String.IsNullOrEmpty(fileName[4]))
                        _headshotUrl = @"/Images/speakers/" + fileName[4] + ".png";
                    else
                        _headshotUrl = value;
                }
                HeadshotLink = value;
                NotifyPropertyChanged("HeadshotUrl");
            }
        }

#if WINDOWS_PHONE
        // bit of a HACK: because we're serializing and storing as XML, 
        // this prevents a circular reference in the object graph
        // TODO: store favorites in Sqlite like the other platforms
        [System.Xml.Serialization.XmlIgnore]
#endif
        //public List<Session> Sessions { get; set; }
        public ObservableCollection<Session> Sessions { get; set; }
        public ObservableCollection<Speaker> Speakers { get; set; }
    }
}
