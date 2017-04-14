using ITI.DocLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DocLib.Impl
{
    internal class DocumentInstance : IDocumentInstance
    {
        public IDocument Document { get; set; }

        public Guid UniqueIdentifier { get; set; }

        public IUser Borrower { get; set; }
    }
}
