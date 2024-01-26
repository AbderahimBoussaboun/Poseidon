using Microsoft.EntityFrameworkCore;

public class NodeController : GenericController<Node>
{
    public NodeController(DbContext dbContext) : base(dbContext)
    {
        // No necesitas agregar más lógica aquí a menos que desees personalizar algo específico para la entidad Producto.
    }
}
