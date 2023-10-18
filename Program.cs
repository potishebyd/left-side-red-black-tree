using System;
using System.Threading.Channels;

namespace Tree
{
    enum Color
    {
        Red,
        Black
    }

    class Node
    {
        public int Value { get; set; }
        public Color Color { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    class RedBlackTree
    {
        private Node root;

        public RedBlackTree()
        {
            root = null;
        }

        // Левый поворот
        private Node RotateLeft(Node node)
        {
            Node leftChild = node.Left;
            Node betweenChild = leftChild.Right;
            leftChild.Right = node;
            node.Left = betweenChild;
            leftChild.Color = node.Color;
            node.Color = Color.Red;
            return leftChild;

        }

        // Правый поворот
        private Node RotateRight(Node node)
        {
            Node rightChild = node.Right;
            Node betweenChild = rightChild.Left;
            rightChild.Left = node;
            node.Right = betweenChild;
            rightChild.Color = node.Color;
            node.Color = Color.Red;
            return rightChild;
        }

        // Смена цвета
        private void SwapColors(Node node)
        {
            node.Color = Color.Red;
            node.Left.Color = Color.Black;
            node.Right.Color = Color.Black;
        }

        public bool Insert(int value)
        {
            if (root != null)
            {
                bool result = AddNode(root, value);
                root = Rebalance(root);
                root.Color = Color.Black;
                return result;
            }
            else
            {
                root = new Node();
                root.Color = Color.Black;
                root.Value = value;
                return true;

            }
        }

        private bool AddNode(Node node, int value)
        {
            if (node.Value == value)
            {
                return false;
            }
            else
            {
                if (node.Value > value)
                {
                    if (node.Left != null)
                    {
                        bool result = AddNode(node.Left, value);
                        node.Left = Rebalance(node.Left);
                        return result;
                    }
                    else
                    {
                        node.Left = new Node();
                        node.Left.Color = Color.Red;
                        node.Left.Value = value;
                        return true;

                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        bool result = AddNode(node.Right, value);
                        node.Right = Rebalance(node.Right);
                        return result;
                    }
                    else
                    {
                        node.Right = new Node();
                        node.Right.Color = Color.Red;
                        node.Right.Value = value;
                        return true;

                    }
                }
            }
        }
        private Node Rebalance(Node node)
        {
            Node result = node;
            bool needRebalance;
            do
            {
                needRebalance = false;
                if (result.Right != null && result.Right.Color == Color.Red && (result.Left == null || result.Left.Color == Color.Black))
                {
                    needRebalance = true;
                    result = RotateRight(result);
                }
                if (result.Left != null && result.Left.Color == Color.Red && result.Left.Left != null && result.Left.Left.Color == Color.Red)
                {
                    needRebalance = true;
                    result = RotateLeft(result);
                }
                if (result.Left != null && result.Left.Color == Color.Red && result.Right != null && result.Right.Color == Color.Red)
                {
                    needRebalance = true;
                    SwapColors(result);
                }
            }
            while (needRebalance);
            return result;
        }
        public void InOrderTraversal()
        {
            InOrderTraversal(root);
        }

        private void InOrderTraversal(Node node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.Write(node.Value + "(" + node.Color + ") ");
                InOrderTraversal(node.Right);
            }
        }

    }


    internal class Program
    {
        static void Main()
        {
            RedBlackTree tree = new RedBlackTree();
            int count = 0;
            while (true)
            {
                tree.Insert(Convert.ToInt32(Console.ReadLine()));
                Console.WriteLine();
                count++;
                tree.InOrderTraversal();
            }
        }
    }

}
