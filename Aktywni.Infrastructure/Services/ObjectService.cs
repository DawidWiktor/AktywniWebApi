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
    public class ObjectService : IObjectService
    {
        private readonly IObjectRepository _objectRepository;
        private readonly IMapper _mapper;
        public ObjectService(IObjectRepository objectRepository, IMapper mapper)
        {
            _objectRepository = objectRepository;
            _mapper = mapper;
        }

        public async Task<ReturnResponse> GetObjectAsync(int objID)
        {
            var obj = await _objectRepository.GetAsync(objID);
            var objectDto = _mapper.Map<Objects, ObjectDTO>(obj);
            return new ReturnResponse { Response = (objectDto == null) ? false.ToString() : true.ToString(), Info = objectDto };

        }

        public async Task<ReturnResponse> GetObjectAsync(string name)
        {
            var obj = await _objectRepository.GetAsync(name);
            var objectDto = _mapper.Map<Objects, ObjectDTO>(obj);
            return new ReturnResponse { Response = (objectDto == null) ? false.ToString() : true.ToString(), Info = objectDto };
        }

        public async Task<ReturnResponse> GetAllObjectsAsync()
        {
            var objects = await _objectRepository.GetAllAsync();
            List<ObjectDTO> listObjectDto = _mapper.Map<IEnumerable<Objects>, List<ObjectDTO>>(objects);
            return new ReturnResponse { Response = (listObjectDto.Count == 0) ? false.ToString() : true.ToString(), Info = listObjectDto };
        }

        public async Task<ReturnResponse> SearchObjectsAsync(string textInput)
        {
            var objects = await _objectRepository.GetFromTextAsync(textInput);
            List<ObjectDTO> listObjectDto = _mapper.Map<IEnumerable<Objects>, List<ObjectDTO>>(objects);
            return new ReturnResponse { Response = (listObjectDto.Count == 0) ? false.ToString() : true.ToString(), Info = listObjectDto };
        }

        public async Task<ReturnResponse> AddObjectAsync(int administratorID, string name, string city, string street, string postcode,
                                        string geographicalCoordinates)
        {
            var newObject = await _objectRepository.GetAsync(name);
            if (newObject != null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Obiekt o takiej nazwie już istnieje." };
            }
            newObject = new Objects(administratorID, name, city, street, postcode, geographicalCoordinates);
            await _objectRepository.AddAsync(newObject);
            return new ReturnResponse { Response = true.ToString(), Info = "Dodano obiekt." };
        }
        public async Task<ReturnResponse> ChangeNameObjectAsync(int objID, string newName)
        {
            var obj = await _objectRepository.GetAsync(objID);
            if (obj == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = $"Brak obiektu o {objID}." };
            }
            if (String.IsNullOrWhiteSpace(newName))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Podano pustą nazwę obiektu." };
            }
            obj.SetName(newName);
            await _objectRepository.UpdateAsync(obj);
            return new ReturnResponse { Response = true.ToString(), Info = "Zmieniono nazwę obiektu." };
        }

        public async Task<ReturnResponse> ChangeAddressObjectAsync(int objID, string city, string street, string postcode, string geographicalCoordinates)
        {
            var obj = await _objectRepository.GetAsync(objID);
            if (obj == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = $"Brak obiektu o {objID}." };
            }
            if (String.IsNullOrWhiteSpace(city))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Nie podano nazwy miasta." };
            }
            if (String.IsNullOrWhiteSpace(street))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Nie podano nazwy ulicy." };
            }
            if (String.IsNullOrWhiteSpace(postcode))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Nie podano kodu pocztowego." };
            }
            if (String.IsNullOrWhiteSpace(geographicalCoordinates))
            {
                return new ReturnResponse { Response = false.ToString(), Error = "Nie podano współrzędnych geograficznych." };
            }

            obj.SetCity(city);
            obj.SetStreet(street);
            obj.SetPostCode(postcode);
            obj.SetGeographicalCoordinates(geographicalCoordinates);
            await _objectRepository.UpdateAsync(obj);
            return new ReturnResponse { Response = true.ToString(), Info = "Zmieniono dane adresowe obiektu." };
        }

        public async Task<ReturnResponse> RateObject(int objID, int rate)
        {
            var obj = await _objectRepository.GetAsync(objID);
            if (obj == null)
            {
                return new ReturnResponse { Response = false.ToString(), Error = $"Brak obiektu o {objID}." };
            }
            obj.SetRating(rate);
            await _objectRepository.UpdateAsync(obj);
            return new ReturnResponse { Response = true.ToString(), Info = "Oceniono obiekt" };
        }

        public async Task<ReturnResponse> RemoveObjectAsync(int objID)
        {
            /*   var obj = await _userRepository.GetAsync(objID);
               if(obj == null)
               {
                   throw new Exception($"Brak obiektu o {id}.");
               }
               await _userRepository.UpdateAsync(user);*/
            return null;
        }

    }
}