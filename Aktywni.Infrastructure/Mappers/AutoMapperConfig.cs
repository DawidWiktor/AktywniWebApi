using Aktywni.Core.Domain;
using Aktywni.Infrastructure.DTO;
using AutoMapper;

namespace Aktywni.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
         => new MapperConfiguration(cfg =>
         {
             cfg.CreateMap<User, AccountDTO>();
         }).CreateMapper();
    }
}