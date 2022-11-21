using AssemblyLib.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BrowserDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new ViewModel();
            DataContext = viewModel;
            var commandBinding = new CommandBinding();
            commandBinding.Command = ApplicationCommands.Open;
            commandBinding.Executed += viewModel.LoadDll;
            Open.CommandBindings.Add(commandBinding);
            

        }
    }
}