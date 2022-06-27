using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Ookii.Dialogs.Wpf;





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
            File_Address.Visibility = Visibility.Hidden;
            ErrorMessage.Visibility = Visibility.Hidden;
            Openfolder.Visibility = Visibility.Hidden;
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

        private void Openfolder_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = @"C:\Users\Usama\Documents\Crypto AES\Decrypted File";
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath
                };
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("{0} Directory does not exist!", folderPath));
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

        private void StartDecryption_Click(object sender, RoutedEventArgs e)
        {

            string fileEncrypted = File_Address.Content.ToString();
            string password = passwordTxtBox.Text;
            if (!File.Exists(fileEncrypted))
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
                byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesDecrypted = MainWindow.AES_Decrypt(bytesToBeDecrypted, passwordBytes);

                string file = Save_File_Address.Content.ToString();
                file = Path.GetExtension(file);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Files (*" + file + ")|*" + file;
                if (sd.ShowDialog() == true)
                {
                    file = sd.FileName;
                    File.WriteAllBytes(file, bytesDecrypted);
                }
                File_Address.Content = "";
                File_Address.Visibility = Visibility.Hidden;
            }
            catch
            {
                MessageBox.Show("File is in use. close other program is using thia file and try again");
                return;
            }

        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.Show();
        }
    }
}
