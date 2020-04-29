using System.ComponentModel.DataAnnotations.Schema;

namespace SEP4_Back_end.Model
{
    [Table("ServoList")]
    public class ServoList 
    {
        [ForeignKey("Room")]
        public int ROOM_ID { get; set; }
        [ForeignKey("Servo")]
        public int SERV_ID { get; set; }
        
        public virtual Servo Servo { get; set; }
        public virtual Room Room { get; set; }
        public ServoList()
        {

        }
    }
}
