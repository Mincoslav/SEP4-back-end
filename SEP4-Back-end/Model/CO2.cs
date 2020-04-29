using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SEP4_Back_end.Model
{
    [Table("CO2")]
    public class CO2 
    {
        [Key]
        public int CO2ID { get; set; }
        public float CO2_value { get; set; }
        public DateTime Date { get; set; }
        
    }
}