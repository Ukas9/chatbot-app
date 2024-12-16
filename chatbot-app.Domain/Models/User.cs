namespace chatbot_app.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public ICollection<Conversation> Conversations { get; set; }
}