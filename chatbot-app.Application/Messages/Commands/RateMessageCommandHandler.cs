using chatbot_app.Persistence.Context;
using MediatR;

namespace chatbot_app.Application.Messages.Commands;

public class RateMessageCommandHandler(ChatBotDbContext context) : IRequestHandler<RateMessageCommand>
{
    public async Task Handle(RateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await context.Messages.FindAsync(request.MessageId);

        if (message == null)
        {
            throw new InvalidOperationException("Message not found.");
        }
      
        message.LikeDislike = request.LikeDislike;
        context.Messages.Update(message);
        await context.SaveChangesAsync(cancellationToken);
    }
}