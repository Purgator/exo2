using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.DocLib.Model
{
    /// <summary>
    /// A document object is uniquely identified in a <see cref="ILibrary"/> by its <see cref="Code"/>.
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Unique identifier of the document.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets or sets the title of the document. Must not be null or empty otherwise a <see cref="ArgumentException"/>
        /// is thrown. It defaults to <see cref="String.Empty"/>.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets the number of <see cref="IDocumentInstance"/>.
        /// </summary>
        int TotalInstanceCount { get; }

        /// <summary>
        /// Gets the number of <see cref="IDocumentInstance"/> that have been borrowed by users.
        /// </summary>
        int BorrowedCount { get; }

        /// <summary>
        /// Gets the number of <see cref="IDocumentInstance"/> that are not borrowed by any users.
        /// </summary>
        int FreeCount { get; }

        /// <summary>
        /// Creates a new <see cref="IDocumentInstance"/> (its <see cref="IDocumentInstance.UniqueIdentifier"/> is automatically generated).
        /// </summary>
        /// <returns>A new document instance, ready to be borrowed by a <see cref="IUser"/>.</returns>
        IDocumentInstance CreateNewInstance();

        /// <summary>
        /// Finds a <see cref="IDocumentInstance"/> by its unique identifier.
        /// It may be borrowed or not.
        /// </summary>
        /// <param name="uniqueIdentifier">The unique identifier of the instance to find.</param>
        /// <returns>The instance or null if not found.</returns>
        IDocumentInstance Find( Guid uniqueIdentifier );

        /// <summary>
        /// Finds a <see cref="IDocumentInstance"/> that is not borrowed yet.
        /// </summary>
        /// <returns>A free instance or null if there is no document to borrow.</returns>
        IDocumentInstance FindFreeInstance();

    }
}
