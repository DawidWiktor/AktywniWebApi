using System.Collections.Generic;
using System.Threading.Tasks;
using Aktywni.Core.Model;
using Aktywni.Infrastructure.DTO;

namespace Aktywni.Infrastructure.Services
{
    public interface IObjectService
    {
        Task<ObjectDTO> GetObjectAsync(int objID);
        Task<IEnumerable<ObjectDTO>> GetAllObjectsAsync();
        Task<IEnumerable<ObjectDTO>> SearchObjectsAsync(string textInput);
        Task AddObjectAsync(int administratorID, string name, string city, string street, string postcode, string geographicalCoordinates);
        Task ChangeNameObjectAsync(int objID, string newName);
        Task ChangeAddressObjectAsync(int objID, string city, string street, string postcode, string geographicalCoordinates);
        Task RateObject(int objID, int rate);
        Task RemoveObjectAsync(int objID);
    }
}