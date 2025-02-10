using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Application.Mapper;
using Backend.Domains.Logging.Application.Mediator.Queries;
using FluentResults.Extensions.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Domains.Logging.Application.Backend.REST;

[ApiController]
[Route("api/logging")]
public class LoggingController(IMediator mediator) : ControllerBase
{
    [HttpGet("entity/{id}")]
    public async Task<IActionResult> ByEntityId(Id id)
    {
        var result = await mediator.Send(new GetLogEntriesByEntityId(id)).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return result.ToActionResult();
        }

        var mapper = new LoggingMapper();
        var response = mapper.ToDto(result.Value).OrderByDescending(it => it.LoggedAt);

        return Ok(response);
    }
}
