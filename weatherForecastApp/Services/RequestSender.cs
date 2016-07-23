using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace weatherForecastApp.Services
{
    public class RequestSender : IRequestSender
    {
        public string SendRequest(string request)
        {
            string response = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                try
                {
                    response = client.DownloadString(request);
                    return response;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Request: {0}; Exception: {1}", request, ex.ToString());
                    return null;
                }
            }
        }

        public async Task<string> SendRequestAsync(string request)
        {
            using (HttpClient client = new HttpClient())
            using (var response = await client.GetAsync(new Uri(request)))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync(); 
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
        }
    }
}