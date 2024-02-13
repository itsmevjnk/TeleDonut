using System.Threading;

namespace TeleDonut
{
    class Program
    {
        static void Main(string[] args)
        {
            var donut = new Donut();

            while(true) {
                var frame = donut.renderFrame();
                donut.advanceFrame();

                Console.WriteLine(donut.convertFrame(frame));

                /* TODO: telnet here */

                Thread.Sleep(20);
            }
        }
    }
}