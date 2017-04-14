using ITI.DocLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using ITI.DocLib.Impl;

namespace ITI.DocLib
{
    internal class Library : ILibrary
    {
        private IUserCollection _users;
        private List<IDocument> _documents;
        public IUserCollection Users => _users ;

        public int Count => _documents.Count;

        public Library()
        {
            _users = new UserCollection(this);
            _documents = new List<IDocument>();
        }

        public event EventHandler<UserEventArgs> UserCreated;
        public event EventHandler<DocumentEventArgs> DocumentCreated;
        public event EventHandler<DocumentInstanceEventArgs> DocumentBorrowed;

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IDocument CreateDocument(string code)
        {
            Document document = new Document { Code = code };
            _documents.Add(document);
            return document;
        }

        public IDocument Find(string code)
        {
            foreach(Document document in _documents)
            {
                if (document.Code.Equals(code))
                    return document;
            }
            return null;
        }

        public IEnumerable<IDocument> FindByTitle(string pattern)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IDocument> GetEnumerator()
        {
            return GetEnumerator();
        }

        public void SaveInto(Stream s)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _documents.GetEnumerator();
        }
    }
}
