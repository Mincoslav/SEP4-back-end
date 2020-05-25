using System;
using System.Collections.Generic;
using System.Text.Json;
using NUnit.Framework;
using SEP4_Back_end.Model;

namespace SEP4_tests
{
    public class Tests
    {
        DatabaseManager _manager = new DatabaseManager();
        DateTime dateTime = DateTime.Now;
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void PersistCo2Test()
        {
            CO2 co2 = new CO2();
            co2.CO2_value = (float) 12.4;
            
            co2.Date = this.dateTime;
            String s;
            s = JsonSerializer.Serialize(co2);
            
            _manager.persistCO2(s, "toilet");
            string co2_JSON = _manager.getCO2(dateTime);
            CO2 co2_2 = JsonSerializer.Deserialize<CO2>(co2_JSON);
            Assert.AreEqual(co2_2.CO2_value, co2.CO2_value);
            
        }

        [Test]
        public void PersistTemperatureTest()
        {
            Temperature temperature = new Temperature();
            temperature.TEMP_value = (float) 20.4;
            
            temperature.Date = this.dateTime;
            String s;
            s = JsonSerializer.Serialize(temperature);
            
            _manager.persistTemperature(s, "toilet");
            string temperature_JSON = _manager.getTemperature(dateTime);
            Temperature temperature2 = JsonSerializer.Deserialize<Temperature>(temperature_JSON);
            Assert.AreEqual(temperature2.TEMP_value, temperature.TEMP_value);
        }
        
        [Test]
        public void PersistHumidityTest()
        {
            Humidity humidity = new Humidity();
            humidity.HUM_value = (float) 69.69;
            
            humidity.Date = this.dateTime;
            String s;
            s = JsonSerializer.Serialize(humidity);
            
            _manager.persistHumdity(s, "toilet");
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
    }
}