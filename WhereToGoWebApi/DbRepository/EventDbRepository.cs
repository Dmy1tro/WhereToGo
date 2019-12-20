using System;
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

        public IQueryable<User> Users => context.Users;

        public async Task<bool> CreateAndSaveEntityAsync<T>(T entity) where T : class
        {
            await context.Set<T>().AddAsync(entity);

            return await this.SaveChangesAsync();
        }

        public async Task<bool> UpdateAndSaveEntityAsync<T>(T entity) where T : class
        {
            context.Set<T>().Update(entity);

            return await this.SaveChangesAsync();
        }

        public async Task<bool> RemoveAndSaveEntityAsync<T>(T entity) where T : class
        {
            context.Set<T>().Remove(entity);

            return await this.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
