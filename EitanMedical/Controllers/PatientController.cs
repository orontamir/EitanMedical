using Base.Models;
using Base.Services;
using EitanMedical.Interfaces;
using EitanMedical.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EitanMedical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        readonly IPatientService _PatientService;
        public PatientController(IPatientService patientService)
        {
            _PatientService = patientService;
        }

        
        [HttpGet("GetAllPatients")]
        public ActionResult<IEnumerable<Patient>> GetAllPatients()
        {
            return Ok(_PatientService.GetAllPatients());
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatientById(int id)
        {
            try
            {
                var patient = _PatientService.GetPatientById(id.ToString());
                if (patient == null)
                    return NotFound();
                return Ok(patient);
            }
            catch (Exception ex) 
            {
                LogService.LogError($"Exception getting patient id: {id}, error message: {ex.Message}");
                return BadRequest($"Exception getting patient id: {id}, error message: {ex.Message}");
            }
            
        }

        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient([FromBody] Patient value)
        {
            try
            {
                var res = await _PatientService.AddPatient(value);
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
                LogService.LogError($"Exception when adding Heart Rate Reading: {value.Name}, error message: {ex.Message}");
                return BadRequest($"Exception when adding Heart Rate Reading: {value.Name}, error message: {ex.Message}");
            }

        }

        [NonAction]
        [HttpPost("AddListPatients")]
        public async Task<IActionResult> AddListPatient([FromBody] List<Patient> values)
        {
            try
            {
                foreach (var value in values)
                {
                    var res = await _PatientService.AddPatient(value);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                LogService.LogError($"Exception when adding Heart Rate Reading, error message: {ex.Message}");
                return BadRequest($"Exception when adding Heart Rate Reading, error message: {ex.Message}");
            }

        }

        // Returns the request count for each patient.
        [HttpGet("RequestTracking")]
        public async Task<ActionResult> GetPatientRequestTracking()
        {
            return Ok(_PatientService.GetAllPatientsRequest());
        }
    }
}
