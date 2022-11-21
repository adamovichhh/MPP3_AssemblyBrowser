using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyLib
{
    public class TypeInfo
    {
        public string Name { get; set; }
        public List<string> MethodsInfo { get; set; }
        public List<string> PropertiesInfo { get; set; }
        public List<string> FieldsInfo { get; set; }

        public TypeInfo()
        {
            MethodsInfo = new List<string>();
            PropertiesInfo = new List<string>();
            FieldsInfo = new List<string>();
        }
    }
}
