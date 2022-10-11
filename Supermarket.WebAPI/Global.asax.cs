using Autofac;
using Supermarket.Repository.Common;
using Supermarket.Repository;
using Supermarket.Service.Common;
using Supermarket.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.WebApi;
using System.Reflection;
using Supermarket.Model;
using Supermarket.Model.Common;
using System.Data;
using AutoMapper;
using System.Security.Cryptography;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.Ajax.Utilities;

namespace Supermarket.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterType<Employee>().As<IEmployee>();
            builder.RegisterType<Product>().As<IProduct>();
            //builder.RegisterType<Mapper>().As<IMapper>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterAutoMapper(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiModelBinderProvider();
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeRest>();
                cfg.CreateMap<EmployeeRest, Employee>();
                cfg.CreateMap<Product, ProductRest>();
                cfg.CreateMap<ProductRest, Product>();
                cfg.CreateMap<List<ProductRest>, List<Product>>();
                cfg.CreateMap<List<Product>, List<ProductRest>>();
                cfg.CreateMap<List<Employee>, List<EmployeeRest>>();
                cfg.CreateMap<List<EmployeeRest>, List<Employee>>();
            })).AsSelf().SingleInstance();
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var configMapper = context.Resolve<MapperConfiguration>();
                return configMapper.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
