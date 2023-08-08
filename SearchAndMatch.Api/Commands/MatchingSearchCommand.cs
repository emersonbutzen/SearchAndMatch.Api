using SearchAndMatch.Application.DTOs;

namespace SearchAndMatch.Api.Commands
{
    public class MatchingSearchCommand : ICommand<EndpointResponse>
    {
        public SearchCreateRequest SearchAndMatch { get; set; }
        public MatchingSearchCommand(SearchCreateRequest searchAndMatch) {
            SearchAndMatch = searchAndMatch;
        }
    }
}
