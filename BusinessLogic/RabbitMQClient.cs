using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CitasMedicas.BusinessLogic
{
    public class RabbitMQClient: IDisposable
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQClient(string hostname, string queueName)
        {
            _hostname = hostname;
            _queueName = queueName;
            Task.Run(async () => await InitializeRabbitMQAsync()).Wait(); // Assure that the connection is established before proceeding
        }

        private async Task InitializeRabbitMQAsync()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public async Task PublishMessageAsync(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            await Task.Run(() => {
                _channel.BasicPublish(exchange: "",
                                      routingKey: _queueName,
                                      basicProperties: null,
                                      body: body);
            });
            Console.WriteLine($"Sent: {message}");
        }

        public void ConsumeMessages()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");
                // Additional processing logic can go here.
            };
            _channel.BasicConsume(queue: _queueName,
                                  autoAck: true,
                                  consumer: consumer);
        }

        public void Close()
        {
            _channel?.Close();
            _connection?.Close();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
