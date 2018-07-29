using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Core.Repositories;
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

        public async Task<ObjectDTO> GetObjectAsync(int objID)
        {
            var obj = await _objectRepository.GetAsync(objID);
            return _mapper.Map<Objects, ObjectDTO>(obj);
        }

        public async Task<ObjectDTO> GetObjectAsync(string name)
        {
            var obj = await _objectRepository.GetAsync(name);
            return _mapper.Map<Objects, ObjectDTO>(obj);
        }

        public async Task<IEnumerable<ObjectDTO>> GetAllObjectsAsync()
        {
            var objects = await _objectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Objects>, IEnumerable<ObjectDTO>>(objects);
        }

        public async Task<IEnumerable<ObjectDTO>> SearchObjectsAsync(string textInput)
        {
            var objects = await _objectRepository.GetFromTextAsync(textInput);
            return _mapper.Map<IEnumerable<Objects>, IEnumerable<ObjectDTO>>(objects);
        }

        public async Task<Tuple<bool,string>> AddObjectAsync(int administratorID, string name, string city, string street, string postcode, 
                                        string geographicalCoordinates)
        {
            var newObject = await _objectRepository.GetAsync(name);
            if (newObject != null)
            {
                return new Tuple<bool, string>(false, "Obiekt o takiej nazwie już istnieje.");
            }
            newObject = new Objects(administratorID, name, city, street, postcode, geographicalCoordinates);
            await _objectRepository.AddAsync(newObject);
            return new Tuple<bool, string>(true, "");
        }
        public async Task ChangeNameObjectAsync(int objID, string newName)
        {
            var obj = await _objectRepository.GetAsync(objID);
            if (obj == null)
            {
                throw new Exception($"Brak obiektu o {objID}.");
            }
            if (String.IsNullOrWhiteSpace(newName))
            {
                throw new Exception("Podano pustą nazwę obiektu.");
            }
            obj.SetName(newName);
            await _objectRepository.UpdateAsync(obj);
        }

        public async Task ChangeAddressObjectAsync(int objID, string city, string street, string postcode, string geographicalCoordinates)
        {
            var obj = await _objectRepository.GetAsync(objID);
            if (obj == null)
            {
                throw new Exception($"Brak obiektu o {objID}.");
            }
            if (String.IsNullOrWhiteSpace(city))
            {
                throw new Exception("Nie podano nazwy miasta.");
            }
            if (String.IsNullOrWhiteSpace(street))
            {
                throw new Exception("Nie podano nazwy ulicy.");
            }
            if (String.IsNullOrWhiteSpace(postcode))
            {
                throw new Exception("Nie podano kodu pocztowego.");
            }
            if (String.IsNullOrWhiteSpace(geographicalCoordinates))
            {
                throw new Exception("Nie podano współrzędnych geograficznych.");
            }

            obj.SetCity(city);
            obj.SetStreet(street);
            obj.SetPostCode(postcode);
            obj.SetGeographicalCoordinates(geographicalCoordinates);
            await _objectRepository.UpdateAsync(obj);
        }

        public async Task RateObject(int objID, int rate)
        {
            var obj = await _objectRepository.GetAsync(objID);
            if(obj == null)
            {
                throw new Exception($"Brak obiektu o {objID}.");
            }
            obj.SetRating(rate);
            await _objectRepository.UpdateAsync(obj);
        }

        public async Task RemoveObjectAsync(int objID)
        {
            /*   var obj = await _userRepository.GetAsync(objID);
               if(obj == null)
               {
                   throw new Exception($"Brak obiektu o {id}.");
               }
               await _userRepository.UpdateAsync(user);*/
        }

    }
}