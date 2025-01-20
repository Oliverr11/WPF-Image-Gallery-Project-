using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImageGallery_WPF_Exam
{
    public partial class MainWindow : Window
    {
        private FileManager _fileManager;
        private Stack<string> _backStack = new Stack<string>(); 
        private Stack<string> _forwardStack = new Stack<string>(); 
        private DisplayFileAndFolder _currentExplorerPage;

        public MainWindow()
        {
            InitializeComponent();
            _fileManager = new FileManager();
            _fileManager.LoadFileSystem(treeViewFileSystem);
            treeViewFileSystem.MouseDoubleClick += TreeViewFileSystem_MouseDoubleClick;
            LoadConfiguration();
            NavigateToFirstDrive();
            this.Closing += MainWindow_Closing;
        }

        private void TreeViewFileSystem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem selectedItem = treeViewFileSystem.SelectedItem as TreeViewItem;
            if (selectedItem != null)
            {
                string newPath = null;

                if (selectedItem.Tag is DirectoryInfo directory)
                {
                    newPath = directory.FullName;
                }
                else if (selectedItem.Tag is FileInfo file)
                {
                    newPath = file.FullName;
                }

                if (newPath != null)
                {
                    NavigateToPath(newPath);
                }
            }
        }

        private void NavigateToFirstDrive()
        {
            var firstDriveItem = treeViewFileSystem.Items.Cast<TreeViewItem>().FirstOrDefault();
            if (firstDriveItem != null)
            {
                firstDriveItem.IsSelected = true;
                TreeViewFileSystem_MouseDoubleClick(treeViewFileSystem, null);
            }
        }
        private void NavigateToPath(string path)
        {
            _currentExplorerPage = new DisplayFileAndFolder();
            if (Directory.Exists(path))
            {
                _currentExplorerPage.SetSelectedDirectory(path);
            }
            else if (File.Exists(path))
            {
                _currentExplorerPage.SetSelectedFile(path);
            }
            displayImage.Navigate(_currentExplorerPage);
            string selectedViewMode = (cmbViewMode.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (!string.IsNullOrEmpty(selectedViewMode))
            {
                _currentExplorerPage.ChangeViewMode(selectedViewMode);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (_currentExplorerPage != null)
            {
                _currentExplorerPage.NavigateBack();
            }
        }

        private void BtnForward_Click(object sender, RoutedEventArgs e)
        {
            if (_currentExplorerPage != null)
            {
                _currentExplorerPage.NavigateForward();
            }
        }
        private void CmbViewMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_currentExplorerPage != null)
            {
                string selectedViewMode = (cmbViewMode.SelectedItem as ComboBoxItem)?.Content.ToString();
                _currentExplorerPage.ChangeViewMode(selectedViewMode);
            }
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var config = new AppConfig
            {
                SelectedViewMode = (cmbViewMode.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText("appConfig.json", json);
        }
        private void LoadConfiguration()
        {
            string configFilePath = "appConfig.json";
            if (File.Exists(configFilePath))
            {
                string json = File.ReadAllText(configFilePath);
                var config = JsonConvert.DeserializeObject<AppConfig>(json);
                if (config != null)
                {
                    // Set the selected view mode
                    if (!string.IsNullOrEmpty(config.SelectedViewMode))
                    {
                        var viewModeItem = cmbViewMode.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == config.SelectedViewMode);
                        if (viewModeItem != null)
                        {
                            cmbViewMode.SelectedItem = viewModeItem;
                        }
                    }
                }
            }
        }
    }
}