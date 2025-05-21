using klepetalko.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace klepetalko.Data
{
    public class klepet : IdentityDbContext<User>
    {
        public klepet(DbContextOptions<klepet> options) : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Chat>().ToTable("Chats");
            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Friendship>().ToTable("Friendships");
            modelBuilder.Entity<Setting>().ToTable("Settings");

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Users)
                .WithMany(u => u.Chats);

            modelBuilder.Entity<Setting>()
                .HasOne(s => s.Userski)
                .WithOne(u => u.Setting)
                .HasForeignKey<Setting>(s => s.UserId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasMany(m => m.ReadBy)
                .WithMany(u => u.ReadMessages)
                .UsingEntity(j => j.ToTable("MessageReads"));

        }
        
    }
}