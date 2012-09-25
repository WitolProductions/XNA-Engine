﻿// -----------------------------------------------------------------------
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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Graphics;

namespace ContentGenerator
{
    [ContentSerializerRuntimeType("Content.ContentTypes.Files, Content")]
    public static class Files
    {
        #region Fields

        /// <summary>
        /// Your Content Folder
        /// </summary>
        [ContentSerializerIgnore]
        const string Content = "Content";

        /// <summary>
        /// Dummy form for creating a graphics device
        /// </summary>
        [ContentSerializerIgnore]
        static readonly Form Form = new Form();

        /// <summary>
        /// Our Graphics Device Service, which holds all the data needed about our screen
        /// </summary>
        [ContentSerializerIgnore]
        static GraphicsDeviceService _gds = null;

        /// <summary>
        /// Our Service Container
        /// </summary>
        [ContentSerializerIgnore]
        static ServiceContainer _services = null;

        /// <summary>
        /// Our Content Manager
        /// </summary>
        [ContentSerializerIgnore]
        static ContentManager _content;

        /// <summary>
        /// The File List
        /// </summary>
        static Dictionary<string, string> _filesList = null;

        /// <summary>
        /// Percent of files that has loaded in
        /// </summary>
        [ContentSerializerIgnore]
        static double _percent = 0;

        /// <summary>
        /// Amount to jump at each point of change in percent
        /// </summary>
        [ContentSerializerIgnore]
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
            _services = new ServiceContainer();
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
                Console.Write("\rLoading In Files: {0}%", (int)_percent);
                
                //Build Path helps determine if we should search for Debug or Release folders
                var buildPath = "";
#if DEBUG
                buildPath = "Debug";
#elif !DEBUG
                buildPath = "Release";
#endif
                //Delete Windows Content file so its not loaded into our File List
                if (File.Exists("..\\..\\x86\\" + buildPath + "\\" + Content + "\\" + Content + ".xnb"))
                    File.Delete("..\\..\\x86\\" + buildPath + "\\" + Content + "\\" + Content + ".xnb");

                //Load all files inside the folders inside our Windows Content folder, this code cannot load files built for other platforms only the windows
                LoadFiles("..\\..\\x86\\" + buildPath + "\\" + Content + "\\", buildPath);

                //Calculate how much we need to jump for each percentage change
                CalculateJump();
                //Next test each file by loading it in, checking its type and recording it, than we will unload the file
                TestFiles();

                //If windows directory exists build there
                if (Directory.Exists("..\\..\\x86\\" + buildPath + "\\" + Content + "\\"))
                {
                    Console.WriteLine("\r\nWrittting Windows Version: " + new FileInfo("..\\..\\x86\\" + buildPath + "\\" + Content + "\\" + Content + ".xnb").FullName);
                    Write(_filesList, null, "..\\..\\x86\\" + buildPath + "\\" + Content + "\\", Content, TargetPlatform.Windows);
                }
                
                //If Windows Phone directory exists build there
                if (Directory.Exists("..\\..\\Windows Phone\\" + buildPath + "\\Content\\"))
                {
                    Console.WriteLine("Writting Windows Phone Version: " + new FileInfo("..\\..\\Windows Phone\\" + buildPath + "\\Content\\" + Content + ".xnb").FullName);
                    Write(_filesList, null, "..\\..\\Windows Phone\\" + buildPath + "\\Content\\", Content, TargetPlatform.WindowsPhone);
                }
                
                //If Xbox directory exists build there
                if (Directory.Exists("..\\..\\xbox\\" + buildPath + "\\Content\\"))
                {
                    Console.WriteLine("Writting Windows Phone Version: " + new FileInfo("..\\..\\xbox\\" + buildPath + "\\Content\\" + Content + ".xnb").FullName);
                    Write(_filesList, null, "..\\..\\xbox\\" + buildPath + "\\Content\\", Content, TargetPlatform.Xbox360);
                }
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
        /// <param name="build"></param>
        static void LoadFiles(string name, string build)
        {

            //Only load files that end in .xnb and remove our Content folders name);
            foreach (var file in from f in Directory.GetFiles(name, "*", SearchOption.AllDirectories)
                                 where new FileInfo(f).Extension == ".xnb"
                                 select f.Replace("..\\..\\x86\\" + build + "\\" + Content + "\\", "")
                                     into file
                                     select file.Replace(".xnb", ""))
            {
                Console.WriteLine("\rFile added: " + file);
                _filesList.Add(file, "Unknown");
            }
        }

        /// <summary>
        /// Test each file
        /// </summary>
        static void TestFiles()
        {
            var tempList = new Dictionary<string, string>();
            foreach (var k in _filesList.Keys)
            {
                //Load in our file for testing and determine what type it is
                var testFile = _content.Load<object>(k);
                tempList.Add(k, testFile.GetType().Name);

                //Unload content because ther eis a 90 MB limit of data usage in XNA
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
            //Do not allow the fucntion to devide by zero
            if (_filesList.Count == 0)
            {
                _percent = 100;
                return;
            }

            _jump = 100m/_filesList.Count;
        }

        #endregion

        #region Write Methods

        /// <summary>
        /// Write our XNB file using the data we have gathered in the previous methods
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="writer"></param>
        /// <param name="path"></param>
        /// <param name="assetName"></param>
        /// <param name="targetPlatform"></param>
        static void Write(Object obj, ContentTypeWriter writer, string path, string assetName, TargetPlatform targetPlatform)
        {
            //Use reflection to get the otherwise internal parameters and methods of the typewriter 
            var constructors = typeof(ContentCompiler).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var addWriter = typeof(ContentCompiler).GetMethod("AddTypeWriter", BindingFlags.NonPublic | BindingFlags.Instance);
            var compileContent = typeof(ContentCompiler).GetMethod("Compile", BindingFlags.NonPublic | BindingFlags.Instance);

            var compiler = constructors[0].Invoke(null) as ContentCompiler;

            //Initialize our writter if it isn't
            if (writer != null)
                addWriter.Invoke(compiler, new object[] { writer });

            //Delete old content file if it exists
            if (File.Exists(path + assetName + ".xnb"))
                File.Delete(path + assetName + ".xnb");

            //Generate our File
            using (var stream = new FileStream(path + assetName + ".xnb", FileMode.Create))
            {
                compileContent.Invoke(compiler,
                    new[] 
                    { 
                         stream, //File writting too
                         obj, //Object being written to file 
                         targetPlatform, //Target Platform such as Windows, Xbox, or Windows Phone
                         GraphicsProfile.Reach, //Graphics Profile. I use Reach 
                         true, //Let me know if anyone knows what this true actually does
                         path, //Output path
                         path, //Output path
                    });
            }

        }

        #endregion
    }
}
