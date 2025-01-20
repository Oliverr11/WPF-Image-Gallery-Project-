using ImageGallery_WPF_Exam.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImageGallery_WPF_Exam
{
    public class FileManager
    {
        public List<string> ImageExtensions { get; } = new List<string>
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp"
        };
        public ObservableCollection<SearchHistoryItem> SearchHistory { get; set; } = new ObservableCollection<SearchHistoryItem>();


        public void LoadFiles(string directoryPath, ListView listViewItems, ComboBox cmbTypeFile)
        {
            try
            {
                listViewItems.Items.Clear();

                string selectedFileType = cmbTypeFile.SelectedItem as string;

                var directories = Directory.GetDirectories(directoryPath);
                foreach (var dir in directories)
                {
                    var dirInfo = new DirectoryInfo(dir);

                    var folderItem = new ListViewItemModel
                    {
                        Name = dirInfo.Name,
                        Icon = "images/3Dfolder.png", 
                        Path = dirInfo.FullName
                    };

                    listViewItems.Items.Add(folderItem);
                }

                var files = Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    if (selectedFileType == "All Files" || fileInfo.Extension.Equals(selectedFileType, StringComparison.OrdinalIgnoreCase))
                    {
                        var fileItem = new ListViewItemModel
                        {
                            Name = fileInfo.Name,
                            Icon = IsImageFile(fileInfo.FullName) ? fileInfo.FullName : "images/3DFile.png", 
                            Path = fileInfo.FullName
                        };
                        listViewItems.Items.Add(fileItem);
                    }
                }
            }
            catch (Exception ex) { }
           
        }
        public bool IsImageFile(string path)
        {
            var imageExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" };
            return imageExtensions.Contains(Path.GetExtension(path).ToLower());
        }
        public void LoadSearchHistory(ComboBox cmbSearchHistory)
        {
            string filePath = "searchHistory.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var history = JsonConvert.DeserializeObject<ObservableCollection<SearchHistoryItem>>(json);
                foreach (var item in history)
                {
                    cmbSearchHistory.Items.Add(item.SearchQuery);
                }
            }
            else
            {
                SearchHistory = new ObservableCollection<SearchHistoryItem>();
            }
        }
        public void ClearSearchHistoryFile()
        {
            string filePath = "searchHistory.json";
            if (File.Exists(filePath))
            {
                try
                {
                    File.WriteAllText(filePath, "[]");
                }
                catch (Exception ex) { }
            }
        }
        public void SaveSearchHistory()
        {
            string filePath = "searchHistory.json";
            List<SearchHistoryItem> existingHistory = new List<SearchHistoryItem>();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                existingHistory = JsonConvert.DeserializeObject<List<SearchHistoryItem>>(json);
            }
            existingHistory.AddRange(SearchHistory);
            string updatedJson = JsonConvert.SerializeObject(existingHistory, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);
        }
        public void LoadFileSystem(TreeView treeViewFileSystem)
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                var driveItem = new TreeViewItem
                {
                    Header = CreateHeader(drive.Name, false ,null , true), 
                    Tag = drive.RootDirectory
                };
                driveItem.Items.Add(null); 
                driveItem.Expanded += DriveItem_Expanded;
                treeViewFileSystem.Items.Add(driveItem);
            }

        }
        private void DriveItem_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == null)
            {
                item.Items.Clear();
                var dir = (DirectoryInfo)item.Tag;
                try
                {
                    foreach (var subDir in dir.GetDirectories())
                    {
                        var subDirItem = new TreeViewItem
                        {
                            Header = CreateHeader(subDir.Name, false), 
                            Tag = subDir
                        };
                        subDirItem.Items.Add(null); 
                        subDirItem.Expanded += DriveItem_Expanded;
                        item.Items.Add(subDirItem);
                    }
                    foreach (var file in dir.GetFiles())
                    {
                        var fileItem = new TreeViewItem
                        {
                            Header = CreateHeader(file.Name, true, file.Extension),
                            Tag = file
                        };
                        item.Items.Add(fileItem);
                    }
                }
                catch (UnauthorizedAccessException) { }
            }
        }

        private StackPanel CreateHeader(string name, bool isFile, string extension = null, bool isDrive = false)
        {
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };

            string iconSource;
            if (isDrive)
            {
                iconSource = "images/drive-icon-png-17.png";
            }
            else if (isFile)
            {
                if (ImageExtensions.Contains(extension?.ToLower()))
                {
                    iconSource = "images/3DImage.png"; 
                }
                else
                {
                    iconSource = "images/3DFile.png";
                }
            }
            else
            {
                iconSource = "images/3Dfolder.png"; 
            }

            // Add the icon
            var image = new Image
            {
                Source = new BitmapImage(new Uri(iconSource, UriKind.Relative)),
                Width = 16,
                Height = 16,
                Margin = new Thickness(0, 0, 5, 0)
            };
            stackPanel.Children.Add(image);

            // Add the name
            var textBlock = new TextBlock { Text = name };
            stackPanel.Children.Add(textBlock);

            return stackPanel;
        }

    }
}