using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5;
using Microsoft.EntityFrameworkCore;
using Poseidon.Repositories.ResourceMaps.Interfaces.Nodes;

namespace Poseidon.Repositories.ResourceMaps.Repositories.Nodes
{
    public class NodesRepository : GenericRepository, INodesRepository
    {
        DataContext _context;
        public NodesRepository(DataContext context) : base(context) { 
            _context = context;
        }

        Expression<Func<Node, object>>[] includes = new Expression<Func<Node, object>>[]
{
    n => n.NodePools,
    n => n.NodePools.Select(np => np.Pool)
};

        public async Task<List<Node>> GetAllNodes()
        {
            return await _context.Nodes
                .Include(n => n.NodePools)
                .ThenInclude(np => np.Pool) // Necesitamos incluir Pool para acceder a sus propiedades
                .ThenInclude(p => p.Virtuals) // Y también incluir Virtuals para acceder a sus propiedades
                .Select(n => new Node
                {
                    Name = n.Name,
                    Ip =n.Ip,
                    Description = n.Description,
                    NodePools = n.NodePools.Select(np => new NodePool
                    {
                        // ...otras propiedades de NodePool 

                        Pool = new Pool
                        {
                            Monitor = np.Pool.Monitor,
                            BalancerType = np.Pool.BalancerType,
                            Description = np.Pool.Description,
                            MonitorId=np.Pool.MonitorId,
                            // Aquí estás creando una nueva entidad para Virtuals, pero quiero que confirmes si es esto lo que quieres realmente
                            Virtuals = np.Pool.Virtuals.Select(v => new Virtual
                            {
                                Name = v.Name // Estoy asumiendo que tienes una propiedad Name en Virtual
                                              // ... otras propiedades de Virtual
                            }).ToList()
                        }
                    }).ToList()
                })
                .ToListAsync();
        }

    }
}