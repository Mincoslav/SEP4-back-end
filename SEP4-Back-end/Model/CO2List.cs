using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP4_Back_end.Model
{
    [Table("CO2List")]
    public class CO2List 
    {
        
        [ForeignKey("Room")]
        public int ROOM_ID { get; set; }
        [ForeignKey("CO2")]
        public int CO2_ID { get; set; }
        
        public virtual CO2 CO2 { get; set; }
        public virtual Room Room { get; set; }
        
        public CO2List()
        {

        }
    }
}
