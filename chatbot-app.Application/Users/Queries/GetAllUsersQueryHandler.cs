using chatbot_app.Application.Users.Dtos;
using chatbot_app.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace chatbot_app.Application.Users.Queries;

public class GetAllUsersQueryHandler(ChatBotDbContext context) : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await context.Users
            .Select(u => new UserDto(u.Id, u.Username))
            .ToListAsync(cancellationToken);
    }
}