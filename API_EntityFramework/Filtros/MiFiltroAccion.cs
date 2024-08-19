using Microsoft.AspNetCore.Mvc.Filters;

namespace API_EntityFramework.Filtros
{
    public class MiFiltroAccion : IActionFilter
    {
        ILogger<MiFiltroAccion> _logger;

        public MiFiltroAccion(ILogger<MiFiltroAccion> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Antes de ejecutar la accion");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Despues de ejecutar la acción");

        }

       
    }
}
