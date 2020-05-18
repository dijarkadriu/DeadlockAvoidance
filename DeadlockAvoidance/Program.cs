using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DeadlockAvoidance
{
    //basic bankers source: https://www.geeksforgeeks.org/program-bankers-algorithm-set-1-safety-algorithm/
    class Program
    {
        // Number of processes 
        static int P = 5;

        // Number of resources 
        static int R = 3;

        // Function to find the need of each process 
        static void calculateNeed(int[,] need, int[,] maxm,
                        int[,] allot)
        {
            // Calculating Need of each P 
            for (int i = 0; i < P; i++)
                for (int j = 0; j < R; j++)

                    // Need of instance = maxm instance - 
                    //             allocated instance 
                    need[i, j] = maxm[i, j] - allot[i, j];
        }

        // Function to find the system is in safe state or not 
        static bool isSafe(int[] processes, int[] avail, int[,] maxm, int[,] allot)
        {
            var x = new List<int>();
            int[,] need = new int[P, R];

            // Function to calculate need matrix 
            calculateNeed(need, maxm, allot);

            // Mark all processes as infinish 
            bool[] finish = new bool[P];

            // To store safe sequence 
            int[] safeSeq = new int[P];

            // Make a copy of available resources 
            int[] work = new int[R];
            for (int i = 0; i < R; i++)
                work[i] = avail[i];

            // While all processes are not finished 
            // or system is not in safe state. 
            int count = 0;
            bool found = false;
            while (count < P)
            {
                // Find a process which is not finish and 
                // whose needs can be satisfied with current 
                // work[] resources. 

                for (int p = 0; p < P; p++)
                {
                    // First check if a process is finished, 
                    // if no, go for next condition 
                    if (finish[p] == false)
                    {
                        // Check if for all resources of 
                        // current P need is less 
                        // than work 
                        int j;
                        for (j = 0; j < R; j++)
                            if (need[p, j] > work[j])
                            {
                                x.Add(p);
                                count++;
                                break;

                            }

                        // If all needs of p were satisfied. 
                        if (j == R)
                        {
                            // Add the allocated resources of 
                            // current P to the available/work 
                            // resources i.e.free the resources 
                            for (int k = 0; k < R; k++)
                                work[k] += allot[p, k];

                            // Add this process to safe sequence. 
                            safeSeq[count++] = p;
                          //  Console.WriteLine("Process " + p + "executed");
                            found = true;
                        }
                    }

                }

              
                if (found == false)
                {
                    Console.Write("System is not in safe state");
                    return false;
                }

            }

            // If system is in safe state then 
            // safe sequence will be as below 
            //Console.Write("System is in safe state.\nSafe"
            //+ " sequence is: ");
            for (int i = 0; i < x.Count; i++)
            {

               // Console.Write(x[i] + " "); ;
            }

            return true;
        }
        static void Main(string[] args)
        {
            int[] processes = { 0, 1, 2, 3, 4 };

          
            int[] avail = { 3, 3, 2 };

          
            int[,] maxm = {

                    {9, 0, 2},
                    {2, 2, 2},
                    {3, 2, 2},
                    {4, 3, 3},
                    {7, 5, 3}};


           
            int[,] allot = {{0, 1, 0},
                    {2, 0, 0},
                    {3, 0, 2},
                    {2, 1, 1},
                    {0, 0, 2}};

           
            Stopwatch s = new Stopwatch();
            
                Console.WriteLine("Original Bankers");
                s.Start();
                isSafe(processes, avail, maxm, allot);
                s.Stop();
                Console.WriteLine("Time taken " + s.Elapsed);
                
                s.Reset();
                Console.WriteLine("------");
                Console.WriteLine("Bankers with stack");
                s.Start();
                DynamicBankers.isSafe();
                Console.WriteLine("Time taken " + s.Elapsed);
                s.Reset();
                Console.WriteLine("METRR");
                s.Start();
                METRR.isSafe();
                Console.WriteLine("Time taken " + s.Elapsed);
                s.Reset();
                       
        }

    }
    public class AlgorithmsModel
    {
        public int Algorithm { get; set; }
        public TimeSpan ExecTime{ get; set; }
    }
}
