using NetCore.RabbitMQ.ExchangeTypes.Operations.Common;
using NetCore.RabbitMQ.ExchangeTypes.Operations.Connection;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace NetCore.RabbitMQ.ExchangeTypes.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = RabbitMQConnection.GetRabbitMQConnection();
            using var channer = connection.CreateModel();
            //durable => true oldugunda uygulama restart atıldıgında kuyruk kaybolmamaktadır.
            channer.ExchangeDeclare(StaticValue.FanoutExchange, durable: true, type: ExchangeType.Fanout);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string message = $"Message {x}";
                var messageBody = Encoding.UTF8.GetBytes(message);
                channer.BasicPublish(StaticValue.FanoutExchange, "", null, messageBody);
                Console.WriteLine($"Mesaj Gönderilmiştir : {message}");
            });
            Console.ReadLine();
        }
    }
}
