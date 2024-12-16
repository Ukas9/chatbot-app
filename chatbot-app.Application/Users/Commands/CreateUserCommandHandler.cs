using chatbot_app.Domain.Models;
using MediatR;
using chatbot_app.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace chatbot_app.Application.Users.Commands;

public class CreateUserCommandHandler(ChatBotDbContext context) : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await context.Users
            .AnyAsync(u => u.Username == request.Username, cancellationToken);

        if (existingUser)
        {
            throw new InvalidOperationException("User already exists.");
        }

        var user = new User { Username = request.Username };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}