using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Entities.DataAccessLayer;
using Entities.Helper;
using Entities.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using Unity.Injection;

namespace WebRegister
{
    public static class DependencyResolver
    {
        public static void Initialize()
        {
            var container = BuildUnityContainer();

            // specify the dependency resolver for MVC View Controller
            System.Web.Mvc.DependencyResolver.SetResolver(new Unity.AspNet.Mvc.UnityDependencyResolver(container));
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            var connectionString = ConfigReader.GetDefaultConnectionString();
            container.RegisterType<Database, SqlDatabase>(new InjectionConstructor(connectionString));
            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<ILoginDataBaseRepository, LoginDataBaseRepository>();
            return container;
        }

    }

}