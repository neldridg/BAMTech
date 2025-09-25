using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stargate.API.Business.Data;
using Stargate.API.Business.Dtos;
using Stargate.API.Controllers;

namespace Stargate.API.Business.Queries
{
    public class GetAstronautDutiesByName : IRequest<GetAstronautDutiesByNameResult>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class GetAstronautDutiesByNameHandler : IRequestHandler<GetAstronautDutiesByName, GetAstronautDutiesByNameResult>
    {
        private readonly StargateContext _context;

        public GetAstronautDutiesByNameHandler(StargateContext context)
        {
            _context = context;
        }

        public async Task<GetAstronautDutiesByNameResult> Handle(GetAstronautDutiesByName request, CancellationToken cancellationToken)
        {
            var result =  
                _context.People.Where(person => person.Name == request.Name)
                    .Include(person => person.AstronautDuties)
                    .Include(person => person.AstronautDuties)
                    .Select(person => new GetAstronautDutiesByNameResult()
                    {
                        Person = new PersonAstronaut()
                        {
                            Name = person.Name,
                            PersonId = person.Id,
                            CareerStartDate = person.AstronautDetail.CareerStartDate,
                            CareerEndDate = person.AstronautDetail.CareerEndDate,
                            CurrentDutyTitle = person.AstronautDetail.CurrentDutyTitle,
                            CurrentRank = person.AstronautDetail.CurrentRank,
                        },
                        AstronautDuties = person.AstronautDuties.OrderByDescending(duty => duty.DutyStartDate).ToList()
                    }).FirstOrDefaultAsync(cancellationToken);

            return await result ?? new GetAstronautDutiesByNameResult()
            {
                Message = "Not Found",
                AstronautDuties = Enumerable.Empty<AstronautDuty>().ToList(),
                Person = null,
                ResponseCode = (int) HttpStatusCode.NotFound,
                Success = false
            };

        }
    }

    public class GetAstronautDutiesByNameResult : BaseResponse
    {
        public PersonAstronaut? Person { get; set; }
        public List<AstronautDuty> AstronautDuties { get; set; } = new List<AstronautDuty>();
    }
}
