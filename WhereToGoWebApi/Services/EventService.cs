﻿using AutoMapper;
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

        public async Task<EventFullViewModel> GetEvent(int eventId)
        {
            var dbEvent = await dbRepository.Events
                .AsNoTracking()
                .Include(x => x.Ratings)
                .Include("Comments.User")
                .FirstOrDefaultAsync(x => x.EventId == eventId);

            var result = mapper.Map<EventFullViewModel>(dbEvent);

            return result;
        }

        public async Task<IEnumerable<EventViewModel>> GetAllEvents() =>
            await dbRepository.Events
            .AsNoTracking()
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

            switch (filter.OrderBy)
            {
                case OrderParameter.Name:
                    result = result.OrderBy(x => x.Name);
                    break;
                case OrderParameter.NameDesc:
                    result = result.OrderByDescending(x => x.Name);
                    break;
                case OrderParameter.Date:
                    result = result.OrderBy(x => x.StartDate);
                    break;
                case OrderParameter.DateDesc:
                    result = result.OrderByDescending(x => x.StartDate);
                    break;
                case OrderParameter.Price:
                    result = result.OrderBy(x => x.Price);
                    break;
                case OrderParameter.PriceDesc:
                    result = result.OrderByDescending(x => x.Price);
                    break;
                default:
                    result = result.OrderBy(x => x.StartDate);
                    break;
            }

            return await result
                .ProjectTo<EventViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
