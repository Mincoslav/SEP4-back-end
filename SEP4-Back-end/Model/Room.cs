using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP4_Back_end.Model
{
    [Table("Room")]
    public class Room
    {
        [Key]
        public int RoomID {get;set;}
        public string Name {get;set;}
        public string Location { get; set; }
    }
}