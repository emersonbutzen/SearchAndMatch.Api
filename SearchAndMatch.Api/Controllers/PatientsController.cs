using MediatR;
using Microsoft.AspNetCore.Mvc;
using SearchAndMatch.Api.Commands;
using SearchAndMatch.Api.Queries.Language.GetLanguageById;
using SearchAndMatch.Application.DTOs;
using SearchAndMatch.Application.Servives;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var result = await _mediator.Send(new GePatientByIdQuery(id));
            return new OkObjectResult(result);
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
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var result = await _mediator.Send(new AddPatientCommand(patient));

            return CreatedAtAction("GetPatient", new { id = result.Id }, result);
        }

        [Route("CreateSearch")]
        [HttpPost]
        public async Task<ActionResult<EndpointResponse>> CreateSearch([FromBody] SearchCreateRequest search)
        {
            var result = await _mediator.Send(new MatchingSearchCommand(search));
            return new OkObjectResult(result);
        }
    }
}
