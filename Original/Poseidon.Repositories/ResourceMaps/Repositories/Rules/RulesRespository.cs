using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5;
using Microsoft.EntityFrameworkCore;
using Poseidon.Repositories.ResourceMaps.Interfaces.Rules;

namespace Poseidon.Repositories.ResourceMaps.Repositories.Rules
{
    public class RulesRepository : GenericRepository, IRulesRepository
    {
        DataContext _context;
        public RulesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Rule>> GetAllRules()
        {
            return await _context.Rules
     .Include(r => r.Irules)
     .Include(r => r.Virtual)
         .ThenInclude(v => v.Pool)
             .ThenInclude(p => p.Monitor) // Incluir Monitor dentro de Pool
     .Include(r => r.Virtual)
         .ThenInclude(v => v.Pool)
             .ThenInclude(p => p.NodePools)
                 .ThenInclude(np=> np.Pool)
                .Select(r => new Rule
                {
                    Name= r.Name,
                    VirtualId=r.VirtualId,
                    Virtual=( new Virtual
                    {
                        Name = r.Virtual.Name,
                        // Copia todas las propiedades necesarias aquí
                        Pool = new Pool
                        {
                            Name = r.Virtual.Pool.Name,
                            MonitorId = r.Virtual.Pool.Monitor.Id,
                            Monitor = r.Virtual.Pool.Monitor,
                            Description = r.Virtual.Pool.Description,
                            BalancerType = r.Virtual.Pool.BalancerType,
                            NodePools = r.Virtual.Pool.NodePools.Select(np => new NodePool
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
                        }
                    }),
                    Irules= r.Irules,
                })
                .ToListAsync();
        }

    }
}