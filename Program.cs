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

            server.start();

            while(true) {
                if(server.clients.Count == 0) continue; // wait for any client before rendering

                var frame = donut.convertFrame(donut.renderFrame());
                donut.advanceFrame();

                // Console.WriteLine(frame);
                server.broadcast(frame);

                Thread.Sleep(20);
            }
        }
    }
}