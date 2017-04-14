using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ITI.DocLib.Model
{
    /// <summary>
    /// A library contains <see cref="IDocument"/> objects (it is itself a <see cref="IEnumerable"/> of documents).
    /// It also contains a collection of <see cref="IUser"/>
    /// </summary>
    public interface ILibrary : IReadOnlyCollection<IDocument>
    {
        /// <summary>
        /// Finds a <see cref="IDocument"/> by its <see cref="IDocument.Code"/>.
        /// </summary>
        /// <param name="code">The code of the document we are looking for.</param>
        /// <returns>A <see cref="IDocument"/> or null if not found.</returns>
        IDocument Find( string code );

        /// <summary>
        /// Creates a new <see cref="IDocument"/> with the code provided (that must uniquely identify the document).
        /// This is the only way to create new documents.
        /// </summary>
        /// <param name="code">A non null, non empty, non existing document code, otherwise a <see cref="DocLibException"/> is thrown.</param>
        /// <returns>The newly created document.</returns>
        IDocument CreateDocument( string code );

        /// <summary>
        /// Clears this library: no more documents and no more <see cref="Users"/> exist once this method has been called.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the collection of <see cref="IUser"/> of this library.
        /// </summary>
        IUserCollection Users { get; }

        /// <summary>
        /// Fires whenever a new user has been created in this library.
        /// </summary>
        event EventHandler<UserEventArgs> UserCreated;

        /// <summary>
        /// Fires whenever a new document has been created in this library.
        /// </summary>
        event EventHandler<DocumentEventArgs> DocumentCreated;

        /// <summary>
        /// Fires whenever a <see cref="IUser"/> borrowed a <see cref="IDocumentInstance"/>.
        /// </summary>
        event EventHandler<DocumentInstanceEventArgs> DocumentBorrowed;

        /// <summary>
        /// Saves the content of this library in a stream. The internal format is not specified: it can be any 
        /// format as long as <see cref="LoadFrom"/> can read it back.
        /// </summary>
        /// <param name="s"></param>
        void SaveInto( Stream s );
        
        /// <summary>
        /// Finds a list of documents given a pattern with wildcards: you can use * and ? (just like file wildcards).
        /// </summary>
        /// <param name="pattern">A pattern like "reserv*dog?".</param>
        /// <returns>A list of documents. An empty list if no such documents.</returns>
        IEnumerable<IDocument> FindByTitle( string pattern );
    }
}
