using System;
using System.Windows;
using System.Windows.Forms;
using TestDocumentationTool.Api;

namespace TestDocumentationTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly Lazy<FolderBrowserDialog> _folderBrowserDialog = new Lazy<FolderBrowserDialog>(() => new FolderBrowserDialog());

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel(new AssemblyLoaderThatAlwaysReturnSomething());
            DataContext = _viewModel;
        }

        private void LoadAssemblies(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_folderBrowserDialog.Value.SelectedPath))
            {
                _folderBrowserDialog.Value.SelectedPath = @"D:\Src\PulsenHandel";
            }

            if (_folderBrowserDialog.Value.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _viewModel.LoadTestAssemblies(_folderBrowserDialog.Value.SelectedPath);
            }
        }

        private void CreateReport(object sender, RoutedEventArgs e)
        {
            if (_folderBrowserDialog.Value.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _viewModel.CreateCsvFile(_folderBrowserDialog.Value.SelectedPath);
            }
        }
    }
}
