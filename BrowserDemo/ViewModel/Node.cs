using System;
using System.Collections.ObjectModel;

namespace AssemblyLib.ViewModel
{
    public class Node
    {
        public string Name { get; set; }
        public ObservableCollection<Node> Nodes { get; set; }

        public Node(string name, ObservableCollection<Node> nodes)
        {
            this.Name = name;
            this.Nodes = nodes;
        }

        public Node(string name)
        {
            this.Name = name;
            Nodes = new ObservableCollection<Node>();
        }
    }
}
