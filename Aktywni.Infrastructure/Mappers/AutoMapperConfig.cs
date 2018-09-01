using Aktywni.Core.Model;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.DTO.MessageUser;
using AutoMapper;

namespace Aktywni.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
         => new MapperConfiguration(cfg =>
         {
             cfg.CreateMap<Users, AccountDTO>();
             cfg.CreateMap<Users, FriendDTO>();
             cfg.CreateMap<Friends, FriendDTO>();
             cfg.CreateMap<Objects, ObjectDTO>(); 
             cfg.CreateMap<Events, EventDTO>();
             cfg.CreateMap<UsersEvents, UserEventDTO>();
             cfg.CreateMap<MessageUser, MessageUserDTO>();
         }).CreateMapper();
    }
}