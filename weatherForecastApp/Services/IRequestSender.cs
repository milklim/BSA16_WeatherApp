using System;
namespace weatherForecastApp.Services
{
    public interface IRequestSender
    {
        string SendRequest(string request);
    }
}
