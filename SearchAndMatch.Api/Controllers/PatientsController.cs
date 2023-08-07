using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchAndMatch.Application.DTOs;
using SearchAndMatch.Application.Servives;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ICreateSearchEngineService _createSearchEngineService;

        public PatientsController(IPatientService patientService, ICreateSearchEngineService createSearchEngineService)
        {
            _patientService = patientService;
            _createSearchEngineService = createSearchEngineService;
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            if (_patientService == null)
            {
                return NotFound();
            }
            var patient = await _patientService.GetPatient(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("CreatePatient")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Patient>> CreatePatient([FromBody] PatientCreateRequest patient)
        {
            if (_patientService == null)
            {
                return Problem("Entity set 'PatientContext.Patients'  is null.");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var formats = new string[] { "yyyy-MM-dd", "dd/MM/yyyy", "d/MM/yyyy", "yyyy/MM/dd" };
            var isValidFormat = DateTime.TryParseExact(patient.DateOfBirth, formats, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateOfBith);
            if (isValidFormat)
            {
                var newPatient = new Patient() { FirstName = patient.FirstName, LastName = patient.LastName, DateOfBirth = dateOfBith.Date.ToString(), DiseaseType = patient.DiseaseType };
                await _patientService.AddPatient(newPatient);

                return CreatedAtAction("GetPatient", new { id = newPatient.Id }, newPatient);
            }
            else
            {
                return BadRequest("Invalid date format, please use yyyy-MM-dd\", \"dd/MM/yyyy\", \"d/MM/yyyy\" or \"yyyy/MM/dd");
            }
        }

        [Route("CreateSearch")]
        [HttpPost]
        public async Task<ActionResult<Patient>> CreateSearch([FromBody] SearchCreateRequest search)
        {
            if (_patientService == null)
            {
                return Problem("Sorry, our database have been facing technical issue.");
            }

            var patient = await _patientService.GetPatient(search.PatientId);
            if (patient == null)
            {
                return NotFound("Sorry, that patient id was not found in our database.");
            }

            var engine = await _patientService.GetEngine(search.MatchEngineId);
            if (engine == null)
            {
                return NotFound("Sorry, that search was not found in our database.");
            }

            dynamic matchEngine = JsonConvert.DeserializeObject(engine.Schema);

            var eng1 = JsonConvert.DeserializeObject<EngineRequest1>(engine.Schema);
            if (eng1.Patient.Forename != null)
            {
                eng1.Patient.Forename = patient.FirstName;
                eng1.Patient.Surname = patient.LastName;
                eng1.Patient.DateOfBirth = Convert.ToDateTime(patient.DateOfBirth).ToString("yyyy-MM-dd");
                eng1.Patient.DiseaseType = patient.DiseaseType;
                await this._createSearchEngineService.EngineMatching1(eng1, engine);
            }

            var eng2 = JsonConvert.DeserializeObject<EngineRequest2>(engine.Schema);
            if (eng2.Patient.FirstName != null)
            {
                eng2.Patient.FirstName = patient.FirstName;
                eng2.Patient.LastName = patient.LastName;
                eng2.Patient.DateOfBirth = Convert.ToDateTime(patient.DateOfBirth).ToString("yyyy-MM-dd");
                eng2.Patient.DiseaseType = patient.DiseaseType;
                await this._createSearchEngineService.EngineMatching1(eng1, engine);
            }

            return patient;
        }
    }
}
