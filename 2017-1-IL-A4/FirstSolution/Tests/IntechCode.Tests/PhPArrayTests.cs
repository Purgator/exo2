using IntechCode.IntechCollection;
using FluentAssertions;
using NUnit.Framework;
using Xunit;
using System;

namespace IntechCode.Tests
{
    [TestFixture]
    public class PhPArrayTests
    {
        [Fact]
        [Test]
        public void access_to_an_element_to_the_phparray()
        {
            PhPArray<int, string> pa = new PhPArray<int, string>();
            pa.Add( 1, "first" );
            pa.At( 0 ).Key.Should().Be( 1 );
            pa.At( 0 ).Value.Should().Be( "first" );
        }

        [Fact]
        [Test]
        public void get_an_element_to_phparray()
        {
            PhPArray<int, string> pa = new PhPArray<int, string>();
            pa.Add( 3712, "a sentence" );
            pa.ValueAt( 0 ).Should().Be( "a sentence" );
        }

        [Fact]
        [Test]
        public void add_5_elements()
        {
            var tab = new PhPArray<string,string>();

            tab.Add( "1", "couille" );
            tab ["1"].Should().Equals("couille1");
            tab [0].Should().Equals( "couille1" );

            tab.Add( "2", "couille" );
            tab ["2"].Should().Equals( "couille2" );
            tab [1].Should().Equals( "couille2" );

            tab.Add( "3", "couille" );
            tab ["3"].Should().Equals( "couille3" );
            tab [2].Should().Equals( "couille3" );

            tab.Add( "4", "couille" );
            tab ["4"].Should().Equals( "couille4" );
            tab [3].Should().Equals( "couille4" );

            tab.Add( "5", "couille" );
            tab ["5"].Should().Equals( "couille5" );
            tab [4].Should().Equals( "couille5" );

        }


        [Fact]
        [Test]
        public void test_iteration_iterates()
        {
            var tab = new PhPArray<string,string>();
            tab.Add( "couille", "couille" );
            tab.Add( "couille2", "couille2" );
            tab.Add( "couille3", "couill3" );
            tab.Add( "couill43", "couill3" );
            tab.Add( "couille323", "couill3" );

            var i=0;
            foreach(var t in tab)
            {
                tab [i].Should().Equals( t );
                i++;
            }


        }


        [Fact]
        [Test]
        public void get_element_by_its_key()
        {
            PhPArray<int, string> pa = new PhPArray<int, string>();
            pa.Add( 56, "cinquante-six" );
            var value = pa.KeyAt( 0 );
            value.Should().Be( 56 );
        }

        [Fact]
        [Test]
        public void can_remove_an_element_just_added()
        {
            PhPArray<string, string> pa = new PhPArray<string, string>();
            pa.Add( "key1", "value1" );
            string s = pa[ 0 ];

            s.Should().Be( "value1" );

            pa.RemoveAt( 0 );

            Action a = () =>
            {
                string str = pa[ 0 ];
            };
            a.ShouldThrow<IndexOutOfRangeException>();
        }

        [Fact]
        [Test]
        public void set_existing_value()
        {
            PhPArray<int, string> pa = new PhPArray<int, string>();
            pa.Add( 5, "cinq" );
            pa.At( 0 ).Value.Should().Be( "cinq" );
            pa.SetValueAt( 0, "five" );
            pa.At( 0 ).Value.Should().Be( "five" );
        }

        [Fact]
        [Test]
        public void try_to_access_to_an_inexisting_value_should_throws_exception()
        {
            PhPArray<int, string> pa = new PhPArray<int, string>();
            pa.Add( 1, "one" );
            Action a = () => { string s = pa[ 6 ]; };
            a.ShouldThrow<IndexOutOfRangeException>();
        }





    }
}
