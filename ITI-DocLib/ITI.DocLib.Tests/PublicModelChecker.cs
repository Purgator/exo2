using ITI.DocLib.Impl;
using ITI.DocLib.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ITI.DocLib.Tests
{
    [TestFixture]
    public class PublicModelChecker
    {
        static readonly Assembly _modelAssembly = typeof( ILibrary ).Assembly;
        static readonly Assembly _implementationAssembly = typeof( LibraryLoader ).Assembly;

        [Explicit]
        [Test]
        public void write_current_public_model_API_to_console_with_double_quotes()
        {
            Console.WriteLine( GetPublicAPI( _modelAssembly ).ToString().Replace( "\"", "\"\"" ) );
        }

        [Explicit]
        [Test]
        public void write_current_public_implementation_to_console_with_double_quotes()
        {
            Console.WriteLine( GetPublicAPI( _implementationAssembly ).ToString().Replace( "\"", "\"\"" ) );
        }

        [Test]
        public void the_implementation_must_only_expose_the_LibraryLoader()
        {
            var model = XElement.Parse( @"
<Assembly Name=""ITI.DocLib.Impl"">
  <Types>
    <Type Name=""ITI.DocLib.Impl.LibraryLoader"">
      <Member Type=""Method"" Name=""Create"" />
      <Member Type=""Method"" Name=""Equals"" />
      <Member Type=""Method"" Name=""GetHashCode"" />
      <Member Type=""Method"" Name=""GetType"" />
      <Member Type=""Method"" Name=""Load"" />
      <Member Type=""Method"" Name=""ToString"" />
    </Type>
  </Types>
</Assembly>" );
            CheckPublicAPI( model, _implementationAssembly );
        }

        [Test]
        public void the_public_API_of_the_model_must_not_be_modified()
        {
            var model = XElement.Parse( @"<Assembly Name=""ITI.DocLib.Model"">
  <Types>
    <Type Name=""ITI.DocLib.Model.DocLibException"">
      <Member Type=""Constructor"" Name="".ctor"" />
      <Member Type=""Constructor"" Name="".ctor"" />
      <Member Type=""Property"" Name=""Data"" />
      <Member Type=""Method"" Name=""Equals"" />
      <Member Type=""Method"" Name=""get_Data"" />
      <Member Type=""Method"" Name=""get_HelpLink"" />
      <Member Type=""Method"" Name=""get_HResult"" />
      <Member Type=""Method"" Name=""get_InnerException"" />
      <Member Type=""Method"" Name=""get_Message"" />
      <Member Type=""Method"" Name=""get_Source"" />
      <Member Type=""Method"" Name=""get_StackTrace"" />
      <Member Type=""Method"" Name=""get_TargetSite"" />
      <Member Type=""Method"" Name=""GetBaseException"" />
      <Member Type=""Method"" Name=""GetHashCode"" />
      <Member Type=""Method"" Name=""GetObjectData"" />
      <Member Type=""Method"" Name=""GetType"" />
      <Member Type=""Method"" Name=""GetType"" />
      <Member Type=""Property"" Name=""HelpLink"" />
      <Member Type=""Property"" Name=""HResult"" />
      <Member Type=""Property"" Name=""InnerException"" />
      <Member Type=""Property"" Name=""Message"" />
      <Member Type=""Method"" Name=""set_HelpLink"" />
      <Member Type=""Method"" Name=""set_Source"" />
      <Member Type=""Property"" Name=""Source"" />
      <Member Type=""Property"" Name=""StackTrace"" />
      <Member Type=""Property"" Name=""TargetSite"" />
      <Member Type=""Method"" Name=""ToString"" />
    </Type>
    <Type Name=""ITI.DocLib.Model.DocumentEventArgs"">
      <Member Type=""Constructor"" Name="".ctor"" />
      <Member Type=""Property"" Name=""Document"" />
      <Member Type=""Method"" Name=""Equals"" />
      <Member Type=""Method"" Name=""get_Document"" />
      <Member Type=""Method"" Name=""GetHashCode"" />
      <Member Type=""Method"" Name=""GetType"" />
      <Member Type=""Method"" Name=""ToString"" />
    </Type>
    <Type Name=""ITI.DocLib.Model.DocumentInstanceEventArgs"">
      <Member Type=""Constructor"" Name="".ctor"" />
      <Member Type=""Property"" Name=""DocumentInstance"" />
      <Member Type=""Method"" Name=""Equals"" />
      <Member Type=""Method"" Name=""get_DocumentInstance"" />
      <Member Type=""Method"" Name=""GetHashCode"" />
      <Member Type=""Method"" Name=""GetType"" />
      <Member Type=""Method"" Name=""ToString"" />
    </Type>
    <Type Name=""ITI.DocLib.Model.IDocument"">
      <Member Type=""Property"" Name=""BorrowedCount"" />
      <Member Type=""Property"" Name=""Code"" />
      <Member Type=""Method"" Name=""CreateNewInstance"" />
      <Member Type=""Method"" Name=""Find"" />
      <Member Type=""Method"" Name=""FindFreeInstance"" />
      <Member Type=""Property"" Name=""FreeCount"" />
      <Member Type=""Method"" Name=""get_BorrowedCount"" />
      <Member Type=""Method"" Name=""get_Code"" />
      <Member Type=""Method"" Name=""get_FreeCount"" />
      <Member Type=""Method"" Name=""get_Title"" />
      <Member Type=""Method"" Name=""get_TotalInstanceCount"" />
      <Member Type=""Method"" Name=""set_Title"" />
      <Member Type=""Property"" Name=""Title"" />
      <Member Type=""Property"" Name=""TotalInstanceCount"" />
    </Type>
    <Type Name=""ITI.DocLib.Model.IDocumentInstance"">
      <Member Type=""Property"" Name=""Borrower"" />
      <Member Type=""Property"" Name=""Document"" />
      <Member Type=""Method"" Name=""get_Borrower"" />
      <Member Type=""Method"" Name=""get_Document"" />
      <Member Type=""Method"" Name=""get_UniqueIdentifier"" />
      <Member Type=""Property"" Name=""UniqueIdentifier"" />
    </Type>
    <Type Name=""ITI.DocLib.Model.ILibrary"">
      <Member Type=""Method"" Name=""add_DocumentBorrowed"" />
      <Member Type=""Method"" Name=""add_DocumentCreated"" />
      <Member Type=""Method"" Name=""add_UserCreated"" />
      <Member Type=""Method"" Name=""Clear"" />
      <Member Type=""Method"" Name=""CreateDocument"" />
      <Member Type=""Event"" Name=""DocumentBorrowed"" />
      <Member Type=""Event"" Name=""DocumentCreated"" />
      <Member Type=""Method"" Name=""Find"" />
      <Member Type=""Method"" Name=""FindByTitle"" />
      <Member Type=""Method"" Name=""get_Users"" />
      <Member Type=""Method"" Name=""remove_DocumentBorrowed"" />
      <Member Type=""Method"" Name=""remove_DocumentCreated"" />
      <Member Type=""Method"" Name=""remove_UserCreated"" />
      <Member Type=""Method"" Name=""SaveInto"" />
      <Member Type=""Event"" Name=""UserCreated"" />
      <Member Type=""Property"" Name=""Users"" />
    </Type>
    <Type Name=""ITI.DocLib.Model.IUser"">
      <Member Type=""Property"" Name=""BorrowedDate"" />
      <Member Type=""Property"" Name=""BorrowedDocument"" />
      <Member Type=""Method"" Name=""get_BorrowedDate"" />
      <Member Type=""Method"" Name=""get_BorrowedDocument"" />
      <Member Type=""Method"" Name=""get_Library"" />
      <Member Type=""Method"" Name=""get_Name"" />
      <Member Type=""Property"" Name=""Library"" />
      <Member Type=""Property"" Name=""Name"" />
      <Member Type=""Method"" Name=""set_BorrowedDocument"" />
    </Type>
    <Type Name=""ITI.DocLib.Model.IUserCollection"">
      <Member Type=""Property"" Name=""Count"" />
      <Member Type=""Method"" Name=""Create"" />
      <Member Type=""Method"" Name=""Find"" />
      <Member Type=""Method"" Name=""get_Count"" />
    </Type>
    <Type Name=""ITI.DocLib.Model.UserEventArgs"">
      <Member Type=""Constructor"" Name="".ctor"" />
      <Member Type=""Method"" Name=""Equals"" />
      <Member Type=""Method"" Name=""get_User"" />
      <Member Type=""Method"" Name=""GetHashCode"" />
      <Member Type=""Method"" Name=""GetType"" />
      <Member Type=""Method"" Name=""ToString"" />
      <Member Type=""Property"" Name=""User"" />
    </Type>
  </Types>
</Assembly>
" );
            CheckPublicAPI( model, _modelAssembly );
        }

        void CheckPublicAPI( XElement model, Assembly assembly )
        {
            XElement current = GetPublicAPI( assembly );
            if( !XElement.DeepEquals( model, current ) )
            {
                string m = model.ToString( SaveOptions.DisableFormatting );
                string c = current.ToString( SaveOptions.DisableFormatting );
                Assert.That( c, Is.EqualTo( m ) );
            }
        }

        XElement GetPublicAPI( Assembly a )
        {
            return new XElement( "Assembly",
                                  new XAttribute( "Name", a.GetName().Name ),
                                  new XElement( "Types",
                                                AllNestedTypes( a.GetExportedTypes() )
                                                 .OrderBy( t => t.FullName )
                                                 .Select( t => new XElement( "Type",
                                                                                new XAttribute( "Name", t.FullName ),
                                                                                t.GetMembers()
                                                                                 .OrderBy( m => m.Name )
                                                                                 .Select( m => new XElement( "Member",
                                                                                                              new XAttribute( "Type", m.MemberType ),
                                                                                                              new XAttribute( "Name", m.Name ) ) ) ) ) ) );
        }

        IEnumerable<Type> AllNestedTypes( IEnumerable<Type> types )
        {
            foreach( Type t in types )
            {
                yield return t;
                foreach( Type nestedType in AllNestedTypes( t.GetNestedTypes() ) )
                {
                    yield return nestedType;
                }
            }
        }
    }
}
