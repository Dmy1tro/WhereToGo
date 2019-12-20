using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models.EventViewModels;
using WhereToGoWebApi.Services.ServiceResults;

namespace WhereToGoWebApi.Services.Interfaces
{
    public interface IEventService
    {
        Task<BaseResult> CreateEvent(EventViewModel model, string userId);
        Task<EventFullViewModel> GetEvent(int eventId);
        Task<IEnumerable<EventViewModel>> GetAllEvents();
        Task<IEnumerable<EventViewModel>> GetEventsByFilters(EventViewModelFilter filter);
    }
}
