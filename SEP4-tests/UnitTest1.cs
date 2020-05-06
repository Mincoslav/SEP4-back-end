using System;
using System.Text.Json;
using NUnit.Framework;
using SEP4_Back_end.Model;

namespace SEP4_tests
{
    public class Tests
    {
        DatabaseManager _manager = new DatabaseManager();
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void PersistCo2Test()
        {
            CO2 co2 = new CO2();
            co2.CO2_value = (float) 12.4;
            DateTime dateTime = DateTime.Today;
            co2.Date = dateTime;
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
            temperature.TEMP_value = (float) 12.4;
            DateTime dateTime = DateTime.Today;
            temperature.Date = dateTime;
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
            humidity.HUM_value = (float) 12.4;
            DateTime dateTime = DateTime.Today;
            humidity.Date = dateTime;
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
            DateTime dateTime = DateTime.Today;
            servo.Date = dateTime;
            String s;
            s = JsonSerializer.Serialize(servo);
            
            _manager.persistServo(s, "toilet");
            string servo_JSON = _manager.getServo(dateTime);
            Servo servo2 = JsonSerializer.Deserialize<Servo>(servo_JSON);
            Assert.AreEqual(servo2.Spinning, servo.Spinning);
        }

        
    }
}