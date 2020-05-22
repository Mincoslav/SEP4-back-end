using System.Security.Authentication;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace SEP4Lora {
    class Program {
        static void Main (string[] args) {
            //Websocket client = new Websocket("wss://iotnet.teracom.dk/app?token=vnoS0AAAABFpb3RuZXQudGVyYWNvbS5kaxomi40vX5EPStvTo_hZokg=");

            using (var ws = new WebSocketSharp.WebSocket ("wss://iotnet.teracom.dk/app?token=vnoS0AAAABFpb3RuZXQudGVyYWNvbS5kaxomi40vX5EPStvTo_hZokg=")) {
                ws.OnMessage += (sender, e) => Console.WriteLine ("Lora says: " + e.Data);
                ws.OnOpen += (sender, e) => Console.WriteLine ("Lora says hi");
                ws.OnError += (sender, e) => Console.WriteLine("Lora said this error" + e.Message + e.Exception);
                ws.OnClose += (sender, e) => Console.WriteLine("Lora said this while closing" + e.Code + e.Reason);
                ws.SslConfiguration.EnabledSslProtocols =(SslProtocols) 3072;
                ws.Connect();
               
                Packet packet1 = new Packet ("rx","C763721FE1A1CCC5", 1, "AABBCCDD");
                Packet packet2 = new Packet ("tx","C763721FE1A1CCC5", 2, "AABBCCDD");
                Packet packet3 = new Packet ("rx","0004A30B00259F36", 3, "AABBCCDD");
                Packet packet4 = new Packet ("tx","0004A30B00259F36", 4, "AABBCCDD");
                String json;
                json = "kms";
                 // Action<bool> synk = i => Console.Write("hello");
                //ws.SendAsync(json,synk);
                Console.WriteLine(ws.ReadyState);
                
                while(true)
                {
                json = JsonSerializer.Serialize(packet1);
                Console.WriteLine(json);
                ws.Send(json);
                json = JsonSerializer.Serialize (packet2);
                ws.Send(json);
                json = JsonSerializer.Serialize (packet3);
                ws.Send(json);
                json = JsonSerializer.Serialize (packet4);
                ws.Send(json);
                }
                
            }

        }
    }
}