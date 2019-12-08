using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WhereToGoWebApi.Common.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappings(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappings(Assembly assembly)
        {
            var types = GetListOfMappingTypes(assembly);

            types.ForEach(type => 
            {
                var instance = Activator.CreateInstance(type);
                type.GetMethod("Mapping")?.Invoke(instance, new object[] { this });
            });
        }

        private List<Type> GetListOfMappingTypes(Assembly assembly) =>
            assembly.GetExportedTypes()
            .Where(x => x.GetInterfaces().Any(i => 
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();
    }
}
