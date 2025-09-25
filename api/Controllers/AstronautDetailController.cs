using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Stargate.API.Business.Commands;
using Stargate.API.Business.Queries;

namespace Stargate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AstronautDetailController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public AstronautDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAstronautDetailByName(string name)
        {
            try
            {
                var result = await _mediator.Send(new GetAstronautDetailByName()
                {
                    Name = name
                });

                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }            
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAstronautDetail([FromBody] CreateAstronautDetail request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                return this.GetResponse(new BaseResponse()
                {
                    Message = ex.Message,
                    Success = false,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                });
            }
        }
    }

}