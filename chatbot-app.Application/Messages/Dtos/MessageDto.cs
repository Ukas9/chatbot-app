using chatbot_app.Domain.Models;

namespace chatbot_app.Application.Messages.Dtos;

public record MessageDto(int Id, string Content, MessageType Type, DateTime CreatedAt, int? LikeDislike = null);