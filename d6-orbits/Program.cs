using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace d6_orbits
{
    class Program
    {
        private const string inputFile = @"input.txt";
        // dictionary of planet objects (tree)
        private static Dictionary<string, Planet> planets = new Dictionary<string, Planet>();
        
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Processing orbital mechanics");
            var lines = File.ReadAllLines(inputFile);
            
            foreach(var line in lines)
            {
                Planet parent = GetPlanet(line.Split(')')[0]);
                Planet child = GetPlanet(line.Split(')')[1]);

                parent.AddChild(child);
            }

            // Part 1
            int sum = 0;
            foreach(var p in planets.Values)
            {
                sum += p.Depth();
            }
            System.Console.WriteLine($"Part 1: {sum}");

            // Part 2
            System.Console.WriteLine($"Part 2: {GetOrbitTransfers(planets["YOU"].Parent, planets["SAN"].Parent)}");

            watch.Stop();
            System.Console.WriteLine($"Execution time: {watch.Elapsed}");
        }

        public static int GetOrbitTransfers(Planet start, Planet end)
        {
            List<Planet> startPath = new List<Planet>();
            List<Planet> endPath = new List<Planet>();

            Planet current = start;
            while(current != null)
            {
                startPath.Add(current);
                current = current.Parent;
            }

            current = end;
            while(current !=  null)
            {
                endPath.Add(current);
                current = current.Parent;
            }

            List<Planet> pathStartToEnd = startPath.Except(endPath).Union(endPath.Except(startPath)).ToList();
            return pathStartToEnd.Count();
        }
        
        public static Planet GetPlanet(string name)
        {
            if(!planets.ContainsKey(name))
            {
                planets.Add(name, new Planet(name));
            }

            return planets[name];
        }
    }

    public class Planet
    {
        public string name;
        public List<Planet> Children;
        public Planet Parent;

        public Planet(string name)
        {
            this.name = name;
            Children = new List<Planet>();
        }

        public void AddChild(string name)
        {
            Children.Add(new Planet(name){ Parent = this });
        }

        public void AddChild(Planet child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public int Depth()
        {
            if(Parent != null)
            {
                return Parent.Depth() + 1;
            }
            return 0;
        }

        public bool IsRoot
        {
            get { return Parent == null; }
        }

        public bool IsLeaf
        {
            get { return Children.Count == 0; }
        }
    }
}
