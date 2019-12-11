using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WhereToGoWebApi.Common.Mapper;

namespace WhereToGoWebApi.Models.AccountViewModels
{
    public class OrganizerProfileViewModel : IMapFrom<Organizer>
    {
        [Required]
        public string InstType { get; set; }

        [Required]
        public string PlaceName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required, MaxLength(50)]
        public string TelNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Organizer, OrganizerProfileViewModel>();
            profile.CreateMap<OrganizerProfileViewModel, Organizer>();
        }
    }
}
