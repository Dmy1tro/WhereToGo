using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Common.Mapper
{
    public interface IMapFrom<T> where T : class
    {
        void Mapping(Profile profile);
    }
}
