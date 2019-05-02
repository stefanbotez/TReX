﻿using System.IO;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using TReX.Discovery.Media.DependencyInjection;
using TReX.Kernel.Utilities;
using Module = Autofac.Module;

namespace TReX.Discovery.Media.Worker
{
    public sealed class WorkerContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMediatr(Assembly.GetExecutingAssembly());
            builder.RegisterModule<AutofacContainer>();
            builder.RegisterType<Application>()
                .AsSelf()
                .SingleInstance();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            builder.RegisterInstance(configuration)
                .As<IConfiguration>()
                .SingleInstance();
        }
    }
}