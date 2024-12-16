using MediatR;

namespace chatbot_app.Application.Users.Commands;

public record CreateUserCommand(string Username):IRequest<int>;