
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SEP4_Back_end.DB;
using SEP4_Back_end.Model;

public class DatabaseManager : IDatabaseManager
{
    private Context _context;
    

    public void persistCO2(string co2,string roomName)
    {
        CO2 co2Object;
        try
        {
            co2Object = JsonSerializer.Deserialize<CO2>(co2);
            _context.CO2.Add(co2Object);
            Room r = getRoomByName(roomName);
            CO2List list = new CO2List();
            list.CO2_ID = co2Object.CO2ID;
            list.ROOM_ID = r.RoomID;
            _context.CO2s.Add(list);
            _context.SaveChanges();
        }
        catch 
        {
            throw new Exception("JSON is corrupt");
        }
       
    }

    public void persistHumdity(string humidity,string roomName)
    {
        Humidity humidityObject;
        try
        {
            humidityObject = JsonSerializer.Deserialize<Humidity>(humidity);
            _context.Humidity.Add(humidityObject);
            Room r = getRoomByName(roomName);
            HumidityList list = new HumidityList();
            list.HUM_ID = humidityObject.HUM_ID;
            list.ROOM_ID = r.RoomID;
            _context.Humidities.Add(list);
            _context.SaveChanges();
        }
        catch 
        {
            throw new Exception("JSON is corrupt");
        }
    }

    public void persistTemperature(string temperature,string roomName)
    {
        Temperature temperatureObject;
        try
        {
            temperatureObject = JsonSerializer.Deserialize<Temperature>(temperature);
            _context.Temperature.Add(temperatureObject);
            Room r = getRoomByName(roomName);
            TemperatureList list = new TemperatureList();
            list.TEMP_ID = temperatureObject.TEMP_ID;
            list.ROOM_ID = r.RoomID;
            _context.Temperatures.Add(list);
            _context.SaveChanges();
        }
        catch 
        {
            throw new Exception("JSON is corrupt");
        }
    }

    public void persistServo(string servo,string roomName)
    {
        Servo servoObject;
        
        try
        {
            servoObject = JsonSerializer.Deserialize<Servo>(servo);
            _context.Servo.Add(servoObject);
            Room r = getRoomByName(roomName);
            ServoList list = new ServoList();
            list.SERV_ID = servoObject.SERV_ID;
            list.ROOM_ID = r.RoomID;
            _context.Servos.Add(list);
            _context.SaveChanges();
        }
        catch 
        {
            throw new Exception("JSON is corrupt");
        }
    }

    public string getCO2(DateTime dateTime)
    {
        List<CO2> co2list = _context.CO2.FromSqlRaw($"SELECT * FROM CO2 WHERE {dateTime} = CO2.Date").ToList(); //might not work
        CO2 co2 = co2list[0];
        string s = JsonSerializer.Serialize(co2);
        return s;
    }

    public string getHumidity(DateTime dateTime)
    {
        List<Humidity> humidities = _context.Humidity.FromSqlRaw($"SELECT * FROM Humidity WHERE {dateTime} = Humidity.Date").ToList(); //might not work
        Humidity h = humidities[0];
        string s = JsonSerializer.Serialize(h);
        return s;
    }

    public string getTemperature(DateTime dateTime)
    {
        List<Temperature> temperatures = _context.Temperature.FromSqlRaw($"SELECT * FROM Temperature WHERE {dateTime} = Temperature.Date").ToList(); //might not work
        Temperature t = temperatures[0];
        string s = JsonSerializer.Serialize(t);
        return s;
    }

    public string getServo(DateTime dateTime)
    {
        List<Servo> servos = _context.Servo.FromSqlRaw($"SELECT * FROM Servo WHERE {dateTime} = Servo.Date").ToList(); //might not work
        Servo t = servos[0];
        string s = JsonSerializer.Serialize(t);
        return s;
    }

    public string getCO2List(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        List<CO2> list =  _context.CO2.FromSqlRaw($"SELECT * FROM CO2 JOIN CO2S ON CO2.CO2_ID = CO2S.CO2_ID  WHERE {id} = ROOM_ID").ToList();
        string s = JsonSerializer.Serialize(list);
        return s;
    }

    public string getHumidityList(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        List<Humidity> list =  _context.Humidity.FromSqlRaw($"SELECT * FROM Humidity JOIN Humidities ON Humidity.HUM_ID = Humidities.HUM_ID  WHERE {id} = ROOM_ID").ToList();
        string s = JsonSerializer.Serialize(list);
        return s;
    }

    public string getTemperatureList(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        List<Temperature> list =  _context.Temperature.FromSqlRaw($"SELECT * FROM Temperature JOIN Temperatures ON Temperature.TEMP_ID = Temperatures.TEMP_ID  WHERE {id} = ROOM_ID").ToList();
        string s = JsonSerializer.Serialize(list);
        return s;
    }

    public string getServoList(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        List<Servo> list = _context.Servo.FromSqlRaw($"SELECT * FROM Servo JOIN Servos ON Servo.SERV_ID = Servos.SERV_ID  WHERE {id} = ROOM_ID").ToList();
        string s = JsonSerializer.Serialize(list);
        return s;
    }

    public Room getRoomByName(string name)
    {
        List<Room> r = _context.Room.FromSqlRaw($"SELECT * FROM Room WHERE {name} = Room.Name").ToList(); //might not work
        Room returnRoom;
        if (r[0].Equals(null))
        {
            Room newRoom = new Room();
            newRoom.Name = name;
            newRoom.RoomID = newRoom.RoomID.GetHashCode();
            _context.Room.Add(newRoom);
            _context.SaveChanges();
            returnRoom = newRoom;
        }
        else
        {
             returnRoom = r[0]; 
        }
        

        return returnRoom;
    }
}