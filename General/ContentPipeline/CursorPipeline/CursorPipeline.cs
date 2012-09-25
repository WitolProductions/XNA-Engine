using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace ContentPipeline.CursorPipeline
{
    #region Importer

    /// <summary>
    /// Custom Content Importer used to convert Cursor files into Cursors for the Engine
    /// </summary>
    [ContentImporter(".ani", ".cur", DefaultProcessor = "Cursor Processor - Engine", DisplayName = "Cursor Importer - Engine")]
    public class CursorImporter : ContentImporter<string>
    {
        #region Overrides of ContentImporter<CursorContent>

        /// <summary>
        /// Handles Importing our Cursor into the system  that converts it into Xnb
        /// </summary>
        /// <param name="filename">Filename is automatically passed in</param>
        /// <param name="context">ContextImporter is automatically passed in</param>
        /// <returns>Cursor is returned as a Windows Forms Cursor</returns>
        public override string Import(string filename, ContentImporterContext context)
        {
            return filename;
        }

        #endregion
    }

    #endregion

    #region Processor

    /// <summary>
    /// Custom content processor that takes windows Cursor files and Image files and creates a Cursor for them
    /// </summary>
    [ContentProcessor(DisplayName = "Cursor Processor - Engine")]
    public class CursorPipeline : ContentProcessor<string, CursorContent>
    {
        /// <summary>
        /// Converts an array of sprite filenames into a sprite sheet object.
        /// </summary>
        public override CursorContent Process(string input, ContentProcessorContext context)
        {
            //Create our Cursor Content variable and get File Info on our Input
            var cursorContent = new CursorContent {Extension = new FileInfo(input).Extension, Data = File.ReadAllBytes(input)};
            
            //Write some output info 
            context.Logger.LogImportantMessage(input, input);

            return cursorContent;
        }
    }

    #endregion

    #region Writer

    [ContentTypeWriter]
    public class CursorWriter : ContentTypeWriter<CursorContent>
    {
        #region Overrides of ContentTypeWriter

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Content.ContentTypes.GameCursorReader, Content";
        }

        #endregion

        #region Overrides of ContentTypeWriter<Cursor>

        protected override void Write(ContentWriter output, CursorContent value)
        {
            output.Write(value.Extension);
            output.Write(value.Data.Length);
            output.Write(value.Data);
        }

        #endregion
    }

    #endregion

    #region Object

    /// <summary>
    /// Our Cursor information that will be written to file
    /// </summary>
    public class CursorContent
    {
        /// <summary>
        /// Data of our file
        /// </summary>
        public byte[] Data = null;

        /// <summary>
        /// Extension of our file
        /// </summary>
        public string Extension = null;
    }

    #endregion
}
