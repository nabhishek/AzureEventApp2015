using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Wac2015.Models;

namespace Wac2015.Helpers
{
    public class FeedbackManager
    {
        private IMobileServiceTable<Feedback2> feedbackTable;
        private IMobileServiceTable<Eventfeedback> EventFeedbackTable;
        private IMobileServiceTable<ContactUs> contactUsTable;
        private IMobileServiceTable<NewsPublish> newsTable;
        public FeedbackManager(IMobileServiceTable<Feedback2> feedbackTable)
        {
            this.feedbackTable = feedbackTable;
        }

        public FeedbackManager(IMobileServiceTable<Feedback2> feedbackTable, IMobileServiceTable<Eventfeedback> eventFeedbackTable)
        {
            this.feedbackTable = feedbackTable;
            this.EventFeedbackTable = eventFeedbackTable;
        }

        public FeedbackManager(IMobileServiceTable<Feedback2> feedbackTable, IMobileServiceTable<Eventfeedback> eventFeedbackTable, IMobileServiceTable<ContactUs> contactUs)
        {
            this.feedbackTable = feedbackTable;
            this.EventFeedbackTable = eventFeedbackTable;
            this.contactUsTable = contactUs;
        }

        public FeedbackManager(IMobileServiceTable<Feedback2> feedbackTable, IMobileServiceTable<Eventfeedback> eventFeedbackTable, IMobileServiceTable<ContactUs> contactUs, IMobileServiceTable<NewsPublish> azureNews)
        {
            this.feedbackTable = feedbackTable;
            this.EventFeedbackTable = eventFeedbackTable;
            this.contactUsTable = contactUs;
            this.newsTable = azureNews;
        }

        public async Task<bool> SaveFeedbackTaskAsync(Feedback2 item)
        {
            try
            {
                await feedbackTable.InsertAsync(item);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
                return false;
            }
        }

        public async Task<bool> SaveEventFeedbackTaskAsync(Eventfeedback item)
        {
            try
            {

                await EventFeedbackTable.InsertAsync(item);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
                return false;
            }
        }

        public async Task<bool> SaveContactUsTaskAsync(ContactUs item)
        {
            try
            {
                await contactUsTable.InsertAsync(item);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
                return false;
            }
        }

        public async Task<NewsPublish> GetTaskAzync(string id)
        {
            try
            {
                return await newsTable.LookupAsync(id);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        public async Task<List<NewsPublish>> GetTaskAzync()
        {
            try
            {
                return new List<NewsPublish>(await newsTable.ReadAsync());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        //public async Task<Feedback2> GetTaskAsync(string id)
        //{
        //    try 
        //    {
        //        return await feedbackTable.LookupAsync(id);
        //    } 
        //    catch (MobileServiceInvalidOperationException msioe)
        //    {
        //        Debug.WriteLine(@"INVALID {0}", msioe.Message);
        //    }
        //    catch (Exception e) 
        //    {
        //        Debug.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //    return null;
        //}

        //public async Task<List<Feedback2>> GetTasksAsync ()
        //{
        //    try 
        //    {
        //        return new List<Feedback2>(await feedbackTable.ReadAsync());
        //    } 
        //    catch (MobileServiceInvalidOperationException msioe)
        //    {
        //        Debug.WriteLine(@"INVALID {0}", msioe.Message);
        //    }
        //    catch (Exception e) 
        //    {
        //        Debug.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //    return null;
        //}



        //public async Task DeleteTaskAsync (Feedback2 item)
        //{
        //    try 
        //    {
        //        await feedbackTable.DeleteAsync(item);
        //    } 
        //    catch (MobileServiceInvalidOperationException msioe)
        //    {
        //        Debug.WriteLine(@"INVALID {0}", msioe.Message);
        //    }
        //    catch (Exception e) 
        //    {
        //        Debug.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //}
    }
}
