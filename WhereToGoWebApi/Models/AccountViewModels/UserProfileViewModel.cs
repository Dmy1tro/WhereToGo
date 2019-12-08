using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WhereToGoWebApi.Common.Mapper;

namespace WhereToGoWebApi.Models.AccountViewModels
{
    public class UserProfileViewModel : IMapFrom<User>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserProfileViewModel>();
        }
    }
}
