using System;
using System.IO;


namespace fuel_calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculating Fuel Required for Payload");
            var lines = File.ReadLines(@"C:\Users\whisl\OneDrive\Documents\Advent of Code 2019\fuel calc\input.txt");
            int sum = 0;
            foreach(var line in lines)
            {
                //int fuel = Convert.ToInt32(line) / 3 - 2;
                int fuel = FuelForPayloadCalc(Convert.ToInt32(line));
                if(fuel >= 0)
                    sum += fuel;
            }
            Console.WriteLine("Payload requires this much fuel: {0}", sum);
            
        }

        public static int FuelForPayloadCalc(int payload)
        {
            int fuel = payload / 3 - 2;
            if(fuel > 0)
                return fuel + FuelForPayloadCalc(fuel);
            return 0;
        }
    }
}
