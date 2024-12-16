namespace chatbot_app.Domain.Models;

public class Message
{
    public int Id { get; set; }
    
    public int ConversationId { get; set; }
    
    public Conversation Conversation { get; set; }
    
    public int? UserId { get; set; }
    
    public User User { get; set; }
    
    public DateTimeOffset  CreatedAt { get; set; }
    
    public DateTimeOffset  UpdatedAt { get; set; }
    public MessageType MessageFrom { get; set; } 
    
    public int? LikeDislike { get; set; }
    
    public string Content { get; set; }
}