using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Stargate.API.Business.Data;
using Stargate.API.Controllers;

namespace Stargate.API.Business.Commands
{
    public class CreateAstronautDetail : IRequest<CreateAstronautDetailResult>
    {
        public required string Name { get; set; }

        public string CurrentRank { get; set; }

        public string CurrentDutyTitle { get; set; }

        public DateTime? CareerStartDate { get; set; }
        public DateTime? CareerEndDate { get; set; }
    }

    public class CreateAstronautDetailPreProcessor : IRequestPreProcessor<CreateAstronautDetail>
    {
        private readonly StargateContext _context;

        public CreateAstronautDetailPreProcessor(StargateContext context)
        {
            _context = context;
        }

        public Task Process(CreateAstronautDetail request, CancellationToken cancellationToken)
        {
            var person = _context.People
                .AsNoTracking()
                .Include(person => person.AstronautDetail)
                .FirstOrDefault(person => person.Name == request.Name);

            if (person is null) throw new BadHttpRequestException("Bad Request");
            return Task.CompletedTask;
        }
    }

    public class CreateAstronautDetailHandler : IRequestHandler<CreateAstronautDetail, CreateAstronautDetailResult>
    {
        private readonly StargateContext _context;

        public CreateAstronautDetailHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<CreateAstronautDetailResult> Handle(CreateAstronautDetail request, CancellationToken cancellationToken)
        {

            var person = await _context.People
                .Where(person => person.Name == request.Name)
                .Include(person => person.AstronautDetail)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (person is null) throw new BadHttpRequestException("Bad Request");

            var newAstronautDetail = new AstronautDetail
            {
                PersonId = person.Id,
                CareerStartDate = request.CareerStartDate,
                CurrentRank = request.CurrentRank,
                CurrentDutyTitle = request.CurrentDutyTitle,
                CareerEndDate = request.CareerEndDate
            };

            if (person.AstronautDetail is not null)
            {
                newAstronautDetail.Id = person.AstronautDetail.Id;
                _context.AstronautDetails.Update(newAstronautDetail);
            }
            else
            {
                await _context.AstronautDetails.AddAsync(newAstronautDetail, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new CreateAstronautDetailResult()
            {
                PersonId = person.Id
            };
        }
    }

    public class CreateAstronautDetailResult : BaseResponse
    {
        public int? PersonId { get; set; }
    }
}
