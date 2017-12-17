﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace AdvancedDLSupport
{
    /// <summary>
    /// Loads libraries on the Windows platform.
    /// </summary>
    public class WindowsPlatformLoader : PlatformLoaderBase
    {
        /// <inheritdoc />
        public override IntPtr LoadLibrary(string path)
        {
            var libraryHandle = kernel32.LoadLibrary(path);
            if (libraryHandle == IntPtr.Zero)
            {
                throw new LibraryLoadingException("Library loading failed.", new Win32Exception(Marshal.GetLastWin32Error()));
            }

            return libraryHandle;
        }

        /// <inheritdoc />
        public override IntPtr LoadSymbol(IntPtr library, string symbolName)
        {
            var symbolHandle = kernel32.GetProcAddress(library, symbolName);
            if (symbolHandle == IntPtr.Zero)
            {
                throw new SymbolLoadingException("Symbol loading failed.", new Win32Exception(Marshal.GetLastWin32Error()));
            }

            return symbolHandle;
        }

        /// <inheritdoc />
        public override bool CloseLibrary(IntPtr library)
        {
            return kernel32.FreeLibrary(library) > 0;
        }
    }
}