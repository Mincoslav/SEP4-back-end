using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP4_Back_end.Model
{
    [Table("Temperature")]
    public class Temperature 
    {
        [Key]
        public int TEMP_ID { get; set; }
        public float TEMP_value { get; set; }
        public DateTime Date { get; set; }
        public Temperature()
        {
        
        }
    }
}