using chatbot_app.Application.Messages.Dtos;
using MediatR;

namespace chatbot_app.Application.Messages.Queries;

public record GetAllUserMessagesCommand(int ConversationId): IRequest<List<MessageDto>>;