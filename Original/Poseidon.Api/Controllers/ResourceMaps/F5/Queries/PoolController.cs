using Microsoft.EntityFrameworkCore;

public class PoolController : GenericController<Pool>
{
    public PoolController(DbContext dbContext) : base(dbContext)
    {
        // No necesitas agregar más lógica aquí a menos que desees personalizar algo específico para la entidad Producto.
    }
}
