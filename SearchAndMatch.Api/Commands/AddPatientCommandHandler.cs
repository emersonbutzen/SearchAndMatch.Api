using System.Globalization;
using SearchAndMatch.Application.Servives;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Api.Commands
{
    public class AddPatientCommandHandler : ICommandHandler<AddPatientCommand, Patient>
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<AddPatientCommandHandler> _logger;
        public AddPatientCommandHandler(IPatientService patientService, ILogger<AddPatientCommandHandler> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        public async Task<Patient> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received AddPatientCommand for {@AddPatientCommand}", request);
            if (_patientService == null)
            {
                throw new Exception("Entity set 'PatientContext.Patients'  is null.");
            }

            var formats = new string[] { "yyyy-MM-dd", "dd/MM/yyyy", "d/MM/yyyy", "yyyy/MM/dd" };
            var isValidFormat = DateTime.TryParseExact(request.PatientToAdd.DateOfBirth, formats, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateOfBith);
            if (isValidFormat)
            {
                var newPatient = new Patient() { FirstName = request.PatientToAdd.FirstName, LastName = request.PatientToAdd.LastName, DateOfBirth = dateOfBith.Date.ToString(), DiseaseType = request.PatientToAdd.DiseaseType };
                var result = await _patientService.AddPatient(newPatient);
                if (!result)
                {
                    throw new Exception("Sorry, problem to insert in database.");
                }

                return newPatient;
            }
            else
            {
                throw new BadHttpRequestException("Invalid date format, please use yyyy-MM-dd\", \"dd/MM/yyyy\", \"d/MM/yyyy\" or \"yyyy/MM/dd");
            }
        }
    }
}
