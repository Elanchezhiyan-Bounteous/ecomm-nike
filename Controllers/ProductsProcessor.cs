using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace ecomm_nike.Controllers
{
    public class ProductsProcessor : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Starting Background processor");
            var accesskey = Environment.GetEnvironmentVariable("accesskey");
            var secretkey = Environment.GetEnvironmentVariable("secretkey");
            var queueurl = Environment.GetEnvironmentVariable("queueurl");
            var credentials = new BasicAWSCredentials(accesskey, secretkey);
            var client = new AmazonSQSClient(credentials, RegionEndpoint.EUNorth1);

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"getting messages from the queue {DateTime.Now}");
                var request = new ReceiveMessageRequest()
                {
                    QueueUrl = queueurl,
                    WaitTimeSeconds = 10,
                    VisibilityTimeout = 5,

                };

                var response = await client.ReceiveMessageAsync(request, stoppingToken);
                if (response.Messages != null && response.Messages.Count > 0)
                {
                    foreach (var message in response.Messages)
                    {
                        Console.WriteLine(message.Body);

                        if (message.Body.Contains("Exception")) continue;

                        else
                        {
                            await client.DeleteMessageAsync(queueurl, message.ReceiptHandle);
                            Console.WriteLine("Message deleted successfully");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No messages received");
                }

            }
        }
    }
}