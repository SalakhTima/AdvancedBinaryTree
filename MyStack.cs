namespace AdvancedBinarySearchTree;

internal class MyStack<T>
{
    private readonly int _size;
    private readonly MyList<T> _list;
    public MyStack(int size)
    {
        if (size <= 0)
        {
            throw new InvalidOperationException();
        }
        _size = size;
        _list = new MyList<T>();
    }

    public MyStack(MyStack<T> current) => _list = current._list;

    public void Push(T item)
    {
        if (_list.Count == _size)
        {
            throw new InvalidOperationException();
        }
        _list.Add(item!);
    }

    public T Pop() => _list.RemoveLast();

    public T? Peek()
    {
        if (_list.Count is 0) return default;

        var cache = _list.RemoveLast();
        _list.Add(cache);
        return cache;
    }
}