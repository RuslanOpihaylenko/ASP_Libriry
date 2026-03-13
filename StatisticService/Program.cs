using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace StatisticService
{
    sealed class Book
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Price { get; set; }
        public int GenreId { get; set; }
        public ICollection<int> AuthorsId { get; set; }
        public string? ImageUrl { get; set; }
    }
    internal class Program
    {
        static List<Book> books = new List<Book>();
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory()

            {

                HostName = "localhost",
                Port = 5672

            };

            var connection = await factory.CreateConnectionAsync();

            var channel = await connection.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, e) =>

            {

                // отримуємо байти повідомлення

                var body = e.Body.ToArray();

                // конвертуємо у string

                var json = Encoding.UTF8.GetString(body);

                // десеріалізуємо JSON у об'єкт

                var message = JsonSerializer.Deserialize<Book>(json);
                if (message != null)
                {
                    books.Add(message);
                    ShowStatistics(books);
                }
                await Task.CompletedTask;
            };
            
            await channel.BasicConsumeAsync(

                 queue: "Books",

                 autoAck: true,

                 consumer: consumer

            );

            Console.WriteLine("Waiting messages...");
   

            Console.ReadLine();

        }
        static void ShowStatistics(List<Book> books)
        {
            if (books == null || books.Count == 0)
            {
                Console.WriteLine("No books found");
                return;
            }
            else
            {
                double averagePrice = books.Average(b => b.Price);

                int lowAverage = books.Count(b => b.Price < averagePrice);

                var booksbyAuthors = books
                .SelectMany(b => b.AuthorsId)
                .GroupBy(a => a)
                .Select(g => new { AuthorsId = g.Key, Count = g.Count() });

                //Console.WriteLine($"Title: {message.Title}");

                //Console.WriteLine($"Year: {message.Year}");
                //Console.WriteLine($"Price: {message.Price}");
                //Console.WriteLine($"GenreId: {message.GenreId}");
                //foreach (var i in message.AuthorsId)
                //{
                //    Console.WriteLine($"AuthorsId: {i}");
                //}

                //Console.WriteLine($"ImageUrl: {message.ImageUrl}");
                Console.WriteLine($"Average price: {averagePrice}");

                Console.WriteLine($"Books which price is low average:{lowAverage}");
                Console.WriteLine("\nBooks by authors:");
                foreach (var i in booksbyAuthors)
                {
                    Console.WriteLine($"Author {i.AuthorsId} : {i.Count}");
                }
            }
        }
    }
}
