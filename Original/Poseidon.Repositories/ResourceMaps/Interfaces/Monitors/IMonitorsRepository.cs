using Poseidon.Entities.ResourceMaps.F5;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Poseidon.Repositories.ResourceMaps.Interfaces.Monitors
{
    public interface IMonitorsRepository : IGenericRepository
    {
        // Puedes agregar métodos específicos para la entidad Virtuals si es necesario
        // Por ejemplo:
        // Task<List<Virtual>> GetVirtualsBySomeCriteriaAsync();
        Task<List<Monitor>> GetAllMonitors();
    }
}