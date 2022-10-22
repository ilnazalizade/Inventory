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

namespace IMSBeta.window
{
    /// <summary>
    /// Interaction logic for win_users.xaml
    /// </summary>
    public partial class win_users : Window
    {
        public win_users()
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
            ShowUserInfo();


        }
        // Connect to Db and Show in datagrid 
        private void ShowUserInfo()
        {
            var query = database.Database.SqlQuery<Vw_Users>("Select * From Vw_Users");
            var u = query.ToList();
            DataGridUser.ItemsSource = u;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            win_add_edit_user w_u = new win_add_edit_user();
            w_u.win_type = 1;
            w_u.ShowDialog();
            ShowUserInfo();
        }

        private void Btn_EditUser_Click(object sender, RoutedEventArgs e)
        {
            object item = DataGridUser.SelectedItem;
            win_add_edit_user w_u = new win_add_edit_user();

            if (item == null)
            {
                MessageBox.Show("Please select a user first", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (item != null)
            {
                w_u.win_type = 2;

                w_u.UserID = Convert.ToInt32((DataGridUser.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                w_u.UserName = (DataGridUser.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                w_u.UserFamily = (DataGridUser.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                w_u.UserTel = (DataGridUser.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                w_u.UserAge = Convert.ToByte((DataGridUser.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
                w_u.UserGender = (DataGridUser.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;
                w_u.UserUserName = (DataGridUser.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;

                w_u.ShowDialog();
            }

            ShowUserInfo();

        }
        private void btn_ActiveUser_Click(object sender, RoutedEventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to deactive the customer?", " Active Customer", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    object item = DataGridUser.SelectedItem;
                    int UserID;
                    UserID = Convert.ToInt32((DataGridUser.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

                    User U = (from Us in database.Users where Us.UserID == UserID select Us).SingleOrDefault();

                    U.UserActive = 1;



                    database.SaveChanges();
                    MessageBox.Show("Customer successfully activated", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                    ShowUserInfo();
                }
                catch
                {
                    MessageBox.Show("There was a problem registering information", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void Btn_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to deactive the user?", "Deactive the user", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    object item = DataGridUser.SelectedItem;
                    int UserID;
                    UserID = Convert.ToInt32((DataGridUser.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

                    User U = (from User in database.Users where User.UserID == UserID select User).SingleOrDefault();

                    U.UserActive = 2;



                    database.SaveChanges();
                    MessageBox.Show("User successfully deactivated", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                    ShowUserInfo();
                }
                catch
                {
                    MessageBox.Show("There was a problem registering information", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

      
    }
}

