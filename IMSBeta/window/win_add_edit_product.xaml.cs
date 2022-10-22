using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using DataModelLayer;

namespace IMSBeta.window
{
    /// <summary>
    /// Interaction logic for win_add_edit_product.xaml
    /// </summary>
    public partial class win_add_edit_product : Window
    {
        public win_add_edit_product()
        {
            InitializeComponent();
        }
        string strName, imageName;
        inventorydbEntities1 database = new inventorydbEntities1();

        public byte win_Type { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_username.Content = PublicVariable.gUserName + " " + PublicVariable.gUserFamily;
            switch (win_Type)
            {
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        txt_productname.Text = ProductName;
                        txt_desc.Text = ProductDesc;
                        /////////////////////////
                       ShowImage();
                        /////////////////////////
                        break;
                    }
            }

        }
        private void ShowImage()
        {
            /////Create Image in DB
            var query = from P in database.Products where P.ProductId == ProductId select P;
            var result = query.ToList();

            if (result[0].ProductImage != null)
            {
                byte[] ImageArray = (byte[])result[0].ProductImage;
                MemoryStream stream = new MemoryStream();
                stream.Write(ImageArray, 0, ImageArray.Length);

                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                image_product.Source = bi;
            }
        }

        private bool CheckNullable()
        {
            if (txt_productname.Text == "")
            {
                MessageBox.Show("Enter the product name", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_productname.Focus();
                return false;
            }
            if (txt_desc.Text == "")
            {
                MessageBox.Show("Enter the product description", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_desc.Focus();
                return false;
            }


            return true;
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNullable())
            {
                return;
            }


            try
            {

                switch (win_Type)
                {
                    case 1:
                        {
                            if (imageName != null)
                            {
                                //// change type of image to String
                                FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                                byte[] imgByteArr = new byte[fs.Length];
                                fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                                fs.Close();


                                /////////////Save in DB
                                database.Sp_ins_product(txt_productname.Text.Trim(), txt_desc.Text.Trim(), imgByteArr, PublicVariable.gUserId);
                                database.SaveChanges();
                                MessageBox.Show("Information saved successfully", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Please select an image for the new item", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (imageName != null)
                            {
                                //Image type to String
                                FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                                byte[] imgByteArr = new byte[fs.Length];
                                fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                                fs.Close();


                                database.Sp_Update_Product(ProductId, txt_productname.Text.Trim(), txt_desc.Text.Trim(), imgByteArr);
                                database.SaveChanges();
                                MessageBox.Show("Information edited successfully", "Save error", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                           




                            break;
                        }

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Close();
            }
        }



        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
        this.Close();   
        }

        private void image_close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void image_product_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                FileDialog filedlg = new OpenFileDialog();
                filedlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                filedlg.ShowDialog();
                {
                    strName = filedlg.SafeFileName;
                    imageName = filedlg.FileName;

                    if (imageName != "")
                    {
                        ImageSourceConverter isc = new ImageSourceConverter();
                        image_product.SetValue(System.Windows.Controls.Image.SourceProperty, isc.ConvertFromString(imageName));
                    }
                }
                filedlg = null;
            }
            catch
            {
                MessageBox.Show("An error has occurred, please check", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
