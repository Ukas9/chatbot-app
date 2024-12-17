using chatbot_app.Domain.Models;
using chatbot_app.Persistence.Context;
using MediatR;

namespace chatbot_app.Application.Conversations.Commands;

public class CreateConversationCommandHandler(ChatBotDbContext context)
    : IRequestHandler<CreateConversationCommand, int>
{
    public async Task<int> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var newConversation = new Conversation
        {
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        context.Conversations.Add(newConversation);
        await context.SaveChangesAsync(cancellationToken);

        return newConversation.Id;
    }
}