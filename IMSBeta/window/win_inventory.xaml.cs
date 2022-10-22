using DataModelLayer;
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
using IMSBeta.Module;

namespace IMSBeta.window
{
    /// <summary>
    /// Interaction logic for win_inventory.xaml
    /// </summary>
    public partial class win_inventory : Window
    {
        public win_inventory()
        {
            InitializeComponent();
        }

        //-- Db instantiation
        inventorydbEntities1 database = new inventorydbEntities1();

        public string ProductName;
        public int ProductID;

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {// LINQ syntax:
            //var query = from u in database.Vw_Users select u;
            //var user =query.ToList();
            //DataGridUser.ItemsSource = user;
            Lbl_Product.Content = ProductName;
            ShowUserInfo();

        }
        //------------

        private void ShowUserInfo()
        {
            var query = database.Database.SqlQuery<Vw_Inventory>("Select * From vw_Inventory Where 1=1 and productId = " + ProductID);
 
             var u =query.ToList();
            DataGridInventory.ItemsSource = u;
        }



        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Btn_AddNewOperation_Click(object sender, RoutedEventArgs e)
        {
            win_AddnewTransaction w_addtrans = new win_AddnewTransaction();
            w_addtrans.productid = this.ProductID;
            w_addtrans.productName = this.ProductName;
            w_addtrans.ShowDialog();
            ShowUserInfo();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
