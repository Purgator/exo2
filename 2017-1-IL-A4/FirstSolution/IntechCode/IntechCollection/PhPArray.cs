using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IntechCode.IntechCollection
{
    public class MyNode<T>
    {
        public T Value;
        public MyNode<T> Next;
    }

    class PhPArray<TKey,TValue> : IEnumerable<KeyValuePair<TKey,TValue>>
    {
        MyNode<KeyValuePair<TKey,TValue>> _myChainedList;

        public TValue this [TKey key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set => throw new NotImplementedException();
        }

        public TValue this [int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set => throw new NotImplementedException();
        }


        public PhPArray ()
        {
            _myChainedList = new MyNode<KeyValuePair<TKey, TValue>>();
            


        }

        #region IEnumerableSupport
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator ()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            throw new NotImplementedException();
        }
        #endregion

        public bool TryGetValue<TKey,TValue>(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        KeyValuePair<TKey,TValue> At(int n)
        {

            throw new NotImplementedException();
        }

        TValue ValueAt ( int n )
        {
            return At( n ).Value;
        }

        TKey KeyAt ( int n )
        {
            return At( n ).Key;
        }

        void RemoveAt(int n)
        {

        }

        void SetValueAt(int n, TValue v)
        {
            var kvp = At(n);
        }


    }
}
