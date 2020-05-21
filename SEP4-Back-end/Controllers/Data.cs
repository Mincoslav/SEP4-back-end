using System;
using Microsoft.AspNetCore.Mvc;
using SEP4_Back_end.DB;

namespace SEP4_Back_end.Controllers
{
    
    /// <summary>
    /// <para>The API exposing <c>Data</c> class.</para>
    /// <para>Contains methods exposed on the API to be reached by the Android Application,
    /// such as changing the servo state and get the current data/measurament to display on the app.</para>
    /// </summary>
    /// <remarks>
    /// <para>This class' methods can be reached at "api/data/".</para>
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class Data : ControllerBase
    {
        private IDatabaseManager db;

        public Data()
        {
            db = new DatabaseManager();
        }

        /// <summary>
        /// <para>Gets data depending on the type and time required.</para>
        /// </summary>
        /// <returns>
        /// <para>Returns a JSON with the serialized object required.</para>
        /// </returns>
        /// <param DateTime="date">The date time to get the object created closer to this time.</param>
        /// <param string="type">Type of the object needed.</param>
        /// <remarks>
        /// <para>This method can be reached with a GET request at "api/data/GetData?date={date}&type={type}".</para>
        ///  <example>"api/data/GetData?date=(DateTime.Now)&type=Humidity"</example>
        /// </remarks>
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

        /// <summary>
        /// Sets servo state depending on the room being used.
        /// </summary>
        /// <returns>
        /// Returns a response depending on the result.
        /// </returns>
        /// <param string="room">Room name.</param>
        /// <param string="servo">Serialized servo object with the value to be changed.</param>
        /// <remarks>
        /// This method can be reached with a GET request at "api/data/SetServo?room={room}&servo={servo}"
        /// <example>"api/data/SetServo?room=RoomA&servo={servoJsonString}"</example>
        /// </remarks>
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