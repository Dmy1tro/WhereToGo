using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper mapper;

        public EventService(IEventDbRepository dbRepository, IMapper mapper)
        {
            this.dbRepository = dbRepository;
            this.mapper = mapper;
        }

        public async Task<BaseResult> CreateEvent(EventViewModel model, string organaizerId)
        {
            if (!dbRepository.Organizers.Any(x => x.OrganizerId == organaizerId))
                return new ErrorResult("Organaizer not found");

            var _event = mapper.Map<Event>(model);
            _event.OrganizerId = organaizerId;

            var result = await dbRepository.CreateAndSaveEntityAsync(_event);

            if (!result)
                return new ErrorResult("Failed to save in DataBase");

            return new OkResult();
        }

        public async Task<IEnumerable<EventViewModel>> GetAllEvents() =>
            await dbRepository.Events
            .ProjectTo<EventViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        public async Task<IEnumerable<EventViewModel>> GetEventsByFilters(EventViewModelFilter filter)
        {
            var result = dbRepository.Events;

            if (!string.IsNullOrEmpty(filter.Name))
                result = result.Where(x => x.Name.Contains(filter.Name, System.StringComparison.InvariantCultureIgnoreCase));

            if (filter.MinDate != null)
                result = result.Where(x => x.StartDate >= filter.MinDate);

            if (filter.MaxDate != null)
                result = result.Where(x => x.StartDate <= filter.MaxDate);

            if (filter.MinPrice != null)
                result = result.Where(x => x.Price >= filter.MinPrice);

            if (filter.MaxPrice != null)
                result = result.Where(x => x.Price <= filter.MaxPrice);

            return await result.ProjectTo<EventViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
