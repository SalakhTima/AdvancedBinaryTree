namespace AdvancedBinarySearchTree;

internal class Program
{
    private static void Main()
    {
        try
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            tree.Insert(19);            
            tree.Insert(20);
            tree.Insert(21);
            tree.Insert(22);
            tree.Insert(11);

            BinarySearchTree<int> tree2 = tree + 10;

            BinarySearchTree<int> tree3 = tree2 + tree;

            Console.WriteLine(tree3);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
