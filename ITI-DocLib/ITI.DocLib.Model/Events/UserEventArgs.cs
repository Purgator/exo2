using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.DocLib.Model
{
    /// <summary>
    /// Defines an event argument related to a <see cref="User"/>.
    /// </summary>
    public class UserEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the user to which this event relates.
        /// </summary>
        public IUser User { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="UserEventArgs"/>.
        /// </summary>
        /// <param name="u"></param>
        public UserEventArgs( IUser u )
        {
            User = u;
        }

    }
}
