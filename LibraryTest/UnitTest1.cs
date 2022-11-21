using System;
using System.IO;
using Xunit;

namespace LibraryTest
{
    public class UnitTest1
    {
        AssemblyLib.AssemblyLoader assemblyLoader;


        public UnitTest1()
        {
            assemblyLoader = new AssemblyLib.AssemblyLoader();
        }
        [Fact]
        public void HelpMethTest()
        {
            Assert.Throws<FileNotFoundException>(()=>assemblyLoader.LoadAssembly("error.dll"));
        }
        
        [Fact]
        public void LoadLib()
        {
            assemblyLoader.LoadAssembly("C:\\Users\\User\\Desktop\\Ycheba_5sem\\SPP\\Assembly-Browser\\AssemblyLib\\bin\\Debug\\netcoreapp3.1\\AssemblyLib.dll");
            Assert.True(true);
        }
    }
}
