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

        }

        public void FanoutExchange()
        {
            //Publisher tarafından gönderilen mesajları alıp route key tanımlaması gibi ayrımlara ihtiyaç duymadan, Fanout Exchange’e bind olan bütün kuyruklara herhangi bir filtreleme yapmadan tüm consumerlara iletilmesini sağlayan yapıdır. Bu yapıda Publisher tarafında kuyruk belirtilmemektedir. Kuyruklar Subscriber tarafında tanımlanacatır. Burada routing key’in bir önemi yoktur. Daha çok broadcast yayınlar için uygundur. Özellikle (MMO) oyunlarda top10 güncellemeleri ve global duyurular için kullanılır. Yine real-time spor haberleri gibi yayınlarda fanout exchange kullanılır.

            var connection = RabbitMQConnection.GetRabbitMQConnection();

            #region CreateModel
            //CreateModel() methodu ile RabbitMQ üzerinde yeni bir channel yaratılır. İşte bu açılan channel yani session ile yeni bir queue oluşturulup istenen mesaj bu channel üzerinden gönderilmektedir.
            using var channer = connection.CreateModel(); 
            #endregion

            #region ExchangeDeclare
            //Exchange Declare işlemi yapılmaktadır.
            //1.Parametre => Exchange ismi verilmektedir.
            //2.Parametre => durable: true oldugunda RabbitMQ Servera reset atıldıgında kuyrugun kaybolmamasını saglar.
            //3.Parametre => ExchangeType olarak Fanout olarak seçilerek Exchange tipi belirtilmektedir.           
            channer.ExchangeDeclare(StaticValue.FanoutExchange, durable: true, type: ExchangeType.Fanout);
            #endregion

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string message = $"Message {x}";
                var messageBody = Encoding.UTF8.GetBytes(message);

                #region BasicPublish
                //BasicPublish() methodu “exchange” aslında mesajın alınıp bir veya daha fazla queue’ya konmasını sağlar. Bu route algoritması exchange tipine ve bindinglere göre farklılık gösterir. “Direct, Fanout ,Topic ve Headers” tiplerinde exchangeler mevcuttur.
                channer.BasicPublish(StaticValue.FanoutExchange, "", null, messageBody); 
                #endregion

                Console.WriteLine($"Mesaj Gönderilmiştir : {message}");
            });
            Console.ReadLine();
        }
    }
}
