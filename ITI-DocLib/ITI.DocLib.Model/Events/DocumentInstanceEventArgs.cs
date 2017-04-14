using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.DocLib.Model
{
    /// <summary>
    /// Defines an event argument related to a <see cref="DocumentInstance"/>.
    /// </summary>
    public class DocumentInstanceEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the document instance to which this event relates.
        /// </summary>
        public IDocumentInstance DocumentInstance { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DocumentInstanceEventArgs"/>.
        /// </summary>
        /// <param name="d">DocumentInstance for the event.</param>
        public DocumentInstanceEventArgs( IDocumentInstance d )
        {
            DocumentInstance = d;
        }

    }
}
