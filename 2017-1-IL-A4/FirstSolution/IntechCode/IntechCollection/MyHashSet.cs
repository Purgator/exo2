using System;
using System.Collections.Generic;
using System.Text;

namespace IntechCode.IntechCollection
{
    public class MyHashSet<T> : IMyHashSet<T>
    {
        T[] _items;
        int _count;

        const int DefaultCapacity = 16;

        public MyHashSet()
        {
            _count = 0;
            _items = new T[ DefaultCapacity ];
        }

        public int Count => _count;

        public bool Add(T item)
        {
            if( Contains( item ) ) return false;
            if( _count == _items.Length )
            {
                T[] newItems = new T[ _items.Length * 2 ];
                Array.Copy( _items, 0, newItems, 0, _count );
                _items = newItems;
            }
            else
            {
                _items[ _count ] = item;
            }
            ++_count;
            return true;
        }

        public void Clear()
        {
            _items = new T[ DefaultCapacity ];
            _count = 0;
        }

        public bool Remove(T item)
        {
            if( !Contains( item ) ) return false;
            Array.Copy( _items, _count + 1, _items, _count, _count - 1 );
            _items[ --_count ] = default( T );
            return true;
        }

        public bool Contains(T item)
        {
            if( item == null ) throw new ArgumentNullException();

            int itemIndex = FindItem( item );
            if( itemIndex >= 0 ) return true;
            return false;
        }

        private int FindItem(T item)
        {
            int i = 0;
            while( i < _items.Length )
            {
                if( item.Equals( _items[ i ] ) ) return i;
                i++;
            }
            return -1;
        }
    }
}
