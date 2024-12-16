using chatbot_app.Application.Users.Dtos;
using chatbot_app.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace chatbot_app.Application.Users.Queries;

public class GetUserByUsernameQueryHandler(ChatBotDbContext context) : IRequestHandler<GetUserByUsernameQuery, UserDto>
{

    public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Where(u => u.Username == request.Username)
            .Select(u => new UserDto(u.Id, u.Username))
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        return user;
    }
}