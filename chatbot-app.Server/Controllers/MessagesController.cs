using chatbot_app.Application.Messages.Commands;
using chatbot_app.Application.Messages.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace chatbot_app.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator; 
        }

        [HttpPost]
        public async Task<MessageDto> SendMessage([FromBody] SendMessageCommand message, CancellationToken cancellationToken)
        {
            return await _mediator.Send(message, cancellationToken);
        }
    }
}
