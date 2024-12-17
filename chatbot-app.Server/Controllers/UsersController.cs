using chatbot_app.Application.Users.Commands;
using chatbot_app.Application.Users.Dtos;
using chatbot_app.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace chatbot_app.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<int> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpGet("{username}")]
    public async Task<UserDto> GetUserByUsername(string username, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetUserByUsernameQuery(username), cancellationToken);
    }
        
    [HttpGet]
    public async Task<List<UserDto>> GetAllUsers( CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
    }
}