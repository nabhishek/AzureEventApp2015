using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wac2015.Models;

namespace Wac2015.ViewModels
{
    public class SpeakerViewModel
    {
        public Speaker Speaker { get; private set; }
        //public ObservableCollection<TwitterStatusItemModel> Twitter { get; private set; }
        public List<Speaker> Speakers
        {
            get
            {
                return new List<Speaker>() { Speaker };
            }
        }

        public SpeakerViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            Speaker = App.CurrentSpeaker;

            // App.Sessions.Where(p => p.Speakers==Speaker.Id).ToObservableCollection();
            //Twitter = Service.GetTwitterFeed(Speaker.Twitter);
        }
    }
}
