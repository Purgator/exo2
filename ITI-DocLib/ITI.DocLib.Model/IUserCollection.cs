using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ITI.DocLib.Model
{
    /// <summary>
    /// The user collection manages <see cref="IUser">users</see> of a <see cref="ILibrary"/>. 
    /// It is an <see cref="IEnumerable"/> of users than can only be obtained thanks to the <see cref="ILibrary.Users"/> property.
    /// </summary>
    public interface IUserCollection : IEnumerable<IUser>
    {
        /// <summary>
        /// Finds a <see cref="IUser"/> by its <see cref="IUser.Name"/>.
        /// </summary>
        /// <param name="name">The name of the user we are looking for.</param>
        /// <returns>A <see cref="IUser"/> or null if not found.</returns>
        IUser Find( string name );

        /// <summary>
        /// Creates a new <see cref="IUser"/> with the name provided (that must uniquely identify the user).
        /// This is the only way to create new users.
        /// </summary>
        /// <param name="name">A non null, non empty, non existing user name, otherwise a <see cref="DocLibException"/> is thrown.</param>
        /// <returns>The newly created user.</returns>
        IUser Create( string name );

        /// <summary>
        /// Gets the number of users that this collection contains.
        /// </summary>
        int Count { get; }

    }
}
