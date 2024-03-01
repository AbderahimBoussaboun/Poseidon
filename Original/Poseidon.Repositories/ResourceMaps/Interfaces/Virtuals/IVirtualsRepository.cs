using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5; // Asegúrate de ajustar el namespace según tu estructura
using Poseidon.Repositories.ResourceMaps.Interfaces;

namespace Poseidon.Repositories.ResourceMaps.Interfaces.Virtuals
{
    public interface IVirtualsRepository : IGenericRepository
    {
        // Puedes agregar métodos específicos para la entidad Virtuals si es necesario
        // Por ejemplo:
        // Task<List<Virtual>> GetVirtualsBySomeCriteriaAsync();
        Task<List<Virtual>> GetVirtuals();
    }
}
