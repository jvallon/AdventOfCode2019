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
            Console.WriteLine("Processing Moon Gravity");
            
            List<Moon> moons = new List<Moon>();
            //test1
            // moons.Add(new Moon(-1,0,2));
            // moons.Add(new Moon(2,-10,-7));
            // moons.Add(new Moon(4,-8,8));
            // moons.Add(new Moon(3,5,-1));
            //test2
            moons.Add(new Moon(-8,-10,0));
            moons.Add(new Moon(5,5,10));
            moons.Add(new Moon(2,-7,3));
            moons.Add(new Moon(9,-8,-3));
            //puzzle input
            // moons.Add(new Moon(-15,1,4));
            // moons.Add(new Moon(1,-10,-8));
            // moons.Add(new Moon(-5,4,9));
            // moons.Add(new Moon(4,6,-2));
            double stepsToRun = 4686774924;

            for(double i=1; i <= stepsToRun; i++)
            {
                foreach(var moon in moons)
                {
                    foreach(var m in moons)
                    {
                        moon.Vx += m.X > moon.X ?  1 : m.X < moon.X ? -1 : 0;
                        moon.Vy += m.Y > moon.Y ?  1 : m.Y < moon.Y ? -1 : 0;
                        moon.Vz += m.Z > moon.Z ?  1 : m.Z < moon.Z ? -1 : 0;
                    }
                }
                foreach(var moon in moons)
                {
                    moon.X += moon.Vx;
                    moon.Y += moon.Vy;
                    moon.Z += moon.Vz;
                }
            }
            
            System.Console.WriteLine(moons.Sum(x => x.GetPotentialEnergy() * x.GetKineticEnergy()));
            //foreach moon, compare coordinates to get velocity 
            //foreach moon, apply velocity to coordinates
            //calculate energy 

            watch.Stop();
            System.Console.WriteLine($"Execution time: {watch.Elapsed}");
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
