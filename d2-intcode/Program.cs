using System;
using System.IO;

namespace d2_intcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Processing IntCodes");
            var file = File.ReadAllText(@"C:\Users\whisl\OneDrive\Documents\code\AdventOfCode2019\d2-intcode\input.txt");

            var program = Array.ConvertAll(file.Split(','), int.Parse);
            Console.WriteLine("Part 1 Program Output: " + ProcessProgram(program,12,2));
            
            for(int i=0; i <= 99; i++)
            {
                for(int j=0; j <= 99; j++)
                {
                    if(ProcessProgram(program,i,j) == 19690720)
                    {
                        Console.WriteLine("Part 2 Result:" + (100 * i + j));
                        i = 100;
                        break;
                    }
                }
            }
        }

        // program is by ref for some reason
        static int ProcessProgram(int[] program, int noun, int verb)
        {
            //var codes = program; 
            int[] codes = (int[])program.Clone();
            codes[1] = noun;
            codes[2] = verb;
            for(int i=0; i < codes.Length;)
            {
                switch(codes[i])
                {
                    case 1:
                    {
                        codes[codes[i+3]] = codes[codes[i+1]] + codes[codes[i+2]];
                        i += 4;
                        break;
                    }
                    case 2:
                    {
                        codes[codes[i+3]] = codes[codes[i+1]] * codes[codes[i+2]];
                        i += 4;
                        break;
                    }
                    case 99:
                    {
                        i = codes.Length + 1;
                        break;
                    }
                }
            }

            return codes[0];
        }
    }
}
