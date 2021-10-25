using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Archiver
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var getEvents =  await EventClient.GetAllEvents("https://localhost:44384/", "event", "");

                foreach(var userEvent in getEvents)
                {
                    if (userEvent.EndDate < DateTime.Now)
                    {
                        await EventClient.DeleteEvent("https://localhost:44384/", "event","" ,userEvent.Id);
                        await EventClient.AddArchive("https://localhost:44384/", "archive", "", userEvent);
                    }
                }
                //await Task.Delay(1000, stoppingToken);
                await Task.Delay(3600*1000, stoppingToken);
            }
        }
    }
}
