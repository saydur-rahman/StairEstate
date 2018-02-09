﻿using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using STAIR.Data.Repository;
using STAIR.Data.Infrastructure;
using STAIR.Service;
using Microsoft.AspNet.Identity.EntityFramework;
using STAIR.Model.Models;
using STAIR.Data.Models;
using Microsoft.AspNet.Identity;

namespace STAIR.Web
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            //Configure AutoMapper
            //AutoMapperConfiguration.Configure();
        }
        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof(sys_userRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof(sys_userService).Assembly)
           .Where(t => t.Name.EndsWith("Service"))
           .AsImplementedInterfaces().InstancePerHttpRequest();

            //builder.RegisterAssemblyTypes(typeof(DefaultFormsAuthentication).Assembly)
            //.Where(t => t.Name.EndsWith("Authentication"))
            //.AsImplementedInterfaces().InstancePerHttpRequest();

            //builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>( new SampleEntities())))
            //.As<UserManager<ApplicationUser>>().InstancePerHttpRequest();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));            
        }
    }
}