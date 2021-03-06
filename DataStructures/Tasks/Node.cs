namespace Tasks
{
    class Node<T>
    {
        public T Value { get; }

        public Node<T> Next { get; set; }

        public Node<T> Previous { get; set; }

        public Node(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
