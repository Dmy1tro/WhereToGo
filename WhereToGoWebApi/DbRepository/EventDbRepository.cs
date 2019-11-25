using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.DataBaseContext;
using WhereToGoWebApi.IDbRepository;
using WhereToGoWebApi.Models;

namespace WhereToGoWebApi.DbRepository
{
    public class EventDbRepository : IEventDbRepository
    {
        private readonly EventDbContext context;
        public EventDbRepository(EventDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Event> Events => context.Events;

        public IQueryable<Comment> Comments => context.Comments;

        public IQueryable<Meeting> Meetings => context.Meetings;

        public IQueryable<EventMeeting> EventMeetings => context.EventMeetings;

        public IQueryable<Organizer> Organizers => context.Organizers;

        public IQueryable<Rating> Ratings => context.Ratings;

        public IQueryable<UserEvent> UserEvents => context.UserEvents;

        public async Task<bool> CreateAndSaveOrganaizerAsync(Organizer organizer)
        {
            await context.Organizers.AddAsync(organizer);
            return await this.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync() =>
            await context.SaveChangesAsync() > 0;
    }
}
