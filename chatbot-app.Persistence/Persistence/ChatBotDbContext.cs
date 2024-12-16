using chatbot_app.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace chatbot_app.Persistence.Persistence;

public class ChatBotDbContext() : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Conversation> Conversations { get; set; }
    
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Conversations)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<User>()
            .HasIndex(c => c.Username)
            .IsUnique();

        modelBuilder.Entity<Conversation>()
            .HasMany(c => c.Messages)
            .WithOne(m => m.Conversation)
            .HasForeignKey(m => m.ConversationId);

        modelBuilder.Entity<Message>()
            .Property(m => m.MessageFrom)
            .IsRequired();
    }
}