using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyLib
{
    public class AssemblyLoader
    {
        
        public string s;
        public string q { get; set; }
        public AssemblyInfo LoadAssembly(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            
            string name = assembly.GetName().Name;

            var types = assembly.GetTypes();
            AssemblyInfo assemblyInfo = new AssemblyInfo();
            assemblyInfo.Name = assembly.GetName().Name;

            List<TypeInfo> typeInfos = new List<TypeInfo>();
            List<NamespaceInfo> namespaceInfos = new List<NamespaceInfo>();
            assemblyInfo.Namespaces = namespaceInfos;
           

            foreach(var type in types)
            {
                TypeInfo typeInfo = new TypeInfo();
                typeInfo.Name = type.Name;
                var meths = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                var fields = type.GetFields();
                var props = type.GetProperties();

                List<string> propsInfo = new List<string>();
                foreach (var prop in props)
                {
                    propsInfo.Add(propInfo(prop));
                }

                List<string> fieldsInfo = new List<string>();
                foreach (var field in fields)
                {
                    fieldsInfo.Add(fieldInfo(field));
                }

                List<string> methsInfo = new List<string>();
                foreach (var meth in meths)
                {
                    if (meth.IsDefined(typeof(ExtensionAttribute), false))
                    {
                        assemblyInfo.ExtMethods.Add(GetExtensionMethod(meth));
                    }
                    else
                    {
                        methsInfo.Add(methodSignature(meth));
                    }
                }    
                
                typeInfo.PropertiesInfo = propsInfo;
                typeInfo.MethodsInfo = methsInfo;
                typeInfo.FieldsInfo = fieldsInfo;
                bool isAdded = false;
                foreach (var ns in namespaceInfos)
                {
                    if (ns.Name.Equals(type.Namespace))
                    {
                        ns.TypesInfo.Add(typeInfo);
                        isAdded = true;
                        break;
                    }
                }
                if (!isAdded)
                {
                    NamespaceInfo namespaceInfo = new NamespaceInfo();
                    namespaceInfo.Name = type.Namespace;
                    namespaceInfos.Add(namespaceInfo);
                    typeInfos.Add(typeInfo);
                }
            }
            return assemblyInfo;
        }

        private ExtensionMethod GetExtensionMethod(MethodInfo meth)
        {
            ExtensionMethod em = new ExtensionMethod();
            var mi = methodSignature(meth);
            em.Signature = mi;
            em.Type = meth.GetParameters()[0].ParameterType.Name;
            return em;
        }

        private string propInfo(PropertyInfo prop)
        {
            return "Property: " + prop.PropertyType + " " + prop.Name;
        }

        private string fieldInfo(FieldInfo fieldInfo)
        {
            return "Field: " + fieldInfo.FieldType + " " + fieldInfo.Name;
        }
        public string methodSignature(MethodInfo methodInfo)
        {
            ParameterInfo[] parameters = methodInfo.GetParameters();
            string result = "Method: ";
            MethodAttributes methodAttributes = methodInfo.Attributes;

            if (methodAttributes.HasFlag(MethodAttributes.Public))
            {
                result += "public ";
            } else
            {
                if (methodAttributes.HasFlag(MethodAttributes.Private))
                    result += "private ";
                else result += "protected ";
            }

            if (methodAttributes.HasFlag(MethodAttributes.Static))
            {
                result += "static ";
            }
                
            if (methodAttributes.HasFlag(MethodAttributes.Virtual))
            {
                result += "virtual ";
            }
                
            if (methodAttributes.HasFlag(MethodAttributes.Abstract))
            {
                result += "abstract ";
            }
                
            if (methodAttributes.HasFlag(MethodAttributes.Final))
            {
                result += "final ";
            }
            result += methodInfo.Name + "(";

            for (int i = 0; i < parameters.Length; i++)
            {
                result += parameters[i].GetType().Name + " " + parameters[i].Name;
                if (i < parameters.Length - 1)
                {
                    result += ", ";
                }
            }

            result += ")";

            return result;
        }
    }
}