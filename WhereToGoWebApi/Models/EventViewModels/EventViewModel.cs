using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using WhereToGoWebApi.Common.Mapper;

namespace WhereToGoWebApi.Models.EventViewModels
{
    public class EventViewModel : IMapFrom<Event>
    {
        public int EventId { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        [Required, MaxLength(500)]
        public string Address { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public double? AvgRate { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventViewModel>()
                .ForMember(d => d.AvgRate, m => m.MapFrom(s => s.Ratings.Average(r => (int?)r.Rate)))
                .IncludeAllDerived()
                .ReverseMap();

            profile.CreateMap<Event, EventFullViewModel>();
        }
    }
}
