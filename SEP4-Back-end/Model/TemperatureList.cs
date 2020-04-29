using System.ComponentModel.DataAnnotations.Schema;

namespace SEP4_Back_end.Model
{
    [Table("TemperatureList")]
    public class TemperatureList 
    {
        [ForeignKey("Room")]
        public int ROOM_ID { get; set; }
        [ForeignKey("Temperature")]
        public int TEMP_ID { get; set; }
        
        public virtual Temperature Temperature { get; set; }
        public virtual Room Room { get; set; }
        public TemperatureList()
        {

        }
    }
}
