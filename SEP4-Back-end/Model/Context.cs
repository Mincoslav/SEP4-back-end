using SEP4_Back_end;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
public class Context : DbContext
{
    public DbSet<Data> Data {get;set;}
    public DbSet<CO2> CO2 {get;set;}
    public DbSet<Humidity> Humidity {get;set;}
    public DbSet<Temperature> Temperature {get;set;} 
    public DbSet<Servo> Servo {get;set;}
    public DbSet<UniversalList> List {get;set;}
    public DbSet<CO2List> CO2s{get;set;}
    public DbSet<HumidityList> Humidities {get;set;}
    public DbSet<TemperatureList> Temperatures {get;set;}
    public DbSet<ServoList> Servos {get;set;}
    public DbSet<Room> Room {get;set;}

    public Context()
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=DESKTOP-I2IQ0UL;Database=sensor;Trusted_Connection=True;");
}