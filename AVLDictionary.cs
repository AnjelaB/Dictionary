using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public class NodeAVL<TKey,TValue> where TKey:IComparable
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public int Height
        {
            get
            {
                if (this.RightChild != null && this.LeftChild != null)
                {
                    return LeftChild.Height > RightChild.Height ? LeftChild.Height +  1: RightChild.Height + 1;
                }
                else if(this.RightChild != null || this.LeftChild != null)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            set { }
        }
        public int BalancFactor
        {
            get
            {
                if(this.LeftChild==null && this.RightChild == null)
                {
                    return 0;
                }
                if (this.LeftChild != null && this.RightChild != null)
                {
                    return LeftChild.Height - RightChild.Height ;
                }
                else if (LeftChild != null)
                {
                    return LeftChild.Height;
                }
                else
                {
                    return -RightChild.Height;
                }
            }
        }
        public NodeAVL<TKey, TValue> LeftChild;
        public NodeAVL<TKey, TValue> RightChild;
        public NodeAVL<TKey, TValue> Parent;
        public NodeAVL(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
            this.Height = 1;
        }


    }
    public class AVLDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable
    {
        private NodeAVL<TKey, TValue> root;
        public TValue this[TKey key]
        {
            get
            {
                return Search(key);
            }
            set
            {
                NodeAVL<TKey, TValue> current = root;
                while (current != null)
                {
                    if (key.Equals(current.Key))
                    {
                        current.Value = value;
                    }
                    else if (key.CompareTo(current.Key) > 0)
                    {
                        current = current.RightChild;
                    }
                    else
                    {
                        current = current.LeftChild;
                    }
                }
                if (current == null)
                {
                    throw new Exception("That key doesn't exist.");
                }
            }
        }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public AVLDictionary(TKey key,TValue value)
        {
            root = new NodeAVL<TKey, TValue>(key, value);
        }
         public AVLDictionary()
        {

        }

        /// <summary>
        /// Method to find the value with given key.
        /// It returns value if tree contains node with that , and default value otherwise.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Search(TKey key)
        {
            NodeAVL<TKey, TValue> current = root;
            while (current != null)
            {
                if (key.Equals(current.Key))
                {
                    return current.Value;
                }
                else if (key.CompareTo(current.Key) > 0)
                {
                    current = current.RightChild;
                }
                else
                {
                    current = current.LeftChild;
                }
            }
            return default(TValue);
        }

        public void Add(TKey key, TValue value)
        {
            NodeAVL<TKey, TValue> node = new NodeAVL<TKey, TValue>(key, value);
            NodeAVL<TKey, TValue> current;
            if (this.root == null)
            {
                this.root = node;  
            }
            else
            {
                current = this.root;
                while (current.LeftChild != null && current.RightChild != null)
                {
                    if (current.Key.CompareTo(key) > 0)
                    {
                        current = current.LeftChild;
                    }
                    else
                    {
                        current = current.RightChild;
                    }
                }
                if (current.Key.CompareTo(key) > 0 && current.LeftChild == null)
                {
                    current.LeftChild = node;
                    node.Parent = current;
                    return;
                }
                else if(current.Key.CompareTo(key)<=0 && current.RightChild == null)
                {
                    current.RightChild = node;
                    node.Parent = current;
                    return;
                }
                else if(current.Key.CompareTo(key)>0 && current.LeftChild != null)
                {
                    current = current.LeftChild;
                }
                else
                {
                    current = current.RightChild;
                }
                if (current.Key.CompareTo(key) > 0)
                {
                    current.LeftChild = node;
                    node.Parent = current;
                    current = node;
                    RightRotation(current);
                }
                else
                {
                    current.RightChild = node;
                    node.Parent = current;
                    current = node;
                    LeftRotation(current);
                }
            }
        }

        public void LeftRotation(NodeAVL<TKey,TValue> current)
        {
            if(current==null || current.Parent == null)
            {
                return;
            }
            if (current.Parent.Parent == null)
            {
                current.Parent.RightChild = current.LeftChild;
                current.LeftChild = current.Parent;
                current.Parent = null;
                current.LeftChild.Parent = current;
                root = current;
            }
            else if (current.Parent.Parent.LeftChild == current.Parent)
            {
                current.Parent.Parent.LeftChild = current;
                current.Parent.RightChild = current.LeftChild;
                current.LeftChild = current.Parent;
                current.Parent = current.Parent.Parent;
                current.LeftChild.Parent = current;
                current = current.LeftChild;
                RightRotation(current);
            }
            else
            {
                current = current.Parent;
                if (current.Parent != root)
                {
                    if (current.Parent.Parent.LeftChild == current.Parent)
                    {
                        current.Parent.Parent.LeftChild = current;
                    }
                    else
                    {
                        current.Parent.Parent.RightChild = current;
                    }
                    current.Parent.RightChild = current.LeftChild;
                    current.LeftChild = current.Parent;
                    current.Parent = current.Parent.Parent;
                    current.LeftChild.Parent = current;
                }
                else
                {
                    current.Parent.RightChild = current.LeftChild;
                    current.LeftChild = current.Parent;
                    current.Parent = current.Parent.Parent;
                    current.LeftChild.Parent = current;
                    root = current;
                }
            }


        }
        public void RightRotation(NodeAVL<TKey,TValue> current)
        {
            if (current == null || current.Parent == null)
            {
                return;
            }
            if (current.Parent.Parent == null)
            {
                current.Parent.LeftChild = current.RightChild;
                current.RightChild = current.Parent;
                current.Parent = null;
                current.RightChild.Parent = current;
                root = current;
            }
            else if (current.Parent.Parent.LeftChild == current.Parent)
            {
                current = current.Parent;
                if (current.Parent != root)
                {
                    if (current.Parent.Parent.LeftChild == current.Parent)
                    {
                        current.Parent.Parent.LeftChild = current;
                    }
                    else
                    {
                        current.Parent.Parent.RightChild = current;
                    }
                    current.Parent.LeftChild = current.RightChild;
                    current.RightChild = current.Parent;
                    current.Parent = current.Parent.Parent;
                    current.RightChild.Parent = current;
                }
                else
                {
                    current.Parent.LeftChild = current.RightChild;
                    current.RightChild = current.Parent;
                    current.Parent = current.Parent.Parent;
                    current.RightChild.Parent = current;
                    root = current;
                }
            }
            else
            {
                current.Parent.Parent.RightChild = current;
                current.Parent.LeftChild = current.LeftChild;
                current.RightChild = current.Parent;
                current.Parent = current.Parent.Parent;
                current.RightChild.Parent = current;
                current = current.RightChild;
                LeftRotation(current);

            }
        }


        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key,item.Value);
        }

        public void Balance(NodeAVL<TKey,TValue> node)
        {

            if (node == null)
                return;
            if (node.BalancFactor < -1 && node.RightChild.BalancFactor == -1)
            {
                LeftRotation(node.RightChild.RightChild);
            }
            else if(node.BalancFactor < -1 && node.RightChild.BalancFactor == 1)
            {
                RightRotation(node.RightChild.LeftChild);
            }
            else if(node.BalancFactor < -1)
            {
                LeftRotation(node.RightChild);
            }
            else if(node.BalancFactor > 1 && node.LeftChild.BalancFactor == -1)
            {
                LeftRotation(node.LeftChild.RightChild);
            }
            else if(node.BalancFactor > 1 && node.LeftChild.BalancFactor == 1)
            {
                RightRotation(node.LeftChild.LeftChild);
            }
            else if(node.BalancFactor>1)
            {
                RightRotation(node.LeftChild);
            }

            Balance(node.Parent);
        }

        public void Clear()
        {
            root=null;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            NodeAVL<TKey, TValue> current = this.root;
            while (current != null)
            {
                if (current.Key.Equals(item.Key))
                {
                    if (item.Value.Equals(current.Value))
                        return true;
                    else
                        return false;
                }
                else if (current.Key.CompareTo(item.Key) > 0)
                {
                    current = current.LeftChild;
                }
                else
                {
                    current = current.RightChild;
                }
            }

            return false;
        }

        public bool ContainsKey(TKey key)
        {
            NodeAVL<TKey, TValue> current = this.root;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return true;
                }
                else if(current.Key.CompareTo(key)>0)
                {
                    current = current.LeftChild;
                }
                else
                {
                    current = current.RightChild;
                }
            }

            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            for(int i = arrayIndex; i < array.Length; i++)
            {
                this.Add(array[i]);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool IsLeaf(NodeAVL<TKey,TValue> node)
        {
            if(node.LeftChild==null && node.RightChild == node)
            {
                return true; ;
            }
            return false;
        }

        public NodeAVL<TKey,TValue> Delete(TKey key)
        {
            NodeAVL<TKey, TValue> node;
            NodeAVL<TKey, TValue> current;
            NodeAVL<TKey, TValue> toReturn;
            if (root.Key.Equals(key))
            {
                if (IsLeaf(root))
                {
                    root = null;
                    return null;
                }
                else if (root.RightChild != null)
                {
                    node = this.root;
                    current = root.RightChild;
                    while (current.LeftChild != null)
                    {
                        current = current.LeftChild;
                    }

                    if (current.Parent.LeftChild == current)
                    {
                        toReturn = current.Parent;
                        current.Parent.LeftChild = current.RightChild;
                        root = current;
                        root.RightChild = node.RightChild;
                    }
                    else
                    {
                        toReturn = root;
                        root = current;
                        root.RightChild = current.RightChild;
                    }

                    root.LeftChild = node.LeftChild;
                    return toReturn;
                }
                else
                {
                    root = root.LeftChild;
                    return root;
                }
            }
            else
            {
                current = root;
                while (current!=null  && !current.Key.Equals(key))
                {
                    if (current.Key.CompareTo(key) > 0)
                    {
                        current = current.LeftChild;
                    }
                    else
                    {
                        current = current.RightChild;
                    }
                }
                if (current == null)
                {
                    return null;
                }
                else if (IsLeaf(current))
                {
                    if (current.Parent.LeftChild == current)
                    {
                        current.Parent.LeftChild = null;
                    }
                    else
                    {
                        current.Parent.RightChild = null;
                    }
                    current = current.Parent;
                }
                else if (current.RightChild != null && current.LeftChild != null)
                {
                    node = current.RightChild;
                    while (node.LeftChild != null)
                    {
                        node = node.LeftChild;
                    }
                    toReturn = node.Parent;
                    current.LeftChild.Parent = node;
                    if (node.Parent.LeftChild == node)
                    {
                        node.Parent.LeftChild = node.RightChild;
                        current.RightChild.Parent = node;
                        node.RightChild = current.RightChild;
                    }

                    if (current.Parent.LeftChild == current)
                    {
                        current.Parent.LeftChild = node;
                    }
                    else
                    {
                        current.Parent.RightChild = node;
                    }
                    node.Parent = current.Parent;
                    node.LeftChild = current.LeftChild;
                    return toReturn;
                }
                else if (current.RightChild != null)
                {
                    if (current.Parent.LeftChild == current)
                    {
                        current.Parent.LeftChild = current.RightChild;
                    }
                    else
                    {
                        current.Parent.RightChild = current.RightChild;
                    }
                    if (current.RightChild != null)
                    {
                        current.RightChild.Parent = current.Parent;
                    }
                    return current.Parent;
                }
                else
                {
                    if (current.Parent.LeftChild == current)
                    {
                        current.Parent.LeftChild = current.LeftChild;
                    }
                    else
                    {
                        current.Parent.RightChild = current.LeftChild;
                    }
                    if (current.LeftChild != null)
                    {
                        current.LeftChild.Parent = current.Parent;
                    }
                    return current.Parent;
                }
            }
            return current;

        }

        public bool Remove(TKey key)
        {
            NodeAVL<TKey, TValue> current;
            if (root.Key.Equals(key))
            {
                Delete(key);
                return true;
            }
            current = Delete(key);
            if (current == null)
            {
                return false;
            }
            else
            {
                Balance(current);
            }
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item))
            {
                Remove(item.Key);
                return true;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            PrintInside(root);
        }

        public void PrintInside(NodeAVL<TKey, TValue> element)
        {
            if (element != null)
            {
                Console.WriteLine(element.Value + " " + element.Key + " " + element.Height + " " + element.BalancFactor);
                Console.WriteLine("Left Child: ");
                PrintInside(element.LeftChild);
                Console.WriteLine("Right Child: ");
                PrintInside(element.RightChild);
            }
        }
    }
}
