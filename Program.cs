using System.Net;
using System.Threading;

namespace TeleDonut
{
    class Program
    {
        static void Main(string[] args)
        {
            var donut = new Donut();
            var server = new Server(IPAddress.Any);

            server.Start();

            while(true) {
                if (server.clients.Count == 0) continue; // wait for any client before rendering

                var frame = donut.ConvertFrame(donut.Render());
                donut.Advance();

                // Console.WriteLine(frame);
                server.Broadcast(frame);

                Thread.Sleep(20);
            }
        }
    }
}