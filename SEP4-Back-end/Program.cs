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
                Packet packet3 = new Packet();
                packet3.cmd = "tx";
                packet3.EUI = "0004A30B00259F36";
                packet3.port = 1;
                packet3.data = "AABBCCDD";
                packet3.confirmed = false;
                Console.WriteLine(packet3.ToString());
                loraReceiver.SendPacket(packet3);
                
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}