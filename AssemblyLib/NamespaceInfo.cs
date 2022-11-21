using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyLib
{
    public class NamespaceInfo
    {
        public string Name { get; set; }
        public List<TypeInfo> TypesInfo { get; set; }

        public NamespaceInfo()
        {
            TypesInfo = new List<TypeInfo>();            
        }
    }
}
