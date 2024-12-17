using MediatR;

namespace chatbot_app.Application.Conversations.Commands;

public record CreateConversationCommand(int UserId) : IRequest<int>;