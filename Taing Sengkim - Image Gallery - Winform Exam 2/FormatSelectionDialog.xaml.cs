using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ImageGallery_WPF_Exam
{
    /// <summary>
    /// Interaction logic for FormatSelectionDialog.xaml
    /// </summary>
    public partial class FormatSelectionDialog : Window
    {
        public string SelectedFormat { get; private set; }

        public FormatSelectionDialog()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbFormats.SelectedItem is ComboBoxItem selectedItem)
            {
                SelectedFormat = selectedItem.Tag.ToString();
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please select a format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
