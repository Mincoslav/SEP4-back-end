using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SEP4_Back_end.Model;

namespace SEP4_Back_end
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseManager _manager = new DatabaseManager();
         
            CO2 co2 = new CO2();
            co2.CO2_value = (float) 12.4;
            DateTime dateTime = DateTime.Now;
            co2.Date = dateTime;

            String s;
            
            s = JsonSerializer.Serialize(co2);
            
            _manager.persistCO2(s, "toilet");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}