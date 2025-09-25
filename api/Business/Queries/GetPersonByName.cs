using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stargate.API.Business.Data;
using Stargate.API.Business.Dtos;
using Stargate.API.Controllers;

namespace Stargate.API.Business.Queries
{
    public class GetPersonByName : IRequest<GetPersonByNameResult>
    {
        public required string Name { get; set; } = string.Empty;
    }

    public class GetPersonByNameHandler : IRequestHandler<GetPersonByName, GetPersonByNameResult>
    {
        private readonly StargateContext _context;
        public GetPersonByNameHandler(StargateContext context)
        {
            _context = context;
        }

        public async Task<GetPersonByNameResult> Handle(GetPersonByName request, CancellationToken cancellationToken)
        {

            var result = await _context.People.Where(person => person.Name == request.Name)
                .Include(person => person.AstronautDetail)
                .Select(person => new GetPersonByNameResult
            {
                Person = new PersonAstronaut
                {
                    Name = person.Name,
                    PersonId = person.Id,
                    CareerEndDate = person.AstronautDetail.CareerEndDate,
                    CareerStartDate = person.AstronautDetail.CareerStartDate,
                    CurrentRank = person.AstronautDetail.CurrentRank,
                    CurrentDutyTitle = person.AstronautDetail.CurrentDutyTitle
                }
            }).FirstOrDefaultAsync(cancellationToken);

            return result ?? new GetPersonByNameResult()
            {
                ResponseCode = (int) HttpStatusCode.NotFound,
                Message = "Not Found",
                Success = false,
                Person = null
            };
        }
    }

    public class GetPersonByNameResult : BaseResponse
    {
        public PersonAstronaut? Person { get; set; }
    }
}
