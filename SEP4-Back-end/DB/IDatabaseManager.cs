using System;
using SEP4_Back_end.Model;

namespace SEP4_Back_end.DB
{
    public interface IDatabaseManager
    {
        void persistCO2(string co2,string roomName);
        void persistHumdity(string humidity,string roomName);
        void persistTemperature(string temperature,string roomName);
        void persistServo(string servo,string roomName);
        string getCO2(DateTime dateTime);
        string getHumidity(DateTime dateTime);
        string getTemperature(DateTime dateTime);
        string getServo(DateTime dateTime);
        string getCO2List(string roomName);
        string getHumidityList(string roomName);
        string getTemperatureList(string roomName);
        string getServoList(string roomName);

        Room getRoomByName(string name);
        string getHumidityList(string roomName, int weekNumber);
        string getCO2List(string roomName,int weekNumber);
        string getTemperatureList(string roomName, int weekNumber);
        string getServoList(string roomName, int weekNumber);
    }
}