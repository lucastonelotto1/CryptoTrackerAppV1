using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.Api
{
    public class ApiResponse : IApiResponse
    {
        public dynamic data;
        public dynamic Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
        public ApiResponse()
        {

        }
        public void GetAPIResponseItem(string mUrl)
        {

            HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(mUrl);

  
            WebResponse mResponse = mRequest.GetResponse();

          
            Stream responseStream = mResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

 
            dynamic mResponseJSON = JsonConvert.DeserializeObject(reader.ReadToEnd());
            
            Data = mResponseJSON;
        }
    }
}
