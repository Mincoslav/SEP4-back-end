
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
    private Context _context= new Context();
    

    public void persistCO2(string co2,string roomName)
    {
        CO2 co2Object;
        try
        {
            co2Object = JsonSerializer.Deserialize<CO2>(co2);
            _context.CO2.Add(co2Object);
            Room r = getRoomByName(roomName);
            _context.SaveChanges();
            CO2List list = new CO2List();
            list.CO2_ID = co2Object.CO2ID;
            list.ROOM_ID = r.RoomID;
            _context.CO2s.Add(list);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            _context.SaveChanges();
            HumidityList list = new HumidityList();
            list.HUM_ID = humidityObject.HUM_ID;
            list.ROOM_ID = r.RoomID;
            _context.Humidities.Add(list);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            _context.SaveChanges();
            TemperatureList list = new TemperatureList();
            list.TEMP_ID = temperatureObject.TEMP_ID;
            list.ROOM_ID = r.RoomID;
            _context.Temperatures.Add(list);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
            _context.SaveChanges();
            ServoList list = new ServoList();
            list.SERV_ID = servoObject.SERV_ID;
            list.ROOM_ID = r.RoomID;
            _context.Servos.Add(list);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("JSON is corrupt");
        }
    }

    public string getCO2(DateTime dateTime)
    {
        List<CO2> co2list = _context.CO2.Where(CO2 =>CO2.Date == dateTime).ToList(); //might  work
        CO2 co2 = co2list[0];
        string s = JsonSerializer.Serialize(co2);
        return s; 
    }

    public string getHumidity(DateTime dateTime)
    {
        List<Humidity> humidities = _context.Humidity.Where(humidity => humidity.Date == dateTime).ToList(); //might  work
        Humidity h = humidities[0];
        string s = JsonSerializer.Serialize(h);
        return s;   
    } 

    public string getTemperature(DateTime dateTime)
    {
        List<Temperature> temperatures = _context.Temperature.Where(temperature =>  temperature.Date == dateTime).ToList(); //might not work
        Temperature t = temperatures[0];
        string s = JsonSerializer.Serialize(t);
        return s;
    }

    public string getServo(DateTime dateTime)
    {
        List<Servo> servos = _context.Servo.Where(servo => servo.Date == dateTime).ToList(); //might  work
        Servo t = servos[0];
        string s = JsonSerializer.Serialize(t);
        return s;
    }

    public string getCO2List(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        
        //return the first list from "list" to get the variables.
        List<CO2List> list =  _context.CO2s.Where(co2List => co2List.ROOM_ID == id).ToList();
        List<CO2> co2list = new List<CO2>();
        
        for (int i = 0; i < list.Capacity; i++)
        {
            int co2Id = list[i].CO2_ID;
            List<CO2> co2 = _context.CO2.Where(co2 => co2.CO2ID == co2Id).ToList();
            co2list[i] = co2[0];
        }        
        
        string s = JsonSerializer.Serialize(co2list);
        return s;
    }

    public string getHumidityList(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;

        //return the first list from "list" to get the variables.
        List<HumidityList> list =  _context.Humidities.Where(humidityList => humidityList.ROOM_ID == id).ToList();
        List<Humidity> humidityList2 = new List<Humidity>();
        
        for (int i = 0; i < list.Capacity; i++)
        {
            int humidityId = list[i].HUM_ID;
            List<Humidity> humidities = _context.Humidity.Where(humidity => humidity.HUM_ID == humidityId).ToList();
            humidityList2[i] = humidities[0];
        }        
        
        string s = JsonSerializer.Serialize(humidityList2);
        return s;
    }

    public string getTemperatureList(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        
        //return the first list from "list" to get the variables.
        List<TemperatureList> list =  _context.Temperatures.Where(temperatureList => temperatureList.ROOM_ID == id).ToList();
        List<Temperature> temperaturelist = new List<Temperature>();
        
        for (int i = 0; i < list.Capacity; i++)
        {
            int TempID = list[i].TEMP_ID;
            List<Temperature> temperatures = _context.Temperature.Where(temperature => temperature.TEMP_ID == TempID).ToList();
            temperaturelist[i] = temperatures[0];
        }        
        
        string s = JsonSerializer.Serialize(temperaturelist);
        return s;
    }

    public string getServoList(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        
        //return the first list from "list" to get the variables.
        List<ServoList> list =  _context.Servos.Where(servoList => servoList.ROOM_ID == id).ToList();
        List<Servo> servoslist = new List<Servo>();
        
        for (int i = 0; i < list.Capacity; i++)
        {
            int ServoID = list[i].SERV_ID;
            List<Servo> servos = _context.Servo.Where(servo => servo.SERV_ID == ServoID).ToList();
            servoslist[i] = servos[0];
        }        
        
        string s = JsonSerializer.Serialize(servoslist);
        return s;
    }

    public Room getRoomByName(string name)
    {
        bool r =_context.Room.Any(room => room.Name==name); //might work
        Room returnRoom;
        if (r == false)
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
            List<Room> rooms =_context.Room.Where(room => room.Name==name).ToList();
            returnRoom = rooms[0]; 
        }
        

        return returnRoom;
    }
}