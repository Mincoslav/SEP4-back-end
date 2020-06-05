using System;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SEP4_Back_end.DB;
using SEP4_Back_end.Model;

namespace SEP4_Back_end
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
                LoraReceiver loraReceiver = new LoraReceiver();
                CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}