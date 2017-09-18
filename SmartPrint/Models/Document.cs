using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
    public class Document
    {
        private readonly string _name;
        private readonly string _path;

        public Document(string name, string path)
        {
            _name = name;
            _path = path;
        }

        /// <summary>
        /// Returns the document display name
        /// </summary>
        public string GetName()
        {
            return _name;
        }

        /// <summary>
        /// Returns the document path
        /// </summary>
        public string GetPath()
        {
            return _path;
        }
    }
}