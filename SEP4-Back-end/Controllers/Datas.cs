using System;
using Microsoft.AspNetCore.Mvc;
using SEP4_Back_end.DB;
namespace SEP4_Back_end.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class Datas : ControllerBase {
        private IDatabaseManager db;

        public Datas () {
            db = new DatabaseManager ();
        }

        [HttpGet ("{room}.{type}")]
        public ActionResult GetData (string room, string type) {
            try {
                switch (type) {
                    case "CO2":
                        return Ok (db.getCO2List (room));
                    case "Humidity":
                        return Ok (db.getHumidityList (room));
                    case "Temperature":
                        return Ok (db.getTemperatureList (room));
                    case "Servo":
                        return Ok (db.getServoList (room));
                    default:
                        return NotFound ("Data lists do not exist for this room");
                }
            } catch (Exception e) {
                Console.WriteLine (e);
                return BadRequest ("Malformed request");
            }
        }

        [HttpGet ("{room}.{type}.{weekNumber}")]
        public ActionResult GetData (string room, string type, int weekNumber) {
            try {
                switch (type) {
                    case "CO2":
                        return Ok (db.getCO2List (room, weekNumber));
                    case "Humidity":
                        return Ok (db.getHumidityList (room, weekNumber));
                    case "Temperature":
                        return Ok (db.getTemperatureList (room, weekNumber));
                    case "Servo":
                        return Ok (db.getServoList (room, weekNumber));
                    default:
                        return NotFound ("Data lists do not exist for this room at this week");
                }
            } catch (Exception e) {
                Console.WriteLine (e);
                return BadRequest ("Malformed request");
            }
        }

        /*[HttpGet ("{room}.{type}.{startDate}.{endDate}")]
        public ActionResult GetData (string room, string type, string startDate, string endDate) {
            try {
                switch (type) {
                    case "CO2":
                        return Ok (db.getCO2List(room, startDate, endDate));
                    case "Humidity":
                        return Ok (db.getHumidityList(room, startDate, endDate));
                    case "Temperature":
                        return Ok (db.getTemperatureList (room, startDate, endDate));
                    case "Servo":
                        return Ok (db.getServoList (room, startDate, endDate));
                    default:
                        return NotFound ("Data lists do not exist for this room at period");
                }
            } catch (Exception e) {
                Console.WriteLine (e);
                return BadRequest ("Malformed request");
            }
        }*/
    }
}