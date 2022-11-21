using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace AssemblyLib.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private ObservableCollection<Node> nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return nodes; }
            set
            {
                nodes = value;
                OnPropertyChanged();
            }
        }

        public void LoadDll(object o, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter += "Dll files(*.dll)|*.dll";
            if (fileDialog.ShowDialog() == false)
                return;

            AssemblyLoader assemblyLoader = new AssemblyLoader();
            AssemblyInfo assemblyInfo = assemblyLoader.LoadAssembly(fileDialog.FileName);
            
            Nodes = new ObservableCollection<Node>
            {
                new Node(assemblyInfo.Name, GetNameSpaces(assemblyInfo.Namespaces, assemblyInfo))
            };
        }

        private ObservableCollection<Node> GetNameSpaces(List<NamespaceInfo> namespaceInfos, AssemblyInfo assemblyInfo)
        {
            ObservableCollection<Node> nsInfo = new ObservableCollection<Node>();
            foreach (var ns in namespaceInfos)
            {
                nsInfo.Add(new Node(ns.Name, GetTypes(ns.TypesInfo, assemblyInfo)));
            }
            return nsInfo;
        }

        private ObservableCollection<Node> GetTypes(List<TypeInfo> typesInfo, AssemblyInfo assemblyInfo)
        {
            ObservableCollection<Node> tpInfo = new ObservableCollection<Node>();
            foreach (var tp in typesInfo)
            {
                tpInfo.Add(new Node(tp.Name, GetTypeInfo(tp)));
            }
            HashSet<string> types = new HashSet<string>();
            Dictionary<string, List<ExtensionMethod>> extensionMethods = new Dictionary<string, List<ExtensionMethod>>();
            foreach (var em in assemblyInfo.ExtMethods)
            {
                if (extensionMethods.ContainsKey(em.Type))
                {
                    extensionMethods[em.Type].Add(em);
                }
                else
                {
                    extensionMethods.Add(em.Type, new List<ExtensionMethod>() { em });
                }
                
            }
            foreach (var em in extensionMethods)
            {
                tpInfo.Add(new Node(em.Key, GetExtMethInfo(em.Value)));
            }
            return tpInfo;
        }

        private ObservableCollection<Node> GetExtMethInfo(List<ExtensionMethod> em)
        {

            ObservableCollection<Node> nodes = new ObservableCollection<Node>();
            foreach (var m in em)
            {
                nodes.Add(new Node(m.Signature));
            }
            return nodes;
        }

        private ObservableCollection<Node> GetTypeInfo(TypeInfo tp)
        {
            var tpi = new ObservableCollection<Node>();
            foreach (var field in tp.FieldsInfo)
            {
                tpi.Add(new Node(field));
            }

            foreach (var property in tp.PropertiesInfo)
            {
                tpi.Add(new Node(property));
            }

            foreach (var method in tp.MethodsInfo)
            {
                tpi.Add(new Node(method));
            }
            return tpi;
        }
    }
}
