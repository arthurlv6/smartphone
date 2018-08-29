using APIModel.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Web.API.Infrastructure
{
    public class APIProfile:Profile
    {
        public APIProfile()
        {
            var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (var assemblyName in assemblyNames)
            {
                var assembly = Assembly.Load(assemblyName);
                var types = assembly.GetExportedTypes();

                var maps = (from t in types
                            from i in t.GetInterfaces()
                            where i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                            !t.IsAbstract &&
                            !t.IsInterface
                            select new
                            {
                                Source = i.GetGenericArguments()[0],
                                Destination = t
                            }).ToArray();

                foreach (var map in maps)
                {
                    CreateMap(map.Source, map.Destination).ReverseMap();
                }
            }
            CreateMap<DataModel.Product, APIModel.Product>().
                ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<APIModel.Product, DataModel.Product>().
                ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<DataModel.Contract, APIModel.Contract>().
                ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ContractProduct.Sum(d=>d.Price*d.Quantity))).
                ForMember(dest => dest.Profit, opt => opt.MapFrom(src => src.ContractProduct.Sum(d => d.Price * d.Quantity*0.4m))).
                ForMember(dest => dest.SalesMan, opt => opt.MapFrom(src => src.Sales.Name)).
                ForMember(dest => dest.Channel, opt => opt.MapFrom(src => src.Channel.Name));
        }
    }
}
