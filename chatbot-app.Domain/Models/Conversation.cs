namespace chatbot_app.Domain.Models;

public class Conversation
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public User User { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<Message> Messages { get; set; }
}