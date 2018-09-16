using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.DTO;
using AutoMapper;

namespace Aktywni.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserEventRepository _userEventRepository;
        private readonly IMapper _mapper;
        public EventService(IEventRepository eventRepository, IUserRepository userRepository,
                            IUserEventRepository userEventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _userEventRepository = userEventRepository;
            _mapper = mapper;
        }

        public async Task<ReturnResponse> GetEventAsync(int eventID)
        {
            var evenTemp = await _eventRepository.GetEventAsync(eventID);
            var eventDto = _mapper.Map<Events, EventDTO>(evenTemp);
            await AddAdminLoginToEvent(eventDto);
            return new ReturnResponse { Response = (eventDto == null) ? false.ToString() : true.ToString(), Info = eventDto };
        }

        public async Task<ReturnResponse> GetEventAsync(string name)
        {
            var evenTemp = await _eventRepository.GetEventAsync(name);
            var eventDto = _mapper.Map<Events, EventDTO>(evenTemp);
            await AddAdminLoginToEvent(eventDto);
            return new ReturnResponse { Response = (eventDto == null) ? false.ToString() : true.ToString(), Info = eventDto };
        }

        public async Task<ReturnResponse> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllEventsAsync();
            List<EventDTO> listEventDto = _mapper.Map<IEnumerable<Events>, List<EventDTO>>(events);
            await AddAdminLoginToEvents(listEventDto);
            return new ReturnResponse { Response = true.ToString(), Info = listEventDto };
        }

        public async Task<ReturnResponse> GetAllMyEventsAsync(int userID)
        {
            var events = await _eventRepository.GetAllMyEventsAsync(userID);
            List<EventDTO> listEventDto = _mapper.Map<IEnumerable<Events>, List<EventDTO>>(events);
            await AddAdminLoginToEvents(listEventDto);
            return new ReturnResponse { Response = true.ToString(), Info = listEventDto };
        }

        public async Task<ReturnResponse> SearchEventsAsync(string textInput)
        {
            var events = await _eventRepository.GetFromTextAsync(textInput);
            List<EventDTO> listEventDto = _mapper.Map<IEnumerable<Events>, List<EventDTO>>(events);
            await AddAdminLoginToEvents(listEventDto);
            return new ReturnResponse { Response = true.ToString(), Info = listEventDto };
        }

        public async Task<ReturnResponse> SearchEventsInDisciplineAsync(string textInput, int disciplineId)
        {
            IEnumerable<Events> events = new List<Events>();
            if(string.IsNullOrWhiteSpace(textInput))
                events = await _eventRepository.GetEventInDisciplineAsync(disciplineId);
            else
                events = await _eventRepository.GetFromTextAndDisciplineAsync(textInput, disciplineId);
            List<EventDTO> listEventDto = _mapper.Map<IEnumerable<Events>, List<EventDTO>>(events);
            await AddAdminLoginToEvents(listEventDto);
            return new ReturnResponse { Response = true.ToString(), Info = listEventDto };
        }

        public async Task<ReturnResponse> SearchEventsInDisciplineAndDistanceAsync(string textInput, int disciplineId, double distance, double latitude, double longitude)
        {
            var events = await _eventRepository.GetFromTextAndDisciplineAndDistanceAsync(textInput, disciplineId, distance, latitude, longitude);
            List<EventDTO> listEventDto = _mapper.Map<IEnumerable<Events>, List<EventDTO>>(events);
            await AddAdminLoginToEvents(listEventDto);
            return new ReturnResponse { Response = true.ToString(), Info = listEventDto };
        }

        public async Task<ReturnResponse> SearchEventsNearest(double latitude, double longitude)
        {
            var events = await _eventRepository.GetNearestEvents(latitude, longitude);
            List<EventDTO> listEventDto = _mapper.Map<IEnumerable<Events>, List<EventDTO>>(events);
            await AddAdminLoginToEvents(listEventDto);
            return new ReturnResponse { Response = true.ToString(), Info = listEventDto };
        }
        public async Task<ReturnResponse> AddEventAsync(string name, int objectID, DateTime date, int whoCreatedID, string description)
        {
            var newEvent = await _eventRepository.GetEventAsync(name);
            if (newEvent != null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Wydarzenie o takiej nazwie już istnieje." };
            }
            newEvent = new Events(name, objectID, date, whoCreatedID, whoCreatedID, description);
            await _eventRepository.AddAsync(newEvent);
            return new ReturnResponse { Response = true.ToString(), Info = "Dodano wydarzenie." };
        }

        //obecnie uzywana
        public async Task<ReturnResponse> AddEventAsync(string name, DateTime date, int whoCreatedID, int disciplineId, string description, decimal latitude, decimal longitude)
        {
            try
            {
                int objectID = 1; // brak obiektu
                var newEvent = await _eventRepository.GetEventAsync(name);
                if (newEvent != null)
                {
                    return new ReturnResponse { Response = false.ToString(), Error = "Wydarzenie o takiej nazwie już istnieje." };
                }
                newEvent = new Events(name, objectID, date, whoCreatedID, whoCreatedID, disciplineId, latitude, longitude, description);
                await _eventRepository.AddAsync(newEvent);
                await AddAdminToUserEvent(name, whoCreatedID);
                return new ReturnResponse { Response = true.ToString(), Info = "Dodano wydarzenie." };
            }
            catch (Exception ex)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błąd tworzenia wydarzenia. " + ex.Message };
            }
        }

        public async Task<ReturnResponse> AddEventAsync(string name, int objectID, DateTime date, int whoCreatedID, int disciplineId, decimal latitude, decimal longitude)
        {
            var newEvent = await _eventRepository.GetEventAsync(name);
            if (newEvent != null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Wydarzenie o takiej nazwie już istnieje." };
            }
            newEvent = new Events(name, objectID, date, whoCreatedID, whoCreatedID, disciplineId, latitude, longitude);
            await _eventRepository.AddAsync(newEvent);
            return new ReturnResponse { Response = true.ToString(), Info = "Dodano wydarzenie." };
        }

        public async Task<ReturnResponse> AddEventAsync(string name, int objectID, DateTime date, int whoCreatedID, int admin, int disciplineId, decimal latitude, decimal longitude, string description)
        {
            var newEvent = await _eventRepository.GetEventAsync(name);
            if (newEvent != null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Wydarzenie o takiej nazwie już istnieje." };
            }
            newEvent = new Events(name, objectID, date, whoCreatedID, whoCreatedID, disciplineId, latitude, longitude, description);
            await _eventRepository.AddAsync(newEvent);
            return new ReturnResponse { Response = true.ToString(), Info = "Dodano wydarzenie." };
        }

        public async Task<ReturnResponse> ChangeNameEventAsync(int eventID, string newName)
        {
            var eventDb = await _eventRepository.GetEventAsync(eventID);
            if (eventDb == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędne ID wydarzenia" };
            }
            if (String.IsNullOrWhiteSpace(newName))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Podano pustą nazwę wydarzenia." };
            }
            var tempEvent = await _eventRepository.GetEventAsync(newName);
            if (tempEvent != null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Już istnieje wydarzenie o takiej nazwie." };
            }

            eventDb.SetName(newName);
            await _eventRepository.UpdateAsync(eventDb);
            return new ReturnResponse { Response = true.ToString(), Info = "Nazwa wydarzenia została zmieniona." };
        }

        public async Task<ReturnResponse> ChangeVisibilityEventAsync(int eventID, string visibility) // W - widoczny, N - niewidoczny, U - usuń
        {
            var eventDb = await _eventRepository.GetEventAsync(eventID);
            if (eventDb == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędne ID wydarzenia" };
            }
            if (String.IsNullOrWhiteSpace(visibility))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Nie wybrano widoczności wydarzenia." };
            }

            eventDb.SetVisibility(visibility);
            await _eventRepository.UpdateAsync(eventDb);
            return new ReturnResponse { Response = true.ToString(), Info = "Widoczność została zmieniona." };
        }

        public async Task<ReturnResponse> ChangeDescription(int eventID, string description)
        {
            var eventDb = await _eventRepository.GetEventAsync(eventID);
            if (eventDb == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędne ID wydarzenia" };
            }
            if (String.IsNullOrWhiteSpace(description))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Podano pusty opis." };
            }

            eventDb.SetDescritpion(description);
            await _eventRepository.UpdateAsync(eventDb);
            return new ReturnResponse { Response = true.ToString(), Info = "Opis został zmieniony." };
        }

        public async Task<ReturnResponse> ChangeDateEventAsync(int eventID, DateTime date)
        {
            var eventDb = await _eventRepository.GetEventAsync(eventID);
            if (eventDb == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędne ID wydarzenia" };
            }

            eventDb.SetDate(date);
            await _eventRepository.UpdateAsync(eventDb);
            return new ReturnResponse { Response = true.ToString(), Info = "Data wydarzenia została zmieniona." };
        }

        public async Task<ReturnResponse> ChangeGeographicalCoordinatesEventAsync(int eventID, double latitude, double longitude)
        {
            var eventDb = await _eventRepository.GetEventAsync(eventID);
            if (eventDb == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędne ID wydarzenia" };
            }

            eventDb.SetGeographicalCoordinates(latitude, longitude);
            await _eventRepository.UpdateAsync(eventDb);
            return new ReturnResponse { Response = true.ToString(), Info = "Miejsce wydarzenia zostało zmienione." };
        }

        public async Task<ReturnResponse> RemoveEventAsync(int eventID)
        {
            var eventDb = await _eventRepository.GetEventAsync(eventID);
            if (eventDb == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Błędne ID wydarzenia" };
            }
            eventDb.SetVisibility("U");
            await _eventRepository.UpdateAsync(eventDb);
            return new ReturnResponse { Response = true.ToString(), Info = "Usunięto wydarzenie." };
        }

        #region [ PRIVATE METHOD ]

        private async Task<EventDTO> AddAdminLoginToEvent(EventDTO eventDto) // dodanie informacjo, kto jest adminem wydarzenia
        {
            eventDto.AdminLogin = await _userRepository.GetLogin(eventDto.Admin);
            return eventDto;
        }

        private async Task<List<EventDTO>> AddAdminLoginToEvents(List<EventDTO> listEvents)
        {
            foreach (var item in listEvents)
            {
                item.AdminLogin = await _userRepository.GetLogin(item.Admin);
            }
            return listEvents;
        }

        private async Task AddAdminToUserEvent(string eventName, int myId) // dodanie admina do uczestników wydarzenia
        {
            int eventId = await _eventRepository.GetIdEvent(eventName);
            UsersEvents newUserEvent = new UsersEvents(eventId, myId, true);
            await _userEventRepository.AddAsync(newUserEvent);
        }

        #endregion [ PRIVATE METHOD ]

    }
}