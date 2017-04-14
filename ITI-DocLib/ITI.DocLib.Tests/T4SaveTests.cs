using System;
using System.Collections.Generic;
using System.Text;
using ITI.DocLib.Model;
using NUnit.Framework;
using ITI.DocLib.Impl;
using System.IO;

namespace ITI.DocLib.Tests_1_Point_Per_Green_Below
{
    [TestFixture]
    public class T4SaveAndLoadTests
    {
        [Test]
        public void t01_saving_and_reloading_a_library_reloads_its_documents()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d1 = lib.CreateDocument( "ISBN1" );
            d1.Title = "La Zone du Dehors";
            IDocument d2 = lib.CreateDocument( "ISBN2" );
            d2.Title = "Dune";
            IDocument d3 = lib.CreateDocument( "ISBN3" );
            d3.Title = "L'étranger";

            ILibrary lib2 = WriteAndRead( lib );

            Assert.That( lib2.Count, Is.EqualTo( 3 ) );
            Assert.That( lib2.Users.Count, Is.EqualTo( 0 ) );
            Assert.That( lib2.Find( "ISBN1" ).Title, Is.EqualTo( "La Zone du Dehors" ) );
            Assert.That( lib2.Find( "ISBN2" ).Title, Is.EqualTo( "Dune" ) );
            Assert.That( lib2.Find( "ISBN3" ).Title, Is.EqualTo( "L'étranger" ) );
        }

        [Test]
        public void t02_saving_and_reloading_a_library_reloads_its_document_instances()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d1 = lib.CreateDocument( "ISBN1" );
            d1.Title = "La Zone du Dehors";
            IDocument d2 = lib.CreateDocument( "ISBN2" );
            d2.Title = "Dune";
            IDocument d3 = lib.CreateDocument( "ISBN3" );
            d3.Title = "L'étranger";
            IDocumentInstance d1i1 = d1.CreateNewInstance();
            IDocumentInstance d1i2 = d1.CreateNewInstance();
            IDocumentInstance d2i1 = d2.CreateNewInstance();
            IDocumentInstance d2i2 = d2.CreateNewInstance();
            IDocumentInstance d2i3 = d2.CreateNewInstance();

            ILibrary lib2 = WriteAndRead( lib );
            Assert.That( lib2.Count, Is.EqualTo( 3 ) );
            Assert.That( lib2.Users.Count, Is.EqualTo( 0 ) );
            
            Assert.That( lib2.Find( "ISBN1" ).Title, Is.EqualTo( "La Zone du Dehors" ) );
            Assert.That( lib2.Find( "ISBN1" ).TotalInstanceCount, Is.EqualTo( 2 ) );
            Assert.That( lib2.Find( "ISBN1" ).Find( d1i1.UniqueIdentifier ), Is.Not.Null );
            Assert.That( lib2.Find( "ISBN1" ).Find( d1i2.UniqueIdentifier ), Is.Not.Null );
            
            Assert.That( lib2.Find( "ISBN2" ).Title, Is.EqualTo( "Dune" ) );
            Assert.That( lib2.Find( "ISBN2" ).TotalInstanceCount, Is.EqualTo( 3 ) );
            Assert.That( lib2.Find( "ISBN2" ).Find( d2i1.UniqueIdentifier ), Is.Not.Null );
            Assert.That( lib2.Find( "ISBN2" ).Find( d2i2.UniqueIdentifier ), Is.Not.Null );
            Assert.That( lib2.Find( "ISBN2" ).Find( d2i3.UniqueIdentifier ), Is.Not.Null );
            
            Assert.That( lib2.Find( "ISBN3" ).Title, Is.EqualTo( "L'étranger" ) );
            Assert.That( lib2.Find( "ISBN3" ).TotalInstanceCount, Is.EqualTo( 0 ) );

        }

        [Test]
        public void t03_SaveAndLoadLibraryWithUsers()
        {
            ILibrary lib = LibraryLoader.Create();
            IUser u1 = lib.Users.Create( "u1" );
            IUser u2 = lib.Users.Create( "u2" );
            IUser u3 = lib.Users.Create( "u3" );
            IUser u4 = lib.Users.Create( "u4" );
            IUser u5 = lib.Users.Create( "u5" );

            ILibrary lib2 = WriteAndRead( lib );

            Assert.That( lib2.Count, Is.EqualTo( 0 ) );
            Assert.That( lib2.Users.Count, Is.EqualTo( 5 ) );
            Assert.That( lib2.Users.Find( "u" ), Is.Null );
            Assert.That( lib2.Users.Find( "u1" ), Is.Not.Null );
            Assert.That( lib2.Users.Find( "u2" ), Is.Not.Null );
            Assert.That( lib2.Users.Find( "u3" ), Is.Not.Null );
            Assert.That( lib2.Users.Find( "u4" ), Is.Not.Null );
            Assert.That( lib2.Users.Find( "u5" ), Is.Not.Null );
            Assert.That( lib2.Users.Find( "u6" ), Is.Null );
        }

        [Test]
        public void t04_saving_and_reloading_a_library_reloads_all_its_information()
        {
            ILibrary lib = LibraryLoader.Create();
            
            IUser u1 = lib.Users.Create( "u1" );
            IUser u2 = lib.Users.Create( "u2" );
            IUser u3 = lib.Users.Create( "u3" );

            IDocument d1 = lib.CreateDocument( "ISBN1" ); d1.Title = "La Zone du Dehors";
            IDocument d2 = lib.CreateDocument( "ISBN2" ); d2.Title = "Dune";
            IDocument d3 = lib.CreateDocument( "ISBN3" ); d2.Title = "La horde du Contrevent";

            d1.CreateNewInstance();
            u1.BorrowedDocument = d1.CreateNewInstance();

            d3.CreateNewInstance();
            d3.CreateNewInstance();
            u2.BorrowedDocument = d3.CreateNewInstance();

            ILibrary xlib = WriteAndRead( lib );
            IUser xu1 = xlib.Users.Find( "u1" );
            IUser xu2 = xlib.Users.Find( "u2" );
            IUser xu3 = xlib.Users.Find( "u3" );

            IDocument xd1 = xlib.Find( "ISBN1" ); 
            IDocument xd2 = xlib.Find( "ISBN2" ); d2.Title = "Dune";
            IDocument xd3 = xlib.Find( "ISBN3" ); d2.Title = "La horde du Contrevent";

            SameDocument( d1, xd1 );
            SameDocument( d2, xd2 );
            SameDocument( d3, xd3 );

            SameUser( u1, xu1 );
            SameUser( u2, xu2 );
            SameUser( u3, xu3 );

        }

        static void SameUser( IUser u, IUser xu )
        {
            Assert.That( xu.Name, Is.EqualTo( u.Name ) );
            Assert.That( xu.BorrowedDate, Is.EqualTo( u.BorrowedDate ) );
            if( u.BorrowedDocument != null )
            {
                Assert.That( xu.BorrowedDocument.Document.Code, Is.EqualTo( u.BorrowedDocument.Document.Code ) );
                Assert.That( xu.BorrowedDocument.UniqueIdentifier, Is.EqualTo( u.BorrowedDocument.UniqueIdentifier ) );
                Assert.That( xu.BorrowedDocument.Borrower.Name, Is.EqualTo( u.BorrowedDocument.Borrower.Name ) );
            }
            else Assert.That( xu.BorrowedDocument, Is.Null );
        }

        static void SameDocument( IDocument d, IDocument xd )
        {
            Assert.That( xd.Title, Is.EqualTo( d.Title ) );
            Assert.That( xd.TotalInstanceCount, Is.EqualTo( d.TotalInstanceCount ) );
            Assert.That( xd.FreeCount, Is.EqualTo( d.FreeCount ) );
            Assert.That( xd.BorrowedCount, Is.EqualTo( d.BorrowedCount ) );
        }
        
        static ILibrary WriteAndRead( ILibrary lib )
        {
            using( MemoryStream s = new MemoryStream() )
            {
                lib.SaveInto( s );
                s.Position = 0;
                return LibraryLoader.Load( s );
            }
        }

    }
}
