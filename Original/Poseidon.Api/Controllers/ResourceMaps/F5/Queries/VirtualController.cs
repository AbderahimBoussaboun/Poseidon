using Microsoft.EntityFrameworkCore;

public class VirtualController : GenericController<Virtual>
{
    public VirtualController(DbContext dbContext) : base(dbContext)
    {
        // No necesitas agregar más lógica aquí a menos que desees personalizar algo específico para la entidad Producto.
    }
}
