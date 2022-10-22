using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Security.Cryptography;

namespace IMSBeta.window
{
    /// <summary>
    /// Interaction logic for win_add_edit_user.xaml
    /// </summary>
    public partial class win_add_edit_user : Window
    {
        public win_add_edit_user()
        {
            InitializeComponent();
        }

        inventorydbEntities1 database = new inventorydbEntities1();

        /// /////////////////////////////////////////////////////////////////
        public byte win_type { get; set; }
        public string UserName { get; set; }
        public string UserFamily { get; set; }
        public string UserTel { get; set; }
        public byte UserAge { get; set; }
        public int UserID { get; set; }
        public string UserGender { get; set; }
        public string UserUserName { get; set; }
        /// /////////////////////////////////////////////////////////////////

        private void image_close_MouseDown(object sender, MouseButtonEventArgs e)
        {
        this.Close();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
         this.Close();
        }

        private void txt_usertel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_userage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_name.Focus();



            switch (win_type)
            {
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        //////Display Info For Editing
                        txt_name.Text = UserName;
                        txt_userfamily.Text = UserFamily;
                        txt_usertel.Text = UserTel;
                        txt_userage.Text = UserAge.ToString();
                        txt_username.Text = UserUserName;
                        txt_username.IsEnabled = false;
                        txt_pass.Password = "******";
                        txt_pass.IsEnabled = false;

                        if (UserGender == "2")
                        { rdb_woman.IsChecked = true; }
                        else
                        { rdb_man.IsChecked = true; }

                        break;
                    }
            }
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {

            if (!CheckNullable())
            {
                return;
            }


            try
            {
                switch (win_type)
                {
                    case 1:
                        {
                            /////////////////////////////////////////////////////////////
                            //If username exist or not
                            var query = from UTable in database.Users
                        where UTable.UserUserName == txt_username.Text.Trim()
                        select UTable;
            var result = query.ToList();
            if (result.Count > 0)
            {
                                MessageBox.Show("This username already exists", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_username.Focus();
                return;
            }

            

                //////////Hashing Password////////////////////////////////
                SHA256CryptoServiceProvider Sha2 = new SHA256CryptoServiceProvider();
                Byte[] B1;
                Byte[] B2;
                B1 = UTF8Encoding.UTF8.GetBytes(txt_pass.Password.Trim());
                B2 = Sha2.ComputeHash(B1);

                string UserPasswordHashed = BitConverter.ToString(B2);
                



                /////////////////////////////////////////////////////////////Add new user
                User U = new User();
            U.UserName = txt_name.Text.Trim();
            U.UserFamily = txt_userfamily.Text.Trim();
            U.UserTel = txt_usertel.Text.Trim();
            U.UserAge = Convert.ToByte(txt_userage.Text.Trim());
            U.UserUserName = txt_username.Text.Trim();

            U.UserPassword = UserPasswordHashed;

            if (rdb_man.IsChecked == true)
            {
                U.UserGender = 1;
            }
            else
            {
                U.UserGender = 2;
            }
            U.UserActive = 1;
                database.Users.Add(U);
                database.SaveChanges();
                            MessageBox.Show("New user added successfully", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                 break;
              }
              case 2:
                        {
                            User U = (from User in database.Users where User.UserID == UserID select User).SingleOrDefault();

                            U.UserName = txt_name.Text.Trim();
                            U.UserFamily = txt_userfamily.Text.Trim();
                            U.UserAge = Convert.ToByte(txt_userage.Text.Trim());
                            U.UserTel = txt_usertel.Text.Trim();

                            if (rdb_man.IsChecked == true)
                            { U.UserGender = 1; }
                            else if (rdb_woman.IsChecked == true)
                            { U.UserGender = 2; }

                            database.SaveChanges();
                            MessageBox.Show("User information has been updated successfully", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;

                        }

                        }
                }

            catch
            {
                MessageBox.Show("There was a problem in registering information. Please try again", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Close();
            }
        }
        private bool CheckNullable()
        {
            if (txt_name.Text.Trim() == "")
            {
                MessageBox.Show("Enter the user's name", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_name.Focus();
                return false;
            }
            /////////
            if (txt_userfamily.Text.Trim() == "")
            {
                MessageBox.Show("Enter the user's last name", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_userfamily.Focus();
                return false;
            }
            ////////
            if (txt_usertel.Text.Trim() == "")
            {
                MessageBox.Show("Enter the user's phone number", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_usertel.Focus();
                return false;
            }
            ///////
            if (txt_userage.Text.Trim() == "")
            {
                MessageBox.Show("Enter the user's age", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_userage.Focus();
                return false;
            }
            ///////
            if (txt_username.Text.Trim() == "")
            {
                MessageBox.Show("Enter the username", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_username.Focus();
                return false;
            }
            //////////
            if (txt_pass.Password.Trim() == "")
            {
                MessageBox.Show("Enter the password", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_pass.Focus();
                return false;
            }
            return true;
        }






    }
}
