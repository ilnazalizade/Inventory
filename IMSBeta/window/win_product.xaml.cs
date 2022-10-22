using DataModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for win_product.xaml
    /// </summary>
    /// 


    public partial class win_product : Window
    {
        public win_product()
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
            ShowProductInfo();




        }
        // Connect to Db and Show in datagrid
        private void ShowProductInfo()
        {
            var query = database.Database.SqlQuery<Vw_Product>("Select * From Vw_Product");
            var u = query.ToList();
            DataGridProduct.ItemsSource = u;
        }



        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }



        private void Btn_ProductPrice_Click(object sender, RoutedEventArgs e)
        {

            object item = DataGridProduct.SelectedItem;

            win_productprice W_price = new win_productprice();

            W_price.ProductName = (DataGridProduct.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            W_price.ProductId = Convert.ToInt32((DataGridProduct.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                       
            W_price.ShowDialog();
        }

        private void BtnShowInventory_Click(object sender, RoutedEventArgs e)
        {

            object item = DataGridProduct.SelectedItem;

            win_inventory W_price = new win_inventory();

            W_price.ProductName = (DataGridProduct.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            W_price.ProductID = Convert.ToInt32((DataGridProduct.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

            W_price.ShowDialog();
            ShowProductInfo();
        }

        private void Btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            win_add_edit_product w_p = new win_add_edit_product();
            w_p.win_Type = 1;//Adding Mode
            w_p.ShowDialog();
            ShowProductInfo();
        }

        private void Btn_EditProduct_Click(object sender, RoutedEventArgs e)
        {
            object item = DataGridProduct.SelectedItem;
            win_add_edit_product w_p = new win_add_edit_product();
            w_p.win_Type = 2;////Editing Mode


            w_p.ProductId = Convert.ToInt32((DataGridProduct.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            w_p.ProductName = (DataGridProduct.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            w_p.ProductDesc = (DataGridProduct.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;

            w_p.ShowDialog();
            ShowProductInfo();
        }
        private void Btn_ActiveProduct_Click(object sender, RoutedEventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to active the customer?", " Active Customer", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    object item = DataGridProduct.SelectedItem;
                    int ProductID;
                    ProductID = Convert.ToInt32((DataGridProduct.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

                    Product P = (from p in database.Products where p.ProductId == ProductID select p).SingleOrDefault();

                    P.ProductActive = 1;



                    database.SaveChanges();
                    MessageBox.Show("Product successfully activated", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                    ShowProductInfo();
                }
                catch
                {
                    MessageBox.Show("There was a problem registering information", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void Btn_DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            object item = DataGridProduct.SelectedItem;

            int ProductCount = Convert.ToInt32((DataGridProduct.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);

            if (ProductCount > 0)
            {
                MessageBox.Show("Because this item has a non-zero inventory, the operation cannot be performed", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Are you sure to deactive the product?", "Deactive Product", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {

                    int ProductID;
                    ProductID = Convert.ToInt32((DataGridProduct.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

                    Product P = (from Pr in database.Products where Pr.ProductId == ProductID select Pr).SingleOrDefault();

                    P.ProductActive = 2;



                    database.SaveChanges();
                    MessageBox.Show("This product has been successfully deactivated", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                    ShowProductInfo();
                }
                catch
                {
                    MessageBox.Show("There was a problem registering information", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        /////
    }
}

  
