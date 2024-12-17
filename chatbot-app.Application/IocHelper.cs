using chatbot_app.Application.Conversations.Commands;
using chatbot_app.Application.Conversations.Queries;
using chatbot_app.Application.Messages.Commands;
using chatbot_app.Application.Messages.Dtos;
using chatbot_app.Application.Users.Commands;
using chatbot_app.Application.Users.Dtos;
using chatbot_app.Application.Users.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace chatbot_app.Application;

public static class IocHelper
{
    public static void RegisterAppServices(IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateUserCommand, int>, CreateUserCommandHandler>();
        services.AddScoped<IRequestHandler<GetUserByUsernameQuery, UserDto>, GetUserByUsernameQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, List<UserDto>>, GetAllUsersQueryHandler>();
        services.AddScoped<IRequestHandler<CreateConversationCommand, int>, CreateConversationCommandHandler>();
        services.AddScoped<IRequestHandler<GetLastConversationQuery, int?>, GetLastConversationQueryHandler>();
        services.AddScoped<IRequestHandler<SendMessageCommand, MessageDto>, SendMessageCommandHandler>();
        services.AddScoped<IRequestHandler<RateMessageCommand>, RateMessageCommandHandler>();
    }
}