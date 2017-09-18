using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
   
        internal sealed class Library : ILibrary
        {
            internal readonly List<Document> Documents;

            /// <summary>
            /// The constructor builds an internal document list from the content of the document directory
            /// </summary>
            /// <param name="directory">The directory to build the document list from.</param>
            public Library(string directory)
            {
                // An exception will be thrown here is the documents directory does not exist
                // The folder already exists in this project, that's where you need to put your documents
                string[] filePaths = Directory.GetFiles(directory);
                Documents = new List<Document>(filePaths.Length);
                foreach (string file in filePaths)
                {
                    Documents.Add(new Document(Path.GetFileName(file), file));
                }
            }

            /// <summary>
            /// Returns the library document count.
            /// </summary>
            public int GetDocumentCount()
            {
                return Documents.Count;
            }

            /// <summary>
            /// Returns the document name for a given index.
            /// </summary>
            /// <param name="index">0 based index of the item in library.</param>
            public string GetDocumentName(int index)
            {
                return Documents[index].GetName();
            }

            /// <summary>
            /// Returns the document full path for a given index.
            /// </summary>
            /// <param name="index">0 based index of the item in library.</param>
            public string GetDocumentPath(int index)
            {
                return Documents[index].GetPath();
            }
        }
    }