using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    /// <summary>
    /// Red and Black colors for red-black tree.
    /// </summary>
    public enum Color { Red, Black };

    /// <summary>
    /// Node for tree , that has key and value.
    /// </summary>
    /// <typeparam name="TKey">Type for Dictionary key.</typeparam>
    /// <typeparam name="TValue">Type for Dictionary value.</typeparam>
    public class Node<TKey, TValue> where TKey : IComparable
    {
        /// <summary>
        /// Node's color,which can be either red or black.
        /// </summary>
        internal Color color { get; set; }

        public Node<TKey, TValue> Parent { get; set; }
        public Node<TKey, TValue> RightChild { get; set; }
        public Node<TKey, TValue> LeftChild { get; set; }
        public Node<TKey, TValue> Sibling
        {
            get
            {
                if (this.Parent != null)
                {
                    if (this.Parent.RightChild == this)
                    {
                        return this.Parent.LeftChild;
                    }
                    else
                    {
                        return this.Parent.RightChild;
                    }

                }
                return null;
            }
            set
            {

            }

        }
        public Node<TKey, TValue> GrandParent
        {
            get
            {
                if (this.Parent != null)
                {
                    return this.Parent.Parent;
                }
                else
                {
                    return null;
                }
            }
            set
            {

            }
        }


        public TValue Value { get; set; }
        public TKey Key { get; set; }

        /// <summary>
        /// Parametrise constructor to create a  node with k Key and v Value.
        /// </summary>
        /// <param name="k">The key.</param>
        /// <param name="v">The Value.</param>
        public Node(TKey k, TValue v)
        {
            Key = k;
            Value = v;
            color = Color.Red;
        }

        /// <summary>
        /// Parametrless constructor.
        /// </summary>
        public Node()
        {

        }

    }
}
