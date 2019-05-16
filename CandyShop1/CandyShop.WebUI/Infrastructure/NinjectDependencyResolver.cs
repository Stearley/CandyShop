using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using CandyShop.Domain.Entities;
using CandyShop.Domain.Abstract;
using CandyShop.Domain.Concrete;
using CandyShop.WebUI.Infrastructure.Abstract;
using CandyShop.WebUI.Infrastructure.Concrete;

namespace CandyShop.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ISweetRepository>().To<EFSweetRepository>();
            kernel.Bind<ISwCatRepository>().To<EFSwCatRepository>();
            kernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            kernel.Bind<IByerRepository>().To<EFByerRepository>();
            kernel.Bind<IOrderRepository>().To<EFOrderRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}