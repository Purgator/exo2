using ITI.DocLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DocLib.Impl
{
    internal class Document : IDocument
    {
        private List<IDocumentInstance> _documentsInstance;

        public Document()
        {
            _documentsInstance = new List<IDocumentInstance>();
        }
        public string Code { get; set; }

        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int TotalInstanceCount => _documentsInstance.Count;

        public int BorrowedCount => _documentsInstance.Count(x => x.Borrower != null);

        public int FreeCount => _documentsInstance.Count;

        public IDocumentInstance CreateNewInstance()
        {
            DocumentInstance documentInstance = new DocumentInstance { UniqueIdentifier = Guid.NewGuid(), Document = this };
            _documentsInstance.Add(documentInstance);
            return documentInstance;
        }

        public IDocumentInstance Find(Guid uniqueIdentifier)
        {
            foreach(DocumentInstance documentInstance in _documentsInstance)
            {
                if (documentInstance.UniqueIdentifier.Equals(uniqueIdentifier))
                    return documentInstance;
            }
            return null;
        }

        public IDocumentInstance FindFreeInstance()
        {
            throw new NotImplementedException();
        }
    }
}
