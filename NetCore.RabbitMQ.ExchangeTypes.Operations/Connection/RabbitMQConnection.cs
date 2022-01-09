using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.RabbitMQ.ExchangeTypes.Operations.Connection
{
    public static class RabbitMQConnection
    {
        public static IConnection GetRabbitMQConnection()
        {
            //ConnectionFactory :RabbitMQ hostuna bağlanmak için kullanılır. Bulunulan sunucudaki host name (localhost),virtual host ve credentials (password) girilir.
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://gtxetrwd:9obFg889y8vl-QPq5UiAMtDpuwRBNkyQ@jaguar.rmq.cloudamqp.com/gtxetrwd");
            return factory.CreateConnection();
        }
    }
}
