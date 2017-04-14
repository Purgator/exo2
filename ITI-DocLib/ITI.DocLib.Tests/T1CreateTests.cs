using System;
using System.Collections.Generic;
using System.Text;
using ITI.DocLib.Model;
using ITI.DocLib.Impl;
using NUnit.Framework;

namespace ITI.DocLib.Tests_1_Point_Per_Green_Below
{
    [TestFixture]
    public class T1CreateTests
    {
        [Test]
        public void t01_a_newly_created_library_has_no_document_and_no_user()
        {
            ILibrary lib = LibraryLoader.Create();
            Assert.That( lib.Count, Is.EqualTo( 0 ) );
            Assert.That( lib.Users.Count, Is.EqualTo( 0 ) );
        }

        [Test]
        public void t02_a_user_created_in_a_library_references_the_library_it_belongs_to()
        {
            ILibrary lib = LibraryLoader.Create();
            IUser u = lib.Users.Create( "Olivier" );
            Assert.That( u.Library, Is.EqualTo( lib ) );
        }       

        [Test]
        public void t03_when_a_new_user_is_created_we_can_find_it()
        {
            ILibrary lib = LibraryLoader.Create();
            string name = Guid.NewGuid().ToString();
            IUser u = lib.Users.Create( name );
            Assert.That( u, Is.Not.Null );
            Assert.That( lib.Users.Find( name ), Is.EqualTo( u ) );
        }

        [Test]
        public void t04_a_lot_of_users_can_be_created_and_found()
        {
            ILibrary lib = LibraryLoader.Create();
            var createdUsers = new List<IUser>();
            for( int i = 0; i < 5000; ++i )
            {
                string name = String.Format( "User n°{0}", i );
                createdUsers.Add( lib.Users.Create( name ) );
            }
            Assert.That( lib.Users.Count, Is.EqualTo( 5000 ) );
            for( int i = 0; i < 5000; ++i )
            {
                string name = String.Format( "User n°{0}", i );
                IUser u = lib.Users.Find( name );
                Assert.That( u, Is.Not.Null );
                Assert.That( u.Name, Is.EqualTo( name ) );
                Assert.That( u, Is.SameAs( createdUsers[i] ) );
            }
        }

        [Test]
        public void t05_when_a_new_document_is_created_we_can_find_it()
        {
            ILibrary lib = LibraryLoader.Create();
            string code = "Un Code: " + Guid.NewGuid().ToString();
            IDocument d0 = lib.CreateDocument( code );
            Assert.That( d0, Is.Not.Null );
            Assert.That( lib.Find( code ), Is.EqualTo( d0 ) );
        }

        
        [Test]
        public void t06_a_lot_of_documents_can_be_created_and_found()
        {
            ILibrary lib = LibraryLoader.Create();
            var createdDocuments = new List<IDocument>();
            for( int i = 0; i < 5000; ++i )
            {
                string code = String.Format( "Document n°{0}", i );
                createdDocuments.Add( lib.CreateDocument( code ) );
            }
            Assert.That( lib.Count, Is.EqualTo( 5000 ) );
            for( int i = 0; i < 5000; ++i )
            {
                string code = String.Format( "Document n°{0}", i );
                IDocument d = lib.Find( code );
                Assert.That( d, Is.Not.Null );
                Assert.That( d.Code, Is.EqualTo( code ) );
                Assert.That( d, Is.SameAs( createdDocuments[i] ) );
            }
        }

        [Test]
        public void t07_creating_a_document_instance_updates_the_TotalInstanceCount_and_FreeCount()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "Un Code" );
            Assert.That( d.TotalInstanceCount, Is.EqualTo( 0 ) );
            Assert.That( d.FreeCount, Is.EqualTo( 0 ) );
            Assert.That( d.BorrowedCount, Is.EqualTo( 0 ) );

            IDocumentInstance i = d.CreateNewInstance();
            Assert.That( i.Document, Is.EqualTo( d ) );
            Assert.That( i.Borrower, Is.Null );

            Assert.That( d.TotalInstanceCount, Is.EqualTo( 1 ) );
            Assert.That( d.FreeCount, Is.EqualTo( 1 ) );
            Assert.That( d.BorrowedCount, Is.EqualTo( 0 ) );
        }

    }
}
