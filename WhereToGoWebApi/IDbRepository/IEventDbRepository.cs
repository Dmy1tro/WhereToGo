using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models;

namespace WhereToGoWebApi.IDbRepository
{
    public interface IEventDbRepository
    {
        IQueryable<Event> Events { get; }
        IQueryable<Comment> Comments { get; }
        IQueryable<Meeting> Meetings { get; }
        IQueryable<EventMeeting> EventMeetings { get; }
        IQueryable<Organizer> Organizers { get; }
        IQueryable<Rating> Ratings { get; }
        IQueryable<UserEvent> UserEvents { get; }
        IQueryable<User> Users { get; }

        Task<bool> CreateAndSaveEntityAsync<T>(T entity) where T : class;
        Task<bool> UpdateAndSaveEntityAsync<T>(T entity) where T : class;
        Task<bool> RemoveAndSaveEntityAsync<T>(T entity) where T : class;
    }
}
