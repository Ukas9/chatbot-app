using chatbot_app.Application.Conversations.Commands;
using chatbot_app.Application.Conversations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace chatbot_app.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConversationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConversationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<int> CreateConversation([FromBody] CreateConversationCommand command,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpGet("{userId}")]
    public async Task<int?> GetLastConversationForUser(int userId, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetLastConversationQuery(userId), cancellationToken);
    }
}