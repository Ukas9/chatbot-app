using MediatR;

namespace chatbot_app.Application.Messages.Commands;

public record RateMessageCommand(int MessageId, int LikeDislike) : IRequest;