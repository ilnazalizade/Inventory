using DataModelLayer;
using IMSBeta.Module;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
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
    /// Interaction logic for win_AddnewTransaction.xaml
    /// </summary>
    public partial class win_AddnewTransaction : Window
    {
        public win_AddnewTransaction()
        {
            InitializeComponent();
        }

        inventorydbEntities1 database = new inventorydbEntities1();
        public string productName;
        public int productid;
        private void image3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            lbl_title.Content = this.productName;
            lbl_username.Content = PublicVariable.gUserName + " " + PublicVariable.gUserFamily;
          


            ///////////////////////
            txt_count.Focus();
            cmb_transType.Items.Add("Add to inventory");
            cmb_transType.Items.Add("Remove from inventory");
            cmb_transType.SelectedIndex = 0;

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
            if (!CheckNullable())
            {
                return;
            }
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {

                    //////////////////////////////////////////////////////////////////////
                    Inventory I = new Inventory();
                    I.InventoryDesc = txt_desc.Text.Trim();
                    I.UserID = PublicVariable.gUserId;
                    I.ProductId = this.productid;

                    ////////Checking Trans Type
                    if (cmb_transType.SelectedIndex == 0)
                    {
                        I.InventoryCount = Convert.ToInt32(txt_count.Text.Trim());
                    }
                    else if (cmb_transType.SelectedIndex == 1)
                    {
                        I.InventoryCount = -Convert.ToInt32(txt_count.Text.Trim());
                    }

                    database.Inventories.Add(I);
                    database.SaveChanges();
                    //////////////////////////////////////////////////////////////////////
                    database.Sp_Update_ProductLastStock(I.InventoryCount, this.productid);
                    database.SaveChanges();


                    TS.Complete();
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


        private bool CheckNullable()
        {
            if (txt_count.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the number", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_count.Focus();
                return false;
            }
            if (txt_desc.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the description text", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_desc.Focus();
                return false;
            }
            return true;
        }



    }
}
