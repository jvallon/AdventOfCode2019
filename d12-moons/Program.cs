using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace d12_moons
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Processing N Body Problem");
            
            List<Moon> moons = new List<Moon>();
            //puzzle input
            moons.Add(new Moon(-15,1,4));
            moons.Add(new Moon(1,-10,-8));
            moons.Add(new Moon(-5,4,9));
            moons.Add(new Moon(4,6,-2));
            double stepsToRun = 1000;

            for(double i=1; i <= stepsToRun; i++)
            {
                foreach(var moon in moons)
                {
                    foreach(var m in moons)
                    {
                        moon.Vx += (m.X > moon.X) ?  1 : m.X < moon.X ? -1 : 0;
                        moon.Vy += (m.Y > moon.Y) ?  1 : m.Y < moon.Y ? -1 : 0;
                        moon.Vz += (m.Z > moon.Z) ?  1 : m.Z < moon.Z ? -1 : 0;
                    }
                }
                foreach(var moon in moons)
                {
                    moon.X += moon.Vx;
                    moon.Y += moon.Vy;
                    moon.Z += moon.Vz;
                }
            }
            
            System.Console.WriteLine($"Part 1: {moons.Sum(x => x.GetPotentialEnergy() * x.GetKineticEnergy())}");
            
            // Part 2
            
            List<Moon> moons2 = new List<Moon>();
            //test1
            // moons2.Add(new Moon(-1,0,2));
            // moons2.Add(new Moon(2,-10,-7));
            // moons2.Add(new Moon(4,-8,8));
            // moons2.Add(new Moon(3,5,-1));
            //test2
            // moons2.Add(new Moon(-8,-10,0));
            // moons2.Add(new Moon(5,5,10));
            // moons2.Add(new Moon(2,-7,3));
            // moons2.Add(new Moon(9,-8,-3));
            //puzzle input
            moons2.Add(new Moon(-15,1,4));
            moons2.Add(new Moon(1,-10,-8));
            moons2.Add(new Moon(-5,4,9));
            moons2.Add(new Moon(4,6,-2));
            long xPeriod = GetPeriod(moons2.Select(x => x.X).ToArray(), moons2.Select(x => x.Vx).ToArray());
            long yPeriod = GetPeriod(moons2.Select(x => x.Y).ToArray(), moons2.Select(x => x.Vy).ToArray());
            long zPeriod = GetPeriod(moons2.Select(x => x.Z).ToArray(), moons2.Select(x => x.Vz).ToArray());

            System.Console.WriteLine( $"Part 2: {LCM(LCM(xPeriod,yPeriod), zPeriod)}");

            watch.Stop();
            System.Console.WriteLine($"Execution time: {watch.Elapsed}");
        }

        public static long LCM(long a, long b)
        {
            return a / GCF(a,b) * b;
        }
        public static long GCF(long a, long b)
        {
            while( b != 0 )
            {
                long t = b;
                b = a % b;
                a = t;
            }

            return a;
        }


        public static long GetPeriod(int[] positions, int[] velocities)
        {
            int[] initialPositions = (int[])positions.Clone();
            int[] initialVelocities = (int[])velocities.Clone();

            long count = 0;
            bool searching = true;
            while(searching)
            {
                for(int i=0; i < positions.Length; i++)
                {
                    for(int j=i+1; j < positions.Length; j++)
                    {
                        if(positions[i] < positions[j])
                        {
                            velocities[i] += 1;
                            velocities[j] -= 1;
                        }
                        else if(positions[i] > positions[j])
                        {
                            velocities[i] -= 1;
                            velocities[j] += 1;
                        }
                    }
                }

                for(int i=0; i < positions.Length; i++)
                {
                    positions[i] += velocities[i];
                }

                count++;
                if(count > 1)
                {
                    searching = false;
                    for(int i=0; i < positions.Length; i++)
                    {
                        if(initialPositions[i] != positions[i] )
                        {
                            searching = true;
                            break;
                        }

                        if(initialVelocities[i] != velocities[i])
                        {
                            searching = true;
                            break;
                        }
                    }
                }
            }
            return count;
        }
    }

    public class Moon 
    {
        public int X;
        public int Y;
        public int Z;
        public int Vx;
        public int Vy;
        public int Vz;
        public Moon(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            Vx = Vy = Vz = 0;
        }

        public int GetPotentialEnergy()
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }
        public int GetKineticEnergy()
        {
            return Math.Abs(Vx) + Math.Abs(Vy) + Math.Abs(Vz);
        }
    }
}
