using AutoMapper;

namespace Aktywni.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
         => new MapperConfiguration(cfg =>
         {

         }).CreateMapper();
    }
}