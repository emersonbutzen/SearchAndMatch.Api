using MediatR;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Api.Queries.Language.GetLanguageById
{
    public class GePatientByIdQuery : IRequest<Patient>
    {
        public int Id { get; private set; }

        public GePatientByIdQuery(int id)
        {
            Id = id;
        }
    }
}
