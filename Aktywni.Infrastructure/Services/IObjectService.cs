using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Infrastructure.Commands;
using Aktywni.Infrastructure.DTO;

namespace Aktywni.Infrastructure.Services
{
    public interface IObjectService
    {
        Task<ReturnResponse> GetObjectAsync(int objID);
        Task<ReturnResponse> GetObjectAsync(string name);
        Task<ReturnResponse> GetAllObjectsAsync();
        Task<ReturnResponse> SearchObjectsAsync(string textInput); // wyszukiwanie po nazwie obiektu i wydarzenia
        Task<ReturnResponse> AddObjectAsync(int administratorID, string name, string city, string street, string postcode, string geographicalCoordinates);
        Task<ReturnResponse> ChangeNameObjectAsync(int objID, string newName);
        Task<ReturnResponse> ChangeAddressObjectAsync(int objID, string city, string street, string postcode, string geographicalCoordinates);
        Task<ReturnResponse> RateObject(int objID, int rate);
        Task<ReturnResponse> RemoveObjectAsync(int objID);
    }
}