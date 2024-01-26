using Microsoft.EntityFrameworkCore;

public class IRuleController : GenericController<IRule>
{
    public IRuleController(DbContext dbContext) : base(dbContext)
    {
        // No necesitas agregar más lógica aquí a menos que desees personalizar algo específico para la entidad Producto.
    }
}
