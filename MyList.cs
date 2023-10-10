namespace AdvancedBinarySearchTree;

internal class MyList<T>
{
    private Node<T>? _head;
    public int Count { get; private set; }
    public void Add(T item)
    {
        Node<T> newNode = new Node<T>
        {
            Data = item
        };

        if (_head is null)
        {
            _head = newNode;
        }
        else
        {
            var current = _head;
            while (current.Next is not null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }

        Count++;
    }
    public T RemoveLast()
    {
        if (_head is null)
        {
            throw new InvalidOperationException();
        }

        if (_head.Next is null)
        {
            var data = _head.Data;
            _head = null;
            Count = 0;
            return data!;
        }

        var current = _head;
        while (current.Next!.Next is not null)
        {
            current = current.Next;
        }

        var last = current.Next;
        current.Next = null;
        Count--;
        return last.Data!;
    }
}