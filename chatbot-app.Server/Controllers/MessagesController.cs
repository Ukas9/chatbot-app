using chatbot_app.Application.Messages.Commands;
using chatbot_app.Application.Messages.Dtos;
using chatbot_app.Application.Messages.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace chatbot_app.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<MessageDto> SendMessage([FromBody] SendMessageCommand message,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(message, cancellationToken);
        }

        [HttpPost("rateMessage")]
        public async Task RateMessage([FromBody] RateMessageCommand message,
            CancellationToken cancellationToken)
        {
            await mediator.Send(message, cancellationToken);
        }

        [HttpGet("{conversationId}")]
        public async Task<List<MessageDto>> GetAllMessages(int conversationId, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetAllUserMessagesCommand(conversationId), cancellationToken);
        }
    }
}