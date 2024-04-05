using Poseidon.Entities.ResourceMaps.F5; // Asegúrate de ajustar el namespace según tu estructura
using Poseidon.Repositories.ResourceMaps.Interfaces;
using Poseidon.Repositories.ResourceMaps.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Poseidon.Repositories.ResourceMaps.Interfaces.Virtuals;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Poseidon.Repositories.ResourceMaps.Repositories.Virtuals
{
    public class VirtualsRepository : GenericRepository, IVirtualsRepository
    {

        DataContext _context;
        public VirtualsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        Expression<Func<Virtual, object>>[] includesVirtual = new Expression<Func<Virtual, object>>[]
{
    v => v.Pool,
    v => v.Rules
};


        public async Task<List<Virtual>> GetVirtuals()
        {
            var result = await _context.Virtuals
                .Include(v => v.Pool)
                    .ThenInclude(p => p.Monitor)
                .Include(v => v.Pool)
                    .ThenInclude(p => p.NodePools)
                        .ThenInclude(np => np.Node)
                .Include(v => v.Rules)
                    .ThenInclude(r => r.Irules)
                .Select(v => new Virtual
                {
                    Name = v.Name,
                    // Copia todas las propiedades necesarias aquí
                    Pool = new Pool
                    {
                        Name= v.Pool.Name,
                        MonitorId= v.Pool.Monitor.Id,
                        Monitor = v.Pool.Monitor,
                        Description = v.Pool.Description,
                        BalancerType = v.Pool.BalancerType,
                        NodePools = v.Pool.NodePools.Select(np => new NodePool
                        {
                            Node = new Node
                            {
                                Name = np.Node.Name,
                                Id = np.Node.Id, // y así sucesivamente para otras propiedades excepto 'NodePools'
                                Ip = np.Node.Ip,
                                Description = np.Node.Description,
                            },
                            NodePort = np.NodePort
                        }).ToList()
                    },
                    Rules = v.Rules,
                })
                .ToListAsync();

            return result;
        }





        // Puedes implementar métodos específicos para la entidad Virtuals aquí
        // Por ejemplo:
        // public async Task<List<Virtual>> GetVirtualsBySomeCriteriaAsync()
        // {
        //     // Implementación...
        // }

        // Puedes usar los métodos generales del GenericRepository para operaciones comunes
        // sin necesidad de implementarlos aquí, ya que el GenericRepository ya los contiene.
    }
}
