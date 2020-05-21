using System;
using Microsoft.AspNetCore.Mvc;
using SEP4_Back_end.DB;

namespace SEP4_Back_end.Controllers
{
    //api/data
    [Route("api/[controller]")]
    [ApiController]
    public class Data : ControllerBase
    {
        private IDatabaseManager db;

        public Data()
        {
            db = new DatabaseManager();
        }

        //GET api/data/GetData?date=DATETIME_VALUE&type=TYPENAME
        [HttpGet("GetData")]
        public ActionResult GetData(DateTime date, string type)
        {
            switch(type)
            {
                case "CO2" : return Ok(db.getCO2(date));
                case "Humidity" : return Ok(db.getHumidity(date));
                case "Temperature" : return Ok(db.getTemperature(date)); 
                case "Servo" : return Ok(db.getServo(date)); 
                default : return NotFound("There is no value at this date " + date.ToString());
            }
        }

        //GET api/data/SetServo?room=toilet&servo=servoName
        [HttpPost("SetServo")]
        public ActionResult SetServo(string room, string servo)
        {
            try
            {
                db.persistServo(servo, room);
            }
            catch
            {
                return BadRequest("You fucked up with the servo");
            }
            return Accepted("All is okay");
        }
    }
}