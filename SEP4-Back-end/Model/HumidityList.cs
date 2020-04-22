using System.ComponentModel.DataAnnotations.Schema;

namespace SEP4_Back_end.Model
{
    [Table("HumidityList")]
    public class HumidityList 
    {
        
        [ForeignKey("Room")]
        public int ROOM_ID { get; set; }
        [ForeignKey("Humidity")]
        public int HUM_ID { get; set; }
        
        public virtual Humidity Humidity { get; set; }
        public virtual Room Room { get; set; }
        public HumidityList()
        {
    
        }
    }
}
