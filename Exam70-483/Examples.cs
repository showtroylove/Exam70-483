using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam70_483
{
    class Examples
    {
        public void DoIt()
        {
            Console.WriteLine("Example::DoIt() called");
        }
    }

    class MoreExamples : Examples
    {
        new public void DoIt()
        {
            Console.WriteLine("MoreExample::DoIt() called");
        }

        public List<Examples> SomeExamples { get; private set; } 
        public MoreExamples()
        {
            SomeExamples = new List<Examples>() { new Examples(), new Examples() };
        }

        public int SumMatrixDiag()
        {
            var tl = 0;
            var tr = 0;
            List<int[]> nums = new List<int[]> { new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 } };
            for(var i = 0; i < nums.Count; i++)
            {
                tl += nums[i][i];
                tr += nums[i][nums.Count - (i + 1)];
            }

            return Math.Abs(tl - tr);
        }
    }
}
