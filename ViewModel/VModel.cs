using AssemblyLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Win32;

namespace ViewModel
{
    class VModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadDll(object o)
        {
            
            fileDialog.Filter += "Dll files(*.dll)|*.dll";
            if (fileDialog.ShowDialog() == false)
                return;

            AssemblyLoader assemblyLoader = new AssemblyLoader();
            assemblyLoader 
        }
    }
}
