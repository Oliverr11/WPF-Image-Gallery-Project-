using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageGallery_WPF_Exam
{
    public partial class frmDisplayImage : Window
    {
        private string currentImagePath;
        private List<string> imagePaths;
        private int currentIndex;
        private double zoomFactor = 1.0; 
        private Point start; // For panning
        private Point origin; 

        public frmDisplayImage(string imagePath, List<string> imagePaths)
        {
            InitializeComponent();
            this.currentImagePath = imagePath;
            this.imagePaths = imagePaths;

            currentIndex = imagePaths.IndexOf(imagePath);

            DisplayImage(currentImagePath);

            picHolder.MouseLeftButtonDown += PicHolder_MouseLeftButtonDown;
            picHolder.MouseLeftButtonUp += PicHolder_MouseLeftButtonUp;
            picHolder.MouseMove += PicHolder_MouseMove;
        }

        private void DisplayImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                MessageBox.Show("Image not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close(); 
                return;
            }
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad; 
                bitmap.EndInit();
                picHolder.Source = bitmap;

                zoomFactor = 1.0;
                imageScaleTransform.ScaleX = zoomFactor;
                imageScaleTransform.ScaleY = zoomFactor;
                imageTranslateTransform.X = 0;
                imageTranslateTransform.Y = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (imagePaths == null || imagePaths.Count == 0)
                return;
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = imagePaths.Count - 1;
            }
            currentImagePath = imagePaths[currentIndex];
            DisplayImage(currentImagePath);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (imagePaths == null || imagePaths.Count == 0)
                return;

            currentIndex++;
            if (currentIndex >= imagePaths.Count)
            {
                currentIndex = 0; 
            }
            currentImagePath = imagePaths[currentIndex];
            DisplayImage(currentImagePath);
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            zoomFactor *= 1.1;
            imageScaleTransform.ScaleX = zoomFactor;
            imageScaleTransform.ScaleY = zoomFactor;
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            // Decrease the zoom factor by 10%
            zoomFactor /= 1.1;
            imageScaleTransform.ScaleX = zoomFactor;
            imageScaleTransform.ScaleY = zoomFactor;
        }

        private void PicHolder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (picHolder.IsMouseCaptured) return;
            picHolder.CaptureMouse();
            start = e.GetPosition(this);
            origin = new Point(imageTranslateTransform.X, imageTranslateTransform.Y);
        }

        private void PicHolder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            picHolder.ReleaseMouseCapture();
        }

        private void PicHolder_MouseMove(object sender, MouseEventArgs e)
        {
            if (!picHolder.IsMouseCaptured) return;
            Point current = e.GetPosition(this);
            imageTranslateTransform.X = origin.X + (current.X - start.X);
            imageTranslateTransform.Y = origin.Y + (current.Y - start.Y);
        }
    }
}