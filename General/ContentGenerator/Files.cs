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
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Graphics;
using Toolset.GraphicsDevice;
using ServiceContainer = System.ComponentModel.Design.ServiceContainer;

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
        /// Dummy form for creating a graphics device
        /// </summary>
        static readonly Form Form = new Form(); 

        /// <summary>
        /// Our Graphics Device Service, which holds all the data needed about our screen
        /// </summary>
        static GraphicsDeviceService _gds = null;

        /// <summary>
        /// Our Service Container
        /// </summary>
        static Toolset.GraphicsDevice.ServiceContainer _services = null;

        /// <summary>
        /// Our Content Manager
        /// </summary>
        static ContentManager _content;

        /// <summary>
        /// The File List
        /// </summary>
        static Dictionary<string, string> _filesList = null;

        /// <summary>
        /// Percent of files that has loaded in
        /// </summary>
        static double _percent = 0;

        /// <summary>
        /// Amount to jump at each point of change in percent
        /// </summary>
        static decimal _jump = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Empty Constructor simply initializing our Files List
        /// </summary>
        static Files()
        {
            _filesList = new Dictionary<string, string>();
            _gds = GraphicsDeviceService.AddRef(Form.Handle, Form.ClientSize.Width, Form.ClientSize.Height);
            _services = new Toolset.GraphicsDevice.ServiceContainer();
            _services.AddService<IGraphicsDeviceService>(_gds);
            _content = new ContentManager(_services, Content);
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
                Console.Write("\rLoading In Files: {0}%", (int)_percent);
                
                //Load all files inside the folders inside Content, Calculate the jump to make, and test each file
                LoadFiles(Content + "\\");
                CalculateJump();
                TestFiles();
                
                //Write out our file
                Write(_filesList, null, Content + ".xnb", TargetPlatform.Windows);
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

            //Only load files that end in .xnb and remove our Content folders name);
            foreach (var file in from f in Directory.GetFiles(name, "*", SearchOption.AllDirectories)
                                 where new FileInfo(f).Extension == ".xnb"
                                 select f.Replace(Content + "\\", "")
                                 into file select file.Replace(".xnb", ""))
            {
                _filesList.Add(file, "Unknown");
            }
        }

        /// <summary>
        /// Test each file
        /// </summary>
        static void TestFiles()
        {
            var tempList = new Dictionary<string, string>();
            foreach(var k in _filesList.Keys)
            {
                //Load in our file for testing and determine what type it is
                var testFile = _content.Load<object>(k);
                tempList.Add(k, testFile.GetType().Name);
                
                //Unload content because there is a 9
                _content.Unload();

                //Update our Percent and write it out
                _percent += (double)_jump;
                Console.Write("\rLoading In Files: {0}%", (int)_percent);
            }

            _filesList = tempList;
        }

        /// <summary>
        /// Simply devide 100 into the total number of files being scanned in
        /// </summary>
        static void CalculateJump()
        {
            _jump = 100m/_filesList.Count;
        }

        #endregion

        #region XML Methods

        /// <summary>
        /// Write our XNB file using the data we have gathered in the previous methods
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="writer"></param>
        /// <param name="fileName"></param>
        /// <param name="targetPlatform"></param>
        static void Write(Object obj, ContentTypeWriter writer, string fileName, TargetPlatform targetPlatform)
        {
            //Use reflection to get the otherwise internal parameters and methods of the typewriter 
            var constructors = typeof(ContentCompiler).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var addWriter = typeof(ContentCompiler).GetMethod("AddTypeWriter", BindingFlags.NonPublic | BindingFlags.Instance);
            var compileContent = typeof(ContentCompiler).GetMethod("Compile", BindingFlags.NonPublic | BindingFlags.Instance);

            var compiler = constructors[0].Invoke(null) as ContentCompiler;

            //Initialize our writter if it isn't
            if (writer != null)
                addWriter.Invoke(compiler, new object[] { writer });
            
            //Split the .xnb from the filename 
            var path = fileName.Replace(".xnb", ""); 

            //Generate our File
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                compileContent.Invoke(compiler,
                    new [] 
                    { 
                         stream, 
                         obj, 
                         targetPlatform,
                         GraphicsProfile.Reach,
                         true,
                         path, 
                         path, 
                    });
            }

            //Test our file
            Console.WriteLine("");
            Console.WriteLine("Testing file...");
            
            //Create our ContentManager
            var services = new GameServiceContainer();
            var content = new ContentManager(services, "");
            //Load our File in
            var test = content.Load<Dictionary<string, string>>(path);
            //Write our files contents
            foreach(var s in test)
                Console.WriteLine(s);
            //Wait for user responce
            Console.ReadKey();
        } 

        #endregion
    }
}
