using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.DocLib.Model
{
    /// <summary>
    /// A user can borrow one (and only one) <see cref="IDocumentInstance"/>.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// The <see cref="ILibrary"/> to which this user belongs.
        /// </summary>
        ILibrary Library { get; }

        /// <summary>
        /// Gets the name of the user. This name is unique in the <see cref="Library"/>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the time at which the <see cref="BorrowedDocument"/> has been set. When <see cref="BorrowedDocument"/> is null, this 
        /// is set to <see cref="DateTime.MinValue"/>.
        /// </summary>
        DateTime BorrowedDate { get; }

        /// <summary>
        /// Gets or sets the document borrowed by this user. See remarks.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When setting to a non null value, the <see cref="BorrowedDate"/> is automatically updated to <see cref="DateTime.Now"/>
        /// and the <see cref="IDocumentInstance.Borrower"/> is set to this user.
        /// This is possible ONLY if this user do NOT already borrow an instance AND if the proposed borrowed instance is NOT borrowed by another user: 
        /// a <see cref="DocLibException"/> is thrown in such cases.
        /// </para>
        /// <para>
        /// When setting to a null value, the <see cref="BorrowedDate"/> is set to <see cref="DateTime.MinValue"/> and the 
        /// previous <see cref="IDocumentInstance.Borrower"/> is set to null: the instance becomes ready to be borrowed by another user.
        /// </para>
        /// <para>
        /// When setting to the current value, nothing happens. Otherwise, the <see cref="ILibrary.DocumentBorrowed"/> is raised: by tracking this 
        /// event one can know when a document is borrowed or becomes free again.
        /// </para>
        /// </remarks>
        IDocumentInstance BorrowedDocument { get; set; }


    }

}
