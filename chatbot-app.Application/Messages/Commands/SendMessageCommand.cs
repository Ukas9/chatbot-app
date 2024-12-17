using chatbot_app.Application.Messages.Dtos;
using MediatR;

namespace chatbot_app.Application.Messages.Commands;

public record SendMessageCommand(int ConversationId, int UserId, string Message) : IRequest<MessageDto>;