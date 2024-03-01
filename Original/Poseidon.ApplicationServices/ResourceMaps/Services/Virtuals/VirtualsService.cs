using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Virtuals;
using Poseidon.Entities.ResourceMaps.F5;
using Poseidon.Repositories.ResourceMaps.Interfaces.Virtuals;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Poseidon.ApplicationServices.ResourceMaps.Services.Virtuals
{
    public class VirtualsService : IVirtualsService
    {
        private readonly IVirtualsRepository _virtualsRepository;

        public VirtualsService(IVirtualsRepository virtualsRepository)
        {
            _virtualsRepository = virtualsRepository;
        }

        public async Task<bool> DeleteVirtual(Guid id)
        {
            var result = await _virtualsRepository.DeleteEntity<Virtual>(id);
            return result;
        }

        public async Task<Virtual> GetVirtualById(Guid id)
        {
            var result = await _virtualsRepository.GetById<Virtual>(id);
            return result;
        }

        public async Task<List<Virtual>> GetVirtuals()
        {
            var result = await _virtualsRepository.GetAll(new Expression<Func<Virtual, object>>[] { });
            return result;
        }

        public async Task<Guid> InsertVirtual(Virtual virtualEntity)
        {
            var result = await _virtualsRepository.InsertEntity(virtualEntity);
            return result;
        }

        public async Task<bool> UpdateVirtual(Guid id, Virtual virtualEntity)
        {
            virtualEntity.Id = id; // Asignar el id al objeto virtual
            var result = await _virtualsRepository.UpdateEntity(virtualEntity);
            return result;
        }
    }
}
