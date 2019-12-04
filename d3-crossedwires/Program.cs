using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace d3_crossedwires
{
    class Program
    {
        static void Main(string[] args)
        {
            // string wire1 = "R8,U5,L5,D3";
            // string wire2 = "U7,R6,D4,L4";
            // string wire1 = "R75,D30,R83,U83,L12,D49,R71,U7,L7";
            // string wire2 = "U62,R66,U55,R34,D71,R55,D58,R83";
            // string wire1 = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
            // string wire2 = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
            Console.WriteLine("Processing Wire Paths");
            var lines = File.ReadLines(@"C:\Users\whisl\OneDrive\Documents\code\AdventOfCode2019\d3-crossedwires\input.txt");
            string wire1 = lines.ElementAt(0);
            string wire2 = lines.ElementAt(1);

            Tuple<int,int> curPos = new Tuple<int, int>(0,0);

            List<Tuple<int,int>> dict1 = new List<Tuple<int, int>>();
            curPos = new Tuple<int, int>(0,0);
            foreach(var move in wire1.Split(','))
            {
                curPos = Move(dict1,move,curPos);
            }

            List<Tuple<int,int>> dict2 = new List<Tuple<int, int>>();
            curPos = new Tuple<int, int>(0,0);
            foreach(var move in wire2.Split(','))
            {
                curPos = Move(dict2,move,curPos);
            }

            var min = dict1.Intersect(dict2).Select(x => Math.Abs(x.Item1) + Math.Abs(x.Item2)).Min();
            System.Console.WriteLine($"Part 1: Closest intersection distance to start is {min}");

            var min2 = dict1.Intersect(dict2).Select(x => DistanceToPoint(dict1,x) + DistanceToPoint(dict2,x)).Min();
            System.Console.WriteLine($"Part 2: Shortest timing interset distance is {min2}");
        }

        public static Tuple<int,int> Move(List<Tuple<int,int>> board, string move, Tuple<int,int> startPos)
        {
            int moveValue = Convert.ToInt32(move.Substring(1,move.Length-1));
            int x = startPos.Item1;
            int y = startPos.Item2;

            switch(move[0])
            {
                case 'U':
                {
                    for(int i=0; i < moveValue; i++)
                    {
                        y++;
                        board.Add(Tuple.Create(x,y));
                    }

                    break;
                }
                case 'D':
                {
                    for(int i=0; i < moveValue; i++)
                    {
                        y--;
                        board.Add(Tuple.Create(x,y));
                    }

                    break; 
                }
                case 'L':
                {
                    for(int i=0; i < moveValue; i++)
                    {
                        x--;
                        board.Add(Tuple.Create(x,y));
                    }

                    break; 
                }
                case 'R':
                {
                    for(int i=0; i < moveValue; i++)
                    {
                        x++;
                        board.Add(Tuple.Create(x,y));
                    }

                    break; 
                }
            }

            return new Tuple<int, int>(x,y);
        }
        
        public static int DistanceToPoint(List<Tuple<int,int>> board, Tuple<int,int> point)
        {
            return board.IndexOf(point) + 1;
        }
    }
}
