using ITI.DocLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ITI.DocLib
{
    internal class UserCollection : IUserCollection
    {
        private Library _library;
        private List<User> _users;

        public int Count => _users.Count;

        public UserCollection(Library library)
        {
            _library = library;
            _users = new List<User>();
        }

        public IUser Create(string name)
        {
            User user = new User(name, _library);
            _users.Add(user);
            return user;
        }

        public IUser Find(string name)
        {
            foreach(User user in _users)
            {
                if (user.Name.Equals(name))
                    return user;
            }
            return null;
        }

        public IEnumerator<IUser> GetEnumerator()
        {
            return _users.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
