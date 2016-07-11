using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace weatherForecastApp.Services
{
    public class RequestSender : IRequestSender
    {
        public string SendRequest(string request)
        {
            string response = string.Empty;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                try
                {
                    response = client.DownloadString(request);
                    return response;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Request: {0}; Exception: {1}",request, ex.ToString());
                    return null;
                }
            }
        }
    }
}