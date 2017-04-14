using FluentAssertions;
using IntechCode.IntechCollection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntechCode.Tests
{
    [TestFixture]
    public class MyHashSetTests
    {
        [Fact]
        [Test]
        public void add_an_element_to_our_hashset()
        {
            MyHashSet<string> mhs = new MyHashSet<string>();
            bool added = mhs.Add( "Item1" );
            added.Should().BeTrue();
            mhs.Count.Should().Be( 1 );
        }

        [Fact]
        [Test]
        public void add_an_element_that_already_exist_must_not_working()
        {
            MyHashSet<string> mhs = new MyHashSet<string>();
            bool added = mhs.Add( "Item1" );
            added.Should().BeTrue();
            added = mhs.Add( "Item1" );
            added.Should().BeFalse();
            mhs.Count.Should().Be( 1 );
        }

        [Fact]
        [Test]
        public void clear_all_the_data_in_hashset()
        {
            MyHashSet<string> mhs = new MyHashSet<string>();
            mhs.Add( "Item1" );
            mhs.Add( "Item2" );
            mhs.Add( "Item3" );

            mhs.Clear();

            mhs.Count.Should().Be( 0 );
        }

        [Fact]
        [Test]
        public void remove_a_item_from_hashcode_change_the_count()
        {
            MyHashSet<string> mhs = new MyHashSet<string>();
            mhs.Add( "Item1" );
            mhs.Add( "Item2" );
            mhs.Add( "Item3" );

            mhs.Remove( "Item3" );

            mhs.Count.Should().Be( 2 );
        }

        [Fact]
        [Test]
        public void remove_a_item_from_hashcode_remove_the_real_item()
        {
            MyHashSet<string> mhs = new MyHashSet<string>();
            mhs.Add( "Item1" );
            mhs.Add( "Item2" );
            mhs.Add( "Item3" );

            mhs.Remove( "Item3" );

            mhs.Contains( "Item3" ).Should().BeFalse();
        }

        [Fact]
        [Test]
        public void tests_capacity_of_hashset()
        {
            MyHashSet<int> mh = new MyHashSet<int>();
            for( int i = 0; i < 128; i++ ) mh.Add( i );
            mh.Count.Should().Be( 127 );
        }
    }
}