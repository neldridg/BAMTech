using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stargate.API.Business.Data;
using Stargate.API.Business.Dtos;
using Stargate.API.Controllers;

namespace Stargate.API.Business.Queries;

public class GetAstronautDetailByName : IRequest<GetAstronautDetailByNameResult>
{ 
    public string Name { get; set; } = string.Empty;
}

 public class GetAstronautDetailByNameHandler : IRequestHandler<GetAstronautDetailByName, GetAstronautDetailByNameResult>
    {
        private readonly StargateContext _context;

        public GetAstronautDetailByNameHandler(StargateContext context)
        {
            _context = context;
        }

        public async Task<GetAstronautDetailByNameResult> Handle(GetAstronautDetailByName request, CancellationToken cancellationToken)
        {
            var result =  
                _context.People.Where(person => person.Name == request.Name)
                    .Include(person => person.AstronautDuties)
                    .Include(person => person.AstronautDuties)
                    .Select(person => new GetAstronautDetailByNameResult()
                    {
                        Person = new PersonAstronaut
                        {
                            Name = person.Name,
                            PersonId = person.Id,
                            CareerStartDate = person.AstronautDetail.CareerStartDate,
                            CareerEndDate = person.AstronautDetail.CareerEndDate,
                            CurrentDutyTitle = person.AstronautDetail.CurrentDutyTitle,
                            CurrentRank = person.AstronautDetail.CurrentRank,
                        }
                    }).FirstOrDefaultAsync(cancellationToken);

            return await result ?? new GetAstronautDetailByNameResult()
            {
                Message = "Not Found",
                Person = null,
                ResponseCode = (int) HttpStatusCode.NotFound,
                Success = false
            };

        }
    }

public class GetAstronautDetailByNameResult : BaseResponse
{
    public PersonAstronaut? Person { get; set; }
}