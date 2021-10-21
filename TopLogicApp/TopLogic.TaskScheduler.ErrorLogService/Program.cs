using System;
using System.Configuration;
using System.Threading.Tasks;

namespace TopLogic.TaskScheduler.ErrorLogService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string queueConnection;
            string queueName;
            TopLogic.Services.ErrorLogService errorLogService;

            try {                
                queueConnection = ConfigurationManager.AppSettings[AppConstants.AzureStorageQueueConnection].ToString();
                queueName = ConfigurationManager.AppSettings[AppConstants.AzureStorageQueueName].ToString(); ;
            }
            catch {
                throw new Exception("Error fetching app config values");
            }

            try {
                errorLogService = new TopLogic.Services.ErrorLogService(
                    queueConnection,
                    queueName);
            }
            catch {
                throw new Exception("Error initializing error log service");
            }

            // fetch from azure queue storage
            await Task.Run(async () => {               
                await errorLogService.ProcessLogs();
            });
        }
    }
}
