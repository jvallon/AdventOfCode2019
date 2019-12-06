using System;
using System.IO;

namespace d5_intcode_additions
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Processing IntCodes");
            var file = File.ReadAllText(@"C:\Users\whisl\OneDrive\Documents\code\AdventOfCode2019\d5-intcode-additions\input.txt");

            var program = file.Split(',');
            // System.Console.WriteLine($"Part 1: {ProcessProgram((string[])program.Clone(),1)}");
            System.Console.WriteLine($"Part 1: {ProcessProgram(Array.ConvertAll(program,int.Parse),1)}");
            System.Console.WriteLine($"Part 2: {ProcessProgram(Array.ConvertAll(program,int.Parse),5)}");
            watch.Stop();
            System.Console.WriteLine($"Execution time: {watch.Elapsed}");
        }
        static int ProcessProgram(int[] codes, int input)
        {
            int output = 0;
            for(int i=0; i < codes.Length;)
            {
                string opcodeFull = codes[i].ToString();
                int opcode = Convert.ToInt32(opcodeFull.Substring(opcodeFull.PadLeft(2,'0').Length-2));
                int p1 = opcodeFull.Length >= 3 ? Convert.ToInt32(opcodeFull.Substring(opcodeFull.Length-3,1)) : 0;
                int p2 = opcodeFull.Length >= 4 ? Convert.ToInt32(opcodeFull.Substring(opcodeFull.Length-4,1)) : 0;
                int p3 = opcodeFull.Length >= 5 ? Convert.ToInt32(opcodeFull.Substring(opcodeFull.Length-5,1)) : 0;
                
                switch(opcode)
                {
                    case 1:
                    {
                        codes[p3 == 0 ? codes[i+3] : i+3] = codes[p1 == 0 ? codes[i+1] : i+1] + codes[p2 == 0 ? codes[i+2] : i+2];
                        i += 4;
                        break;
                    }
                    case 2:
                    {
                        codes[p3 == 0 ? codes[i+3] : i+3] = codes[p1 == 0 ? codes[i+1] : i+1] * codes[p2 == 0 ? codes[i+2] : i+2];
                        i += 4;
                        break;
                    }
                    case 3:
                    {
                        codes[p1 == 0 ? codes[i+1] : i+1] = input;
                        i+=2;
                        break;
                    }
                    case 4:
                    {
                        output = codes[p1 == 0 ? codes[i+1] : i+1];
                        i+=2;
                        break;
                    }
                    case 5:
                    {
                        if(codes[p1 == 0 ? codes[i+1] : i+1] != 0)
                        {
                            i = codes[p2 == 0 ? codes[i+2] : i+2];
                        }
                        else{ i+=3; }
                        break;
                    }
                    case 6:
                    {
                        if(codes[p1 == 0 ? codes[i+1] : i+1] == 0)
                        {
                            i = codes[p2 == 0 ? codes[i+2] : i+2];
                        } 
                        else{ i+=3; }
                        break;
                    }
                    case 7:
                    {
                        if(codes[p1 == 0 ? codes[i+1] : i+1] < codes[p2 == 0 ? codes[i+2] : i+2])
                        {
                            codes[p3 == 0 ? codes[i+3] : i+3] = 1;
                        } 
                        else
                        {
                            codes[p3 == 0 ? codes[i+3] : i+3] = 0;
                        }
                        i+=4;
                        break;
                    }
                    case 8:
                    {
                        if(codes[p1 == 0 ? codes[i+1] : i+1] == codes[p2 == 0 ? codes[i+2] : i+2])
                        {
                            codes[p3 == 0 ? codes[i+3] : i+3] = 1;
                        } 
                        else
                        {
                            codes[p3 == 0 ? codes[i+3] : i+3] = 0;
                        }
                        i+=4;
                        break;
                    }
                    case 99:
                    {
                        i = codes.Length + 1;
                        break;
                    }
                    default:
                    {
                        System.Console.WriteLine($"Unknown opcode: {codes[i]}");
                        i = codes.Length + 1;
                        break;
                    }
                }
            }

            return output;
        }
    }
}
