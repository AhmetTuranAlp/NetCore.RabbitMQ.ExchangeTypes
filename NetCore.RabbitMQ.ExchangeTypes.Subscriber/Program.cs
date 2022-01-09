using NetCore.RabbitMQ.ExchangeTypes.Operations.Common;
using NetCore.RabbitMQ.ExchangeTypes.Operations.Connection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace NetCore.RabbitMQ.ExchangeTypes.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
  
        }

        public void FanoutExchange()
        {
            var connection = RabbitMQConnection.GetRabbitMQConnection();

            using var channer = connection.CreateModel();

            var queueName = channer.QueueDeclare().QueueName;
            channer.QueueBind(queueName, StaticValue.FanoutExchange, "", null);
            channer.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channer);
            channer.BasicConsume(queueName, false, consumer);

            Console.WriteLine("Kanal Dinleniyor.");

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Thread.Sleep(1500);
                Console.WriteLine($"Alınan Mesaj : {message}");
                channer.BasicAck(e.DeliveryTag, false);
            };

            Console.ReadLine();
        }
    }
}
