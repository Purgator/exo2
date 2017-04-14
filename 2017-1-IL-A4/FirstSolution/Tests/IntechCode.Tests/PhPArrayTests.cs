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
            string str = pa[ 0 ];
            str.Should().Be( null );
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
            a.ShouldThrow<InvalidOperationException>();
        }
    }
}
