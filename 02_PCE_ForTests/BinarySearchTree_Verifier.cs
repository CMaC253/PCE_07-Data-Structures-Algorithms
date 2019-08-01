using System;
using System.IO; // For the Console.Out stuff
using System.Text; // For StringBuilder

using NUnit.Framework;

/*
 * This file contains helper classes for the tests.  It does NOT contain any tests itself.
 * 
 * These helper routines are put here, in a separate file, so that it's easy to
 * copy-and-paste this single file between all the different starter projects that get
 * handed out, and yet still have a single, coherent copy of the code.
 * (Yeah, there's probably a better way to do this, but I wanted to keep things simple
 * for y'all :)  )
 */

namespace PCE_StarterProject
{
    public class BinarySearchTree_Verifier : BinarySearchTree
    {
        const int BLANK = Int32.MinValue; // easier to read :)
            // This is a duplicate of the same value in the 'tests to run' project
            // It means that a slot in the array does NOT have a corresponding node in the tree

        private string errorMessage = "";

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="serializedTree"></param>
        /// <returns>True, if the tree matches what's in the array
        ///         False otherwise (including if the array is null)</returns>
        public bool ValidateTree(int[] serializedTree)
        {
            Console.WriteLine("Beginning tree validation");

            if (serializedTree == null)
                return false; // probably an error in the test, but better to notice
            // an extra failure than to pass a bad test

            if (serializedTree.Length == 0)
            {
                if (this.top == null)
                {
                    Console.WriteLine("Real tree is empty, and the array containing the serialized tree is also empty.  (This means that the test has passed)");
                    return true;
                }
                else // tree NOT empty
                {
                    Console.WriteLine("ERROR: Real tree is NOT empty, yet the array containing the serialized tree is also empty.");
                    return false;
                }
            }
            else
            {
                return ValidateTree(this.top, serializedTree, 0);
            }
        }
        /// <summary>
        /// index 0 == top of tree
        /// index 1 = left child of 0
        /// index 2 = right child of 0
        /// index 3 = left child of 1
        /// index 4 = right child of 1
        /// index 5 = left child of 2
        /// index 6 = right child of 2
        /// therefore - 
        /// index left child = ( (index of current + 1) * 2  ) - 1
        /// index right child = ( (index of current + 1) * 2  )
        /// 
        /// Note that extra array slots (added so that there can be a node w/o children, 
        /// yet still have other nodes at that level) contain Int32.MinValue.  They're ignored
        /// in this code
        /// 
        /// </summary>
        /// <param name="cur">This must NOT be null</param>
        /// <param name="serializedTree">Must contain at least ONE value(virtual node)</param>
        /// <param name="nodeIndex">Which node start the process at, where index 0 == top of tree, etc, etc (as described above)</param>
        /// <returns></returns>
        private bool ValidateTree(BinarySearchTree.BSTNode cur, int[] serializedTree, int nodeIndex)
        {
            Console.WriteLine("Current node: " + cur.Data + " nodeIndex: " + nodeIndex + " value@nodeIndex: " + serializedTree[nodeIndex] + (cur.Data == serializedTree[nodeIndex]? "\tThis node contains the expected value":""));

            if (cur.Data != serializedTree[nodeIndex] )
            {
                errorMessage = "Expected node @ index " + nodeIndex + " to have the Data value " + serializedTree[nodeIndex] + ", but actually found " + cur.Data + " instead";
                Console.WriteLine("\t" + errorMessage);
                return false;
            }

            // Deal with the left child ////////////////////////////////////////////////////////

            if (LeftNode(nodeIndex) < serializedTree .Length && 
                serializedTree[LeftNode(nodeIndex)] != BLANK) 
            {
                // serialized tree has a left child, so we
                // expect that the real tree does, too

                if (cur.Left == null )
                {
                    errorMessage = "Expected to find a left child of node @ index " + nodeIndex +".  The left child should have had the value " +serializedTree[LeftNode(nodeIndex)] + " but instead of finding a BST node with that value, we instead found null" ;
                    Console.WriteLine("\t" + errorMessage);
                    return false;
                }

                if( ValidateTree(cur.Left, serializedTree, LeftNode(nodeIndex)) == false)
                    return false;
            }
            else // expect to find NO left child node
            {
                if (cur.Left != null)
                {
                    errorMessage = "Expected NO left child of node with a value of " + cur.Data + " (in array @ index " + nodeIndex + "), but unexpectedly actually found a left child node with value " + cur.Left.Data + " instead";
                    Console.WriteLine("\t" + errorMessage);
                    return false;
                }
                else
                {
                    Console.WriteLine("\tCurrent node has no left child, which is correct");
                }
            }

            // Deal with the right child ///////////////////////////////////////////////////////

            if (RightNode(nodeIndex) < serializedTree.Length &&
                serializedTree[RightNode(nodeIndex)] != BLANK)
            {
                // this is symmetric to the above logic 
                if (cur.Right == null)
                {
                    errorMessage = "Expected to find a right child of node @ index " + nodeIndex + ".  The right child should have had the value " + serializedTree[RightNode(nodeIndex)] + " but instead of finding a BST node with that value, we instead found null";
                    Console.WriteLine("\t" + errorMessage);
                    return false;
                }

                if (ValidateTree(cur.Right, serializedTree, RightNode(nodeIndex)) == false)
                    return false;
            }
            else 
            {
                if (cur.Right != null)
                {
                    errorMessage = "Expected NO right child of node with a value of "+cur.Data+" (in array @ index " + nodeIndex + "), but unexpectedly actually found a right child node with value " + cur.Right.Data + " instead";
                    Console.WriteLine("\t"+errorMessage);
                    return false;
                }
                else
                {
                    Console.WriteLine("\tCurrent node has no right child, which is correct");
                }
            }

            // this node is ok, 
            // any left and/or right children are ok, 
            // so this subtree is ok
            // 
            // return true to indicate that everything matches
            return true;
        }

        // Given the node at index "nodeIndex", 
        // calculate the index of it's left child
        private int LeftNode(int nodeIndex)
        {
            return ((nodeIndex + 1) * 2) - 1;
        }

        // Given the node at index "nodeIndex", 
        // calculate the index of it's right child
        private int RightNode(int nodeIndex)
        {
            return (nodeIndex + 1) * 2;
        }
    }
}