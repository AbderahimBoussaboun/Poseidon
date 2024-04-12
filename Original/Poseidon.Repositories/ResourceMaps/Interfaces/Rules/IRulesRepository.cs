using Poseidon.Entities.ResourceMaps.F5;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Poseidon.Repositories.ResourceMaps.Interfaces.Rules
{
    public interface IRulesRepository : IGenericRepository
    {
        // Puedes agregar métodos específicos para la entidad Virtuals si es necesario
        // Por ejemplo:
        // Task<List<Virtual>> GetVirtualsBySomeCriteriaAsync();
        Task<List<Rule>> GetAllRules();
    }
}