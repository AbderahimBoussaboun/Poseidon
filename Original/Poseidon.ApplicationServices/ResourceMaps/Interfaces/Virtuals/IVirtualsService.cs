using Poseidon.Entities.ResourceMaps.F5; // Asegúrate de ajustar el namespace según tu estructura
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Interfaces.Virtuals
{
    public interface IVirtualsService
    {
        Task<List<Virtual>> GetVirtuals();
        Task<Virtual> GetVirtualById(Guid virtualId);
        Task<Guid> InsertVirtual(Virtual virtualEntity);
        Task<bool> DeleteVirtual(Guid virtualId);
        Task<bool> UpdateVirtual(Guid virtualId, Virtual virtualEntity);
    }
}
