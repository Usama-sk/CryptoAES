using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace CryptoAES
{
    /// <summary>
    /// Interaction logic for DecryptionWindow.xaml
    /// </summary>
    public partial class DecryptionWindow : Window
    {
        public DecryptionWindow()
        {
            InitializeComponent();
        }

      

        

        private void browes_Click(object sender, RoutedEventArgs e)
        {
            // Configure printer dialog box
            var dialog = new System.Windows.Controls.PrintDialog();
            dialog.PageRangeSelection = System.Windows.Controls.PageRangeSelection.AllPages;
            dialog.UserPageRangeEnabled = true;

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

        }
    }
}
