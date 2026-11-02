using System;
using System.ComponentModel;
using System.IO;

namespace NativeFileBrowser
{
    public class VistaFolderBrowserDialog : VistaFileDialog
    {
        private string _selectedPath;
        private Environment.SpecialFolder _rootFolder;

        #region Public Properties

        [Description(
            "Gets or sets the folder path selected by the user. The path of the folder first selected in the dialog box or the last folder selected by the user. The default is an empty string (\"\")."
        )]
        [DefaultValue("")]
        public string SelectedPath
        {
            get { return _selectedPath; }
            set { _selectedPath = value ?? string.Empty; }
        }

        #endregion

        internal override IFileDialog CreateFileDialog()
        {
            return (IFileDialog)new FileOpenDialogRCW();
        }

        protected override void Reset()
        {
            _selectedPath = string.Empty;
            base.Reset();
        }

        internal override void SetDialogProperties(IFileDialog dialog)
        {
            base.SetDialogProperties(dialog);

            dialog.SetOptions(
                FOS.FOS_PICKFOLDERS | FOS.FOS_FORCEFILESYSTEM | FOS.FOS_FILEMUSTEXIST
            );

            if (!string.IsNullOrEmpty(SelectedPath) && Directory.Exists(SelectedPath))
            {
                IShellItem folder = NativeMethods.CreateItemFromParsingName(SelectedPath);

                dialog.SetFolder(folder);
            }
        }

        internal override void GetResult(IFileDialog dialog)
        {
            IShellItem item;
            dialog.GetResult(out item);
            item.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out _selectedPath);
        }
    }
}
