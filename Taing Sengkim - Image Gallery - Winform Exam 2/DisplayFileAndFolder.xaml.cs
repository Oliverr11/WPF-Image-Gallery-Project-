using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ImageGallery_WPF_Exam.Model;
namespace ImageGallery_WPF_Exam
{
    public partial class DisplayFileAndFolder : Page
    {
        private string _currentDirectory; 
        public ObservableCollection<ListViewItemModel> Items { get; set; } = new ObservableCollection<ListViewItemModel>();
        public ObservableCollection<SearchHistoryItem> SearchHistory { get; set; } = new ObservableCollection<SearchHistoryItem>();
        private Stack<string> _backStack = new Stack<string>();
        private Stack<string> _forwardStack = new Stack<string>();
        private FileManager _fileManager = new FileManager();
        public DisplayFileAndFolder()
        {
            InitializeComponent();
            cmbTypeFile.Items.Add("All Files");
            cmbTypeFile.Items.Add(".jpg");
            cmbTypeFile.Items.Add(".jpeg");
            cmbTypeFile.Items.Add(".png");
            cmbTypeFile.Items.Add(".gif");
            cmbTypeFile.Items.Add(".bmp");
            cmbTypeFile.Items.Add(".tiff");
            cmbTypeFile.Items.Add(".webp");
            cmbTypeFile.SelectedIndex = 0;

            cmbSearchType.Items.Add("File Name");
            cmbSearchType.Items.Add("Extension (.extension)");
            cmbSearchType.Items.Add("Size (Byte)");
            cmbSearchType.Items.Add("Date Created ( MM DD YYYY )");
            cmbSearchType.SelectedIndex = 0;
            _fileManager.LoadSearchHistory(cmbSearchHistory);
            cmbSearchHistory.SelectionChanged += cmbSearchHistory_SelectionChanged;
        }
        public void NavigateBack()
        {
            if (_backStack.Count > 1) 
            {
                string currentPath = _backStack.Pop();
                _forwardStack.Push(currentPath);
                string previousPath = _backStack.Peek();
                NavigateToPath(previousPath, isBackNavigation: true);
            }
        }
        public void NavigateForward()
        {
            if (_forwardStack.Count > 0) 
            {
                string nextPath = _forwardStack.Pop();
                _backStack.Push(nextPath);
                NavigateToPath(nextPath, isForwardNavigation: true);
            }
        }
        private void NavigateToPath(string path, bool isBackNavigation = false, bool isForwardNavigation = false)
        {
            if (Directory.Exists(path))
            {
                SetSelectedDirectory(path, isBackNavigation, isForwardNavigation);
            }
            else if (File.Exists(path))
                SetSelectedFile(path, isBackNavigation, isForwardNavigation);
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            string searchType = cmbSearchType.SelectedItem as string;
            if (string.IsNullOrEmpty(searchQuery))
            {
                _fileManager.LoadFiles(_currentDirectory, listViewItems, cmbTypeFile);
                return;
            }
            _fileManager.SearchHistory.Add(new SearchHistoryItem
            {
                SearchQuery = searchQuery,
                SearchType = searchType,
                Timestamp = DateTime.Now
            });

            _fileManager.SaveSearchHistory();
            cmbSearchHistory.Items.Add(searchQuery);
            var filteredItems = SearchFiles(searchQuery, searchType);
            listViewItems.ItemsSource = null; 
            listViewItems.Items.Clear(); 
            foreach (var item in filteredItems)
            {
                listViewItems.Items.Add(item);
            }
        }
        private ObservableCollection<ListViewItemModel> SearchFiles(string searchQuery, string searchType)
        {
            var filteredItems = new ObservableCollection<ListViewItemModel>();
            try
            {
                var files = Directory.GetFiles(_currentDirectory);
                var directories = Directory.GetDirectories(_currentDirectory);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    bool matchesCriteria = false;
                    string fileName = fileInfo.Name.Trim();
                    string query = searchQuery.Trim();

                    switch (searchType)
                    {
                        case "File Name":
                            matchesCriteria = fileName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
                            break;
                        case "Extension (.extension)":
                            matchesCriteria = fileInfo.Extension.Equals(query, StringComparison.OrdinalIgnoreCase);
                            break;
                        case "Size (Byte)":
                            if (long.TryParse(query, out long size))
                            {
                                matchesCriteria = fileInfo.Length >= size;
                            }
                            break;
                        case "Date Created ( MM DD YYYY )":
                            if (DateTime.TryParse(query, out DateTime date))
                            {
                                matchesCriteria = fileInfo.CreationTime.Date == date.Date;
                            }
                            break;
                    }
                    if (matchesCriteria)
                    {
                        filteredItems.Add(new ListViewItemModel
                        {
                            Name = fileInfo.Name,
                            Icon = _fileManager.IsImageFile(fileInfo.FullName) ? fileInfo.FullName : "images/3Dfile.png",
                            Path = fileInfo.FullName
                        });
                    }
                }
                foreach (var dir in directories)
                {
                    var dirInfo = new DirectoryInfo(dir);

                    if (searchType == "File Name" && dirInfo.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        filteredItems.Add(new ListViewItemModel
                        {
                            Name = dirInfo.Name,
                            Icon = "images/3Dfolder.png",
                            Path = dirInfo.FullName
                        });
                    }
                }
            }
            catch (Exception ex) {}
            return filteredItems;
        }
        public void SetSelectedDirectory(string directoryPath, bool isBackNavigation = false, bool isForwardNavigation = false)
        {
            _currentDirectory = directoryPath;
            txtDisplay.Text = $"Directory: {directoryPath}";
            _fileManager.LoadFiles(directoryPath, listViewItems, cmbTypeFile);
            if (!isBackNavigation && !isForwardNavigation)
            {
                if (_backStack.Count == 0 || _backStack.Peek() != directoryPath)
                {
                    _backStack.Push(directoryPath);
                }
                _forwardStack.Clear(); 
            }
        }

        public void SetSelectedFile(string filePath, bool isBackNavigation = false, bool isForwardNavigation = false)
        {
            txtDisplay.Text = $"Selected File: {filePath}";

            DisplayFileContent(filePath);

            if (!isBackNavigation && !isForwardNavigation)
            {
                // Only push to the back stack if this is a new navigation (not back/forward)
                if (_backStack.Count == 0 || _backStack.Peek() != filePath)
                {
                    _backStack.Push(filePath);
                }
                _forwardStack.Clear(); // Clear forward stack when navigating to a new path
            }
        }

        private void CmbTypeFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _fileManager.LoadFiles(_currentDirectory, listViewItems, cmbTypeFile);
        }
        private void DisplayFileContent(string filePath)
        {
            listViewItems.Visibility = Visibility.Collapsed;
            imgFileContent.Visibility = Visibility.Visible;
            if (_fileManager.IsImageFile(filePath))
            {
                var bitmap = new BitmapImage(new Uri(filePath));
                imgFileContent.Source = bitmap;
            }
            else
            {
                imgFileContent.Source = new BitmapImage(new Uri("images/file.png", UriKind.Relative));
            }
        }
   
        private void ListViewFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFile();
        }
        private void OpenFile()
        {
            try
            {
                var selectedItem = listViewItems.SelectedItem as ListViewItemModel;
                if (selectedItem != null)
                {
                    if (Directory.Exists(selectedItem.Path))
                    {
                        SetSelectedDirectory(selectedItem.Path);
                    }
                    else if (File.Exists(selectedItem.Path))
                    {
                        if (_fileManager.IsImageFile(selectedItem.Path))
                        {
                            var imageFiles = GetImageFilesInDirectory(_currentDirectory);
                            var displayImageWindow = new frmDisplayImage(selectedItem.Path, imageFiles);
                            displayImageWindow.ShowDialog();
                        }
                        else
                        {
                            OpenFileWithDefaultApplication(selectedItem.Path);
                        }
                    }
                }
            }catch (Exception ex) { }
        }
        private void OpenFileWithDefaultApplication(string filePath)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }catch (Exception ex) { }
        }
        private List<string> GetImageFilesInDirectory(string directoryPath)
        {
            var imageExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" };
            var imageFiles = new List<string>();

            try
            {
                var files = Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    if (imageExtensions.Contains(Path.GetExtension(file).ToLower()))
                    {
                        imageFiles.Add(file);
                    }
                }
            }
            catch (Exception ex) { }
            return imageFiles;
        }
        private void miOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }
        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listViewItems.SelectedItem as ListViewItemModel;
            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to delete '{selectedItem.Name}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (File.Exists(selectedItem.Path))
                        {
                            File.Delete(selectedItem.Path);
                        }
                        else if (Directory.Exists(selectedItem.Path))
                        {
                            Directory.Delete(selectedItem.Path, true);
                        }
                        Items.Remove(selectedItem);
                        _fileManager.LoadFiles(_currentDirectory, listViewItems, cmbTypeFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting file or folder: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void listViewItems_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var selectedItem = listViewItems.SelectedItem as ListViewItemModel;
                if (selectedItem != null)
                {
                    // Create a DataObject to hold the file path
                    var data = new DataObject(DataFormats.FileDrop, new string[] { selectedItem.Path });

                    // Initiate the drag-and-drop operation
                    DragDrop.DoDragDrop(listViewItems, data, DragDropEffects.Copy);
                }
            }
        }
        private void listViewItems_Drop(object sender, DragEventArgs e)
        {
            // Check if the dropped data contains files
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Get the array of file paths
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Check if the source directory is the same as the current directory
                foreach (string filePath in files)
                {
                    string sourceDirectory = Path.GetDirectoryName(filePath);
                    if (sourceDirectory.Equals(_currentDirectory, StringComparison.OrdinalIgnoreCase))
                    {
                        return; // exit the method to prevent the drop
                    }
                }
                foreach (string filePath in files)
                {
                    try
                    {
                        GenerateUniqueFilePath(filePath);
                    }
                    catch (Exception ex) { }
                   
                }

                // Refresh the file list to show the new files
                _fileManager.LoadFiles(_currentDirectory, listViewItems, cmbTypeFile);
            }
        }
        private void GenerateUniqueFilePath(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string newFilePath = Path.Combine(_currentDirectory, fileName);
            if (File.Exists(newFilePath))
            {
                // Generate a unique name by appending "_copy" and a number
                int copyNumber = 1;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                string fileExtension = Path.GetExtension(filePath);
                string newFileName;
                do
                {
                    newFileName = $"{fileNameWithoutExtension}_copy{copyNumber}{fileExtension}";
                    newFilePath = Path.Combine(_currentDirectory, newFileName);
                    copyNumber++;
                } while (File.Exists(newFilePath));

            }
            File.Copy(filePath, newFilePath);
        }
        private void listViewItems_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy; // Allow copying the files
            }
            else
            {
                e.Effects = DragDropEffects.None; // Disallow dropping if the data is not files
            }
        }

        private void listViewItems_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (listViewItems.SelectedItem != null)
            {
                var selectedItem = listViewItems.SelectedItem as ListViewItemModel;
                if (selectedItem != null)
                {
                    treeViewFileStructure.Items.Clear();
                    var rootNode = new TreeViewItemModel { Name = "Details" };

                    if (Directory.Exists(selectedItem.Path))
                    {
                        // Handle folder details
                        var dirInfo = new DirectoryInfo(selectedItem.Path);
                        rootNode.Details.Add(new TreeViewItemModel { Name = $"Name: {dirInfo.Name}" });
                        rootNode.Details.Add(new TreeViewItemModel { Name = $"Path: {dirInfo.FullName}" });
                        rootNode.Details.Add(new TreeViewItemModel { Name = $"Created: {dirInfo.CreationTime}" });
                        rootNode.Details.Add(new TreeViewItemModel { Name = $"Last Modified: {dirInfo.LastWriteTime}" });

                        // Get the number of files and subfolders
                        int fileCount = dirInfo.GetFiles().Length;
                        int folderCount = dirInfo.GetDirectories().Length;
                        rootNode.Details.Add(new TreeViewItemModel { Name = $"Contains: {fileCount} files, {folderCount} folders" });
                    }
                    else if (File.Exists(selectedItem.Path))
                    {
                        // Handle file details
                        rootNode.Details.Add(new TreeViewItemModel { Name = $"Name: {selectedItem.Name}" });
                        rootNode.Details.Add(new TreeViewItemModel { Name = $"Path: {selectedItem.Path}" });

                        var fileInfo = new FileInfo(selectedItem.Path);
                        string fileSize = (fileInfo.Length / 1024) + " KB";
                        rootNode.Details.Add(new TreeViewItemModel { Name = $"Size: {fileSize}" });
                        rootNode.Details.Add(new TreeViewItemModel { Name = $"Created: {fileInfo.CreationTime}" });
                        rootNode.Details.Add(new TreeViewItemModel { Name = $"Last Modified: {fileInfo.LastWriteTime}" });

                        if (_fileManager.IsImageFile(selectedItem.Path))
                        {
                            var bitmap = new BitmapImage(new Uri(selectedItem.Path));
                            string dimensions = $"{bitmap.PixelWidth}x{bitmap.PixelHeight}";
                            rootNode.Details.Add(new TreeViewItemModel { Name = $"Dimensions: {dimensions}" });
                        }
                    }
                    treeViewFileStructure.Items.Add(rootNode);
                    ExpandTreeViewItem(treeViewFileStructure, rootNode);
                }
            }
        }
        private void ExpandTreeViewItem(ItemsControl parent, TreeViewItemModel item)
        {
            var container = parent.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
            if (container != null)
            {
                container.IsExpanded = true;
                foreach (var child in item.Details)
                {
                    ExpandTreeViewItem(container, child);
                }
            }
        }
        private void miConvertFormat_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listViewItems.SelectedItem as ListViewItemModel;
            if (selectedItem != null && _fileManager.IsImageFile(selectedItem.Path))
            {
                var formatDialog = new FormatSelectionDialog();
                if (formatDialog.ShowDialog() == true)
                {
                    string selectedFormat = formatDialog.SelectedFormat;
                    string outputPath = GetUniqueOutputPath(selectedItem.Path, selectedFormat);

                    try
                    {
                        // Convert the image to the selected format
                        ConvertImageFormat(selectedItem.Path, outputPath, selectedFormat);

                        // Refresh the file list to show the new file
                        _fileManager.LoadFiles(_currentDirectory, listViewItems, cmbTypeFile);

                        MessageBox.Show($"Image successfully converted to {selectedFormat}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error converting image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an image file to convert.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void miInsert_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = true, 
                Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp; *.tiff; *.webp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff;*.webp|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                foreach (string filePath in selectedFiles)
                {
                    try
                    {
                        GenerateUniqueFilePath(filePath);
                    }catch(Exception ex) { }
                }
                _fileManager.LoadFiles(_currentDirectory, listViewItems, cmbTypeFile);
            }
        }
        private string GetUniqueOutputPath(string inputPath, string targetFormat)
        {
            string directory = Path.GetDirectoryName(inputPath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{fileNameWithoutExtension}{targetFormat}");
            if (File.Exists(outputPath))
            {
                int copyNumber = 1;
                string newFileName;
                do
                {
                    newFileName = $"{fileNameWithoutExtension}_copy{copyNumber}{targetFormat}";
                    outputPath = Path.Combine(directory, newFileName);
                    copyNumber++;
                } while (File.Exists(outputPath));
            }
            return outputPath;
        }
        private void ConvertImageFormat(string inputPath, string outputPath, string targetFormat)
        {
            // Load the image 
            BitmapDecoder decoder = BitmapDecoder.Create(
                new Uri(inputPath, UriKind.Absolute),
                BitmapCreateOptions.PreservePixelFormat,
                BitmapCacheOption.OnLoad);
            BitmapFrame frame = decoder.Frames[0];
            // Create a BitmapEncoder based on the target format
            BitmapEncoder encoder = GetBitmapEncoder(targetFormat);
            // Add the frame to the encoder
            encoder.Frames.Add(BitmapFrame.Create(frame));
            using (var stream = File.Create(outputPath))
            {
                encoder.Save(stream);
            }
        }
        private BitmapEncoder GetBitmapEncoder(string format)
        {
            switch (format.ToLower())
            {
                case ".png":
                    return new PngBitmapEncoder();
                case ".jpg":
                case ".jpeg":
                    return new JpegBitmapEncoder();
                case ".bmp":
                    return new BmpBitmapEncoder();
                case ".gif":
                    return new GifBitmapEncoder();
                case ".tiff":
                    return new TiffBitmapEncoder();
                default:
                    throw new NotSupportedException($"Unsupported format: {format}");
            }
        }

        private void miCopy_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listViewItems.SelectedItem as ListViewItemModel;
            if (selectedItem != null)
            {
                // Create a System.Collections.Specialized.StringCollection to hold the file/folder paths
                var fileDropList = new System.Collections.Specialized.StringCollection();

                // Add the selected file/folder path to the collection
                fileDropList.Add(selectedItem.Path);

                // Copy the file/folder to the clipboard in a format that Windows Explorer understands
                Clipboard.SetFileDropList(fileDropList);
                MessageBox.Show($"Copied: {selectedItem.Path}", "Copy", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select an item to copy.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void miPaste_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsData(DataFormats.FileDrop))
            {
                string[] files = (string[])Clipboard.GetData(DataFormats.FileDrop);

                bool isCutOperation = Clipboard.ContainsData("IsCutOperation")
                    && (bool)Clipboard.GetData("IsCutOperation");

                foreach (string filePath in files)
                {
                    try
                    {
                        string newFilePath = GetUniqueFilePath(filePath);

                        if (isCutOperation)
                        {
                            File.Move(filePath, newFilePath);
                        }
                        else
                        {
                            File.Copy(filePath, newFilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
                if (isCutOperation)
                {
                    Clipboard.Clear();
                }

                _fileManager.LoadFiles(_currentDirectory, listViewItems, cmbTypeFile);
            }
        }
        private string GetUniqueFilePath(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string newFilePath = Path.Combine(_currentDirectory, fileName);

            if (File.Exists(newFilePath))
            {
                int copyNumber = 1;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);

                do
                {
                    newFilePath = Path.Combine(_currentDirectory, $"{fileNameWithoutExtension}_copy{copyNumber}{extension}");
                    copyNumber++;
                } while (File.Exists(newFilePath));
            }

            return newFilePath;
        }
        private void listViewItems_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy; // Allow copying the files
            }
            else
                e.Effects = DragDropEffects.None; // Disallow dropping if the data is not files
        }
        public void ChangeViewMode(string viewMode)
        {
            switch (viewMode)
            {
                case "Large Icons":
                    listViewItems.ItemTemplate = (DataTemplate)Application.Current.Resources["LargeIconsTemplate"];
                    listViewItems.ItemsPanel = (ItemsPanelTemplate)Application.Current.Resources["IconsPanelTemplate"];
                    break;
                case "Medium Icons":
                    listViewItems.ItemTemplate = (DataTemplate)Application.Current.Resources["MediumIconsTemplate"];
                    listViewItems.ItemsPanel = (ItemsPanelTemplate)Application.Current.Resources["IconsPanelTemplate"];
                    break;
                case "Small Icons":
                    listViewItems.ItemTemplate = (DataTemplate)Application.Current.Resources["SmallIconsTemplate"];
                    listViewItems.ItemsPanel = (ItemsPanelTemplate)Application.Current.Resources["IconsPanelTemplate"];
                    break;
                case "List":
                    listViewItems.ItemTemplate = (DataTemplate)Application.Current.Resources["ListTemplate"];
                    listViewItems.ItemsPanel = (ItemsPanelTemplate)Application.Current.Resources["ListPanelTemplate"];
                    break;
            }
        }
        private void listViewItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = listViewItems.SelectedItem as ListViewItemModel;
            if (selectedItem != null)
            {
                imgSelectedIcon.Source = new BitmapImage(new Uri(selectedItem.Icon, UriKind.RelativeOrAbsolute));
            }
            else
                imgSelectedIcon.Source = null;
        }
        private void cmbSearchHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSearchHistory.SelectedItem != null)
            {
                txtSearch.Text = cmbSearchHistory.SelectedItem.ToString();
                cmbSearchHistory.SelectedIndex = -1; 
            }
        }
        private void btnClearHistory_Click(object sender, RoutedEventArgs e)
        {
            cmbSearchHistory.Items.Clear();
            SearchHistory.Clear();
            _fileManager.ClearSearchHistoryFile();
            MessageBox.Show("Search history cleared successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void miCut_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listViewItems.SelectedItem as ListViewItemModel;
            if (selectedItem != null)
            {
                var fileDropList = new System.Collections.Specialized.StringCollection();
                fileDropList.Add(selectedItem.Path);

                // create a daataObject with both the file list and a "IsCutOperation" flag
                var data = new DataObject();
                data.SetFileDropList(fileDropList);
                data.SetData("IsCutOperation", true);

                Clipboard.SetDataObject(data);
                MessageBox.Show($"Cut: {selectedItem.Path}", "Cut", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select an item to cut.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}