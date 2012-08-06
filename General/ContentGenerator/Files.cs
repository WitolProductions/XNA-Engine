// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: Files.cs Version: 1.0 Last Edited: 8/6/2012
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace ContentGenerator
{
    public static class Files
    {
        #region Fields

        /// <summary>
        /// Your Content Folder
        /// </summary>
        const string Content = "Content";
        
        /// <summary>
        /// The File List
        /// </summary>
        static List<string> _filesList = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Empty Constructor simply initializing our Files List
        /// </summary>
        static Files()
        {
            _filesList = new List<string>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load in all our Files
        /// </summary>
        public static void LoadInFiles()
        {
            if (Directory.Exists(Content))
            {
                Console.WriteLine(Content + " folder found");

                //Load all files inside Content
                LoadFiles(Content + "\\");
                //Load all files inside the folders inside Content
                foreach (var f in Directory.GetDirectories(Content))
                    LoadFiles(f);

                Serialize();
            }
            else
            {
                Console.WriteLine("Content folder does not exist.");
                Console.ReadKey();
            }

        }

        /// <summary>
        /// Load Files based on Directory sent
        /// </summary>
        /// <param name="name"></param>
        static void LoadFiles(string name)
        {
            //Only load files that end in .xnb and remove our Content folders name
            foreach (var file in from f in Directory.GetFiles(name)
                                 where new FileInfo(f).Extension == ".xnb"
                                 select f.Replace(Content + "\\", "")
                                 into file select file.Replace(".xnb", ""))
                _filesList.Add(file);
        }

        #endregion

        #region XML Methods

        static void Serialize()
        {
            //Serialize our File list
            using (var writer = XmlWriter.Create(Content + ".xml", new XmlWriterSettings { Indent = true }))
                IntermediateSerializer.Serialize(writer, _filesList, null);
        }

        #endregion
    }
}
