using System;
using System.Threading;
using System.Threading.Tasks;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Fivet.ZeroIce;

namespace Fivet.Server
{
    internal class FivetService : IHostedService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<FivetService> _logger;
        
        /// <summary>
        /// Comunicator
        /// </summary>
        private readonly Communicator _communicator;
        
        /// <summary>
        /// port
        /// </summary>
        private readonly int _port = 8080;

        /// <summary>
        /// The system interface
        /// </summary>
        private readonly TheSystemDisp_ _theSystem;
        private readonly ContratosDisp_ _contratos;

        /// <summary>
        /// Contratos interface
        /// </summary>
        /// <param name="logger"></param>
        public FivetService(ILogger<FivetService> logger, TheSystemDisp_ theSystem , ContratosDisp_ contratos)
        {
            _logger = logger;
            _logger.LogDebug("Building FivetSerive");
            _theSystem =theSystem;
            _contratos = contratos;
            _communicator = buildCommunicator();
        } 

        /// <summary>
        ///  start async task
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync (CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting the FivetService...");
            //The Adapter 
            var adapter = _communicator.createObjectAdapterWithEndpoints("Contratos","tcp -z -t 15000 -p "+_port);

            //The interface    
            //TheSystem theSystem = new TheSystemImpl();
            //adapter.add(_theSystem,Util.stringToIdentity("TheSystem"));
            //adapter.add(theSystem,Util.stringToIdentity("TheSystem"));
           
            
            adapter.add(_contratos,Util.stringToIdentity("Contratos"));
            adapter.activate();
            return Task.CompletedTask;
        }

        /// <summary>
        /// stop async task
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync (CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting the FivetService...");
            _communicator.shutdown();
            _logger.LogDebug("Communicator Stopped!");

            return Task.CompletedTask;
        }
        /// <summary>
        /// communicator 
        /// </summary>
        /// <returns></returns>
        private Communicator buildCommunicator()
        {
            _logger.LogDebug("Initializing Communicator v{0} ({1}) .. ", Ice.Util.stringVersion(),Ice.Util.intVersion());

            //ZeroC Properties
            Properties properties =  Util.createProperties();
            // https://doc.zeroc.com/ice/latest/property-reference/ice-trace
            // properties.setProperty("Ice.Trace.Admin.Properties", "1");
            // properties.setProperty("Ice.Trace.Locator", "2");
            // properties.setProperty("Ice.Trace.Network", "3");
            // properties.setProperty("Ice.Trace.Protocol", "1");
            // properties.setProperty("Ice.Trace.Slicing", "1");
            // properties.setProperty("Ice.Trace.ThreadPool", "1");
            // properties.setProperty("Ice.Compression.Level", "9");
            InitializationData initializationData = new InitializationData();
            initializationData.properties = properties;
            return Ice.Util.initialize(initializationData);
        }
        /// <summary>
        /// destroy the communicator
        /// </summary>
        public void Dispose(){
            _communicator.destroy();
        }

    }

   
}