
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.CookiePolicy;

namespace API_EntityFramework.Servicios
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly IWebHostEnvironment _env;
        private readonly String _FileName = "File1.txt";
        private Timer _timer;
        public EscribirEnArchivo(IWebHostEnvironment env)
        {
            _env = env;
         
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            Escribir("Proceso Iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Dispose();
            Escribir("Proceso Terminado");
            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            Escribir("Proceso en ejecucion" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }
        private void Escribir(string mensaje)
        {
            var ruta = $@"{_env.ContentRootPath}\wwwroot\{_FileName}";

            using (StreamWriter writer =new StreamWriter(ruta, append: true))
            {
                writer.WriteLine(mensaje);

            }
        }
    }
}
