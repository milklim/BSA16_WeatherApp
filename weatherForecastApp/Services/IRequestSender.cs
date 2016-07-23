using System.Threading.Tasks;


namespace weatherForecastApp.Services
{
    public interface IRequestSender
    {
        string SendRequest(string request);
        Task<string> SendRequestAsync(string request);
    }
}
