using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using weatherForecastApp.Services;
using weatherForecastApp.Models;

namespace weatherForecastApp.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;


        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }


        private void AddBindings()
        {
            kernel.Bind<IRequestSender>().To<RequestSender>();
            kernel.Bind<WeatherService>().To<OpenWeather>();
        }
        
        
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }


        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}