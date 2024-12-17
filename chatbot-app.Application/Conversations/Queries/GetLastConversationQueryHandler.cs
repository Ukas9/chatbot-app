using chatbot_app.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace chatbot_app.Application.Conversations.Queries;

public class GetLastConversationQueryHandler(ChatBotDbContext context) : IRequestHandler<GetLastConversationQuery, int?>
{
    public async Task<int?> Handle(GetLastConversationQuery request, CancellationToken cancellationToken)
    {
        return await context.Conversations
            .Where(c => c.UserId == request.UserId)
            .OrderByDescending(c => c.UpdatedAt)
            .Select(c => c.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}