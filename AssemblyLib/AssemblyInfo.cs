using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyLib
{
    public class AssemblyInfo
    {
        public List<ExtensionMethod> ExtMethods { get; set; }

        public string Name { get; set; }
        public List<NamespaceInfo> Namespaces { get; set; }

        public AssemblyInfo()
        {
            Namespaces = new List<NamespaceInfo>();
            ExtMethods = new List<ExtensionMethod>();
        }
    }
}
