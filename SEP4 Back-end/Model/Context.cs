using SEP4_Back_end.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
public class Context : DbContext
{
    public DbSet<Data> Data {get;set;}
    public DbSet<Servo> Servo {get;set;}
    public DbSet<UniversalList> List {get;set;}
    public DbSet<Room> Room {get;set;}

    public Context()
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=sensor.db");
}