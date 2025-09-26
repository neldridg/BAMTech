using MediatR;
using Microsoft.EntityFrameworkCore;
using Stargate.API.Business.Data;
using Stargate.API.Business.Dtos;
using Stargate.API.Controllers;

namespace Stargate.API.Business.Queries
{
    public class GetPeople : IRequest<GetPeopleResult>
    {

    }

    public class GetPeopleHandler : IRequestHandler<GetPeople, GetPeopleResult>
    {
        private readonly StargateContext _context;
        public GetPeopleHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<GetPeopleResult> Handle(GetPeople request, CancellationToken cancellationToken)
        {
            var people = 
                await _context.People
                    .AsNoTracking()
                    .Include(person => person.AstronautDetail)
                    .Select(person => new PersonAstronaut
                    {
                        Name = person.Name,
                        PersonId = person.Id,
                        CurrentRank = person.AstronautDetail.CurrentRank,
                        CurrentDutyTitle = person.AstronautDetail.CurrentDutyTitle,
                        CareerStartDate = person.AstronautDetail.CareerStartDate,
                        CareerEndDate = person.AstronautDetail.CareerEndDate,
                    })
                    .ToListAsync(cancellationToken);
            var result = new GetPeopleResult()
            {
                People = people
            };

            return result;
        }
    }

    public class GetPeopleResult : BaseResponse
    {
        public List<PersonAstronaut> People { get; set; } = new List<PersonAstronaut> { };

    }
}
