using Microsoft.EntityFrameworkCore;

namespace SEP4_Back_end.Model
{
    public class Context : DbContext
    {
    
        public DbSet<CO2> CO2 {get;set;}
        public DbSet<Humidity> Humidity {get;set;}
        public DbSet<Temperature> Temperature {get;set;} 
        public DbSet<Servo> Servo {get;set;}
        public DbSet<CO2List> CO2s{get;set;}
        public DbSet<HumidityList> Humidities {get;set;}
        public DbSet<TemperatureList> Temperatures {get;set;}
        public DbSet<ServoList> Servos {get;set;}
        public DbSet<Room> Room {get;set;}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
            modelBuilder.Entity<CO2List>().HasKey(c => new {c.CO2_ID, c.ROOM_ID});
            modelBuilder.Entity<HumidityList>().HasKey(c => new {c.HUM_ID, c.ROOM_ID});
            modelBuilder.Entity<TemperatureList>().HasKey(c => new {c.TEMP_ID, c.ROOM_ID});
            modelBuilder.Entity<ServoList>().HasKey(c => new {c.SERV_ID, c.ROOM_ID});
            
            
        }


        public Context()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost;Database=sensor;Trusted_Connection=True;");
    }
}