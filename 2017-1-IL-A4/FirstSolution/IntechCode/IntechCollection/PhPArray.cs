using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IntechCode.IntechCollection
{
    public class MyNode<T>
    {
        public T Data;
        public MyNode<T> Next;
        public MyNode<T> Prev;

        public MyNode(T data, MyNode<T>prev)
        {
            Data = data;
            Prev = prev;
        }
    }

    public class PhPArray<TKey,TValue> : IEnumerable<KeyValuePair<TKey,TValue>>
    {
        MyNode<KeyValuePair<TKey,TValue>> _myChainedList;

        public TValue this [TKey key]
        {
            get
            {
                TValue v;
                if ( TryGetValue( key, out v ) ) return v;
                else throw new KeyNotFoundException();
            }
            set
            {
                MyNode<KeyValuePair<TKey,TValue >> n;
                if(TryGetNode(key, out n))
                {
                    ChangeNodeValue( n, value );
                }
                else
                {
                    Add( key, value );
                }
            }
        }

        public TValue this [int index]
        {
            get
            {
                return At( index ).Value;
            }
            set
            {
                ChangeNodeValue( AtNode( index ), value );
            }
        }

        public PhPArray ()
        {
            // c===3
            _count = 0;
        }

        int _count;
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        void ChangeNodeValue(MyNode<KeyValuePair<TKey,TValue>> node, TValue value)
        {
            node.Data = new KeyValuePair<TKey, TValue>( node.Data.Key, value );
        }

        #region IEnumerableSupport
        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator ()
        {
            // vas-y bande Spi <3
            var currentNode = _myChainedList;
            do
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;

            } while ( currentNode != null );
        }


        #endregion

        /// <summary>
        ///  OK
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            MyNode < KeyValuePair<TKey, TValue> > node;
            if (TryGetNode(key, out node))
            {
                value = node.Data.Value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }

        }
        
        /// <summary>
        /// OK
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        bool TryGetNode(TKey key, out MyNode<KeyValuePair<TKey,TValue>> node)
        {
            var currentNode = _myChainedList;
            while(currentNode != null)
            {
                if(currentNode.Data.Key.Equals(key))
                {
                    node = currentNode;
                    return true;
                }

                currentNode = currentNode.Next;
            }

            node = null;
            return false;
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="output"></param>
        /// <param name="CanMoveNext"></param>
        /// <returns></returns>
        bool TryGetLastNode( out MyNode<KeyValuePair<TKey, TValue>> output, Func<MyNode<KeyValuePair<TKey,TValue>>,bool> CanMoveNext=null)
        {
            var node = _myChainedList;
            if(node == null)
            {
                output = null;
                return true;
            }

            while(node != null && node.Next!= null)
            {
                if(CanMoveNext != null && !CanMoveNext(node))
                {
                    output = null;
                    return false;
                }
                node = node.Next;
            }
            output = node;
            return true;

        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key,TValue value)
        {
            MyNode < KeyValuePair<TKey, TValue> > lastNode;
            if (TryGetLastNode(out lastNode, (n) => { return !n.Data.Key.Equals(key); } ))
            {
                MyNode<KeyValuePair<TKey, TValue>> newNode = new MyNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key,value), lastNode);

                if ( lastNode == null)
                {
                    _myChainedList = newNode;
                }
                else
                {
                    lastNode.Next = newNode;
                }
                _count++;
            }
            else
            {
                throw new Exception( "Key already exists" );
            }
        }

        /// <summary>
        ///  OK
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public KeyValuePair<TKey,TValue> At(int n)
        {
            return AtNode( n ).Data;
        }

        /// <summary>
        ///  OK
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        MyNode<KeyValuePair<TKey, TValue>> AtNode ( int n )
        {
            if ( n >= Count ) throw new IndexOutOfRangeException();

            var node = _myChainedList;
            for(var i = 0; i<n;i++ )
            {
                node = node.Next;
            }
            return node;
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public TValue ValueAt ( int n )
        {
            return At( n ).Value;
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public TKey KeyAt ( int n )
        {
            return At( n ).Key;
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="n"></param>
        public void RemoveAt(int n)
        {
            var currentnode = AtNode( n );

            if(currentnode.Prev != null)
            {
                currentnode.Prev.Next = currentnode.Next;
            }
            if( currentnode.Next != null)
            {
                currentnode.Next.Prev = currentnode.Prev;
            }

            _count--;

        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="n"></param>
        /// <param name="v"></param>
        public void SetValueAt(int n, TValue v)
        {
            this [n] = v;
        }

        
    }
}
