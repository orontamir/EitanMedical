using Base.Models;
using Base.Services;
using EitanMedical.Interfaces;
using EitanMedical.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EitanMedical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartRateReadingController : ControllerBase
    {
        readonly IHeartRateReadingService _HeartRateReadingService;
        public HeartRateReadingController(IHeartRateReadingService heartRateReadingService)
        {
            _HeartRateReadingService = heartRateReadingService;
        }

     

        [HttpPost("AddHeartRateReading")]
        public async Task<IActionResult> AddHeartRateReading([FromBody] HeartRateReading value)
        {
            try
            {
                var res = await _HeartRateReadingService.AddHeartRateReading(value);
                if (res)
                {
                    return Ok();
                }
                else 
                {
                    return BadRequest();
                }
            }
            catch (Exception ex) 
            {
                LogService.LogError($"Exception when adding Heart Rate Reading: {value.HeartRate}, error message: {ex.Message}");
                return BadRequest($"Exception when adding Heart Rate Reading: {value.HeartRate}, error message: {ex.Message}");
            }
           
        }

        [HttpPost("AddListHeartRateReadings")]
        public async Task<IActionResult> AddListHeartRateReading([FromBody] List<HeartRateReading> values)
        {
            try
            {
                foreach (var value in values)
                {
                    var res = await _HeartRateReadingService.AddHeartRateReading(value);
                }
                    return Ok();
            }
            catch (Exception ex)
            {
                LogService.LogError($"Exception when adding Heart Rate Reading, error message: {ex.Message}");
                return BadRequest($"Exception when adding Heart Rate Reading, error message: {ex.Message}");
            }

        }

        // Returns all heart rate readings where the heart rate exceeds 100 bpm.
        [HttpGet("HighHeartRate")]
        public async Task<ActionResult<IEnumerable<HeartRateReading>>> GetHighHeartRateEvents()
        {
            var events =  _HeartRateReadingService.GetAllHeartRateReadings().Result
                        .Where(r => r.HeartRate > 100)
                        .Select(r => new
                        {
                            PatientId = r.PatientId,
                            Timestamp = r.Timestamp,
                            HeartRate = r.HeartRate
                        })
                        .ToList();
            return Ok(events);
        }

        // Calculates the average, maximum, and minimum heart rate for the specified patient in the given time range.
        [HttpGet("analytics/{id}")]
        public async Task<ActionResult> GetHeartRateAnalytics(string id, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            
            try
            {
                //Update Patient Request counter
                if (await _HeartRateReadingService.UpdatePatientRequest(id))
                {
                    var readings = _HeartRateReadingService.GetAllHeartRateReadings().Result
                          .Where(r => r.PatientId == id && r.Timestamp >= start && r.Timestamp <= end)
                          .ToList();
                    if (readings.Count == 0)
                        return NotFound("No readings found in the specified time range.");

                    var analytics = new
                    {
                        Average = readings.Average(r => r.HeartRate),
                        Maximum = readings.Max(r => r.HeartRate),
                        Minimum = readings.Min(r => r.HeartRate)
                    };

                    return Ok(analytics);
                }
                else
                { 
                    return BadRequest(); 
                }
               
            }
            catch (Exception ex) 
            {
                LogService.LogError($"Exception when Get Heart Rate Analytics, error message: {ex.Message}");
                return BadRequest($"Exception when Get Heart Rate Analytics, error message: {ex.Message}");
            }
           
        }
    }
}
