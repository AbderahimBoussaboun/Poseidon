using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5;
using Microsoft.EntityFrameworkCore;
using Poseidon.Repositories.ResourceMaps.Interfaces.Pools;

namespace Poseidon.Repositories.ResourceMaps.Repositories.Pools
{
    public class PoolsRepository : GenericRepository, IPoolsRepository
    {
        DataContext _context;
        public PoolsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        

        public async Task<List<Pool>> GetAllPools()
        {
            return await _context.Pools
                .Include(p => p.Monitor)
                .Include(p => p.Virtuals)
                .ThenInclude(v => v.Rules)
                .ThenInclude(r=> r.Irules)
                .Include(p=> p.NodePools)
                .ThenInclude(np => np.Node)
                .Select(p => new Pool
                {
                    Name = p.Name,
                    Description = p.Description,
                    BalancerType = p.BalancerType,
                    Monitor = p.Monitor,
                    MonitorId = p.MonitorId,
                    NodePools = p.NodePools.Select(np => new NodePool
                    {
                        Node = np.Node,
                        NodePort= np.NodePort
                    }).ToList(),
                    Virtuals = p.Virtuals.Select(v => new Virtual
                    {
                        Name = v.Name, // Estoy asumiendo que tienes una propiedad Name en Virtual
                        Rules = v.Rules.Select(r=> new Rule
                        {
                            Name = r.Name,
                            Irules = r.Irules,
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
        }

    }
}