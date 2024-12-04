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
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Chat>().ToTable("Chats");
            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Friendship>().ToTable("Friendships");
            modelBuilder.Entity<Participant>().ToTable("Participants");
        }
        
    }
}