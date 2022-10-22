using DataModelLayer;
using IMSBeta.Module;
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


namespace IMSBeta.window
{
    /// <summary>
    /// Interaction logic for win_customer.xaml
    /// </summary>
    public partial class win_customer : Window
    {
        public win_customer()
        {
            InitializeComponent();
        }
        //-- Db instantiation
        inventorydbEntities1 database = new inventorydbEntities1();

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {// LINQ syntax: 
            //var query = from u in database.Vw_Users select u;
            //var user =query.ToList();
            //DataGridUser.ItemsSource = user;
            ShowCustomerInfo();


        }
        // Connect to Db and Show in datagrid 
        private void ShowCustomerInfo()
        {
            var query = database.Database.SqlQuery<Vw_Customer>("Select * From Vw_Customer");
            var u = query.ToList();
            DataGridCustomer.ItemsSource = u;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Btn_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            win_add_edit_customer w_ae = new win_add_edit_customer();
            w_ae.win_Type = 1; // adding customer
            w_ae.ShowDialog();
            ShowCustomerInfo();
        }

        private void Btn_EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            object item = DataGridCustomer.SelectedItem;
            win_add_edit_customer w_ae = new win_add_edit_customer();
            w_ae.win_Type = 2; // Update Customer


            if (item == null)
            {
                MessageBox.Show("Please select a customer first", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            w_ae.CID = Convert.ToInt32((DataGridCustomer.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            w_ae.CName = (DataGridCustomer.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            w_ae.CTel = (DataGridCustomer.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
            w_ae.CAddress = (DataGridCustomer.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;



            w_ae.ShowDialog();
            ShowCustomerInfo();
            
           
        }

        private void Btn_DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
           



            if (MessageBox.Show("Are you sure you want to deactive the customer?", "Deactive Customer", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        object item = DataGridCustomer.SelectedItem;
                        int CustomerID;
                        CustomerID = Convert.ToInt32((DataGridCustomer.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

                        Customer C = (from Cu in database.Customers where Cu.CustomerID == CustomerID select Cu).SingleOrDefault();

                        C.CustomerActive = 2;



                        database.SaveChanges();
                    MessageBox.Show("Customer successfully deactivated", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                        ShowCustomerInfo();
                    }
                    catch
                    {
                    MessageBox.Show("There was a problem registering information", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }


          
        }

        private void btn_ActiveCustomer_Click(object sender, RoutedEventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to active the customer?", " Active Customer", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    object item = DataGridCustomer.SelectedItem;
                    int CustomerID;
                    CustomerID = Convert.ToInt32((DataGridCustomer.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

                    Customer C = (from Cu in database.Customers where Cu.CustomerID == CustomerID select Cu).SingleOrDefault();

                    C.CustomerActive = 1;



                    database.SaveChanges();
                    MessageBox.Show("Customer successfully activated", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                    ShowCustomerInfo();
                }
                catch
                {
                    MessageBox.Show("There was a problem registering information", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

            private void DataGridCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            }
        }
}
