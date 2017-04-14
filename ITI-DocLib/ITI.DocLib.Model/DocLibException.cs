using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ITI.DocLib.Model
{
    [Serializable]
    public class DocLibException : ApplicationException
    {
        public DocLibException( string message )
            : base( message )
        {
        }
        
        public DocLibException( string message, Exception inner )
            : base( message, inner )
        {
        }

        protected DocLibException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }
}
