using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace d10_monitoring_station
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Processing Asteroid Map");

            string[] input = File.ReadAllLines(@"input.txt");
            //string[] input =  {".#..#",".....","#####","....#","...##"};
            // string[] input = {
            //     ".#..##.###...#######",
            //     "##.############..##.",
            //     ".#.######.########.#",
            //     ".###.#######.####.#.",
            //     "#####.##.#.##.###.##",
            //     "..#####..#.#########",
            //     "####################",
            //     "#.####....###.#.#.##",
            //     "##.#################",
            //     "#####.##.###..####..",
            //     "..######..##.#######",
            //     "####.##.####...##..#",
            //     ".#####..#.######.###",
            //     "##...#.##########...",
            //     "#.##########.#######",
            //     ".####.#.###.###.#.##",
            //     "....##.##.###..#####",
            //     ".#.#.###########.###",
            //     "#.#.#.#####.####.###",
            //     "###.##.####.##.#..##"
            // };
            List<Point> asteroids = new List<Point>();

            for(int y=0; y < input.GetLength(0); y++)
            {
                for(int x=0; x < input[y].Length; x++)
                {
                    if(input[y][x] == '#')
                    {
                        asteroids.Add(new Point(x,y));
                    }
                }
            }

            Point station = new Point();
            int visibleCount = 0;
            foreach(var start in asteroids)
            {
                SortedDictionary<double,Point> visible = new SortedDictionary<double, Point>();
                foreach(var end in asteroids)
                {
                    double angle = CalcAngle(start,end);
                    if(start != end)
                    {
                        if(! visible.ContainsKey(angle))
                        {
                            visible.Add(angle,end);
                        }
                    }
                }
                
                if(visible.Count() > visibleCount) 
                {
                    station = start;
                    visibleCount = visible.Count();
                }
            }

            System.Console.WriteLine($"Part 1: {visibleCount} @ {station}");
            
            List<MappedAsteroid> mappederoids = new List<MappedAsteroid>();
            foreach(var a in asteroids)
            {
                if(a != station)
                {
                    mappederoids.Add(new MappedAsteroid() { 
                        DistanceFromStation = Math.Sqrt(Math.Pow(station.X - a.X,2) + Math.Pow(station.Y - a.Y,2)),
                        AngleFromStation = CalcAngle(station,a),
                        Location = a
                        });
                }
            }
            
            mappederoids = mappederoids.OrderBy(x => x.AngleFromStation).ToList();
            double laserAngle = 0;
            int count = 0;
            while(mappederoids.Count() > 0)
            {
                MappedAsteroid target;
                if(mappederoids.Count(x => x.AngleFromStation == laserAngle) > 0)
                {
                    target = mappederoids.Where(x => x.AngleFromStation == laserAngle).OrderBy(x => x.DistanceFromStation).First();
                }
                else
                {
                    target = mappederoids.Where(x => x.AngleFromStation > laserAngle).OrderBy(x => x.AngleFromStation).FirstOrDefault();
                }
                
                if( target.AngleFromStation == mappederoids.Select(x => x.AngleFromStation).Max())
                {
                    laserAngle = 0;
                }
                else
                {
                    laserAngle = mappederoids.Where(x => x.AngleFromStation > laserAngle).OrderBy(x => x.AngleFromStation).FirstOrDefault().AngleFromStation;             
                }
                count++;
                if(count == 200) System.Console.WriteLine($"Part 2: Removed {target.Location} for target {count}");
                mappederoids.Remove( target );
            }

            watch.Stop();
            System.Console.WriteLine($"Execution time: {watch.Elapsed}");
        }

        static double CalcAngle(Point origin, Point p)
        {
            double radians = Math.Atan2(origin.X - p.X, origin.Y - p.Y) * -1;
            return (radians * (180 / Math.PI) + 360) % 360;
        }
    }

    public class MappedAsteroid
    {
        public double DistanceFromStation;
        public double AngleFromStation;
        public Point Location;
    }
}
