using Poseidon.Entities.ResourceMaps.F5; // Aseg�rate de ajustar el namespace seg�n tu estructura
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
                    // Copia todas las propiedades necesarias aqu�
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
                                Id = np.Node.Id, // y as� sucesivamente para otras propiedades excepto 'NodePools'
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





        // Puedes implementar m�todos espec�ficos para la entidad Virtuals aqu�
        // Por ejemplo:
        // public async Task<List<Virtual>> GetVirtualsBySomeCriteriaAsync()
        // {
        //     // Implementaci�n...
        // }

        // Puedes usar los m�todos generales del GenericRepository para operaciones comunes
        // sin necesidad de implementarlos aqu�, ya que el GenericRepository ya los contiene.
    }
}
