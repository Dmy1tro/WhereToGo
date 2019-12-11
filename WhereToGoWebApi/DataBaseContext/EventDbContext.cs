using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhereToGoWebApi.Models;

namespace WhereToGoWebApi.DataBaseContext
{
    public class EventDbContext : IdentityDbContext<User>
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(EventDbContext).Assembly);
        }

        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<EventMeeting> EventMeetings { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
    }
}
