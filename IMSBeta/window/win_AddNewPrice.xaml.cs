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
using System.Transactions;

namespace IMSBeta.window
{
    /// <summary>
    /// Interaction logic for win_AddNewPrice.xaml
    /// </summary>
    public partial class win_AddNewPrice : Window
    {
        public win_AddNewPrice()
        {
            InitializeComponent();
        }
        inventorydbEntities1 database = new inventorydbEntities1();
        public string productName;
        public int productid;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ////////// Display info in Lables/////////
            
            lbl_title.Content = this.productName;
            lbl_username.Content = PublicVariable.gUserName + " " + PublicVariable.gUserFamily;


            //////////////////////////////////////////
            txt_purch.Focus();
        }

        private void image3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void txt_purch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_sale_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    /////////////////////////////////////////////////Add New Price
                    ProductPrice PP = new ProductPrice();
                    PP.ProductPricePurch = Convert.ToInt64(txt_purch.Text.Trim());
                    PP.ProductPriceSell = Convert.ToInt64(txt_sale.Text.Trim());
                    PP.ProductPriceDesc = txt_desc.Text.Trim();
                    PP.ProductId = this.productid;
                    PP.UserId = PublicVariable.gUserId;

                    database.ProductPrices.Add(PP);
                    database.SaveChanges();
                    ////////////////////////////////////// Update Price
                    database.Sp_UpdateFee_Product(this.productid, Convert.ToInt64(txt_sale.Text.Trim()));
                    database.SaveChanges();

                    ts.Complete();
                    MessageBox.Show("Information saved successfully", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch
                {
                    MessageBox.Show("There is a problem in registering information", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    this.Close();
                }
            }
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool CheckNullable()
        {
            if (txt_purch.Text == "")
            {
                MessageBox.Show("Enter the purchase price", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_purch.Focus();
                return false;
            }
            if (txt_sale.Text == "")
            {
                MessageBox.Show("Enter the selling price", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_sale.Focus();
                return false;
            }

            return true;
        }
    }
}
