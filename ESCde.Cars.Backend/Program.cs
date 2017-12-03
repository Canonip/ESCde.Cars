using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ESCde.Cars.Backend.Model;
using ESCde.Cars.Model;

namespace ESCde.Cars.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cars = Database.Instance.Cars;
            cars.Add(new Car()
            {
                Vendor = "Audi",
                Color = "Silver",
                Model = "R8",
                YearOfManufacture = 2016
            });

            cars.Add(new Car()
            {
                Vendor = "Ford",
                Color = "Aqua",
                Model = "F350",
                YearOfManufacture = 2005
            });

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
