using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace d7_amplifier
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Processing IntCodes");

            var input = File.ReadAllText(@"input.txt");
            var program = Array.ConvertAll(input.Split(','),int.Parse);
            int ampMax = int.MinValue;
            foreach(var perm in GetPermutationsNoRep(new int[] {0,1,2,3,4},0,4))
            {
                int x = Amplify(program, perm);
                if( x > ampMax ) ampMax = x;
            }
            System.Console.WriteLine($"Part 1: {ampMax}");

            watch.Stop();
            System.Console.WriteLine($"Execution time: {watch.Elapsed}");
        }

        static int Amplify(int[] program, int[] phaseSettings)
        {
            //int[] phaseSettings = Array.ConvertAll(sequence.Split(','),int.Parse);
            IntcodeComputer ampA = new IntcodeComputer(phaseSettings[0]);
            IntcodeComputer ampB = new IntcodeComputer(phaseSettings[1]);
            IntcodeComputer ampC = new IntcodeComputer(phaseSettings[2]);
            IntcodeComputer ampD = new IntcodeComputer(phaseSettings[3]);
            IntcodeComputer ampE = new IntcodeComputer(phaseSettings[4]);

            var ampAVal = ampA.ProcessProgram((int[])program.Clone(), 0);
            var ampBVal = ampB.ProcessProgram((int[])program.Clone(), ampAVal);
            var ampCVal = ampC.ProcessProgram((int[])program.Clone(), ampBVal);
            var ampDVal = ampD.ProcessProgram((int[])program.Clone(), ampCVal);
            return ampE.ProcessProgram((int[])program.Clone(), ampDVal);
        }

        static IEnumerable<int[]> GetPermutationsNoRep(int[] sequence, int start, int end)
        {
            if(start == end)
            { 
                yield return sequence;
            }
            else
            {
                for(int i= start; i <= end; i++)
                {
                    sequence = Swap(sequence, start, i);
                    foreach(int[] s in GetPermutationsNoRep(sequence,start+1,end))
                    {
                        yield return s;
                    }
                    sequence = Swap(sequence, start, i);
                }
            }
            yield break;
        }

        static string SequenceToString(int[] sequence)
        {
            string temp = "";
            for(int i=0; i<sequence.Length; i++)
            {
                temp += sequence[i].ToString();
            }
            return temp;
        }

        static int[] Swap(int[] sequence, int l, int r)
        {
            int[] s = (int[])sequence.Clone();
            s[l] = sequence[r];
            s[r] = sequence[l];
            return s;
        }
    }
}
