using System;
using NUnit.Framework;

/*
 * This file contains all the tests that will be run.
 * 
 * If you want to find out what a test does (or why it's failing), look in here
 * 
 */

namespace PCE_StarterProject
{
    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    public class NUnit_Tests_BST_Add_Iterative : TestHelpers
    {
        // By using this 'Verifier" subclass, we can make use of the ValidateTree method
        // ValidateTree can access the proteced members of the BST, most importantly
        // the BST nodes themselves.  Therefore, it can verify that the actual 
        // STRUCTURE of the tree is correct (not just that it prints out the right thing)
        BinarySearchTree_Verifier tree;

        [SetUp]
        public void SetUp()
        {
            tree = new BinarySearchTree_Verifier();
        }

        const int BLANK = Int32.MinValue; // easier to read :)
        [TestCase(new int[] { })]
        [TestCase(new int[] { 100 })]
        [TestCase(new int[] { 100, 50 })]
        [TestCase(new int[] { 100, BLANK, 200 })]
        [TestCase(new int[] { 100, 50, 200 })]
        [TestCase(new int[] { 100, 50, 200, BLANK, BLANK, BLANK, 250 })]
        [TestCase(new int[] { 100, 50, 200, BLANK, BLANK, 150, 250 })]
        [TestCase(new int[] { 100, 50, 200, 20, 75, 150, 250 })]
        [Category("BST Add Iterative")]
        public void Add_Values(int[] valuesToAdd)
        {
            for (int i = 0; i < valuesToAdd.Length; i++)
                if (valuesToAdd[i] != BLANK)
                    tree.Add(valuesToAdd[i]);

            Console.WriteLine("The array (these are the values the tree should contain):");
            PrintArray(valuesToAdd);

            bool result = tree.ValidateTree(valuesToAdd);
            Assert.That(result == true, "Problem verifying tree for test case " + Array_ToString(valuesToAdd) +
                "Error message: " + tree.ErrorMessage);
        }
    }

    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    public class NUnit_Tests_BST_Find_Iterative : TestHelpers
    {
        BinarySearchTree tree;

        [SetUp]
        public void SetUp()
        {
            tree = new BinarySearchTree();
            tree.Add(1000);
            tree.Add(500);
            tree.Add(1500);
            tree.Add(100);
            tree.Add(300);
        }

        [Test]
        [Category("BST Find Iterative")]
        public void Find_Present([Values(1000, 500, 1500, 100, 300)]int targetThatIsPresentInTheTree)
        {
            Assert.That(tree.Find(targetThatIsPresentInTheTree),
                "Unable to find " + targetThatIsPresentInTheTree + ", despite the fact that the value is in the tree!");
        }

        [Test]
        [Category("BST Find Iterative")]
        public void Find_Absent([Values(1001, -500, 150000, 1400, 600, 99, 199, 301)]int targetThatIsNOTPresentInTheTree)
        {
            Assert.That(false == tree.Find(targetThatIsNOTPresentInTheTree),
                "Somehow found " + targetThatIsNOTPresentInTheTree + ", despite the fact that the value is NOT in the tree!");
        }

        [Test]
        [Category("BST Find Iterative")]
        public void Find_In_Empty_Tree()
        {
            tree = new BinarySearchTree();
            int target = 10;
            Assert.That(false == tree.Find(target),
                "Somehow found " + target + ", despite the fact that the tree is empty!");
        }
    }

}