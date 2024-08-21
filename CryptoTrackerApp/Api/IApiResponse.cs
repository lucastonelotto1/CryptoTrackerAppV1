using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.Api
{
    public interface IApiResponse
    {
        dynamic Data { get; set; }
        void GetAPIResponseItem(string mUrl);

    }
}
