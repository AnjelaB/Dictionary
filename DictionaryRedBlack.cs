using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{


    /// <summary>
    /// Dictionary which elements saves in red-black tree.
    /// </summary>
    /// <typeparam name="TKey">Type of Dictionary key.</typeparam>
    /// <typeparam name="TValue">Type of Dictionary value.</typeparam>
    public class DictionaryRedBlack<TKey, TValue>:  IDictionary<TKey,TValue> where TKey: IComparable
    {
        
        Node<TKey, TValue> root;
        Color col;
        public TValue this[TKey key]
        {
            get
            {
                return Search(key);
            }
            set
            {
                Node<TKey, TValue> current = root;
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

        public ICollection<TKey> Keys
        {
            get
            {
                return Keys;
            }
            set
            {

            }
        }

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count { get; }

        public bool IsReadOnly => throw new NotImplementedException();

        /// <summary>
        /// Parametrless constructor.
        /// </summary>
        public DictionaryRedBlack()
        {
            root = null;
            Count = 0;
        }

        /// <summary>
        /// Constructor with Parametres to create a root of tree with Key and Value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public DictionaryRedBlack(TKey key,TValue value)
        {
            root = new Node<TKey, TValue>(key, value);
            root.color = Color.Black;
            Count = 1;
        }

        /// <summary>
        /// Method to find the value with given key.
        /// It returns value if tree contains node with that , and default value otherwise.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Search(TKey key)
        {
            Node<TKey, TValue> current = root;
            while (current != null)
            {
                if (key.Equals(current.Key))
                {
                    return current.Value;
                }
                else if (key.CompareTo(current.Key)>0)
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



        public void Recolor(Node<TKey, TValue> current)
        {
            if (current == null)
            {
                return;
            }
            if (current == root)
            {
                current.color = Color.Black;
                return;
            }
            if (current.Parent.color == Color.Black)
                return;

            if ((current.Parent.Parent.LeftChild == current.Parent && current.Parent.Sibling != null && current.Parent.Sibling.color == Color.Red) || (current.Parent.Parent.RightChild == current.Parent && current.Parent.Sibling != null && current.Parent.Sibling.color == Color.Red))
            {
                current.Parent.color = Color.Black;
                current.Parent.Sibling.color = Color.Black;
                current.GrandParent.color = Color.Red;
                current = current.GrandParent;
                Recolor(current);
            }
            else if(current.Parent.LeftChild == current)
            {
                RightRotation(current);
            }
            else
            {
                LeftRotation(current);
            }
            
        }
        public void LeftRotation(Node<TKey, TValue> current)
        {
            if (current.Parent.Parent.LeftChild == current.Parent)
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
                    current.color = Color.Black;
                    current.LeftChild.color = Color.Red;
                }
                else
                {
                    current.Parent.RightChild = current.LeftChild;
                    current.LeftChild = current.Parent;
                    current.Parent = current.Parent.Parent;
                    current.LeftChild.Parent = current;
                    current.color = Color.Black;
                    current.LeftChild.color = Color.Red;
                    root = current;
                }
            }


        }
         public void RightRotation(Node<TKey, TValue> current)
        {
            if(current==null || current.Parent == null)
            {
                return;
            }
            else if (current.Parent == root)
            {
                current.Parent.LeftChild = current.RightChild;
                current.RightChild = current.Parent;
                current.Parent = null;
                current.RightChild.Parent = current;
                current.color = Color.Black;
                current.RightChild.color = Color.Red;
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
                    current.color = Color.Black;
                    current.RightChild.color = Color.Red;
                }
                else
                {
                    current.Parent.LeftChild = current.RightChild;
                    current.RightChild = current.Parent;
                    current.Parent = current.Parent.Parent;
                    current.RightChild.Parent = current;
                    current.color = Color.Black;
                    current.RightChild.color = Color.Red;
                    root = current;
                }
            }
            else
            {
                current.Parent.Parent.RightChild= current;
                current.Parent.LeftChild = current.LeftChild;
                current.RightChild = current.Parent;
                current.Parent = current.Parent.Parent;
                current.RightChild.Parent = current;
                current = current.RightChild;
                LeftRotation(current);

            }
        }

        public void Add(TKey key, TValue value)
        {
            Node<TKey, TValue> node = new Node<TKey, TValue>(key, value);
            Node<TKey, TValue> current = root;

            if (root == null)
            {
                node.color = Color.Black;
                root = node;
                current = root;
            }
            else
            {
                while (current.LeftChild != null && current.RightChild != null)
                {
                    if (node.Key.CompareTo(current.Key) < 0)
                    {
                        current = current.LeftChild;
                    }
                    else if (node.Key.CompareTo(current.Key) > 0)
                    {
                        current = current.RightChild;
                    }
                    else
                    {
                        throw new Exception("An element with the same key already exists in the Dictioanry.");
                    }
                }


                if (current.Key.CompareTo(node.Key) >= 0 && current.LeftChild == null)
                {
                    node.Parent = current;
                    current.LeftChild = node;
                }
                else if (current.Key.CompareTo(node.Key) < 0 && current.RightChild == null)
                {
                    node.Parent = current;
                    current.RightChild = node;
                }
                else if (current.Key.CompareTo(node.Key) > 0 && current.LeftChild != null)
                {
                    node.Parent = current.LeftChild;
                    if (current.LeftChild.Key.CompareTo(node.Key) > 0)
                    {
                        current.LeftChild.LeftChild = node;
                    }
                    else
                    {
                        current.LeftChild.RightChild = node;
                    }
                }
                else if (current.Key.CompareTo(node.Key) < 0 && current.RightChild != null)
                {
                    node.Parent = current.RightChild;
                    if (current.RightChild.Key.CompareTo(node.Key) > 0)
                    {
                        current.RightChild.LeftChild = node;
                    }
                    else
                    {
                        current.RightChild.RightChild = node;
                    }
                }
                current = node;
                Recolor(current);
               
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            root = null;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Search(item.Key).Equals(item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            Node<TKey, TValue> current = root;
            while (current != null)
            {
                if (key.Equals(current.Key))
                {
                    return true;
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

        public Node<TKey, TValue> Delete(TKey key)
        {
            Node<TKey, TValue> current = this.root;
            int cmp;
            while (current != null)
            {
                cmp = key.CompareTo(current.Key);
                if (cmp < 0)
                {
                    current = current.LeftChild;
                }
                else if (cmp > 0)
                {
                    current = current.RightChild;

                }
                else
                {
                    break;
                }
            }
            if (current == null)
            {
                return null;
            }

            if (current.Parent == null)
            {
                if (current.RightChild != null)
                {
                    Node<TKey, TValue> node = current.RightChild;
                    while (node.LeftChild != null)
                    {
                        node = node.LeftChild;
                    }
                    current.Key = node.Key;
                    current.Value = node.Value;
                    if (node == node.Parent.LeftChild)
                    {
                        node.Parent.LeftChild = node.RightChild;
                        node.RightChild.Parent = node.Parent;
                        this.col = node.color;
                        return node.RightChild;
                    }
                    else
                    {
                        node.Parent.RightChild = node.RightChild;
                        if (node.RightChild != null)
                        {
                            node.RightChild.Parent = node.Parent;
                            this.col = node.color;
                            return node.RightChild;
                        }
                        this.col = node.color;
                        return node.Sibling;

                    }

                }
                else
                {
                    root = current.LeftChild;
                    root.Parent = null;
                    root.color = Color.Black;
                    return null;
                }
            }
            else
            {
                if (current.RightChild != null)
                {
                    Node<TKey, TValue> node = current.RightChild;
                    while (node.LeftChild != null)
                    {
                        node = node.LeftChild;
                    }
                    current.Key = node.Key;
                    current.Value = node.Value;

                    if (node == node.Parent.LeftChild)
                    {
                        node.Parent.LeftChild = node.RightChild;
                        if (node.RightChild != null)
                        {
                            node.RightChild.Parent = node.Parent;
                            this.col = node.color;
                            return node.RightChild;
                        }
                        this.col = node.color;
                        return node.Sibling;
                        
                    }
                    else
                    {
                        node.Parent.RightChild = node.RightChild;
                        node.RightChild.Parent = node.Parent;
                        this.col = node.color;
                        return node.LeftChild;

                    }

                }
                else
                {
                    this.col = current.color;
                    return current.LeftChild;
                }
            }



        }

        public bool Remove(TKey key)
        {
            Node<TKey, TValue> current;
            Node<TKey, TValue> node;
            if (root.Key.Equals(key) && IsLeaf(root))
            {
                root = null;
                return true;
            }
            current = Delete(key);
            if (current==null)
                return false;
            if (this.col == Color.Red)
                return true;
            else if(current.color == Color.Red)
            {
                current.color = Color.Red;
                return true;
            }
            if (current != root)
            {

                if (current.Sibling.color == Color.Red)
                {
                    current.Parent.color = Color.Red;
                    current.Sibling.color = Color.Black;
                    current = current.Sibling;
                    if (current.Parent.LeftChild == current)
                        LeftRotation(current);
                    else
                        RightRotation(current);

                }
                else if (IsLeaf(current.Sibling) || current.Sibling.RightChild == null || current.Sibling.RightChild == null)
                {
                    current.color = Color.Black;

                }
                else if (current.Sibling.LeftChild.color == Color.Black && current.Sibling.RightChild.color == Color.Black)
                {
                    current.Sibling.color = Color.Red;
                    current.Parent.color = Color.Black;
                }
                else if (current.Sibling.RightChild.color == Color.Black && current.Sibling.LeftChild.color == Color.Red)
                {
                    current.Sibling.color = Color.Red;
                    current.Sibling.color = Color.Black;
                    current = current.Sibling;
                    RightRotation(current);
                }
                else if (current.Sibling.RightChild.color == Color.Red)
                {
                    current.Sibling.color = current.Sibling.Parent.color;
                    current.Sibling.RightChild.color = Color.Black;
                    current.Parent.color = Color.Black;
                    current = current.Sibling;
                    if (current.Parent.LeftChild == current)
                        LeftRotation(current);
                    else
                        RightRotation(current);
                }

            }
            else
            {
                if (root.RightChild == null && root.LeftChild.color == Color.Red && !IsLeaf(root.LeftChild))
                {
                    root.RightChild.color = Color.Black;
                    root.color = Color.Red;
                    node = root;
                    root = root.RightChild;
                    node.LeftChild = root.RightChild;
                    root.RightChild = node;

                }
            }
            return true;
        }

        internal bool IsLeaf(Node<TKey,TValue> node)
        {
            if (node.LeftChild != null || node.RightChild != null)
                return false;
            else
                return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
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

        public void PrintInside(Node<TKey,TValue> element)
        {
            if (element != null)
            {
                Console.WriteLine(element.Value + " " + element.Key + " " + element.color);
                Console.WriteLine("Left Child: ");
                PrintInside(element.LeftChild);
                Console.WriteLine("Right Child: ");
                PrintInside(element.RightChild);
            }
        }
    }
}
