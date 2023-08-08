using SearchAndMatch.Application.DTOs;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Api.Commands
{
    public class AddPatientCommand : ICommand<Patient>
    {
        public PatientCreateRequest PatientToAdd { get; set; }

        public AddPatientCommand(PatientCreateRequest patientToAdd)
        {
            PatientToAdd = patientToAdd;
        }
    }
}
