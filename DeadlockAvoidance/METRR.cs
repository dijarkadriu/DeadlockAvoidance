using System;
using System.Collections.Generic;
using System.Text;

namespace DeadlockAvoidance
{
    public static class METRR
    {
        static PriorityQueue<MEETRTempModel> pq = new PriorityQueue<MEETRTempModel>();
        public static int P = 5;

        // Number of resources 
        public static int R = 3;
        static int[,] need = new int[P, R];
        static void calculateNeed(int[,] maxm, int[,] allot)
        {

            for (int i = 0; i < pq.Count; i++)
                for (int j = 0; j < R; j++)


                    need[i, j] = maxm[i, j] - allot[i, j];
        }

        // Function to find the system is in safe state or not 
        public static bool isSafe()
        {
            var x = new List<int>();
            for (int i = 0; i < P; i++)
            {
                var pqElement = new MEETRTempModel() { Priority = i, Process = i };
                pq.Enqueue(pqElement.Priority, pqElement);
            }




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


            int count = 0;

            while (count < pq.Count)
            {

                for (int p = 0; p < pq.Count; p++)
                {
                    int j;
                    for (j = 0; j < R; j++)
                    {
                        if (need[p, j] > work[j])
                        {

                            count++;
                            x.Add(p);
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



                        Console.Write( p + " ");
                    }
                }



            }


            for (int i = 0; i < x.Count; i++)
            {
                Console.Write(x[i] + " ");
            }


            return true;
        }


    }
    public class MEETRTempModel
    {
        public int Process { get; set; }
        public int Priority { get; set; }
    }

}
