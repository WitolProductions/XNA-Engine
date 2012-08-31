// -----------------------------------------------------------------------
//                       Created By: Justin Witol
//                       www.WitolProductions.com
// If you add or alter any code please include your name below if you wish.
//                             Special Thanks: 
// 
// 
// You are free to use this code in any way you want. I only ask if you do
//       use it you please mention my website and or name.
// Document Name: CustomContentManager.cs Version: 1.0 Last Edited: 8/4/2012
// ------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

#if WINDOWS
using System.Windows.Forms;
#elif WINDOWS_PHONE
using Microsoft.Xna.Framework.GamerServices;
#endif

namespace Content
{
    /// <summary>
    /// Class is a modified version of ContentManager to ensure that we can safly dispose of objects and load them back in for increased performance
    /// </summary>
    public class CustomContentManager : ContentManager
    {
        #region Fields

        string _tempAssetName;

        #endregion

        #region Properties

        Dictionary<string, object> _loadedAssets = new Dictionary<string, object>();
        Dictionary<string, IDisposable> _disposableAssets = new Dictionary<string, IDisposable>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider">Our Service Provider <see cref="IServiceProvider"/></param>
        /// <param name="content">Location of our Content</param>
        public CustomContentManager(IServiceProvider serviceProvider, string content) : base(serviceProvider, content)
        {
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load an Asset In
        /// </summary>
        /// <typeparam name="T">Type of Asset</typeparam>
        /// <param name="assetName">Name of Asset to Load</param>
        /// <returns>Returns our Asset</returns>
        public override T Load<T>(string assetName)
        {
            if (_loadedAssets.ContainsKey(assetName))
                return (T)_loadedAssets[assetName];

            _tempAssetName = assetName;

            object asset = null;

            try
            {
                asset = ReadAsset<T>(assetName, RecordDisposableAsset);
                _loadedAssets.Add(assetName, asset);
            }
            catch (Exception e)
            {
#if WINDOWS
                MessageBox.Show(e.Message, "Error");
#elif WINDOWS_PHONE
                //TODO: Move this to Input Handler
                Guide.BeginShowMessageBox("Error", e.Message, new List<string> { "OK"}, 0, MessageBoxIcon.Alert, null, null);
#endif
            }

            return (T) asset;
        }
            
        /// <summary>
        ///Dispose of a specific asset
        /// </summary>
        /// <param name="assetName">Name of Asset to dispose</param>
        public void DisposeObject(string assetName)
        {
            if (_disposableAssets.ContainsKey(assetName))
                _disposableAssets[assetName].Dispose();

            _disposableAssets.Remove(assetName);
            _loadedAssets.Remove(assetName);
        }
        
        /// <summary>
        /// Reloads an object into our system - Useful for when you do not need to Dispose of an object completly and then want to reload it all back in
        /// </summary>
        /// <param name="assetName">Name of Asset to reload</param>
        public T ReloadObject<T>(string assetName)
        {
            DisposeObject(assetName);

            T asset = default(T);

            try
            {
                _tempAssetName = assetName;
                asset = ReadAsset<T>(assetName, RecordDisposableAsset);
            }
            catch (Exception e)
            {
#if WINDOWS
                MessageBox.Show(e.Message, "Error");
#elif WINDOWS_PHONE
                //TODO: Move this to Input Handler
                Guide.BeginShowMessageBox("Error", e.Message, new List<string> { "OK"}, 0, MessageBoxIcon.Alert, null, null);
#endif
            }

            return asset;
        }

        /// <summary>
        /// Unloads all objects
        /// </summary>
        public override void Unload()
        {
            foreach (var disposable in _disposableAssets)
                disposable.Value.Dispose();

            _loadedAssets.Clear();
            _disposableAssets.Clear();
        }

        /// <summary>
        /// Record all objects in as a disposable object
        /// </summary>
        /// <param name="disposable">Object that is disposable</param>
        void RecordDisposableAsset(IDisposable disposable)
        {
            if (!_disposableAssets.ContainsKey(_tempAssetName))
                _disposableAssets.Add(_tempAssetName, disposable);
        }

        /// <summary>
        /// Returns if the object exists or not as a loaded asset
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exists(string name)
        {
            return _loadedAssets.Any(asset => asset.Key == name);
        }

        #endregion
    }
}
