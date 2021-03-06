using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using NUnit.Framework;
using SEP4_Back_end.DB;
using SEP4_Back_end.Model;

namespace SEP4_tests
{
    public class Tests
    {
        DatabaseManager _manager = new DatabaseManager();
        static DateTime dateTimeNow = DateTime.Now;
        DateTime dateTime = dateTimeNow.AddDays(4);

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void PersistCo2Test()
        {
            Random r = new Random();
          
            CO2 co2 = new CO2();
            co2.CO2_value = (float) r.NextDouble() * 100;
            
            co2.Date = this.dateTime;
            String s;
            s = JsonSerializer.Serialize(co2);
            
            _manager.persistCO2(co2.CO2_value.ToString(),co2.Date, "toilet");
            string co2_JSON = _manager.getCO2(dateTime);
            CO2 co2_2 = JsonSerializer.Deserialize<CO2>(co2_JSON);
            Assert.AreEqual(co2_2.CO2_value, co2.CO2_value);
            
        }

        [Test]
        public void PersistTemperatureTest()
        {
            Random r = new Random();
            Temperature temperature = new Temperature();
            temperature.TEMP_value = (float) (float) r.NextDouble() * 30;
            
            temperature.Date = this.dateTime;
            String s;
            s = JsonSerializer.Serialize(temperature);
            
            _manager.persistTemperature(temperature.TEMP_value.ToString(),temperature.Date, "toilet");
            string temperature_JSON = _manager.getTemperature(dateTime);
            Temperature temperature2 = JsonSerializer.Deserialize<Temperature>(temperature_JSON);
            Assert.AreEqual(temperature2.TEMP_value, temperature.TEMP_value);
        }
        
        [Test]
        public void PersistHumidityTest()
        {
            Random r = new Random();
            Humidity humidity = new Humidity();
            humidity.HUM_value = (float) (float) (float) r.NextDouble() * 100;
            
            humidity.Date = this.dateTime;
            String s;
            s = JsonSerializer.Serialize(humidity);
            
            _manager.persistHumdity(humidity.HUM_value.ToString(),humidity.Date, "toilet");
            string humidity_JSON = _manager.getHumidity(dateTime);
            Humidity humidity2 = JsonSerializer.Deserialize<Humidity>(humidity_JSON);
            Assert.AreEqual(humidity2.HUM_value, humidity.HUM_value);
        }
        
        [Test]
        public void PersistServoTest()
        {
            Servo servo = new Servo();
            servo.Spinning = true;
            servo.Date = this.dateTime;
            String s;
            s = JsonSerializer.Serialize(servo);
            
            _manager.persistServo(s, "toilet");
            string servo_JSON = _manager.getServo(dateTime);
            Servo servo2 = JsonSerializer.Deserialize<Servo>(servo_JSON);
            Assert.AreEqual(servo2.Spinning, servo.Spinning);
        }

        [Test]
        public void Co2Test()
        {
            List<CO2> list = JsonSerializer.Deserialize<List<CO2>>(_manager.getCO2List("toilet"));
            Assert.IsInstanceOf<CO2>(list[0]);
            Assert.IsNotEmpty(list);
        }
        
        [Test]
        public void HumiditiesTest()
        {
            List<Humidity> list = JsonSerializer.Deserialize<List<Humidity>>(_manager.getHumidityList("toilet"));
            Assert.IsInstanceOf<Humidity>(list[0]);
            Assert.IsNotEmpty(list);
        }
        
        [Test]
        public void TemperatureTest()
        {
            List<Temperature> list = JsonSerializer.Deserialize<List<Temperature>>(_manager.getTemperatureList("toilet"));
            Assert.IsInstanceOf<Temperature>(list[0]);
            Assert.IsNotEmpty(list);
        }
        
        [Test]
        public void ServoTest()
        {
            List<Servo> list = JsonSerializer.Deserialize<List<Servo>>(_manager.getServoList("toilet"));
            Assert.IsInstanceOf<Servo>(list[0]);
            Assert.IsNotEmpty(list);
        }

        [Test]
        public void GetRoomByNameTest()
        {
            Room r = _manager.getRoomByName("randomNameThatForSureDoesntExist");
            Room r2 = _manager.getRoomByName("randomNameThatForSureDoesntExist");
            Assert.AreEqual(r, r2);
        }

        [Test]
        public void GetHumidityListTest()
        {
            
            Humidity humidity = new Humidity();
            humidity.HUM_ID = 1;
            humidity.Date = this.dateTime;
            humidity.HUM_value = (float) 12.4;
            List<Humidity> humidityList = new List<Humidity>();
            humidityList.Add(humidity);

            String humList = JsonSerializer.Serialize(humidityList);

            List<Humidity> humidities = JsonSerializer.Deserialize<List<Humidity>>(humList);
            
            String databaseList = _manager.getHumidityList("toilet", 22);
            List<Humidity> testerList = new List<Humidity>();
            
            testerList =  JsonSerializer.Deserialize<List<Humidity>>(databaseList);
           // System.Console.WriteLine(databaseList);
           
           /*
           Assert.AreEqual(humidities[0].Date, testerList[0].Date);
           */
            Assert.AreEqual(humidities[0].HUM_value, testerList[0].HUM_value);
            Assert.AreEqual(humidities[0].HUM_ID, testerList[0].HUM_ID);
        }

        [Test]
        public void PacketTestingGateway()
        {
            string quote = "\"";
            String s = "{"+quote+"cmd"+quote+":"+quote+"gw"+quote+","+quote+"seqno"+quote+":22,"+quote+"EUI"+quote+":"+quote+"0004A30B00259F36"+quote+","+quote+"ts"+quote+":1590665669281,"+quote+"fcnt"+quote+":0,"+quote+"port"+quote+":2,"+quote+"freq"+quote+":867300000,"+quote+"toa"+quote+":0,"+quote+"dr"+quote+":"+quote+"SF12 BW125 4/5"+quote+","+quote+"ack"+quote+":false,"+quote+"gws"+quote+":[{"+quote+"rssi"+quote+":-113,"+quote+"snr"+quote+":-11,"+quote+"ts"+quote+":1590665669281,"+quote+"tmms"+quote+":50000,"+quote+"time"+quote+":"+quote+"2020-05-28T11:34:29.153078765Z"+quote+","+quote+"gweui"+quote+":"+quote+"7076FFFFFF019BCE"+quote+","+quote+"ant"+quote+":0,"+quote+"lat"+quote+":55.809815,"+quote+"lon"+quote+":9.623305999999957}],"+quote+"bat"+quote+":255,"+quote+"data"+quote+":"+quote+"000041be0000"+quote+"}";

            Packet p = JsonSerializer.Deserialize<Packet>(s,new JsonSerializerOptions { IgnoreNullValues = true });
            Console.WriteLine(s);
            Console.WriteLine(p.seqno);
            
            Assert.NotNull(p);
            Assert.True(p.cmd == "gw");
        }

        [Test]
        public void PacketTestingDownLink()
        {
            string quote = "\"";
            String s = "{"+quote+"cmd"+quote+":"+quote+"rx"+quote+","+quote+"seqno"+quote+":33,"+quote+"EUI"+quote+":"+quote+"0004A30B00259F36"+quote+","+quote+"ts"+quote+":1590740416625,"+quote+"fcnt"+quote+":0,"+quote+"port"+quote+":2,"+quote+"freq"+quote+":867900000,"+quote+"rssi"+quote+":-114,"+quote+"snr"+quote+":-18,"+quote+"toa"+quote+":1318,"+quote+"dr"+quote+":"+quote+"SF12 BW125 4/5"+quote+","+quote+"ack"+quote+":false,"+quote+"bat"+quote+":255,"+quote+"offline"+quote+":false,"+quote+"data"+quote+":"+quote+"000041b40000"+quote+"}";
            Packet p = JsonSerializer.Deserialize<Packet>(s,new JsonSerializerOptions { IgnoreNullValues = true });
            
            Console.WriteLine(s);
            Console.WriteLine(p.seqno);
            
            Assert.NotNull(p);
            Assert.True(p.cmd == "rx");
        }
    }
}