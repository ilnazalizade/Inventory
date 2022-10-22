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
using DataModelLayer;
using IMSBeta.Module;
using System.Security.Cryptography;


namespace IMSBeta.window
{
    /// <summary>
    /// Interaction logic for win_SetNewPassword.xaml
    /// </summary>
    public partial class win_SetNewPassword : Window
    {
        public win_SetNewPassword()
        {
            InitializeComponent();
        }

        inventorydbEntities1 database = new inventorydbEntities1();

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void image_close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_username.Content = PublicVariable.gUserName + " " + PublicVariable.gUserFamily;
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt_newpass.Password == "" || txt_oldPass.Password == "")
            {
                MessageBox.Show("Old password or new password must not be empty", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try { 
             ///////////////////////////////////////////////////Hashing Old Password
                SHA256CryptoServiceProvider Sha_oldPass = new SHA256CryptoServiceProvider();
            Byte[] B1_oldpass;
            Byte[] B2_oldpass;

            B1_oldpass = UTF8Encoding.UTF8.GetBytes(txt_oldPass.Password.Trim());
            B2_oldpass = Sha_oldPass.ComputeHash(B1_oldpass);

            string UserOldPass = BitConverter.ToString(B2_oldpass);
            /////////////////////////////////////////////////////
            var query = from User in database.Users
                        where User.UserID == PublicVariable.gUserId
                        where User.UserPassword == UserOldPass
                        select User;
            var result = query.ToList();

            if (result.Count == 0)
            {
                    MessageBox.Show("The old password was entered incorrectly", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (result.Count == 1)
            {
                    ///////////////////////////////////////////////////Hashing New Password
                    SHA256CryptoServiceProvider Sha_newPass = new SHA256CryptoServiceProvider();
                Byte[] B1_newpass;
                Byte[] B2_newpass;

                B1_newpass = UTF8Encoding.UTF8.GetBytes(txt_newpass.Password.Trim());
                B2_newpass = Sha_newPass.ComputeHash(B1_newpass);

                string Usernewpass = BitConverter.ToString(B2_newpass);
                //////////////////////////////////////////////////

                var UpdatePasswordQuery = (from U in database.Users where U.UserID == PublicVariable.gUserId select U).SingleOrDefault();

                UpdatePasswordQuery.UserPassword = Usernewpass;

                database.SaveChanges();
                    MessageBox.Show("Password edited successfully", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }

            }
            catch
            {
                MessageBox.Show("There is a problem in registering information", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
    

