using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace DeadlockAvoidance
{
    public class DynamicBankers
    {
        static Stack<int> stack = new Stack<int>();
        public static int P = 5;

        // Number of resources 
        public static int R = 3;
        static int[,] need = new int[P, R];
        static void calculateNeed(int[,] maxm, int[,] allot)
        {

            for (int i = 0; i < P; i++)
                for (int j = 0; j < R; j++)


                    need[i, j] = maxm[i, j] - allot[i, j];
        }

        // Function to find the system is in safe state or not 
        public static bool isSafe()
        {         
            int[] avail = { 3, 3, 2 };
          
            int[,] maxm = {{4, 4, 1},

                    {9, 0, 2},
                    {2, 2, 2},
                    {3, 2, 2},
                    {4, 3, 3}};
            int[,] allot = {{0, 1, 0},
                    {2, 0, 0},
                    {3, 0, 2},
                    {2, 1, 1},
                    {0, 0, 2}};


         
            calculateNeed(maxm, allot);
      

            int[] safeSeq = new int[P];
          
            int[] work = new int[R];
            for (int i = 0; i < R; i++)
                work[i] = avail[i];

            bool found = false;
            int count = 0;

            while (count < P)
            {

                for (int p = 0; p < P; p++)
                {                    
                        int j;
                        for (j = 0; j < R; j++)
                        {
                            if (need[p, j] > work[j])
                            {
                                stack.Push(p);
                                count++;
                                break;
                            }
                        }

                        if (j == R)
                        {
                            for (int k = 0; k < R; k++)
                            {
                                work[k] += allot[p, k];
                            }
                            safeSeq[count++] = p;
                            found = true;

                            //Console.WriteLine("Process " + p + "executed");
                        }
                    }              


                if (found == false)
                {
                    Console.Write("System is not in safe state");
                    return false;
                }
            }
            //Console.WriteLine("System is in safe state.\nSafe"
            //+ " sequence is: ");
            var x = sortStack(stack);
            var y = new Stack<int>(x);
            //for (int i = 0; i < x.Count; i++)
            //{
            //    Console.WriteLine("Exectued from stack " + y.Pop() + " "); ;
            //}
            return true;
        }
        public static Stack<int> sortStack(Stack<int> input)
        {
            List<int> tempList = new List<int>();
            List<TempModel> memberList = new List<TempModel>();
            Stack<int> a = new Stack<int>();
            Stack<int> b = new Stack<int>(input);
            while(b.Count != 0)
            {
                var x = b.Pop();
                memberList.Add(new TempModel
                {
                    Process = x,
                    Need = new int[3] {
                        need[x, 0],
                        need[x, 1],
                        need[x, 2]

                    }
                });
            }

           
            memberList = memberList.OrderBy(x => x.Need.Sum()).ToList();
            for (int i = 0; i < memberList.Count; i++)
            {
                a.Push(memberList[i].Process);
            }
            return a;
        }

    }
    public class TempModel
    {
        public int Process { get; set; }
        public int[] Need { get; set; }
    }
}
