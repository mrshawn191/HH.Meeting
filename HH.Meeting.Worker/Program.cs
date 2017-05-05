using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using SimpleInjector;

namespace HH.Meeting.Worker
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Initialize dependency injection
            var container = new Container();
            new SimpleInjectorContainer().Initialize(container);

            var _storageConn = ConfigurationManager
                .ConnectionStrings["MyStorageConnection"].ConnectionString;

            var _dashboardConn = ConfigurationManager
                .ConnectionStrings["MyDashboardConnection"].ConnectionString;

            var _serviceBusConn = ConfigurationManager
                .ConnectionStrings["MyServiceBusConnection"].ConnectionString;

            var config = new JobHostConfiguration
            {
                StorageConnectionString = _storageConn,
                DashboardConnectionString = _dashboardConn
            };



            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}