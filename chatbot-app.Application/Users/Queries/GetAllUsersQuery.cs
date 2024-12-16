using chatbot_app.Application.Users.Dtos;
using MediatR;

namespace chatbot_app.Application.Users.Queries;

public record GetAllUsersQuery() : IRequest<List<UserDto>>;