using chatbot_app.Application.Messages.Dtos;
using chatbot_app.Domain.Models;
using chatbot_app.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace chatbot_app.Application.Messages.Commands;

public class SendMessageCommandHandler(ChatBotDbContext context)
    : IRequestHandler<SendMessageCommand, MessageDto>
{
    public async Task<MessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        var conversation =
            await context.Conversations.FirstOrDefaultAsync(x => x.Id == request.ConversationId, cancellationToken);

        if (conversation == null)
        {
            throw new InvalidOperationException("Conversation not found.");
        }

        var message = new Message
        {
            ConversationId = request.ConversationId,
            Conversation = conversation,
            UserId = request.UserId,
            User = user,
            Content = request.Message,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            MessageFrom = MessageType.User
        };

        context.Messages.Add(message);

        var botResponse = GenerateBotResponse();

        var botMessage = new Message
        {
            ConversationId = request.ConversationId,
            Conversation = conversation,
            Content = botResponse,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            MessageFrom = MessageType.Chat
        };
        
        context.Messages.Add(botMessage);

        
        await context.SaveChangesAsync(cancellationToken);

        var botResponseDto = new MessageDto(botMessage.Id, botMessage.Content, botMessage.MessageFrom, botMessage.CreatedAt);
        
        return botResponseDto;
    }

    private string GenerateBotResponse()
    {
        var random = new Random();
        var responses = new[]
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ut aliquet nulla. Vestibulum non mollis ante. Fusce porttitor ante vel sapien finibus, a tristique ex scelerisque. Fusce fermentum blandit sagittis. Mauris in laoreet eros, at euismod nulla. Nulla eget tempus ante. Phasellus non nunc dapibus, luctus enim viverra, sodales nisl. Phasellus nec nunc vel neque luctus cursus. Pellentesque erat velit, congue et velit non, eleifend tincidunt risus. Fusce gravida felis eget sollicitudin varius. Curabitur porttitor lacus non tincidunt semper. Duis at enim imperdiet orci porta varius.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ut aliquet nulla. Vestibulum non mollis ante. Fusce porttitor ante vel sapien finibus, a tristique ex scelerisque. Fusce fermentum blandit sagittis. Mauris in laoreet eros, at euismod nulla.",
            "Etiam sed dapibus lectus. Nam commodo nisi arcu, vitae iaculis odio volutpat blandit. Donec et ipsum tristique, laoreet sem vitae, tempor elit. Nunc in blandit magna, eget suscipit libero. Phasellus sit amet laoreet nulla. Cras et ipsum dapibus, volutpat velit ut, faucibus nisi. Fusce mollis lacus vel quam ullamcorper convallis. Fusce maximus vitae mi ac accumsan. Donec eu purus eget lorem gravida ullamcorper. Nunc pellentesque est vel velit ultricies, non varius lectus suscipit. Nulla sed sodales velit. Vivamus suscipit mollis nisl. Proin eleifend a nisi mollis luctus. Pellentesque orci risus, malesuada tincidunt vestibulum id, suscipit ac velit. Phasellus a massa vel nisi dapibus placerat. ",
            "Nulla sed sodales velit. Vivamus suscipit mollis nisl. Proin eleifend a nisi mollis luctus.",
            "Cras id vulputate tellus, a luctus ante. Nam sed metus posuere ligula malesuada consequat. ",
            "Aliquam sed dapibus sem. Mauris mollis lectus interdum turpis vehicula, eu congue felis feugiat. Fusce vel libero condimentum, maximus massa ac, bibendum sapien. Nunc nisl magna, scelerisque in mi et, maximus tristique sapien. Etiam placerat, magna id posuere bibendum, tortor ex placerat eros, ut ultrices tortor neque quis eros. Sed non rutrum libero. Cras a odio quam. Donec sed nunc vitae velit malesuada tempus quis nec lorem. Nunc non sem ultrices, posuere felis eget, sollicitudin libero. Nullam est eros, consequat eget risus id, convallis tincidunt libero. Nullam eget lacus sem. Aliquam posuere metus sem, ac scelerisque nisi consectetur suscipit. Aenean sed imperdiet quam. Integer vitae fringilla lectus.",
            "Aliquam sed dapibus sem. Mauris mollis lectus interdum turpis vehicula, eu congue felis feugiat. Fusce vel libero condimentum, maximus massa ac, bibendum sapien.",
            "Nunc nisl magna, scelerisque in mi et, maximus tristique sapien. Etiam placerat, magna id posuere bibendum, tortor ex placerat eros, ut ultrices tortor neque quis eros. Sed non rutrum libero. Cras a odio quam. Donec sed nunc vitae velit malesuada tempus quis nec lorem.",
            " Nunc non sem ultrices, posuere felis eget, sollicitudin libero. Nullam est eros, consequat eget risus id, convallis tincidunt libero. Nullam eget lacus sem. Aliquam posuere metus sem, ac scelerisque nisi consectetur suscipit. Aenean sed imperdiet quam. Integer vitae fringilla lectus."
        };
        
        return responses[random.Next(responses.Length)];
    }
}