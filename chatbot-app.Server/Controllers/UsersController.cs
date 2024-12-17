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
    public async Task<int> CreateUser([FromBody] CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet("{username}")]
    public async Task<UserDto> GetUserByUsername(string username)
    {
        return await _mediator.Send(new GetUserByUsernameQuery(username));
    }
        
    [HttpGet]
    public async Task<List<UserDto>> GetAllUsers()
    {
        return await _mediator.Send(new GetAllUsersQuery());
    }
}