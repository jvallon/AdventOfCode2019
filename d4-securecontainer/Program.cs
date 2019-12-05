using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace d4_securecontainer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculating possible passwords.");
            Regex rxDouble = new Regex(@"(\d)\1+");
            Regex rxIncreasing = new Regex(@"^(?=\d{6}$)0*?1*?2*?3*?4*?5*?6*?7*?8*?9*?$");
            
            // Part 1
            int count1 = 0;
            int count2 = 0;
            for(int i = 138241; i <= 674034; i++)
            {
                string istr = i.ToString();
                if(rxIncreasing.IsMatch(istr))
                {
                    MatchCollection results = rxDouble.Matches(istr);
                    if(results.Count > 0)
                    {
                        count1++;
                        if(results.Count(x => x.Length == 2) > 0)
                        {
                            count2++;
                        }
                    }
                }
            }
            System.Console.WriteLine($"Part 1 Result: {count1}");
            System.Console.WriteLine($"Part 2 Result: {count2}");
        }
    }
}
