using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace d8_imager
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Processing Image");

            var input = File.ReadAllText(@"input.txt");
            int width = 25;
            int height = 6;
            int layerSize = width * height;
            int layerCount = input.Length / layerSize;
            
            string[] layers = new string[layerCount];
            for(int i=0; i<layerCount; i++)
            {
                layers[i] = input.Substring(i * layerSize, layerSize);
            }

            int minZeroCount = int.MaxValue;
            int ones = 0;
            int twos = 0;

            for(int i = 0; i<layerCount; i++)
            {
                int zeros = 0;
                zeros = layers[i].Count(x => x == '0');
                if(zeros < minZeroCount)
                {
                    minZeroCount = zeros;
                    ones = layers[i].Count(x => x == '1');
                    twos = layers[i].Count(x => x == '2');
                }
            }

            System.Console.WriteLine($"Part 1: {ones * twos}");
            
            string result = "";
            for(int i = 0; i < layerSize; i++)
            {
                for(int j = 0; j < layerCount; j++)
                {
                    if(layers[j][i] == '0')
                    {
                        result = $"{result} ";  //makes it a bit easier to read
                        break;
                    }
                    else if(layers[j][i] == '1')
                    {
                        result = $"{result}{layers[j][i]}";
                        break;
                    }
                }
            }
            System.Console.WriteLine($"Part 2:");
            for(int i = 0; i < height; i++)
            {
                System.Console.WriteLine($"{result.Substring(i*width,width)}");
            }

            watch.Stop();
            System.Console.WriteLine($"Execution time: {watch.Elapsed}");
        }
    }
}
