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
using IMSBeta.window;
using Microsoft.Win32;
using IMSBeta.Module;
using DataModelLayer;
using System.Security.Cryptography;

namespace IMSBeta
{
    /// <summary>
    /// Interaction logic for win_login.xaml
    /// </summary>
    public partial class win_login : Window
    {
        public win_login()
        {
            InitializeComponent();
        }
      

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
          
            System.Environment.Exit(0);
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            inventorydbEntities1 database = new inventorydbEntities1();
            ///////////////////////////////////////Change Password type to String
            
            SHA256CryptoServiceProvider Sha2 = new SHA256CryptoServiceProvider();
            Byte[] B1;
            Byte[] B2;

            B1 = UTF8Encoding.UTF8.GetBytes(TbxPassword.Password.Trim());
            B2 = Sha2.ComputeHash(B1);

            string UserPasswordHashed = BitConverter.ToString(B2);

            //////////////////////////////////////////////////Check if User and pass in DB
            var query = from U in database.Users
                        where U.UserUserName == TbxUserName.Text.Trim()
                        where U.UserPassword == UserPasswordHashed
                        where U.UserActive == 1
                        select U;
            var user = query.ToList();
            /////////////////////////////////////////////////////////////////////



          if (user.Count == 1)

            {
                RegistryKey UserNameKey = Registry.CurrentUser.CreateSubKey("software\\IMSBeta");
                try
                {
                    UserNameKey.SetValue("UserNameRegister", TbxUserName.Text.Trim());

                    PublicVariable.gUserName = user[0].UserName;
                    PublicVariable.gUserFamily = user[0].UserFamily;
                    PublicVariable.gUserId = user[0].UserID;
                }
                catch
                {
                    MessageBox.Show("Error Happenend for user login", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }
                finally
                {
                    UserNameKey.Close();
                }

                this.Close();
            }
            else
            {

                
                MessageBox.Show("Wrong Credential", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ////////////////////////////////Registery
            RegistryKey masterkey = Registry.CurrentUser.CreateSubKey("software\\IMSBeta");
            TbxUserName.Text = (string)masterkey.GetValue("UserNameRegister");
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
