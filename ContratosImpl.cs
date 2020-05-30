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
        /// <summary>
        /// Get Ficha by number
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override Ficha obtenerFicha(int numero, Current current)
        {
            using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                Ficha ficha = fc.Fichas.Find(numero);
                fc.SaveChanges();
                return ficha;
            }
        }
        /// <summary>
        /// insert ficha
        /// </summary>
        /// <param name="ficha"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override Ficha registrarFicha(Ficha ficha, Current current)
        {
            using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Fichas.Add(ficha);
                fc.SaveChanges();
                return ficha;
            }
        }
        /// <summary>
        /// insert Persona
        /// </summary>
        /// <param name="persona"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override Persona registrarPersona(Persona persona, Current current)
        {
            using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Personas.Add(persona);
                fc.SaveChanges();
                return persona;
            }
        }
        /// <summary>
        /// insert Control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override Control registrarControl(Control control, Current current)
        {
            using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Controles.Add(control);
                fc.SaveChanges();
                return control;
            }
        }
        /// <summary>
        /// insert Foto
        /// </summary>
        /// <param name="foto"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override Foto agregarFoto(Foto foto, Current current)
        {
           using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Fotos.Add(foto);
                fc.SaveChanges();
                return foto;
            }
        }
        /// <summary>
        /// get delay whit client
        /// </summary>
        /// <param name="clientTime"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override long getDelay(long clientTime, Current current = null)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - clientTime;
        }
    }
}