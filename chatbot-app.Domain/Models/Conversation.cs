namespace chatbot_app.Domain.Models;

public class Conversation
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public User User { get; set; }
    
    public DateTimeOffset  CreatedAt { get; set; }
    
    public DateTimeOffset  UpdatedAt { get; set; }
    
    public ICollection<Message> Messages { get; set; }
}