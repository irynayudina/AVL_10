using System;
using System.Collections.Generic;
using System.Text;

namespace AVL_10
{
    internal class NODE<T>
    {
        public T data;
        public NODE<T> next;
        public NODE(T d)
        {
            data = d;
            next = null;
        }
    }
    class SingleLinkedList<Type>
    {
        private NODE<Type> head;

        public void InsertLast(SingleLinkedList<Type> singlyList, Type new_data)
        {
            NODE<Type> new_node = new NODE<Type>(new_data);
            if (singlyList.head == null)
            {
                singlyList.head = new_node;
                return;
            }
            NODE<Type> lastNode = GetLastNode(singlyList);
            lastNode.next = new_node;
        }
        public NODE<Type> GetLastNode(SingleLinkedList<Type> singlyList)
        {
            NODE<Type> temp = singlyList.head;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            return temp;
        }
        public int Count()
        {
            int n = 0;
            NODE<Type> temp = head;
            while (temp != null)
            {
                temp = temp.next;
                n += 1;
            }
            return n;
        }
        public NODE<Type> this[int i]
        {
            get
            {
                int k = 0;
                NODE<Type> temp = head;
                while (temp != null)
                {
                    if (k == i)
                    {
                        return temp;
                    }
                    k++;
                    temp = temp.next;
                }
                return null;
            }
            set
            {
                int k = 0;
                NODE<Type> temp = head;
                while (temp != null)
                {
                    if (k == i)
                    {
                        temp = value;
                    }
                    k++;
                    temp = temp.next;
                }

            }
        }

    }
}
