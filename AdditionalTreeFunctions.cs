namespace AdvancedBinarySearchTree;

internal static class AdditionalTreeFunctions<T> where T : struct, IComparable<T>
{
    public static T FindMinValue(TreeNode<T> node)
    {
        var minValue = node.Data;
        while (node.Left is not null)
        {
            minValue = node.Left.Data;
            node = node.Left;
        }
        return minValue;
    }

    public static string InfixTraverse(BinarySearchTree<T> tree) => InfixTraverse(tree.Root);
    private static string InfixTraverse(TreeNode<T>? node)
    {
        if (node is null) return string.Empty;

        var output = InfixTraverse(node.Left);
        output += node.Data + " ";
        output += InfixTraverse(node.Right);

        return output;
    }

    public static string PrefixTraverse(BinarySearchTree<T> tree) => PrefixTraverse(tree.Root);
    private static string PrefixTraverse(TreeNode<T>? node)
    {
        if (node is null) return string.Empty;

        var output = node.Data + " ";
        output += PrefixTraverse(node.Left);
        output += PrefixTraverse(node.Right);

        return output;
    }

    public static string PostfixTraverse(BinarySearchTree<T> tree) => PostfixTraverse(tree.Root);
    private static string PostfixTraverse(TreeNode<T>? node)
    {
        if (node is null) return string.Empty;

        var output = InfixTraverse(node.Left);
        output += InfixTraverse(node.Right);
        output += node.Data + " ";

        return output;
    }

    public static void FillTwoDimensionalArray(string?[,] tree, TreeNode<T>? node, int level, int left, int right)
    {
        while (true)
        {
            if (node is null) return;

            var middle = (left + right) / 2;
            tree[level, middle] = node.Data.ToString();

            FillTwoDimensionalArray(tree, node.Left, level + 1, left, middle - 1);
            node = node.Right;
            level += 1;
            left = middle + 1;
        }
    }

    public static int GetTreeHeight(TreeNode<T>? node)
    {
        if (node is null) return 0;

        var leftHeight = GetTreeHeight(node.Left);
        var rightHeight = GetTreeHeight(node.Right);

        return Math.Max(leftHeight, rightHeight) + 1;
    }

    private static int GetBalance(TreeNode<T>? node)
    {
        if (node is null) return 0;

        var leftHeight = GetTreeHeight(node.Left);
        var rightHeight = GetTreeHeight(node.Right);

        return leftHeight - rightHeight; 
    }

    public static void BalanceTree(BinarySearchTree<T> tree)
    {
        if (tree.Root is null) throw new InvalidOperationException("Tree is empty.");

        switch (GetBalance(tree.Root))
        {
            case > 1 when GetBalance(tree.Root.Left) >= 0: 
                tree.Root = SmallRightRotation(tree.Root);
                return;
            case > 1:
                tree.Root = BigRightRotation(tree.Root);
                return;
            case < -1 when GetBalance(tree.Root.Right) <= 0:
                tree.Root = SmallLeftRotation(tree.Root);
                return;
            case < -1:
                tree.Root = BigLeftRotation(tree.Root);
                return;
            default:
                return;
        }
    }

    private static TreeNode<T> SmallLeftRotation(TreeNode<T> root)
    {
        var newRoot = root.Right;
        root.Right = newRoot!.Left;
        newRoot.Left = root;
        return newRoot;
    }

    private static TreeNode<T> SmallRightRotation(TreeNode<T> root)
    {
        var newRoot = root.Left;
        root.Left = newRoot!.Right;
        newRoot.Right = root;
        return newRoot;
    }

    private static TreeNode<T> BigLeftRotation(TreeNode<T> root)
    {
        root.Left = SmallLeftRotation(root.Left!);
        return SmallRightRotation(root);
    }

    private static TreeNode<T> BigRightRotation(TreeNode<T> root)
    {
        root.Right = SmallRightRotation(root.Right!);
        return SmallLeftRotation(root);
    }
}