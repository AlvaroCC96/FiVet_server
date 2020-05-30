using System;
using Fivet.Dao;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fivet.ZeroIce
{
    public class ContratosImpl : ContratosDisp_
    {
        /// <summary>
        /// Logger 
        /// </summary>
        private readonly ILogger<ContratosImpl> _logger;

        /// <summary>
        /// provider of db context
        /// </summary>
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceScopeFactory"></param>
        public ContratosImpl( ILogger<ContratosImpl> logger, IServiceScopeFactory serviceScopeFactory){
            _logger = logger;
            _logger.LogDebug("Building ContratosIMPL");
            _serviceScopeFactory = serviceScopeFactory;
            
            //Creating db   
            _logger.LogInformation("Creating the Database...");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Database.EnsureCreated();
                fc.SaveChanges();
            }
            _logger.LogInformation("Done!!");
        }

        public override Ficha obtenerFicha(int numero, Current current)
        {
            return null;
        }

        public override Ficha registrarFicha(Ficha ficha, Current current)
        {
            return null;
        }

        public override Persona registrarPersona(Persona persona, Current current)
        {
            return null;
        }

        public override Control registrarControl(Control control, Current current)
        {
            return null;
        }

        public override bool agregarFoto(Foto foto, Current current)
        {
            return false;
        }

        public override long getDelay(long clientTime, Current current = null)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - clientTime;
        }
    }
}