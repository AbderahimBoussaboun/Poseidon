using System.Collections.Generic;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5; // Aseg�rate de ajustar el namespace seg�n tu estructura
using Poseidon.Repositories.ResourceMaps.Interfaces;

namespace Poseidon.Repositories.ResourceMaps.Interfaces.Virtuals
{
    public interface IVirtualsRepository : IGenericRepository
    {
        // Puedes agregar m�todos espec�ficos para la entidad Virtuals si es necesario
        // Por ejemplo:
        // Task<List<Virtual>> GetVirtualsBySomeCriteriaAsync();
        Task<List<Virtual>> GetVirtuals();
    }
}
