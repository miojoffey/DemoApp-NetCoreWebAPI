using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;

namespace TopLogic.Services
{
    public class ErrorLogService
    {
        readonly CloudStorageAccount _storageAccount;
        readonly string _queueName;

        public ErrorLogService(string queueConnString,
            string queueName)
        {
            _storageAccount = CloudStorageAccount.Parse(queueConnString);            
            _queueName = queueName;
        }

        public async Task WriteAsync(object queueObject)
        {
            try {
                await Task.Run(() => {
                    var queueClient = _storageAccount.CreateCloudQueueClient();
                    var queueRef = queueClient.GetQueueReference(_queueName);
                    var queueMsg = new CloudQueueMessage(JsonConvert.SerializeObject(queueObject));
                   
                    queueRef.AddMessage(queueMsg);
                });
            }
            catch {
                throw;
            }
        }

        public async Task ProcessLogs()
        {
            try {
                await Task.Run(async () => {
                    var queueClient = _storageAccount.CreateCloudQueueClient();
                    var queueRef = queueClient.GetQueueReference(_queueName);
                    var queueMessages = await queueRef.GetMessagesAsync(30);

                    foreach (var msg in queueMessages) {
                        // do something in here ...

                        await queueRef.DeleteMessageAsync(msg);
                    }
                });
            }
            catch {
                throw;
            }
        }
    }
}
