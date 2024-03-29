#define TESTING
using System;
using System.Collections.Generic;

/*
 * STUDENTS: Your answers (your code) goes into this file!!!!
 * 
  * NOTE: In addition to your answers, this file also contains a 'main' method, 
 *      in case you want to run a normal console application.
 * 
 * If you want to / have to put something into 'Main' for these PCEs, then put that 
 * into the Program.Main method that is located below, 
 * then select this project as startup object 
 * (Right-click on the project, then select 'Set As Startup Object'), then run it 
 * just like any other normal, console app: use the menu item Debug->Start Debugging, or 
 * Debug->Start Without Debugging
 * 
 */

namespace PCE_StarterProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, world!");

            Two_Pow_Implementations tpi = new Two_Pow_Implementations();
            tpi.RunExercise();
        }
    }

    class Two_Pow_Implementations
    {
        //  Pow1 will multiply 1 by the base, then equal that number.
        //  The for loop will iterate this process until the exponent is reached.
        //  The results are 1,3,27,64,289 and -8
        //  O(N) time and space
        //
        //  Pow2 will check if exponent is 0 or 1, where the  answer will be 1 or base respectively
        //  then if the exponent is not even we multiply the base by the base however many times
        //  the exponent minus 1. Otherwise it will return the base times the base and divide the exponent by 2  
        //  until we can't recurse any more
        //  O(N) for space O(1) for time.
        // 

        // Note: if you want to run this code from this project's Main (in the Program.cs file),
        // you'll need to set this project as the startup object (in the Solution Explorer, 
        // right click on the project, then select 'Set As StartUp Project'
        public void RunExercise()
        {
            Console.WriteLine("  ******   Pow1 Tests: ******\n");
            Console.WriteLine("3^0: {0}", Two_Pow_Implementations.Pow1(3, 0));
            Console.WriteLine("3^1: {0}", Two_Pow_Implementations.Pow1(3, 1));
            Console.WriteLine("3^3: {0}", Two_Pow_Implementations.Pow1(3, 3));

            Console.WriteLine("2^6: {0}", Two_Pow_Implementations.Pow1(2, 6));
            Console.WriteLine("17^2: {0}", Two_Pow_Implementations.Pow1(17, 2));
            Console.WriteLine("-2^3: {0}", Two_Pow_Implementations.Pow1(-2, 3));


            Console.WriteLine("  ******   Pow2 Tests: ******\n");
            Console.WriteLine("3^0: {0}", Two_Pow_Implementations.Pow2(3, 0));
            Console.WriteLine("3^1: {0}", Two_Pow_Implementations.Pow2(3, 1));
            Console.WriteLine("3^3: {0}", Two_Pow_Implementations.Pow2(3, 3));

            Console.WriteLine("2^6: {0}", Two_Pow_Implementations.Pow2(2, 6));
            Console.WriteLine("17^2: {0}", Two_Pow_Implementations.Pow2(17, 2));
            Console.WriteLine("-2^3: {0}", Two_Pow_Implementations.Pow2(-2, 3));

            Console.WriteLine();
        }

        //  This method calculates bas raised to the power of exponent 
        //      (aka bas ^ exponent)
        //  It does this iteratively
        public static double Pow1(double bas, uint exponent)
        {
            double retVal = 1;
            for (int i = 0; i < exponent; i++)
            {
                retVal *= bas;
            }
            return retVal;
        }

        //  This method also calculates bas raised to the power of exponent 
        //      (aka bas ^ exponent)
        //  It does this recursively
        public static double Pow2(double bas, uint exponent)
        {
            if (exponent == 0)
                return 1;
            if (exponent == 1)
                return bas;

            // Assume that IsPowerOfTwo returns true when
            // exponent is an even power of 2 – 2, 4, 8, 16, 32, 64, etc
            // and false when exponent is anything else
            // Assume that it will run in constant time (if you’ve seen 
            // Big Oh notation – if not, ignore this last line ?  )
            if (IsPowerOfTwo(exponent) != true)
                return bas * Pow2(bas, exponent - 1);
            else
                return Pow2(bas * bas, exponent / 2);
        }

        private static bool IsPowerOfTwo(uint num)
        {
            // In a nutshell, a number is an even power of two if there's exactly
            // one (or zero) bit set.

            const int NUM_BITS_IN_UINT = 64;
            uint bitMask = 1;
            bool foundBit = false;

            for (int i = 0; i < NUM_BITS_IN_UINT; i++)
            {
                if ((bitMask & num) != 0) // note that single & is bitwise-AND
                {
                    if (!foundBit)
                        foundBit = true;
                    else
                        return false;
                }
            }
            return true;
        }
    }

    public class BinarySearchTree
    {
        // Protected so that the BST_Verifier subclass can get at it
        // and verify that the tree looks right (for NUnit tests)
        // Normally this should be private.
        protected BSTNode top; // also (semi-)traditional to name this 'root', instead of 'top'

        // You should make the BSTNode class a nested class
        // Normally should be private; it's protected for the reasons explained above
        protected class BSTNode
        {
            public BSTNode Left;
            public BSTNode Right;
            public int Data;
            public BSTNode(int val)
            {
                Data = val;
            }
        }

        public void Add(int newValueToAdd)
        {
            if (top == null)
            {
              top = new BSTNode(newValueToAdd);
              return;
            }

            BSTNode cur = top;
            while(true)
              {
                if (newValueToAdd < cur.Data)
                {
                    if (cur.Left == null)
                    {
                        cur.Left = new BSTNode(newValueToAdd);
                        break;
                    }
                    else
                        cur = cur.Left;
                }
                else if (newValueToAdd > cur.Data)
                {
                    if (cur.Right == null)
                    {
                        cur.Right = new BSTNode(newValueToAdd);
                        break;
                    }
                    else
                        cur = cur.Right;
                }
                else
                    throw new Exception("No Duplicates!");
            }
        }

        public bool Find(int target)
        {
            if (top == null)
                return false;

            BSTNode cur = top;
            while(cur!= null)
            {
                if (target == cur.Data)
                    return true;
                else if (target > cur.Data)
                    cur = cur.Right;
                else if (target < cur.Data)
                    cur = cur.Left;
            }


            return false;
        }
    }
}