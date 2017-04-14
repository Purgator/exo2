using System;
using System.Collections.Generic;
using System.Text;
using ITI.DocLib.Model;
using NUnit.Framework;
using ITI.DocLib.Impl;

namespace ITI.DocLib.Tests_1_Point_Per_Green_Below
{
    [TestFixture]
    public class T3EventsTests
    {
        [Test]
        public void t01_when_a_document_is_created_DocumentCreated_event_fires()
        {
            ILibrary lib = LibraryLoader.Create();
            
            int docCreated = 0;

            string docName = null;
            lib.DocumentCreated += ( sender, e ) =>
            {
                ++docCreated;
                Assert.That( e.Document.Code, Is.EqualTo( docName ) );
            };

            docName = "Document: n°" + Guid.NewGuid().ToString();
            lib.CreateDocument( docName );
            docName = "Document: n°" + Guid.NewGuid().ToString();
            lib.CreateDocument( docName );
            docName = "Document: n°" + Guid.NewGuid().ToString();
            lib.CreateDocument( docName );

            Assert.That( docCreated, Is.EqualTo( 3 ) );
        }

        [Test]
        public void t02_when_a_user_is_created_UserCreated_event_fires()
        {
            ILibrary lib = LibraryLoader.Create();

            int userCreated = 0;

            lib.UserCreated += ( sender, e ) =>
            {
                ++userCreated;
                Assert.That( e.User.Name, Is.StringStarting( "User:" ) );
            };

            lib.Users.Create( "User: n°1" );
            Assert.That( userCreated, Is.EqualTo( 1 ) );
            lib.Users.Create( "User: n°2" );
            lib.Users.Create( "User: n°3" );
            Assert.That( userCreated, Is.EqualTo( 3 ) );
            lib.Users.Create( "User: n°4" );
            Assert.That( userCreated, Is.EqualTo( 4 ) );
        }

        [Test]
        public void t03_when_a_document_instance_is_borrowed_DocumentBorrowed_event_fires()
        {
            int docBorrowedCount = 0;

            ILibrary lib = LibraryLoader.Create();
            IUser u1 = lib.Users.Create( "User: n°1" );
            IUser u2 = lib.Users.Create( "User: n°2" );

            lib.DocumentBorrowed += ( object sender, DocumentInstanceEventArgs e ) =>
            {
                ++docBorrowedCount;
                Assert.That( e.DocumentInstance.Borrower, Is.Not.Null );
            };

            IDocument d1 = lib.CreateDocument( "Document: n°1" );
            IDocument d2 = lib.CreateDocument( "Document: n°2" );

            u1.BorrowedDocument = d1.CreateNewInstance();
            Assert.That( docBorrowedCount, Is.EqualTo( 1 ) );

            u2.BorrowedDocument = d2.CreateNewInstance();
            Assert.That( docBorrowedCount, Is.EqualTo( 2 ) );
        }

        [Test]
        public void t04_borrowing_or_bringing_back_fires_DocumentBorrowed_event()
        {
            int nbEvents = 0;
            int docBorrowed = 0;

            ILibrary lib = LibraryLoader.Create();

            lib.DocumentBorrowed += ( object sender, DocumentInstanceEventArgs e ) =>
            {
                ++nbEvents;
                if( e.DocumentInstance.Borrower == null ) --docBorrowed;
                else ++docBorrowed;
            };

            IUser u1 = lib.Users.Create( "User: n°1" );
            IUser u2 = lib.Users.Create( "User: n°2" );
            IDocument d1 = lib.CreateDocument( "Document: n°1" );
            IDocument d2 = lib.CreateDocument( "Document: n°2" );

            u1.BorrowedDocument = d1.CreateNewInstance();
            Assert.That( nbEvents, Is.EqualTo( 1 ) );
            Assert.That( docBorrowed, Is.EqualTo( 1 ) );

            u2.BorrowedDocument = d2.CreateNewInstance();
            Assert.That( nbEvents, Is.EqualTo( 2 ) );
            Assert.That( docBorrowed, Is.EqualTo( 2 ) );

            u1.BorrowedDocument = null;
            Assert.That( nbEvents, Is.EqualTo( 3 ) );
            Assert.That( docBorrowed, Is.EqualTo( 1 ) );
            
            u2.BorrowedDocument = null;
            Assert.That( nbEvents, Is.EqualTo( 4 ) );
            Assert.That( docBorrowed, Is.EqualTo( 0 ) );

            u1.BorrowedDocument = d2.CreateNewInstance();
            Assert.That( nbEvents, Is.EqualTo( 5 ) );
            Assert.That( docBorrowed, Is.EqualTo( 1 ) );

        }

    }
}
