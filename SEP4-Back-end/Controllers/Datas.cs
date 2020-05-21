using System;
using Microsoft.AspNetCore.Mvc;
using SEP4_Back_end.DB;
namespace SEP4_Back_end.Controllers {
    
    /// <summary>
    /// <para>The API exposing <c>Datas</c> class.</para>
    /// <para>Contains methods exposed on the API to be reached by the Android Application,
    /// these being a dataset of the measurements.</para>
    /// </summary>
    /// <remarks>
    /// <para>This class' methods can be reached at "api/datas/".</para>
    /// </remarks>
    [Route ("api/[controller]")]
    [ApiController]
    public class Datas : ControllerBase {
        private IDatabaseManager db;

        public Datas () {
            db = new DatabaseManager ();
        }
        
        /// <summary>
        /// <para>Gets data depending on the type and room required.</para>
        /// </summary>
        /// <returns>
        /// <para>Returns a JSON with the serialized list of objects required.</para>
        /// </returns>
        /// <param name="room">Room where the device is located.</param>
        /// <param name="type">Type of object/measurement needed.</param>
        /// <remarks>
        /// <para>This method can be reached with a GET request at "api/datas/GetDataList?room={room}&type={type}".</para>
        /// <example>"api/datas/GetDataList?room=RoomA&type=Humidity"</example>
        /// </remarks>
        [HttpGet ("GetDataList")]
        public ActionResult GetDataList (string room, string type) {
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
        /// <summary>
        /// <para>Gets data depending on the type of measurement, room and week required.</para>
        /// </summary>
        /// <returns>
        /// <para>Returns a JSON with the serialized list of objects required.</para>
        /// </returns>
        /// <param name="room">Room where the device is located.</param>
        /// <param name="type">Type of object/measurement needed.</param>
        /// <param name="weekNumber">Week in which the required measurements were saved.</param>
        /// <remarks>
        /// <para>This method can be reached with a GET request at:
        /// "api/datas/GetByWeek?weeknumber={weeknumber}&type={type}&room={room}".</para>
        /// <example>"api/datas/GetByWeek?weeknumber=15&type=Humidity&room=RoomA"</example>
        /// </remarks>
        [HttpGet ("GetByWeek")]
        public ActionResult GetDataByWeek (int weekNumber, string type, string room) {
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