namespace AdvancedBinarySearchTree;

internal class Program
{
    private static void Main()
    {
        try
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            tree.Insert(19);
            tree.Insert(11);
            tree.Insert(57);
            tree.Insert(88);
            tree.Insert(34);
            tree.Insert(99);
            tree.Insert(100);
            Console.WriteLine(tree);

            AdditionalTreeFunctions<int>.BalanceTree(tree);
            Console.WriteLine(tree);
            Console.WriteLine(AdditionalTreeFunctions<int>.PrefixTraverse(tree));
            tree.Remove(57);

            Console.WriteLine(AdditionalTreeFunctions<int>.PrefixTraverse(tree));
            Console.WriteLine(tree);
            Console.WriteLine(tree.Find(99));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

