namespace API_EntityFramework.Servicios
{
    public interface IServicio
    {
        void RealizarTarea();
    }

    public class ServicioA : IServicio
    {   
        private ILogger<ServicioA> _logger;

        public ServicioA(ILogger<ServicioA> logger)
        {
            _logger = logger;
        }

        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }

    public class ServicioB : IServicio
    {
        private ILogger<ServicioA> _logger;

        public ServicioB(ILogger<ServicioA> logger)
        {
            _logger = logger;
        }

        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }
}
