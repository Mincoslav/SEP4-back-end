
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using SEP4_Back_end.DB;
using SEP4_Back_end.Model;

public class DatabaseManager : IDatabaseManager
{
    private Context _context= new Context();

    /// <summary>
    /// <para>Calculate first of day of the week based on a year and week number.</para>
    /// </summary>
    /// <param name="year">Year , usually the current year.</param>
    /// <param name="weekOfYear">Week number,usually the current week. </param>
    /// <returns>
    /// <para>Returns first day of a specific week.</para>
    /// </returns>
    public static DateTime FirstDateOfWeek(int year, int weekOfYear)
    {
        DateTime jan1 = new DateTime(year, 1, 1);
        int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
        DateTime firstMonday = jan1.AddDays(daysOffset);
        int firstWeek = 
            CultureInfo.CurrentCulture.Calendar.GetWeekOfYear
                (jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        if (firstWeek <= 1)
        {
            weekOfYear -= 1;
        }    
        return firstMonday.AddDays(weekOfYear * 7);
    }
    
    
    /// <summary>
    /// <para>Adds a CO2 measurement to the database based on the room it belongs.</para>
    /// </summary>
    /// <param name="co2">CO2 serialized object in JSON</param>
    /// <param name="roomName">Room where the measurement was taken.</param>
    public void persistCO2(string co2,DateTime date,string roomName)
        //persist means adding to the database
    {
        CO2 co2Object;
        try
        {
            co2Object = new CO2();
            Console.WriteLine(co2+"---------------------------------------------------------");
            co2Object.CO2_value = float.Parse(co2);
            Console.WriteLine(co2Object.CO2_value+"---------------------------------------");
            co2Object.Date = date;
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

    /// <summary>
    /// <para>Adds a Humidity measurement to the database based on the room it belongs.</para>
    /// </summary>
    /// <param name="humidity">Humidity serialized object in JSON</param>
    /// <param name="roomName">Room where the measurement was taken.</param>
    public void persistHumdity(string humidity,DateTime date,string roomName)
        //persist means adding to the database
    {
        Humidity humidityObject;
        try
        {
            humidityObject = new Humidity();
            humidityObject.HUM_value = float.Parse(humidity);
            humidityObject.Date = date;
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

    /// <summary>
    /// <para>Adds a temperature measurement to the database based on the room it belongs.</para>
    /// </summary>
    /// <param name="temperature">temperature serialized object in JSON</param>
    /// <param name="roomName">Room where the measurement was taken.</param>
    public void persistTemperature(string temperature,DateTime date,string roomName) 
        //persist means adding to the database
    {
        Temperature temperatureObject;
        try
        {
            temperatureObject = new Temperature();
            temperatureObject.TEMP_value = float.Parse(temperature);
            temperatureObject.Date = date;
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

    
    /// <summary>
    /// <para>Adds a Servo change of state to the database based on the room it belongs.</para>
    /// </summary>
    /// <param name="servo">Servo serialized object in JSON</param>
    /// <param name="roomName">Room where the measurement was taken.</param>
    public void persistServo(string servo,string roomName)
        //persist means adding to the database
    {
        Servo servoObject;
        LoraReceiver lora = new LoraReceiver();
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
            
            Packet packet3 = new Packet();
            packet3.cmd = "tx";
            packet3.EUI = "0004A30B00259F36";
            packet3.port = 1;
            packet3.data = "AABBCCDD";
            packet3.confirmed = false;
            lora.SendPacket(packet3);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("JSON is corrupt");
        }
    }

    
    /// <summary>
    /// <para>Gets a CO2 object from the database base on the time of the measurement</para>
    /// </summary>
    /// <param name="dateTime">Date and time of the measurement</param>
    /// <returns>
    /// <para>CO2 serialized into JSON</para>
    /// </returns>
    public string getCO2(DateTime dateTime)
    {
        List<CO2> co2s;
        co2s = _context.CO2.Where(CO2 =>CO2.Date == dateTime).ToList(); //might  work
        CO2 co2 = co2s[0];
        string s = JsonSerializer.Serialize(co2);
        return s; 
    }

    
    /// <summary>
    /// <para>Gets a Humidity object from the database based on the time of the measurement</para>
    /// </summary>
    /// <param name="dateTime">Date and time of the measurement</param>
    /// <returns>
    /// <para>Humidity object serialized into JSON</para>
    /// </returns>
    public string getHumidity(DateTime dateTime)
    {
        List<Humidity> humidities = _context.Humidity.Where(humidity => humidity.Date == dateTime).ToList(); //might  work
        Humidity h = humidities[0];
        string s = JsonSerializer.Serialize(h);
        return s;   
    }

    /// <summary>
    /// <para>Gets a Temperature object from the database based on the time of the measurement</para>
    /// </summary>
    /// <param name="dateTime">Date and time of the measurement</param>
    /// <returns>
    /// <para>Temperature object serialized into JSON</para>
    /// </returns>
    public string getTemperature(DateTime dateTime)
    {
        List<Temperature> temperatures = _context.Temperature.Where(temperature =>  temperature.Date == dateTime).ToList(); //might not work
        Temperature t = temperatures[0];
        string s = JsonSerializer.Serialize(t);
        return s;
    }

    /// <summary>
    /// <para>Gets a Servo object from the database based on the time of the measurement</para>
    /// </summary>
    /// <param name="dateTime">Date and time of the measurement</param>
    /// <returns>
    /// <para>Servo object serialized into JSON</para>
    /// </returns>
    public string getServo(DateTime dateTime)
    {
        List<Servo> servos = _context.Servo.Where(servo => servo.Date == dateTime).ToList(); //might  work
        Servo t = servos[0];
        string s = JsonSerializer.Serialize(t);
        return s;
    }

    /// <summary>
    /// <para>Gets a list of all the CO2 objects in the database from a certain Room.</para>
    /// </summary>
    /// <param name="roomName">Room name</param>
    /// <returns>
    /// <para>List of CO2 objects serialized into JSON</para>
    /// </returns>
    public string getCO2List(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        
        //return the first list from "list" to get the variables.
        List<CO2List> list =  _context.CO2s.Where(co2List => co2List.ROOM_ID == id).ToList();
        List<CO2> co2list = new List<CO2>();
        
        List<CO2> co2 = new List<CO2>();
        
        for (int i = 0; i < list.Count; i++)
        {
            int co2Id = list[i].CO2_ID;
            
            co2 = _context.CO2.Where(co2 => co2.CO2ID == co2Id).ToList();
            
            
            co2list.Add(co2[0]);
        }        
        
        string s = JsonSerializer.Serialize(co2list);
        return s;
    }

    /// <summary>
    /// <para>Gets a list of all the Humidity objects in the database from a certain Room.</para>
    /// </summary>
    /// <param name="roomName">Room name</param>
    /// <returns>
    /// <para>List of Humidity objects serialized into JSON</para>
    /// </returns>
    public string getHumidityList(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;

        //return the first list from "list" to get the variables.
        List<HumidityList> list =  _context.Humidities.Where(humidityList => humidityList.ROOM_ID == id).ToList();
        List<Humidity> humidityList2 = new List<Humidity>();
        
        for (int i = 0; i < list.Count; i++)
        {
            int humidityId = list[i].HUM_ID;
            List<Humidity> humidities = _context.Humidity.Where(humidity => humidity.HUM_ID == humidityId).ToList();
            humidityList2.Add(humidities[0]);
        }        
        
        string s = JsonSerializer.Serialize(humidityList2);
        return s;
    }

    /// <summary>
    /// <para>Gets a list of all the Temperature objects in the database from a certain Room.</para>
    /// </summary>
    /// <param name="roomName">Room name</param>
    /// <returns>
    /// <para>List of Temperature objects serialized into JSON</para>
    /// </returns>
    public string getTemperatureList(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        
        //return the first list from "list" to get the variables.
        List<TemperatureList> list =  _context.Temperatures.Where(temperatureList => temperatureList.ROOM_ID == id).ToList();
        List<Temperature> temperaturelist = new List<Temperature>();
        
        for (int i = 0; i < list.Count; i++)
        {
            int TempID = list[i].TEMP_ID;
            List<Temperature> temperatures = _context.Temperature.Where(temperature => temperature.TEMP_ID == TempID).ToList();
            temperaturelist.Add(temperatures[0]);
        }        
        
        string s = JsonSerializer.Serialize(temperaturelist);
        return s;
    }

    /// <summary>
    /// <para>Gets a list of all the Servo objects in the database from a certain Room.</para>
    /// </summary>
    /// <param name="roomName">Room name</param>
    /// <returns>
    /// <para>List of Servo objects serialized into JSON</para>
    /// </returns>
    public string getServoList(string roomName)
    {
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        
        //return the first list from "list" to get the variables.
        List<ServoList> list =  _context.Servos.Where(servoList => servoList.ROOM_ID == id).ToList();
        List<Servo> servoslist = new List<Servo>();
        
        for (int i = 0; i < list.Count; i++)
        {
            int ServoID = list[i].SERV_ID;
            List<Servo> servos = _context.Servo.Where(servo => servo.SERV_ID == ServoID).ToList();
            servoslist.Add(servos[0]);
        }        
        
        string s = JsonSerializer.Serialize(servoslist);
        return s;
    }

    
    /// <summary>
    /// <para>Gets a room object from the database based on its name,
    /// if it does not exist it is then created.</para>
    /// </summary>
    /// <param name="name">Room name</param>
    /// <returns>
    /// <para>Room object</para>
    /// </returns>
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

    /// <summary>
    /// <para>Gets a list of all the <c>Humidity</c> objects in the database from a certain room in a certain week.</para>
    /// </summary>
    /// <param name="roomName">Room name</param>
    /// <param name="weekNumber">Week number</param> 
    /// <returns>
    /// <para>List of <c>Humidity</c> objects serialized into JSON</para>
    /// </returns>
    /// <seealso cref="DatabaseManager.getRoomByName(string)"/>
    public string getHumidityList(string roomName, int weekNumber)
    {
        //getting the room ID
        Room r = getRoomByName(roomName);
        var id = r.RoomID;

        DateTime dateFromWeekNumber = FirstDateOfWeek(2020, weekNumber);
        DateTime dateFromWeekNumberPlusOneWeek = FirstDateOfWeek(2020, weekNumber+1);

        //gets the list of objects in the database within the given dates and room
        var list =
            (from humidity in _context.Humidity
            join humidities in _context.Humidities on humidity.HUM_ID equals humidities.HUM_ID
            where humidities.ROOM_ID == id && humidity.Date >= dateFromWeekNumber && humidity.Date < dateFromWeekNumberPlusOneWeek
            select new { DATE = humidity.Date, ROOM = humidities.ROOM_ID, VALUE = humidity.HUM_value, HUM_ID = humidity.HUM_ID }).ToList(); //produces flat sequence
        
        //temporary list to store the objects retrieved
        List<Humidity> humidityList2 = new List<Humidity>();
        
        //transforming the data in list to objects and adding it to temperatureList
        for (int i = 0; i < list.Count; i++)
        {
            Humidity temp_humidity = new Humidity();

            temp_humidity.Date = list[i].DATE;
            temp_humidity.HUM_value = list[i].VALUE;
            temp_humidity.HUM_ID = list[i].HUM_ID;
            
            Console.WriteLine(temp_humidity);
            humidityList2.Add(temp_humidity);
        }        
        
        //object serialization
        string s = JsonSerializer.Serialize(humidityList2);
        return s;
    }

    
    /// <summary>
    /// <para>Gets a list of all the <c>CO2</c> objects in the database from a certain room in a certain week.</para>
    /// </summary>
    /// <param name="roomName">Room name</param>
    /// <param name="weekNumber">Week number</param> 
    /// <returns>
    /// <para>List of <c>CO2</c> objects serialized into JSON</para>
    /// </returns>
    /// See <see cref="DatabaseManager.getRoomByName(string)"/> to understand how the room is retrieved.
    /// See <see cref="DatabaseManager.FirstDateOfWeek(int,int)"/>  to understand how te first day of the week is calculated.
    public string getCO2List(string roomName, int weekNumber)
    {
        //getting the room ID
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        
        //first day of the week
        DateTime dateFromWeekNumber = FirstDateOfWeek(DateTime.Now.Year, weekNumber);
        //first day of the week after
        DateTime dateFromWeekNumberPlusOneWeek = FirstDateOfWeek(DateTime.Now.Year, weekNumber+1);

        //gets the list of objects in the database within the given dates and room
        var list =
            (from co2 in _context.CO2
                join co2s in _context.CO2s on co2.CO2ID equals co2s.CO2_ID
                where co2s.ROOM_ID == id && co2.Date >= dateFromWeekNumber && co2.Date < dateFromWeekNumberPlusOneWeek
                select new { DATE = co2.Date, ROOM = co2s.ROOM_ID, VALUE = co2.CO2_value, CO2_ID = co2.CO2ID }).ToList(); //produces flat sequence
        
        //temporary list to store the objects retrieved
        List<CO2> co2list = new List<CO2>();
        
        //transforming the data in list to objects and adding it to temperatureList
        for (int i = 0; i < list.Count; i++)
        {
            CO2 temp_co2 = new CO2();

            temp_co2.Date = list[i].DATE;
            temp_co2.CO2_value = list[i].VALUE;
            temp_co2.CO2ID = list[i].CO2_ID;
            
            co2list.Add(temp_co2);
        }      
        
        //object serialization
        string s = JsonSerializer.Serialize(co2list);
        return s;
    }

    /// <summary>
    /// <para>Gets a list of all the <c>Temperature</c> objects in the database from a certain room in a certain week.</para>
    /// </summary>
    /// <param name="roomName">Room name</param>
    /// <param name="weekNumber">Week number</param> 
    /// <returns>
    /// <para>List of <c>Temperature</c> objects serialized into JSON</para>
    /// </returns>
    /// <seealso cref="DatabaseManager.getRoomByName(string)"/>
    public string getTemperatureList(string roomName, int weekNumber)
    {
        //getting the room ID
        Room r = getRoomByName(roomName);
        var id = r.RoomID;
        
        //first day of the week
        DateTime dateFromWeekNumber = FirstDateOfWeek(DateTime.Now.Year, weekNumber);
        //first day of the week after
        DateTime dateFromWeekNumberPlusOneWeek = FirstDateOfWeek(DateTime.Now.Year, weekNumber+1);

        //gets the list of objects in the database within the given dates and room
        var list =
            (from temperature in _context.Temperature
                join temperatureList in _context.Temperatures on temperature.TEMP_ID equals temperatureList.TEMP_ID
                where temperatureList.ROOM_ID == id && temperature.Date >= dateFromWeekNumber && temperature.Date < dateFromWeekNumberPlusOneWeek
                select new { DATE = temperature.Date, ROOM = temperatureList.ROOM_ID, VALUE = temperature.TEMP_value, TEMP_ID = temperature.TEMP_ID }).ToList(); //produces flat sequence
       
        //temporary list to store the objects retrieved
        List<Temperature> temperaturelist = new List<Temperature>();
        
        //transforming the data in list to objects and adding it to temperatureList
        for (int i = 0; i < list.Capacity; i++)
        {
            Temperature temp_TEMP = new Temperature();

            temp_TEMP.Date = list[i].DATE;
            temp_TEMP.TEMP_value = list[i].VALUE;
            temp_TEMP.TEMP_ID = list[i].TEMP_ID;
            
            temperaturelist.Add(temp_TEMP);
        }        
        
        //object serialization
        string s = JsonSerializer.Serialize(temperaturelist);
        return s;
    }

    public string getServoList(string roomName, int weekNumber)
    {
        throw new NotImplementedException(); 
    }
}