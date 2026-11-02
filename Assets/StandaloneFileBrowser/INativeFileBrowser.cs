using System;

namespace NativeFileBrowser
{
    public interface INativeFileBrowser
    {
        string[] OpenFilePanel(
            string title,
            bool multiselect,
            string defaultPath = null,
            ExtensionFilter[] extensions = null
        );

        void OpenFilePanelAsync(
            string title,
            bool multiselect,
            Action<string[]> cb,
            ExtensionFilter[] extensions = null
        );

        string[] OpenFolderPanel(string title, string defaultPath = null);
        void OpenFolderPanelAsync(string title, Action<string[]> callback);

        string SaveFilePanel(
            string title,
            string defaultName,
            string defaultPath = null,
            ExtensionFilter[] extensions = null
        );

        void SaveFilePanelAsync(
            string title,
            string defaultName,
            Action<string> cb,
            ExtensionFilter[] extensions = null
        );
    }
}
