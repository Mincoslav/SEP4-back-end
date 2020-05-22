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
                if (Packet.port == 1)
                {
                    //Humidity send to DB
                    String humidity = Packet.data[0].ToString() + Packet.data[1].ToString();
                    int decValueHum = Convert.ToInt32(humidity, 16);
                    // ReSharper disable once PossibleLossOfFraction
                    double humidityValueAfterConversion = decValueHum / 100;
                    _manager.persistHumdity(humidityValueAfterConversion.ToString(),"roomName");
                    
                    //CO2 send to DB
                    String CO2 = Packet.data[2].ToString() + Packet.data[3].ToString();
                    int decValueCo2 = Convert.ToInt32(CO2, 16);
                    // ReSharper disable once PossibleLossOfFraction
                    double co2ValueAfterConversion = decValueCo2 / 100;
                    _manager.persistCO2(co2ValueAfterConversion.ToString(), "roomName");
                    
                    //Temperature send to DB
                    String temperature = Packet.data[4].ToString() + Packet.data[5].ToString();
                    int decValueTemperature = Convert.ToInt32(temperature, 16);
                    // ReSharper disable once PossibleLossOfFraction
                    double temperatureValueAfterConversion = decValueTemperature / 100;
                    _manager.persistTemperature(temperatureValueAfterConversion.ToString(), "roomName");
                }
                //else if (packet.port == number) 
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