/* C# port of Andy Sloane (a1k0n)'s math-less donut.c */

namespace TeleDonut
{
    class Donut
    {
        public double sinA, cosA, sinB, cosB;

        public Donut(double sA = 0, double cA = 1, double sB = 1, double cB = 0)
        {
            sinA = sA; cosA = cA; sinB = sB; cosB = cB;
        }

        private (double, double) Rotate(double tanAngle, (double x, double y) xy) {
            var temp = xy.x;
            xy.x -= tanAngle * xy.y;
            xy.y += tanAngle * temp;
            temp = (3 - xy.x*xy.x - xy.y*xy.y) / 2;
            xy.x *= temp; xy.y *= temp;
            return xy;
        }

        public char[,] Render() {
            var outBuffer = new char[80, 22];
            var zBuffer = new double[80, 22];
            for (int y = 0; y < 22; y++)
            {
                for (int x = 0; x < 80; x++) 
                {
                    outBuffer[x, y] = ' ';
                    zBuffer[x, y] = 0;
                }
            }

            double sinJ = 0, cosJ = 1;
            for (int j = 0; j < 90; j++)
            {
                double sinI = 0, cosI = 1;
                for (int i = 0; i < 314; i++)
                {
                    double h = cosJ + 2;
                    double D = 1 / (sinI * h * sinA + sinJ * cosA + 5);
                    double t = sinI * h * cosA - sinJ * sinA;

                    int x = (int) (40 + 30 * D * (cosI * h * cosB - t * sinB));
                    int y = (int) (12 + 15 * D * (cosI * h * sinB + t * cosB));
                    int N = (int) (8 * ((sinJ * sinA - sinI * cosJ * cosA) * cosB - sinI * cosJ * sinA - sinJ * cosA - cosI * cosJ * sinB));

                    if (x >= 0 && x < 80 && y >= 0 && y < 22 && D > zBuffer[x, y])
                    {
                        zBuffer[x, y] = D;
                        outBuffer[x, y] = ".,-~:;=!*#$@"[(N > 0) ? N : 0];
                    }

                    (cosI, sinI) = Rotate(0.02, (cosI, sinI));
                }

                (cosJ, sinJ) = Rotate(0.07, (cosJ, sinJ));
            }

            return outBuffer;
        }

        public void Advance() {
            (cosA, sinA) = Rotate(0.04, (cosA, sinA));
            (cosB, sinB) = Rotate(0.02, (cosB, sinB));
        }

        public string ConvertFrame(char[,] frame) {
            string output = "\x1B[J\x1B[H";
            for (int y = 0; y < 22; y++)
            {
                for (int x = 0; x < 80; x++)
                {
                    output += frame[x,y];
                }
                output += "\r\n";
            }
            return output;
        }
    }
}