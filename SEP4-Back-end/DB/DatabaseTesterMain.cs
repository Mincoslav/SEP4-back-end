/*using System;
using System.IO;
using System.Text.Json;
using SEP4_Back_end.Model;

namespace SEP4_Back_end.DB
{
    public class DatabaseTesterMain
    {
        

        public static void main(string[] args)
        {
            DatabaseManager _manager = new DatabaseManager();
         
            CO2 co2 = new CO2();
            co2.CO2_value = (float) 12.4;
            DateTime dateTime = DateTime.Now;
            co2.Date = dateTime;

            String s;
            
            s = JsonSerializer.Serialize(co2);
            
            _manager.persistCO2(s, "toilet");

        }
        
    }
}*/