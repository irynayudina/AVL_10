using System;

namespace AVL_10
{
    public class Node
    {
        public int Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node()
        {

        }
        public Node(int data)
        {
            this.Data = data;

        }
        public override string ToString()
        {
            return $"Data: {Data}";
        }
        public override bool Equals(object obj)
        {
            Node oj = obj as Node;
            if (oj != null && oj.Data == this.Data)
            {
                if (oj.Left == null && this.Left == null && oj.Right == null && this.Right == null)
                {
                    return true;
                }
                if (oj.Left == null && this.Left == null)
                {
                    if (oj.Right != null && this.Right != null)
                    {
                        if (oj.Right.Data == this.Right.Data)
                        {
                            return true;
                        }
                    }
                }
                if (oj.Right == null && this.Right == null)
                {
                    if (oj.Left != null && this.Left != null)
                    {
                        if (oj.Left.Data == this.Left.Data)
                        {
                            return true;
                        }
                    }
                }
                if (oj.Left != null && this.Left != null && oj.Right != null && this.Right != null)
                {
                    if (oj.Left.Data == this.Left.Data && oj.Right.Data == this.Right.Data)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public static bool operator ==(Node n1, Node n2)
        {
            if (object.ReferenceEquals(n1, null))
            {
                return object.ReferenceEquals(n2, null);
            }
            return n1.Equals(n2);
        }
        public static bool operator !=(Node n1, Node n2)
        {
            if (object.ReferenceEquals(n1, null))
            {
                return !object.ReferenceEquals(n2, null);
            }
            return !n1.Equals(n2);
        }
    }
    public class BinaryTree
    {
        private Node _root;
        public BinaryTree()
        {
            _root = null;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////INSERT////////////////////////////     
        public void Insert(int data)
        {
            Node newItem = new Node(data);
            if (_root == null)
            {
                _root = newItem;
            }
            else
            {
                _root = InsertRec(_root, newItem);
            }
        }
        private Node InsertRec(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.Data < current.Data)
            {
                current.Left = InsertRec(current.Left, n);
                current = Balance(current);
            }
            else if (n.Data >= current.Data)
            {
                current.Right = InsertRec(current.Right, n);
                current = Balance(current);
            }
            return current;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////REMOVE//////////////////////
        public void Remove(int value)
        {
            _root = Remove(_root, value);
        }
        private Node Remove(Node parent, int key)
        {
            if (parent == null) return parent;

            if (key < parent.Data) {
                parent.Left = Remove(parent.Left, key);
                if (balance_factor(parent) == -2)
                {
                    if (balance_factor(parent.Right) <= 0)
                    {
                        parent = RRcaseLeftOrdinary(parent);
                    }
                    else
                    {
                        parent = RLcaseLeftBig(parent);
                    }
                }
            }
                
            else if (key > parent.Data)
            {
                parent.Right = Remove(parent.Right, key);
                if (balance_factor(parent) == 2)
                {
                    if (balance_factor(parent.Left) >= 0)
                    {
                        parent = LLcaseRightOrdinary(parent);
                    }
                    else
                    {
                        parent = LRcaseRightBig(parent);
                    }
                }
            }
                
            // if value is same as parent's value, then this is the node to be deleted  
            else
            {
                // node with only one child or no child  
                if (parent.Left == null)
                    return parent.Right;
                else if (parent.Right == null)
                    return parent.Left;
                // node with two children: Get the inorder successor (smallest in the right subtree)  
                parent.Data = MinValue(parent.Right);
                // Delete the inorder successor  
                parent.Right = Remove(parent.Right, parent.Data);
                parent = Balance(parent);

            }
            return parent;
        }
        private int MinValue(Node node)
        {
            int minv = node.Data;
            while (node.Left != null)
            {
                minv = node.Left.Data;
                node = node.Left;
            }
            return minv;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////PRINT//////////////////////
        public Node Root { get { return _root; } }
        public void Print()
        {
            Root.Print();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////FIND BY KEY STRAIGHT preORDER//////
        public void TraversePreOrder(Node parent)
        {
            if (parent != null)
            {
                Console.Write(parent.Data + " ");
                TraversePreOrder(parent.Left);
                TraversePreOrder(parent.Right);
            }

        }
        public Node Find(int value)
        {
            if (this.Find(value, this._root) == null)
            {
                Console.WriteLine("Node is not found");
            }
            return this.Find(value, this._root);
        }
        private Node Find(int value, Node parent)
        {
            if (parent != null)
            {
                if (value == parent.Data) return parent;
                if (value < parent.Data)
                    return Find(value, parent.Left);
                else
                    return Find(value, parent.Right);
            }

            return null;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////FIND BY KEY BACKWARDS postORDER/////
        public void TraversePostOrder(Node parent)
        {
            if (parent != null)
            {
                TraversePostOrder(parent.Left);
                TraversePostOrder(parent.Right);
                Console.Write(parent.Data + " ");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////FIND MIN//////////////////////
        public Node FindMin()
        {
            return Min(_root);
        }
        private Node Min(Node node)
        {
            int minv;
            if (node != null)
            {
                minv = node.Data;

                while (node.Left != null)
                {
                    minv = node.Left.Data;
                    node = node.Left;
                }
            }
            return node;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////FIND MAX///////////////////////
        public Node FindMax()
        {
            return Max(_root);
        }
        private Node Max(Node node)
        {
            int minv;
            if (node != null)
            {
                minv = node.Data;

                while (node.Right != null)
                {
                    minv = node.Right.Data;
                    node = node.Right;
                }
            }
            return node;
        }
        //////////////////////////////////////////////////////////////////GET LEVEL OF NODE////////////////////////////////////////
        private int getLevelUtil(Node root, Node searched, int level)
        {
            if (root == null)
            {
                return 0;
            }

            if (root == searched)
            {
                return level;
            }

            int downlevel = getLevelUtil(root.Left, searched, level + 1);
            if (downlevel != 0)
            {
                return downlevel;
            }

            downlevel = getLevelUtil(root.Right, searched, level + 1);
            return downlevel;
        }
        public int getLevel(Node node, Node searched)
        {
            return getLevelUtil(node, searched, 1);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////balance
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.Left);
                int r = getHeight(current.Right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(Node current)
        {
            int l = getHeight(current.Left);
            int r = getHeight(current.Right);
            int b_factor = l - r;
            return b_factor;
        }
        public Node LLcaseRightOrdinary(Node root)
        {
            Node newnode = root.Left;
            root.Left = newnode.Right;
            newnode.Right = root;
            return newnode;
        }
        public Node RRcaseLeftOrdinary(Node root)
        {
            Node newnode = root.Right;
            root.Right = newnode.Left;
            newnode.Left = root;
            return newnode;
        }
        public Node LRcaseRightBig(Node root)
        {
            Node newnode = root;
            newnode.Left = RRcaseLeftOrdinary(newnode.Left);
            newnode = LLcaseRightOrdinary(newnode);
            return newnode;
        }
        public Node RLcaseLeftBig(Node root)
        {
            Node newnode = root;
            newnode.Right = LLcaseRightOrdinary(newnode.Right);
            newnode = RRcaseLeftOrdinary(newnode);
            return newnode;
        }
        public Node Balance(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.Left) >= 0)
                {
                    current = LLcaseRightOrdinary(current);
                }
                else
                {
                    current = LRcaseRightBig(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.Right) > 0)
                {
                    current = RLcaseLeftBig(current);
                }
                else
                {
                    current = RRcaseLeftOrdinary(current);
                }
            }
            return current;
        }
    }
    
    public static class BTreePrinter
    {
        class NodeInfo
        {
            public Node NoDe;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }

        public static void Print(this Node root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new SingleLinkedList<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { NoDe = next, Text = next.Data.ToString(textFormat) };
                if (level < last.Count())
                {
                    item.StartPos = last[level].data.EndPos + spacing;
                    last[level].data = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.InsertLast(last, item);//Add(item)
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1].data;
                    if (next == item.Parent.NoDe.Left)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                    }
                }
                next = next.Left ?? next.Right;
                for (; next == null; item = item.Parent)
                {
                    int top = rootTop + 2 * level;
                    Print(item.Text, top, item.StartPos);
                    if (item.Left != null)
                    {
                        Print("/", top + 1, item.Left.EndPos);
                        Print("_", top, item.Left.EndPos + 1, item.StartPos);
                    }
                    if (item.Right != null)
                    {
                        Print("_", top, item.EndPos, item.Right.StartPos - 1);
                        Print("\\", top + 1, item.Right.StartPos - 1);
                    }
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos + 1;
                        next = item.Parent.NoDe.Right;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos - 1;
                        else
                            item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count() - 1);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree();
            string line;
            int a = 0, b = 0;
            while (a != 9)
            {
                Console.WriteLine("choose action: \n1-add\n2-print\n3-remove\n4-traverse prefix\n5-traverse postfix\n6-find min\n7-find max\n8-find with algorithm\n9-exit");
                line = Console.ReadLine();
                a = int.Parse(line);
                switch (a)
                {
                    case 1:
                        Console.WriteLine("enter value: ");
                        line = Console.ReadLine();
                        b = int.Parse(line);
                        tree.Insert(b);
                        break;
                    case 2:
                        tree.Root.Print();
                        break;
                    case 3:
                        Console.WriteLine("enter value: ");
                        line = Console.ReadLine();
                        b = int.Parse(line);
                        tree.Remove(b);
                        break;
                    case 4:
                        tree.TraversePreOrder(tree.Root);
                        Console.WriteLine();
                        break;
                    case 5:
                        tree.TraversePostOrder(tree.Root);
                        Console.WriteLine();
                        break;
                    case 6:
                        Console.WriteLine(tree.FindMin());
                        break;
                    case 7:
                        Console.WriteLine(tree.FindMax());
                        break;
                    case 8:
                        Console.WriteLine("enter value: ");
                        line = Console.ReadLine();
                        b = int.Parse(line);
                        Node n1 = tree.Find(b);
                        Console.WriteLine(n1 + $" Level: {tree.getLevel(tree.Root, n1)}");
                        break;
                }
            }
        }
    }
}
