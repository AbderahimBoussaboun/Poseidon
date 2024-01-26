using Microsoft.EntityFrameworkCore;

public class RuleController : GenericController<Rule>
{
    public RuleController(DbContext dbContext) : base(dbContext)
    {
        // No necesitas agregar más lógica aquí a menos que desees personalizar algo específico para la entidad Producto.
    }
}
