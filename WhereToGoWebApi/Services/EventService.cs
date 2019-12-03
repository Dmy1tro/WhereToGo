using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.IDbRepository;
using WhereToGoWebApi.Models;
using WhereToGoWebApi.Models.EventViewModels;
using WhereToGoWebApi.Services.Interfaces;
using WhereToGoWebApi.Services.ServiceResults;

namespace WhereToGoWebApi.Services
{
    public class EventService : IEventService
    {
        private readonly IEventDbRepository dbRepository;

        public EventService(IEventDbRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }

        public async Task<BaseResult> CreateEvent(EventViewModel model, string userName)
        {
            var user = await dbRepository.Users.Include(x => x.Organizer).FirstOrDefaultAsync(x => x.UserName.Equals(userName));
            var organaizerId = user.Organizer.OrganizerId;

            var _event = new Event 
            {
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Price = model.Price,
                Quantity = model.Quantity,
                OrganizerId = organaizerId
            };

            var result = await dbRepository.CreateAndSaveEventAsync(_event);

            if (!result)
                return new BaseResult("Failed save to DataBase");

            return new BaseResult();
        }

        public async Task<IEnumerable<EventViewModel>> GetAllEvents() =>
            await dbRepository.Events
            .Select(x => new EventViewModel 
            {
                EventId = x.EventId,
                Description = x.Description,
                Address = x.Address,
                StartDate = x.StartDate,
                StartTime = x.StartTime,
                EndDate = x.EndDate,
                EndTime = x.EndTime,
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity
            })
            .ToListAsync();
    }
}
