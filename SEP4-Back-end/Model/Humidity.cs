using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP4_Back_end.Model
{
    [Table("Humidity")]
    public class Humidity 
    {
        [Key]
        public int HUM_ID { get; set; }
        public float HUM_value { get; set; }
        public DateTime Date { get; set; }
        public Humidity()
        {
        
        }
    }
}