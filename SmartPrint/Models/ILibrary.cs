using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrint.Models
{
    /// <summary>
    /// A very simple interface to defines what are the required methods.
    /// The implementation is in the Library class where it is populated from file system.
    /// It can easily be changed to populate the library from XML file, database, etc...
    /// </summary>
    internal interface ILibrary
    {
        /// <summary>
        /// Returns the library document count.
        /// </summary>
        int GetDocumentCount();

        /// <summary>
        /// Returns the document name for a given index.
        /// </summary>
        /// <param name="index">0 based index of the item in library.</param>
        string GetDocumentName(int index);

        /// <summary>
        /// Returns the document full path for a given index.
        /// </summary>
        /// <param name="index">0 based index of the item in library.</param>
        string GetDocumentPath(int index);
    }
}

