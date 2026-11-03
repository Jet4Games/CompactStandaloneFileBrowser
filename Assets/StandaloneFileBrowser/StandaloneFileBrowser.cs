using System;

namespace NativeFileBrowser
{
    public struct ExtensionFilter
    {
        public string Name;
        public string[] Extensions;

        public ExtensionFilter(string filterName, params string[] filterExtensions)
        {
            Name = filterName;
            Extensions = filterExtensions;
        }
    }

    public static class StandaloneFileBrowser
    {
        private static readonly NativeFileBrowserWindows NativeFileBrowser;

        static StandaloneFileBrowser()
        {
            NativeFileBrowser = new NativeFileBrowserWindows();
        }

        /// <summary>
        /// Native open file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
        public static string[] OpenFilePanel(
            string title,
            bool multiselect,
            ExtensionFilter[] extensions = null
        )
        {
            return NativeFileBrowser.OpenFilePanel(title, multiselect, null, extensions);
        }

        public static string[] OpenFilePanel(
            string title,
            bool multiselect,
            string defaultPath,
            ExtensionFilter[] extensions = null
        )
        {
            return NativeFileBrowser.OpenFilePanel(title, multiselect, defaultPath, extensions);
        }

        /// <summary>
        /// Native open folder dialog
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
        public static string[] OpenFolderPanel(string title)
        {
            return NativeFileBrowser.OpenFolderPanel(title);
        }

        public static string[] OpenFolderPanel(string title, string defaultPath = null)
        {
            return NativeFileBrowser.OpenFolderPanel(title, defaultPath);
        }

        /// <summary>
        /// Native open folder dialog async
        /// </summary>
        /// <param name="title"></param>
        /// <param name="cb">Callback")</param>
        public static void OpenFolderPanelAsync(string title, Action<string[]> cb)
        {
            NativeFileBrowser.OpenFolderPanelAsync(title, cb);
        }

        public static string SaveFilePanel(
            string title,
            string defaultName,
            string filterName,
            string defaultPath,
            ExtensionFilter[] extensions = null
        )
        {
            return SaveFilePanel(title, defaultName, defaultPath, extensions);
        }

        /// <summary>
        /// Native save file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <returns>Returns chosen path. Empty string when cancelled</returns>
        public static string SaveFilePanel(
            string title,
            string defaultName,
            ExtensionFilter[] extensions = null
        )
        {
            return NativeFileBrowser.SaveFilePanel(title, defaultName, null, extensions);
        }

        public static string SaveFilePanel(
            string title,
            string defaultName,
            string defaultPath,
            ExtensionFilter[] extensions = null
        )
        {
            return NativeFileBrowser.SaveFilePanel(title, defaultName, defaultPath, extensions);
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="filterName">filter name</param>
        /// <param name="extension">File extension</param>
        /// <param name="cb">Callback")</param>
        public static void SaveFilePanelAsync(
            string title,
            string defaultName,
            string filterName,
            Action<string> cb,
            string extension = null
        )
        {
            var extensions = string.IsNullOrEmpty(extension)
                ? null
                : new[] { new ExtensionFilter(filterName, extension) };
            SaveFilePanelAsync(title, defaultName, cb, extensions);
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="cb">Callback")</param>
        public static void SaveFilePanelAsync(
            string title,
            string defaultName,
            Action<string> cb,
            ExtensionFilter[] extensions = null
        )
        {
            NativeFileBrowser.SaveFilePanelAsync(title, defaultName, cb, extensions);
        }
    }
}
