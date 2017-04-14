using System;
using System.Collections.Generic;
using System.Text;
using ITI.DocLib.Model;
using NUnit.Framework;
using ITI.DocLib.Impl;
using System.Linq;

namespace ITI.DocLib.Tests_1_Point_Per_Green_Below
{
    [TestFixture]
    public class T5BonusTests
    {

        [Test]
        public void t01_clearing_a_library_removes_the_documents_and_the_users()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "ISBN 2-07-032288-2" );
            IUser u = lib.Users.Create( "Olivier" );

            Assert.That( lib.Count, Is.EqualTo( 1 ) );
            Assert.That( lib.Users.Count, Is.EqualTo( 1 ) );

            lib.Clear();

            Assert.That( lib.Count, Is.EqualTo( 0 ) );
            Assert.That( lib.Users.Count, Is.EqualTo( 0 ) );
        }

        [Test]
        public void t02_the_document_title_must_be_trimmed_and_must_not_be_null()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "ISBN 2-07-032288-2" );
            Assert.That( d.Title, Is.EqualTo( String.Empty ) );
            d.Title = "Le mythe de Sisyphe";
            Assert.That( d.Title, Is.EqualTo( "Le mythe de Sisyphe" ) );
            d.Title = "        Le mythe de Sisyphe        ";
            Assert.That( d.Title, Is.EqualTo( "Le mythe de Sisyphe" ) );
            Assert.Throws<ArgumentException>( () => d.Title = null );
            Assert.Throws( typeof( ArgumentException ), delegate() { d.Title = ""; } );
            Assert.Throws( typeof( ArgumentException ), delegate() { d.Title = "   "; } );
        }

        [Test]
        public void t03_documents_can_be_searched_by_piece_of_text_in_their_title()
        {
            ILibrary lib = LibraryLoader.Create();
            lib.CreateDocument( "1" ).Title = "AB";
            lib.CreateDocument( "2" ).Title = "ABCD";
            lib.CreateDocument( "3" ).Title = "ABCDEF";
            lib.CreateDocument( "4" ).Title = "ABCDEFGH";
            Assert.That( lib.FindByTitle( "ABCDEFGH" ).Select( d => d.Code ), Is.EquivalentTo( new[] { "4" } ) );
            Assert.That( lib.FindByTitle( "ABCD" ).Select( d => d.Code ).OrderBy( c => c ), Is.EquivalentTo( new[] { "2", "3", "4" } ) );
            Assert.That( lib.FindByTitle( "DE" ).Select( d => d.Code ).OrderBy( c => c ), Is.EquivalentTo( new[] { "3", "4" } ) );
            Assert.That( lib.FindByTitle( "H" ).Select( d => d.Code ), Is.EquivalentTo( new[] { "4" } ) );
            Assert.That( lib.FindByTitle( "Z" ), Is.Empty );
        }

        [Test]
        public void t04_find_documents_by_title_supports_stars_and_question_marks_wildcards()
        {
            ILibrary lib = LibraryLoader.Create();
            lib.CreateDocument( "1" ).Title = "AB";
            lib.CreateDocument( "2" ).Title = "ABCD";
            lib.CreateDocument( "3" ).Title = "ABCDEF";
            lib.CreateDocument( "4" ).Title = "ABCDEFGH";
            lib.CreateDocument( "5" ).Title = "AH";
            lib.CreateDocument( "6" ).Title = "AXXD";
            Assert.That( lib.FindByTitle( "A*H" ).Select( d => d.Code ).OrderBy( c => c ), Is.EquivalentTo( new[] { "4", "5" } ) );
            Assert.That( lib.FindByTitle( "A??D" ).Select( d => d.Code ).OrderBy( c => c ), Is.EquivalentTo( new[] { "2", "3", "4", "6" } ) );
        }

        [Test]
        public void t05_find_documents_by_title_supports_stars_and_question_marks_wildcards()
        {
            ILibrary lib = LibraryLoader.Create();
            string door = Guid.NewGuid().ToString();
            string now = Guid.NewGuid().ToString();
            string[] i1 = { "I", "You", "We", "They" };
            string[] i2 = { "did", "rock", "open", "close" };
            string[] i3 = { "it", "the " + door, "the tests", "my plan" };
            string[] i4 = { now, "later", "yesterday" };

            foreach( string s1 in i1 )
            {
                foreach( string s2 in i2 )
                {
                    foreach( string s3 in i3 )
                    {
                        foreach( string s4 in i4 )
                        {
                            IDocument d = lib.CreateDocument( Guid.NewGuid().ToString() );
                            d.Title = String.Format( "{0} {1} {2} {3}.", s1, s2, s3, s4 );
                        }
                    }
                }
            }
            Assert.That( lib.Count, Is.EqualTo( i1.Length * i2.Length * i3.Length * i4.Length ) );
            
            Assert.That( lib.FindByTitle( "I did it " + now + "." ).Single().Title, Is.EqualTo( "I did it " + now + "." ) );
            Assert.That( lib.FindByTitle( "I did it " + now + "..." ).Count, Is.EqualTo( 0 ) );
            Assert.That( lib.FindByTitle( "Murfn..." ).Count, Is.EqualTo( 0 ) );
            Assert.That( lib.FindByTitle( "*"+door+"*" ).Count, Is.EqualTo( i1.Length * i2.Length * i4.Length ) );
            Assert.That( lib.FindByTitle( "*" + door + "*" + now + "*" ).Count, Is.EqualTo( i1.Length * i2.Length ) );
            Assert.That( lib.FindByTitle( "We*did*" + now + "*" ).Count, Is.EqualTo( i3.Length ) );
        }

        [Test]
        public void t06_a_document_title_can_contain_stars_and_question_marks_and_be_searched_by_escaping_the_wildcard()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d1 = lib.CreateDocument( "d1" );
            IDocument d2 = lib.CreateDocument( "d2" );
            IDocument d3 = lib.CreateDocument( "d3" );

            string word = Guid.NewGuid().ToString();
            d1.Title = "A *" + word + "* ?";
            d2.Title = "*** ? ***";
            d3.Title = "A *big" + word + "* !";

            Assert.That( lib.FindByTitle( "A*" + word + "*" ).Count, Is.EqualTo( 2 ) );
            Assert.That( lib.FindByTitle( @"A \*" + word + "*" ), Is.EquivalentTo( new[]{ d1 } ) );
            Assert.That( lib.FindByTitle( "*" ).Count, Is.EqualTo( 3 ) );
            Assert.That( lib.FindByTitle( @"*\?" ).OrderBy( d => d.Code ), Is.EquivalentTo( new[] { d1, d2 } ) );
            Assert.That( lib.FindByTitle( @"\*\**?" ), Is.EquivalentTo( new[] { d2 } ) );
            Assert.That( lib.FindByTitle( @"\**\?" ).OrderBy( d => d.Code ), Is.EquivalentTo( new[] { d1, d2 } ) );
        }

    }
}
