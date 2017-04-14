using ITI.DocLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DocLib
{
    internal class User : IUser
    {
        string _name;
        ILibrary _library;

        public User(string name, ILibrary library)
        {
            _name = name;
            _library = library;
        }

        public ILibrary Library => _library;

        public string Name => _name;

        public DateTime BorrowedDate => throw new NotImplementedException();

        public IDocumentInstance BorrowedDocument { get; set; }
    }
}
