using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace CryptoAES
{
    /// <summary>
    /// Interaction logic for EncryptionWindow.xaml
    /// </summary>
    public partial class EncryptionWindow : Window
    {
        public EncryptionWindow()
        {
            InitializeComponent();
            File_Address.Visibility = Visibility.Hidden;
            ErrorMessage.Visibility = Visibility.Hidden;
         
        }





        private void browes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Multiselect = false,
                Title = "Crypto AES"
            };

            if (openFileDialog1.ShowDialog() == true)
            {
                File_Address.Content = openFileDialog1.FileName;
                File_Address.Visibility = Visibility.Visible;
            }
        }

      


        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordTxtBox.Text = passwordBox.Password;
            passwordBox.Visibility = Visibility.Collapsed;
            passwordTxtBox.Visibility = Visibility.Visible;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = passwordTxtBox.Text;
            passwordTxtBox.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
        }

      
        private void StartEncryption_Click_1(object sender, RoutedEventArgs e)
        {
            string file = File_Address.Content.ToString();
            string password = passwordTxtBox.Text;
            if (!File.Exists(file))
            {
                MessageBox.Show("File does not exist.");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password empty. please enter your password.");
                return;
            }
            try
            {

               
                byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesEncrypted = MainWindow.AES_Encrypt(bytesToBeEncrypted, passwordBytes);

                string fileEncrypted = File_Address.Content.ToString();

                fileEncrypted = Path.GetExtension(fileEncrypted);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Files (*" + fileEncrypted + ")|*" + fileEncrypted;
                if (sd.ShowDialog() == true)
                {
                    File.WriteAllBytes(sd.FileName, bytesEncrypted);
                }
                
            }
            catch
            {
                MessageBox.Show("File is in use. close other program is using thia file and try again");
                return;
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.Show();
        }
    }
}
