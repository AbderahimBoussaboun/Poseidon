using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Poseidon.Entities.ResourceMaps.F5;
using Microsoft.EntityFrameworkCore;
using Poseidon.Repositories.ResourceMaps.Interfaces.Monitors;

namespace Poseidon.Repositories.ResourceMaps.Repositories.Monitors
{
    public class MonitorsRepository : GenericRepository, IMonitorsRepository
    {
        DataContext _context;
        public MonitorsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Monitor>> GetAllMonitors()
        {
            return await _context.Monitors
                .Include(m => m.Pools)
                    .ThenInclude(p => p.NodePools)
                        .ThenInclude(np => np.Node) // Incluir la relación Node dentro de NodePools
                .Select(m => new Monitor
                {
                    Name= m.Name,
                    Adaptive = m.Adaptive,
                    Cipherlist = m.Cipherlist,
                    Compatibility = m.Compatibility,
                    Debug = m.Debug,
                    Defaults_from = m.Defaults_from,
                    Description = m.Description,
                    Destination = m.Destination,
                    IP_DSCP = m.IP_DSCP,
                    Interval = m.Interval,
                    Password = m.Password,
                    RECV = m.RECV,
                    RECV_disable = m.RECV_disable,
                    Reverse = m.Reverse,
                    SEND = m.SEND,
                    Server = m.Server,
                    Service = m.Service,
                    get = m.get,
                    ssl_profile = m.ssl_profile,
                    time_until_up = m.time_until_up,
                    timeout = m.timeout,
                    username = m.username,
                    Pools = m.Pools.Select(p => new Pool
                    {
                        Name= p.Name,
                        Description=p.Description,
                        BalancerType=p.BalancerType,
                        MonitorId= p.MonitorId,
                        Monitor=p.Monitor,
                        // Incluir propiedades de Pool si es necesario
                        NodePools = p.NodePools.Select(np => new NodePool
                        {
                            // Incluir propiedades de NodePool si es necesario
                            Node = new Node
                            {
                                Name=np.Node.Name,
                                Description =np.Node.Description,
                                Ip=np.Node.Ip
                            },
                            NodePort = np.NodePort
                        }).ToList(),
                        Virtuals = p.Virtuals.Select(v => new Virtual
                        {
                            Name = v.Name // Estoy asumiendo que tienes una propiedad Name en Virtual
                                          // ... otras propiedades de Virtual
                        }).ToList()
                    }).ToList()
                }).ToListAsync();
        }


    }
}