using System;
using System.IO;

namespace NativeFileBrowser
{
    public class NativeFileBrowserWindows : INativeFileBrowser
    {
        public string[] OpenFilePanel(
            string title,
            bool multiselect,
            string defaultPath = null,
            ExtensionFilter[] extensions = null
        )
        {
            var fd = new VistaOpenFileDialog();
            fd.Title = title;
            if (extensions != null)
            {
                fd.Filter = GetFilterFromFileExtensionList(extensions);
                fd.FilterIndex = 1;
            }
            else
            {
                fd.Filter = string.Empty;
            }

            if (defaultPath != null)
            {
                fd.InitialDirectory = defaultPath;
            }

            fd.Multiselect = multiselect;

            var res = fd.ShowDialog();
            var filenames = res == true ? fd.FileNames : Array.Empty<string>();
            return filenames;
        }

        public void OpenFilePanelAsync(
            string title,
            bool multiselect,
            Action<string[]> cb,
            ExtensionFilter[] extensions = null
        )
        {
            cb.Invoke(OpenFilePanel(title, multiselect, null, extensions));
        }

        public string[] OpenFolderPanel(string title, string defaultPath = null)
        {
            var fd = new VistaFolderBrowserDialog();
            fd.Title = title;

            if (defaultPath != null)
            {
                fd.SelectedPath = defaultPath;
            }

            var res = fd.ShowDialog();
            var filenames = res == true ? new[] { fd.SelectedPath } : Array.Empty<string>();
            return filenames;
        }

        public void OpenFolderPanelAsync(string title, Action<string[]> callback)
        {
            callback.Invoke(OpenFolderPanel(title));
        }

        public string SaveFilePanel(
            string title,
            string defaultName,
            string defaultPath = null,
            ExtensionFilter[] extensions = null
        )
        {
            var fd = new VistaSaveFileDialog();
            fd.Title = title;

            var finalFilename = "";

            if (!string.IsNullOrEmpty(defaultName))
            {
                finalFilename += defaultName;
            }

            fd.FileName = finalFilename;
            if (extensions != null)
            {
                fd.Filter = GetFilterFromFileExtensionList(extensions);
                fd.FilterIndex = 1;
                fd.DefaultExt = extensions[0].Extensions[0];
                fd.AddExtension = true;
            }
            else
            {
                fd.DefaultExt = string.Empty;
                fd.Filter = string.Empty;
                fd.AddExtension = false;
            }

            if (defaultPath != null)
            {
                fd.InitialDirectory = defaultPath;
            }

            var res = fd.ShowDialog();
            var filename = res == true ? fd.FileName : "";

            return filename;
        }

        public void SaveFilePanelAsync(
            string title,
            string defaultName,
            Action<string> cb,
            ExtensionFilter[] extensions = null
        )
        {
            cb.Invoke(SaveFilePanel(title, defaultName, null, extensions));
        }

        // .NET Framework FileDialog Filter format
        // https://msdn.microsoft.com/en-us/library/microsoft.win32.filedialog.filter
        private static string GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
        {
            var filterString = "";
            foreach (var filter in extensions)
            {
                filterString += filter.Name + "(";

                foreach (var ext in filter.Extensions)
                {
                    filterString += "*." + ext + ",";
                }

                filterString = filterString.Remove(filterString.Length - 1);
                filterString += ") |";

                foreach (var ext in filter.Extensions)
                {
                    filterString += "*." + ext + "; ";
                }

                filterString += "|";
            }

            filterString = filterString.Remove(filterString.Length - 1);
            return filterString;
        }

        private static string GetDirectoryPath(string directory)
        {
            var directoryPath = Path.GetFullPath(directory);
            if (!directoryPath.EndsWith("\\"))
            {
                directoryPath += "\\";
            }

            return Path.GetDirectoryName(directoryPath) + Path.DirectorySeparatorChar;
        }
    }
}
