using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam70_483
{
    /* Enter your code here. Read input from STDIN. Print output to STDOUT */
   
        class CountNode
        {
            public int Cnt;
            public int Sum;
            public int SkipCnt;
        }

        class Soluction
        {
            public static IList<string> Merge(IList<string> list1, IList<string> list2)
            {
                var result1 = new List<string>();
                result1.AddRange(list1);
                result1.AddRange(list2);            
                result1.Sort();
                return result1;

            }
            public static void BuildCountArray(string s1, string s2, int index)
            {
                CountNode n = new CountNode();
                if (s1 == null)
                {
                    n.Cnt = s2.Length;
                    n.Sum = s2.Length;
                    n.SkipCnt = 0;
                    all.Add(n);
                    return;
                }

                int i = 0;
                while (i < s1.Length)
                {
                    if (s1[i] != s2[i])
                    {
                        break;
                    }
                    i++;
                }

                n.Cnt = s2.Length - i;
                n.SkipCnt = i;
                if (index > 0)
                {
                    n.Sum = all[index - 1].Sum + n.Cnt;
                }
                else n.Sum = n.Cnt;

                all.Add(n);
            }
            public static void BuildStatistic()
            {
                string p = null;
                int i = 0;
                foreach (string s in result)
                {
                    BuildCountArray(p, s, i++);
                    p = s;
                }
            }

            public static string Find(int k)
            {
                int end = all.Count - 1;
                if (k > all[end].Sum)
                    return "INVALID";
                int from = BinarySearch(0, end, k);

                int psum = from > 0 ? all[from - 1].Sum : 0;

                int cnt = all[from].SkipCnt + k - psum;

                return result[from].Substring(0, cnt);
            }

            public static int BinarySearch(int startindex, int endindex, int k)
            {
                if (startindex >= endindex)
                {
                    return endindex;
                }

                int mid = (startindex + endindex) / 2;

                if (all[mid].Sum > k)
                {
                    return BinarySearch(startindex, mid, k);
                }
                if (all[mid].Sum < k)
                {
                    return BinarySearch(mid + 1, endindex, k);
                }
                return mid;
            }

            public static IList<string> BuildSuffixArray(string input)
            {

                int len = input.Length;
                var result = new string[len];
                for (int i = 0; i < len; i++)
                {
                    result[i] = (input.Substring(i, len - i));
                    
                }
                Array.Sort(result, Com);
                return result;
            }
            public static int Com(string s1, string s2)
            {
                return String.Compare(s1, s2);
            }
            public static IList<CountNode> all = new List<CountNode>();
            public static IList<String> result = null;
            public static void DoIt(string[] s, int[] qr)
            {
                int n = s.Length;
                //int n = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < n; i++)
                {
                    IList<String> r = BuildSuffixArray(s[i]);//Console.ReadLine());
                    if (result != null)
                    {
                       result = Merge(r, result);
                    }
                    else result = r;

                }
                BuildStatistic();

                int q = qr.Length;//Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("IList<string> result: {0}\nIList<CountNode> all: {1}", result.Count, all.Count);

                for (int i = 0; i < q; i++)
                {
                    int k = qr[i]; //Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(Find(k));
                }
            }
        }
   
}
