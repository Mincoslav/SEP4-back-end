using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SEP4_Back_end.Model;
using WebSocketSharp;

namespace SEP4_Back_end.DB
{
    public class LoraReceiver
    {
        private IDatabaseManager _manager;
        public Packet Packet { get; set; }

        private WebSocket ws;
        public LoraReceiver()
        {
            _manager = new DatabaseManager();
            Packet = new Packet();
            ws = new WebSocket("wss://iotnet.teracom.dk/app?token=vnoS0AAAABFpb3RuZXQudGVyYWNvbS5kaxomi40vX5EPStvTo_hZokg=");
            OnMessage();
            OnOpen();
            OnError();
            OnClose();
            //ws.SslConfiguration.EnabledSslProtocols =(SslProtocols) 3072;
            ws.Connect();
        }

        private void OnMessage()
        {
            ws.OnMessage += (sender, e) =>
            {
                Console.WriteLine("Lora says: " + e.Data);
                String jsonString = e.Data;

                Packet = JsonSerializer.Deserialize<Packet>(jsonString);
                //RoomName TODO set the name

                switch (Packet.cmd)
                {
                    case "rx":

                        switch (Packet.port)
                        {
                            case 1:
                                //TODO for the future
                                break;

                            case 2:
                            {
                                DateTime epoch = new DateTime(1970,1,1,2,0,0,DateTimeKind.Local);
                                Console.WriteLine(Packet.data);
                                
                                //Humidity send to DB
                                String humidity = Packet.data[0].ToString() + Packet.data[1].ToString() + Packet.data[2].ToString() + Packet.data[3].ToString();
                                Console.WriteLine(humidity);
                                int decValueHum = Convert.ToInt32(humidity, 16);
                                Console.WriteLine("--------------" + decValueHum);
                                // ReSharper disable once PossibleLossOfFraction
                                double humidityValueAfterConversion = decValueHum; //probably doesn't work
                                Console.WriteLine("--------------" + humidityValueAfterConversion);
                                _manager.persistHumdity(humidityValueAfterConversion.ToString(),epoch.AddSeconds((double) (Packet.ts/1000)), "toilet");

                                //Temperature send to DB
                                String temperature = Packet.data[4].ToString() + Packet.data[5].ToString() + Packet.data[6].ToString() + Packet.data[7].ToString();
                                Console.WriteLine(temperature);
                                int decValueTemperature = Convert.ToInt32(temperature, 16);
                                Console.WriteLine("--------------" + decValueTemperature);
                                // ReSharper disable once PossibleLossOfFraction
                                double temperatureValueAfterConversion = decValueTemperature; //should work
                                Console.WriteLine("--------------" + temperatureValueAfterConversion);
                                _manager.persistTemperature(temperatureValueAfterConversion.ToString(),epoch.AddSeconds((double) (Packet.ts/1000)) ,"toilet");
                                
                                //CO2 send to DB
                                String CO2 = Packet.data[8].ToString() + Packet.data[9].ToString() + Packet.data[10].ToString() + Packet.data[11].ToString();
                                int decValueCo2 = Convert.ToInt32(CO2, 16);
                                Console.WriteLine(CO2);
                                Console.WriteLine("--------------" + decValueCo2);
                                // ReSharper disable once PossibleLossOfFraction
                                double co2ValueAfterConversion = decValueCo2; //probably doesn't work
                                Console.WriteLine("--------------" + co2ValueAfterConversion);
                                _manager.persistCO2(co2ValueAfterConversion.ToString(),epoch.AddSeconds((double) (Packet.ts/1000)), "toilet");

                                
                            }
                                break;
                        }
                        break;
                    
                    case "gw":
                        Console.WriteLine("GATEWAY DEBUG:");
                        Console.WriteLine(Packet);
                    break;
                    
                    case "tx":
                        Console.WriteLine("DOWNLINK DEBUG:");
                        Console.WriteLine(Packet);
                        break;
                    
                    default:
                        Console.WriteLine("Not recognized packet");
                        Console.WriteLine(Packet);
                        break;
                }
            };
        }

        private void OnOpen()
        {
            ws.OnOpen += (sender, e) => Console.WriteLine ("Lora says hi");
            
        }

        private void OnError()
        {
            ws.OnError += (sender, e) => Console.WriteLine("Lora said this error" + e.Message + e.Exception);
        }

        private void OnClose()
        {
            ws.OnClose += (sender, e) => Console.WriteLine("Lora said this while closing" + e.Code + e.Reason);
        }

        public void SendPacket(Packet packet)
        {
            String jsonPacket = JsonSerializer.Serialize(packet);
            ws.Send(jsonPacket);
        }
    }
}