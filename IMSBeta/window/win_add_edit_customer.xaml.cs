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
using IMSBeta.Module;

namespace IMSBeta.window
{
    /// <summary>
    /// Interaction logic for win_add_edit_customer.xaml
    /// </summary>
    public partial class win_add_edit_customer : Window
    {
        public win_add_edit_customer()
        {
            InitializeComponent();
        }

        public byte win_Type;
        public string CName;
        public string CAddress;
        public string CTel;
        public int CID;

        inventorydbEntities1 database = new inventorydbEntities1();

        private void image_close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNullable())
            {
                return;
            }
            //////////////////////////////////////////////////
            try
            {

                switch (win_Type)

                {
                    case 1:
                        {///////Insert Block
                            Customer C = new Customer();

                            C.CustomerName = txt_customerName.Text.Trim();
                            C.CustomerTel = txt_telcustomer.Text.Trim();
                            C.CustomerAddress = txt_customerAddress.Text.Trim();
                            C.UserID = PublicVariable.gUserId;

                            database.Customers.Add(C);
                            database.SaveChanges();

                            
                            MessageBox.Show("New customer added successfully", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        }
                    case 2:
                        {///////Update Block
                            Customer Cu = (from C in database.Customers where C.CustomerID == this.CID select C).SingleOrDefault();

                            Cu.CustomerName = txt_customerName.Text.Trim();
                            Cu.CustomerTel = txt_telcustomer.Text.Trim();
                            Cu.CustomerAddress = txt_customerAddress.Text.Trim();
                            //  Cu.UpdateDate = lbl_date.Content.ToString();


                            database.SaveChanges();
                            MessageBox.Show("Customer information has been updated successfully", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                         
                            break;
                        }
                }
            }
            catch
            {
                MessageBox.Show("There was a problem with the database. Please try again", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
            finally
            {
                this.Close();
            }
        }

          
        private bool CheckNullable()
        {
            if (txt_customerName.Text == "")
            {
                MessageBox.Show("Customer name is Empty", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("Customer name is empty");
                txt_customerName.Focus();
                return false;
            }
            if (txt_telcustomer.Text == "")
            {
                MessageBox.Show("The customer's phone is empty", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_telcustomer.Focus();
                return false;
            }
            if (txt_customerAddress.Text == "")
            {
                MessageBox.Show("Customer address is empty", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_customerAddress.Focus();
                return false;
            }
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_customerName.Focus();
            lbl_username.Content = PublicVariable.gUserName + " " + PublicVariable.gUserFamily;

            switch (win_Type)
            {
                case 2:
                    txt_customerName.Text = this.CName;
                    txt_customerAddress.Text = this.CAddress;
                    txt_telcustomer.Text = this.CTel;

                    ////////////
                    break;
            }
        }

        private void txt_telcustomer_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_telcustomer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); e.Handled = regex.IsMatch(e.Text);
        }
    }
}
