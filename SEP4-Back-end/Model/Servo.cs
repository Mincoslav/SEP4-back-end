using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP4_Back_end.Model
{
    [Table("Servo")]
    public class Servo
    {
        [Key]
        public int SERV_ID {get;set;}
        public bool Spinning{get;set;}
        public DateTime Date { get; set; }

        public Servo()
        {
        
        }
    }
}