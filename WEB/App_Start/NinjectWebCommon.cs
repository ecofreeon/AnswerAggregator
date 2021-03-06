using System.Configuration;
using System.IO;
using AutoMapper;
using BL.DTO;
using BL.Infrastructure;
using BL.Services;
using BL.Services.Interfaces;
using Ninject.Modules;
using WEB.Models.Account;
using WEB.Models.Proflie;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WEB.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WEB.App_Start.NinjectWebCommon), "Stop")]

namespace WEB.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                CreateMappings();
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;
            string emailAddress = "suppservice.aa@gmail.com";
            string password = "fLicIaBitA";
            string serverFilesPath = HttpRuntime.AppDomainAppPath;
            string logsRoot = serverFilesPath;

            var modules = new INinjectModule[] { new NinjectBLModule(connectionString, emailAddress, password, serverFilesPath, logsRoot) };

            kernel.Load(modules);

            kernel.Bind<IUserService>().To<UserService>();
        }

        private static void CreateMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperBlModule>();
                cfg.AddProfile<AutoMapperWebModule>();
            });
        }
    }
}
