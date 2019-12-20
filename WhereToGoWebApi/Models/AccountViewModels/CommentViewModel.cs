using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Common.Mapper;

namespace WhereToGoWebApi.Models.AccountViewModels
{
    public class CommentViewModel : IMapFrom<Comment>
    {
        public int CommentId { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required, MaxLength(300)]
        public string BodyText { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentViewModel>()
                .IncludeAllDerived()
                .ReverseMap();

            profile.CreateMap<Comment, CommentFullViewModel>()
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.User.UserName));
        }
    }
}
