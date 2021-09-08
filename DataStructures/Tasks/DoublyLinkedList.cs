using System;
using System.Collections;
using System.Collections.Generic;

using Tasks.DoNotChange;

namespace Tasks
{
    class NodeReachedArgs<T> : EventArgs
    {
        public bool BreakSearch { get; set; }

        public Node<T> Node { get; }

        public NodeReachedArgs(Node<T> node)
        {
            Node = node;
        }
    }

    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        Node<T> firstNode;
        Node<T> lastNode;

        event Action CollectionIsEmpty;

        event Action<NodeReachedArgs<T>> LastNodeReached;
        event Action<NodeReachedArgs<T>> NodeAtIndexReached;
        event Action<NodeReachedArgs<T>> NodeWithValueReached;

        public int Length { get; private set; }

        public void Add(T item)
        {
            ValidateItem(item);

            var nodeToAdd = new Node<T>(item);

            CollectionIsEmpty += () => InitializeCollectionWithNode(nodeToAdd);
            LastNodeReached += args => AddNode(args, nodeToAdd, true);
            LastNodeReached += BreakSearch;

            IterateNodes(startFromEnd: true);
        }

        public void AddAt(int index, T item)
        {
            if (index == Length)
            {
                Add(item);
                return;
            }

            ValidateIndex(index);
            ValidateItem(item);

            var nodeToAdd = new Node<T>(item);

            CollectionIsEmpty += () => InitializeCollectionWithNode(nodeToAdd);
            NodeAtIndexReached += args => AddNode(args, nodeToAdd, false);
            NodeAtIndexReached += BreakSearch;

            IterateNodes(index: index);
        }

        public T ElementAt(int index)
        {
            ValidateIndex(index);

            T result = default;

            NodeAtIndexReached += args => GetNodeValue(args, ref result);
            NodeAtIndexReached += BreakSearch;

            IterateNodes(index: index);

            return result;
        }

        public void Remove(T item)
        {
            ValidateItem(item);

            NodeWithValueReached += RemoveNode;
            NodeWithValueReached += BreakSearch;

            IterateNodes(item, true);
        }

        public T RemoveAt(int index)
        {
            ValidateIndex(index);

            T result = default;

            NodeAtIndexReached += args => GetNodeValue(args, ref result);
            NodeAtIndexReached += RemoveNode;
            NodeAtIndexReached += BreakSearch;

            IterateNodes(index: index);

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = firstNode;

            for (int i = 0; i < Length; i++)
            {
                yield return currentNode.Value;

                currentNode = currentNode?.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void IterateNodes(T searchValue = default, bool searchValueProvided = false, int? index = null, bool? startFromEnd = null)
        {
            Node<T> GetStartNode(bool reversedOrder) => reversedOrder ? lastNode : firstNode;

            int GetCurrentIndex(bool reversedOrder, int i) => reversedOrder ? Length - 1 - i : i;

            Node<T> GetNextNode(bool reversedOrder, Node<T> currentNode) => reversedOrder ? currentNode?.Previous : currentNode?.Next;

            var reversedOrder = startFromEnd ?? index > (Length / 2);
            var currentNode = GetStartNode(reversedOrder);

            for (int i = 0; i < Length; i++)
            {
                var currentIndex = GetCurrentIndex(reversedOrder, i);

                var args = new NodeReachedArgs<T>(currentNode);

                if (currentIndex == Length - 1)
                    LastNodeReached?.Invoke(args);

                if (currentIndex == index)
                    NodeAtIndexReached?.Invoke(args);

                if (searchValueProvided && currentNode.Value.Equals(searchValue))
                    NodeWithValueReached?.Invoke(args);

                if (args.BreakSearch)
                    break;

                currentNode = GetNextNode(reversedOrder, currentNode);
            }

            if (Length == 0)
                CollectionIsEmpty?.Invoke();

            ClearEvents();
        }

        void ClearEvents()
        {
            CollectionIsEmpty = null;
            LastNodeReached = null;
            NodeAtIndexReached = null;
            NodeWithValueReached = null;
        }

        void InitializeCollectionWithNode(Node<T> nodeToAdd)
        {
            firstNode = nodeToAdd;
            lastNode = nodeToAdd;

            Length++;
        }

        void GetNodeValue(NodeReachedArgs<T> args, ref T result)
        {
            result = args.Node.Value;
        }

        void AddNode(NodeReachedArgs<T> args, Node<T> nodeToAdd, bool addToEnd)
        {
            var nextNode = addToEnd ? null : args.Node;
            var prevNode = addToEnd ? args.Node : args.Node.Previous;

            if (prevNode != null)
                prevNode.Next = nodeToAdd;
            else
                firstNode = nodeToAdd;

            if (nextNode != null)
                nextNode.Previous = nodeToAdd;
            else
                lastNode = nodeToAdd;

            nodeToAdd.Previous = prevNode;
            nodeToAdd.Next = nextNode;

            Length++;
        }

        void RemoveNode(NodeReachedArgs<T> args)
        {
            var prevNode = args.Node.Previous;
            var nextNode = args.Node.Next;

            if (prevNode != null)
                prevNode.Next = nextNode;
            else
                firstNode = nextNode;

            if (nextNode != null)
                nextNode.Previous = prevNode;
            else
                lastNode = prevNode;

            Length--;
        }

        void BreakSearch(NodeReachedArgs<T> args)
        {
            args.BreakSearch = true;
        }

        void ValidateIndex(int index)
        {
            if (index < 0 || index >= Length)
                throw new IndexOutOfRangeException(nameof(index));
        }

        void ValidateItem(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
        }
    }
}
