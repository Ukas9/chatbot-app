using MediatR;

namespace chatbot_app.Application.Conversations.Queries;

public record GetLastConversationQuery(int UserId) : IRequest<int?>;