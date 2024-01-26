using Microsoft.EntityFrameworkCore;

public class MonitorController : GenericController<Monitor>
{
    public MonitorController(DbContext dbContext) : base(dbContext)
    {
        // No necesitas agregar más lógica aquí a menos que desees personalizar algo específico para la entidad Producto.
    }
}
