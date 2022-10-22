using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using IMSBeta.Module;

namespace IMSBeta.window
{
    /// <summary>
    /// Interaction logic for win_main.xaml
    /// </summary>
    public partial class win_main : Window
    {
        public win_main()
        {
            InitializeComponent();
        }

        private void Btn_Con_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Email : name@email.com");
        }
        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // SetDimension();
            lbl_name.Content = PublicVariable.gUserName;
            lbl_family.Content = PublicVariable.gUserFamily;

            /////////////////////
            lbl_time.Content = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        }


        //private void SetDimension()
        //{
        //    this.MaxHeight = 650;
        //    this.MinHeight = 650;
        //    this.MaxWidth = 1200;
        //    this.MinWidth = 1200;
        //}

        private void Btn_ShowUser_Click(object sender, RoutedEventArgs e)
        {
            win_users win_Users = new win_users();
            win_Users.ShowDialog();

        }

        private void btn_SetnewPass_click(object sender, RoutedEventArgs e)
        {

          


                win_SetNewPassword w_newpass = new win_SetNewPassword();
                w_newpass.ShowDialog();
            }
           

        private void Btn_ShowCustomer_Click(object sender, RoutedEventArgs e)
        {
            win_customer win_Customer = new win_customer();
            win_Customer.ShowDialog();
        }

        private void Btn_ShowProduct_Click(object sender, RoutedEventArgs e)
        {
            win_product win_Product = new win_product();
            win_Product.ShowDialog();
        }

        private void Ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }///////
}///////
