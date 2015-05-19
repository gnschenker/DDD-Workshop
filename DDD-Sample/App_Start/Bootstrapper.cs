using System;
using System.IO;
using System.Web.Http;
using DDD_Sample.Controllers;
using DDD_Sample.Domain;
using DDD_Sample.Infrastructure;
using DDD_Sample.Infrastructure.Configuration;
using log4net;
using log4net.Config;
using StructureMap;
using WebApiContrib.IoC.StructureMap;

namespace DDD_Sample
{
    public class Bootstrapper
    {
        public static void Init()
        {
            InitLogger();
            InitContainer();
        }

        private static void InitContainer()
        {
            ObjectFactory.Initialize(init =>
            {
                init.For<ILog>().Use(x => LogManager.GetLogger(x.Root.ConcreteType));
                init.For<INHibernateSettings>().Use<Settings>();
                init.For<IRepository>().Use<NHibernateRepository>();
                init.For<ISamplesApplicationService>().Use<SamplesApplicationService>();
                init.AddRegistry(new NhibernateRegistry());
            });

            GlobalConfiguration.Configuration.DependencyResolver =
                new StructureMapResolver(ObjectFactory.Container);
        }

        private static void InitLogger()
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
        }
    }
}