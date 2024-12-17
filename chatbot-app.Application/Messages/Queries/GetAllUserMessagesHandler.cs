using chatbot_app.Application.Messages.Dtos;
using chatbot_app.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace chatbot_app.Application.Messages.Queries;

public class GetAllUserMessagesHandler(ChatBotDbContext context) : IRequestHandler<GetAllUserMessagesCommand, List<MessageDto>>
{
    public async Task<List<MessageDto>> Handle(GetAllUserMessagesCommand request, CancellationToken cancellationToken)
    {
        var conversation = await context.Conversations
            .FirstOrDefaultAsync(x => x.Id == request.ConversationId, cancellationToken);

        if (conversation == null)
        {
            throw new InvalidOperationException("Conversation not found.");
        }
        
        var messages =await  context.Messages
            .Where(x => x.ConversationId == request.ConversationId)
            .Select(x => new MessageDto(x.Id, x.Content, x.MessageFrom, x.CreatedAt, x.LikeDislike))
            .ToListAsync(cancellationToken);

        return messages;
    }
}