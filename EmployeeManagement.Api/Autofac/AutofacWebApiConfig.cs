using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using EmployeeManagement.Core.Implementation;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Data.Interfaces;
using EmployeeManagement.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace EmployeeManagement.Api.Autofac
{
    public class AutofacWebApiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());         

            builder.RegisterType<EmployeeManager>().As<IEmployeeManager>();
            builder.RegisterType<EmployeeRepositiory>().As<IEmployeeRepository>();                   

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}