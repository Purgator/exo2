using System;
using System.Collections.Generic;
using System.Text;
using ITI.DocLib.Model;
using ITI.DocLib.Impl;
using NUnit.Framework;
using System.Threading;

namespace ITI.DocLib.Tests_1_Point_Per_Green_Below
{
    [TestFixture]
    public class T2BorrowTests
    {
        [Test]
        public void t01_a_user_borrows_a_document_instance_by_setting_the_BorrowedDocument_property()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "ISBN" );
            IDocumentInstance i = d.CreateNewInstance();
            IUser u = lib.Users.Create( "Olivier" );
            u.BorrowedDocument = i;
            Assert.That( u.BorrowedDocument, Is.EqualTo( i ), "Setting the BorrowedDocument did its job..." );
            Assert.That( i.Borrower, Is.EqualTo( u ), "...and the invert relation is up to date: the instance.Borrower references the user." );
        }

        [Test]
        public void t02_borrowing_a_document_instance_updates_the_BorrowedDate_property_of_the_user()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "ISBN" );
            IDocumentInstance i = d.CreateNewInstance();
            IUser u = lib.Users.Create( "Olivier" );
            Assert.That( u.BorrowedDate, Is.EqualTo( DateTime.MinValue ) );
            u.BorrowedDocument = i;
            Assert.That( u.BorrowedDate, Is.EqualTo( DateTime.Now ).Within( TimeSpan.FromMilliseconds( 2 ) ), "Setter did its job." );
        }

        [Test]
        public void t03_when_the_user_brings_back_the_document_its_BorrowedDate_is_set_back_to_DateTimeMinValue()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "ISBN" );
            IDocumentInstance i = d.CreateNewInstance();
            IUser u = lib.Users.Create( "Olivier" );
            Assert.That( u.BorrowedDate, Is.EqualTo( DateTime.MinValue ) );
            u.BorrowedDocument = i;
            Assert.That( u.BorrowedDate, Is.EqualTo( DateTime.Now ).Within( TimeSpan.FromMilliseconds( 2 ) ), "Setter did its job." );
            u.BorrowedDocument = null;
            Assert.That( u.BorrowedDate, Is.EqualTo( DateTime.MinValue ) );
        }

        [Test]
        public void t04_when_the_user_brings_back_the_document_instance_the_document_BorrowedCount_and_FreeCount_properties_are_updated()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "ISBN" );
            IDocumentInstance i = d.CreateNewInstance();
            IUser u = lib.Users.Create( "Olivier" );
            u.BorrowedDocument = i;
            Assert.That( i.Borrower, Is.EqualTo( u ), "The invert relation is up to date." );
            
            // Checks counts
            Assert.That( d.BorrowedCount, Is.EqualTo( 1 ) );
            Assert.That( d.FreeCount, Is.EqualTo( 0 ) );
            IDocumentInstance i2 = d.CreateNewInstance();
            Assert.That( d.BorrowedCount, Is.EqualTo( 1 ) );
            Assert.That( d.FreeCount, Is.EqualTo( 1 ) );

            // Bring it back.
            u.BorrowedDocument = null;
            Assert.That( i.Borrower, Is.Null, "The invert relation is up to date." );

            // Checks counts
            Assert.That( d.BorrowedCount, Is.EqualTo( 0 ) );
            Assert.That( d.FreeCount, Is.EqualTo( 2 ) );
        }


        [Test]
        public void t05_two_users_can_not_borrow_the_same_document_instance()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "ISBN" );
            IDocumentInstance i = d.CreateNewInstance();
            IUser u1 = lib.Users.Create( "Olivier" );
            u1.BorrowedDocument = i;

            Assert.That( i.Borrower, Is.EqualTo( u1 ) );
            IUser u2 = lib.Users.Create( "Albert" );
            Assert.Throws<DocLibException>( () => u2.BorrowedDocument = i );
        
            // u1 brings it back.
            u1.BorrowedDocument = null;
            Assert.That( i.Borrower, Is.Null );
            u2.BorrowedDocument = i;
            Assert.That( i.Borrower, Is.EqualTo( u2 ) );

            Assert.Throws<DocLibException>( () => u1.BorrowedDocument = i );
        }

        [Test]
        public void t06_a_user_can_not_borrow_more_than_one_document()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "ISBN" );
            IDocumentInstance i1 = d.CreateNewInstance();
            IDocumentInstance i2 = d.CreateNewInstance();
            IUser u = lib.Users.Create( "Olivier" );
            u.BorrowedDocument = i1;
            // Setting it twice is okay.
            u.BorrowedDocument = i1;

            Assert.Throws<DocLibException>( () => u.BorrowedDocument = i2 );
            u.BorrowedDocument = null;
            Assert.That( i1.Borrower, Is.Null );

            Assert.DoesNotThrow( () => u.BorrowedDocument = i2 );
        }
        [Test]
        public void t07_when_the_same_user_borrows_the_same_document_instance_nothing_change()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "ISBN" );
            IDocumentInstance i = d.CreateNewInstance();
            IUser u = lib.Users.Create( "Olivier" );

            u.BorrowedDocument = i;
            DateTime date = u.BorrowedDate;
            int freeCount = d.FreeCount;
            int total = d.TotalInstanceCount;
            Thread.Sleep( 5 );
            
            // Setting it twice is okay.
            u.BorrowedDocument = i;
            Assert.That( u.BorrowedDate, Is.EqualTo( date ) );
            Assert.That( d.FreeCount, Is.EqualTo( freeCount ) );
            Assert.That( d.TotalInstanceCount, Is.EqualTo( total ) );
        }

        [Test]
        public void t08_find_a_document_instance_that_can_borrowed()
        {
            ILibrary lib = LibraryLoader.Create();
            IDocument d = lib.CreateDocument( "d" );

            Assert.That( d.FindFreeInstance(), Is.Null );

            IDocumentInstance i1 = d.CreateNewInstance();
            IDocumentInstance i2 = d.CreateNewInstance();
            IDocumentInstance i3 = d.CreateNewInstance();
            IDocumentInstance i4 = d.CreateNewInstance();
            IDocumentInstance i5 = d.CreateNewInstance();

            Assert.That( d.FindFreeInstance(), Is.Not.Null );

            IUser u1 = lib.Users.Create( "u1" );
            IUser u2 = lib.Users.Create( "u2" );
            IUser u3 = lib.Users.Create( "u3" );
            IUser u4 = lib.Users.Create( "u4" );
            IUser u5 = lib.Users.Create( "u5" );

            u1.BorrowedDocument = i1;
            u2.BorrowedDocument = i2;
            u3.BorrowedDocument = i3;
            u4.BorrowedDocument = i4;
            u5.BorrowedDocument = i5;

            Assert.That( d.FindFreeInstance(), Is.Null );

            u3.BorrowedDocument = null;

            Assert.That( d.FindFreeInstance(), Is.EqualTo( i3 ) );

        }

    }
}
