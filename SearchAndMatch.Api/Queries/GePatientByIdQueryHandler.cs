using MediatR;
using SearchAndMatch.Api.Exceptions;
using SearchAndMatch.Application.Servives;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Api.Queries.Language.GetLanguageById
{
    public class GePatientByIdQueryHandler : IRequestHandler<GePatientByIdQuery, Patient>
    {
        private readonly IPatientService _patientService;

        public GePatientByIdQueryHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<Patient> Handle(GePatientByIdQuery request, CancellationToken cancellationToken)
        {

            var patient = await _patientService.GetPatient(request.Id);

            if (patient == null)
            {
                throw new NotFoundException("Sorry, that patient id was not found in our database.");
            }

            return patient;
        }
    }
}
