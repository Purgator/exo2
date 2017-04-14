using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.DocLib.Model
{
    /// <summary>
    /// A document instance can be borrowed by a <see cref="IUser"/>.
    /// </summary>
    public interface IDocumentInstance
    {
        /// <summary>
        /// Gets the document.
        /// </summary>
        IDocument Document { get; }

        /// <summary>
        /// Gets the unique identifier of this instance.
        /// </summary>
        Guid UniqueIdentifier { get; }

        /// <summary>
        /// Gets the user borrowed by this user. 
        /// </summary>
        IUser Borrower { get; }
    }

}
