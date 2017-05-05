using System;
using System.Collections.Generic;
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

        }
    }
}