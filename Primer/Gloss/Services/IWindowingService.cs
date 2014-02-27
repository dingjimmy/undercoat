﻿// Copyright (c) James Dingle

namespace Primer.Windows
{

    public interface IWindowingService
    {

        void Initialize();

        bool ShowPopupWindow(ViewModel content, System.Windows.Window owner);

        string ShowFilePicker(FilePickerMode mode, string filename, System.Windows.Window owner, string defaultExtension, params string[] validExtensions);

        System.Windows.Window ShowWindow(string identifier, ViewModel content, System.Windows.Window owner);

        System.Windows.Window ShowAsyncWindow(string identifier, ViewModel content);

    }


    public enum FilePickerMode
    {
        OpenFile,
        SaveFile
    }

}