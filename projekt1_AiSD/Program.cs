using System;
using System.Diagnostics;

namespace projekt1_AiSD
{
    class Program
    {
        static ulong OpComparisonEQ;
        static int[] TestVector;
        const int NIter = 10; // liczba powtórzeń testu

        // liniowe instrumentacja:
        static bool IsPresent_LinearInstr(int[] Vector, int Number)
        {
            for (int i = 0; i < Vector.Length; i++)
            {
                OpComparisonEQ++;
                if (Vector[i] == Number) return true;
            }
            return false;
        }

        // liniowe do pomiaru czasu
        static bool IsPresent_LinearTim(int[] Vector, int Number)
        {
            for (int i = 0; i < Vector.Length; i++)
                if (Vector[i] == Number)
                    return true;
            return false;
        }

        // binarne instrumentacja
        static bool IsPresent_BinaryInstr(int[] Vector, int Number)
        {
            int Left = 0, Right = Vector.Length - 1, Middle;
            while (Left <= Right)
            {
                Middle = (Left + Right) / 2;
                OpComparisonEQ++;
                if (Vector[Middle] == Number) return true;
                else
                {
                    if (Vector[Middle] > Number) Right = Middle - 1;
                    else Left = Middle + 1;
                }
            }
            return false;
        }

        // binarne do pomiaru czasu
        static bool IsPresent_BinaryTim(int[] Vector, int Number)
        {
            int Left = 0, Right = Vector.Length - 1, Middle;
            while (Left <= Right)
            {
                Middle = (Left + Right) / 2;
                if (Vector[Middle] == Number) return true;
                else if (Vector[Middle] > Number) Right = Middle - 1;
                else Left = Middle + 1;
            }
            return false;
        }

        // instrumentacja maksymalnego kosztu wyszukiwania liniowego
        static void LinearMaxInstr()
        {
            OpComparisonEQ = 0;
            bool Present = IsPresent_LinearInstr(TestVector, TestVector.Length - 1);
            Console.Write("\t" + OpComparisonEQ);
        }

        // pomiar czasu maksymalnego kosztu wyszukiwania liniowego
        static void LinearMaxTim()
        {
            double ElapsedSeconds;
            long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long StartingTime = Stopwatch.GetTimestamp();
                bool Present = IsPresent_LinearTim(TestVector, TestVector.Length - 1);
                long EndingTime = Stopwatch.GetTimestamp();
                IterationElapsedTime = EndingTime - StartingTime;
                ElapsedTime += IterationElapsedTime;
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
            }
            ElapsedTime -= (MinTime + MaxTime);
            ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
            Console.Write("\t" + ElapsedSeconds.ToString("F4"));
        }

        // instrumentacja maksymalnego kosztu wyszukiwania binarnego
        static void BinaryMaxInstr()
        {
            OpComparisonEQ = 0;
            bool Present = IsPresent_BinaryInstr(TestVector, TestVector.Length - 1);
            Console.Write("\t" + OpComparisonEQ);
        }

        // pomiar czasu maksymalnego kosztu wyszukiwania binarnego 
        static void BinaryMaxTim()
        {
            double ElapsedSeconds;
            long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long StartingTime = Stopwatch.GetTimestamp();
                bool Present = IsPresent_BinaryTim(TestVector, TestVector.Length - 1); 
                long EndingTime = Stopwatch.GetTimestamp();
                IterationElapsedTime = EndingTime - StartingTime;
                ElapsedTime += IterationElapsedTime;
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
            }
            ElapsedTime -= (MinTime + MaxTime);
            ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
            Console.Write("\t" + ElapsedSeconds.ToString("F10")); 
        }

        // pomiar średniego czasu instrumentacji wyszukiwania liniowego
        static void LinearAvgInstr()
        {
            OpComparisonEQ = 0;
            bool Present;
            for (int i = 0; i < TestVector.Length; ++i)
                Present = IsPresent_LinearInstr(TestVector, i);
            Console.Write("\t" + ((double)OpComparisonEQ / (double)TestVector.Length).ToString("F1"));
        }

        static void LinearAvgTim()
        {
            double ElapsedSeconds;
            long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long StartingTime = Stopwatch.GetTimestamp();
                bool Present = IsPresent_LinearTim(TestVector, TestVector.Length - 1);
                long EndingTime = Stopwatch.GetTimestamp();
                IterationElapsedTime = EndingTime - StartingTime;
                ElapsedTime += IterationElapsedTime;
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
            }
            ElapsedTime -= (MinTime + MaxTime);
            ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
            Console.Write("\t\t" + (ElapsedSeconds / (double)TestVector.Length).ToString("F15")); 
        }

        // pomiar średniego czasu instrumentacji wyszukiwania binarnego 
        static void BinaryAvgInstr()
        {
            OpComparisonEQ = 0;
            bool Present;
            for (int i = 0; i < TestVector.Length; ++i)
                Present = IsPresent_BinaryInstr(TestVector, i);
            Console.Write("\t" + ((double)OpComparisonEQ / (double)TestVector.Length - 1).ToString("F1"));
        }

        static void BinaryAvgTim()
        {
            double ElapsedSeconds;
            long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long StartingTime = Stopwatch.GetTimestamp();
                bool Present = IsPresent_BinaryTim(TestVector, TestVector.Length - 1);
                long EndingTime = Stopwatch.GetTimestamp();
                IterationElapsedTime = EndingTime - StartingTime;
                ElapsedTime += IterationElapsedTime;
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
            }
            ElapsedTime -= (MinTime + MaxTime);
            ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));
            Console.Write("\t" + (ElapsedSeconds / (double)TestVector.Length).ToString("F20"));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Size\t\tLMaxI\t\tLMaxT\tBMaxI\tBMaxT");

            for (int ArraySize = 26843545; ArraySize <= 268435450; ArraySize += 26843545)
            {
                Console.Write(ArraySize);
                // tworzymy tablicę
                TestVector = new int[ArraySize];
                // wypełniamy tablicę
                for (int i = 0; i < TestVector.Length; ++i)
                    TestVector[i] = i;

                LinearMaxInstr(); // liniowe max instrumentacja
                LinearMaxTim(); // liniowe max czas
                BinaryMaxInstr(); // binarne max instrumentacja
                BinaryMaxTim(); // binarne max czas

                Console.Write("\n");
            }

            Console.Write("\n");
            Console.WriteLine("Size\tLAvgI\t\tLAvgT\t\t\tBAvgI\tBAvgT");

            for (int ArraySize = 50000; ArraySize <= 500000; ArraySize += 50000)
            {
                Console.Write(ArraySize);
                TestVector = new int[ArraySize];
                for (int i = 0; i < TestVector.Length; ++i)
                    TestVector[i] = i;

                LinearAvgInstr(); // liniowe średnia instrumentacja
                LinearAvgTim(); // liniowe średnia czas
                BinaryAvgInstr(); // binarne średnia instrumentacja
                BinaryAvgTim(); // binarne średnia czas

                Console.Write("\n");
            } 
        }
    }
}
