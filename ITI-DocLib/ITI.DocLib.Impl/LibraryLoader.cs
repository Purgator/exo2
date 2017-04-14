using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITI.DocLib.Model;
using System.IO;

namespace ITI.DocLib.Impl
{

    /// <summary>
    /// Factory for <see cref="ILibrary"/> object. This is the only public class of this assembly
    /// since the whole implementation is inside it and only interfaces that are defined in ITI.DocLib.Model 
    /// must be accessible to the universe.
    /// </summary>
    public static class LibraryLoader
    {
        /// <summary>
        /// Creates a new <see cref="ILibrary"/> object.
        /// </summary>
        /// <returns></returns>
        static public ILibrary Create()
        {
            return new Library();
        }

        /// <summary>
        /// Loads a library previously saved thanks to <see cref="ILibrary.SaveInto"/> method.
        /// </summary>
        /// <param name="s">A stream object.</param>
        static public ILibrary Load( Stream s )
        {
            return null;
        }

    }
}
