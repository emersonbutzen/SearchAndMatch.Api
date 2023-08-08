using Newtonsoft.Json;
using SearchAndMatch.Api.Exceptions;
using SearchAndMatch.Application.DTOs;
using SearchAndMatch.Application.Servives;

namespace SearchAndMatch.Api.Commands
{
    public class MatchingSearchCommandHandler : ICommandHandler<MatchingSearchCommand, EndpointResponse>
    {
        private readonly IPatientService _patientService;
        private readonly ICreateSearchEngineService _createSearchEngineService;
        private readonly ILogger<AddPatientCommandHandler> _logger; public MatchingSearchCommandHandler(IPatientService patientService, ICreateSearchEngineService createSearchEngineService, ILogger<AddPatientCommandHandler> logger)
        {
            _patientService = patientService;
            _createSearchEngineService = createSearchEngineService;
            _logger = logger;
        }

        public async Task<EndpointResponse> Handle(MatchingSearchCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received MatchingSearchCommand for {@MatchingSearchCommand}", request);
            if (_patientService == null)
            {
                throw new Exception("Sorry, our database have been facing technical issue.");
            }

            var patient = await _patientService.GetPatient(request.SearchAndMatch.PatientId);
            if (patient == null)
            {
                throw new NotFoundException("Sorry, that patient id was not found in our database.");
            }

            var engine = await _patientService.GetEngine(request.SearchAndMatch.MatchEngineId);
            if (engine == null)
            {
                throw new NotFoundException("Sorry, that search was not found in our database.");
            }

            var result = new EndpointResponse();
            var eng1 = JsonConvert.DeserializeObject<EngineRequest1>(engine.Schema);
            if (eng1.Patient.Forename != null)
            {
                eng1.Patient.Forename = patient.FirstName;
                eng1.Patient.Surname = patient.LastName;
                eng1.Patient.DateOfBirth = Convert.ToDateTime(patient.DateOfBirth).ToString("yyyy-MM-dd");
                eng1.Patient.DiseaseType = patient.DiseaseType;
                result = await this._createSearchEngineService.EngineMatching1Async(eng1, engine);
            }

            var eng2 = JsonConvert.DeserializeObject<EngineRequest2>(engine.Schema);
            if (eng2.Patient.FirstName != null)
            {
                eng2.Patient.FirstName = patient.FirstName;
                eng2.Patient.LastName = patient.LastName;
                eng2.Patient.DateOfBirth = Convert.ToDateTime(patient.DateOfBirth).ToString("yyyy-MM-dd");
                eng2.Patient.DiseaseType = patient.DiseaseType;
                result = await _createSearchEngineService.EngineMatching2Async(eng2, engine);
            }

            return result;
        }
    }
}
