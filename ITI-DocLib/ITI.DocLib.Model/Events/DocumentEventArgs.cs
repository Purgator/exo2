using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.DocLib.Model
{
    /// <summary>
    /// Defines an event argument related to a <see cref="Document"/>.
    /// </summary>
    public class DocumentEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the document to which this event relates.
        /// </summary>
        public IDocument Document { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DocumentEventArgs"/>.
        /// </summary>
        /// <param name="d">Document for the event.</param>
        public DocumentEventArgs( IDocument d )
        {
            Document = d;
        }

    }
}
