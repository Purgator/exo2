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

    class PhPArray<TKey,TValue> : IEnumerable<KeyValuePair<TKey,TValue>>
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
                    throw new NotImplementedException();
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
                var node = AtNode(index);
                node.Data = new KeyValuePair<TKey, TValue>( node.Data.Key, value );
            }
        }

        public PhPArray ()
        {
            // c===3

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
        public bool TryGetValue<TKey,TValue>(TKey key, out TValue value)
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

        bool TryGetNode<TKey,TValue>(TKey key, out MyNode<KeyValuePair<TKey,TValue>> node)
        {
            /*
            foreach(MyNode<KeyValuePair<TKey, TValue>> n in this)
            {
                if(n.Key.Equals(key))
                {
                    node = n;

                }
            }
            */

            throw new NotImplementedException();
        }

        bool TryGetLastNodeWithKeySecurity<TKey,TValue>( TKey key, out MyNode<KeyValuePair<TKey, TValue>> output)
        {


            throw new NotImplementedException();
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key,TValue value)
        {
            MyNode < KeyValuePair<TKey, TValue> > lastNode;
            if (TryGetLastNodeWithKeySecurity(key, out lastNode))
            {
                MyNode<KeyValuePair<TKey, TValue>> newNode = new MyNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key,value), lastNode);
                lastNode.Next = newNode;
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

        MyNode<KeyValuePair<TKey, TValue>> AtNode ( int n )
        {
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

            currentnode.Prev.Next = currentnode.Next;
            currentnode.Next.Prev = currentnode.Prev;

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
