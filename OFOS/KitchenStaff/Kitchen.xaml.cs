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

namespace OFOS.KitchenStaff
{
    /// <summary>
    /// Interaction logic for Kitchen.xaml
    /// </summary>
    //Author -->Shravani
    public partial class Kitchen : Window
    {
        ofosDataContext dc = new ofosDataContext();

        public Kitchen()
        {
            InitializeComponent();
             KitchenGrid.ItemsSource = dc.Orders;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Aquamarine;
            if (KitchenGrid.SelectedItem as Order != null)
            {
                //Creating the object
                Order obj = KitchenGrid.SelectedItem as Order;
                //create reference of object

                var Order = (from ord in dc.Orders where ord.OrderId == obj.OrderId select e).Single();

                if (obj.Status == "placed")
                {
                    obj.Status = "prepared";
                }
                else if (obj.Status == "prepared")
                {
                    obj.Status = "delivered";
                }
                else
                {
                    MessageBox.Show("Ordered Delivered", "orderstatus");
                }
            }
            dc.SubmitChanges();
        }
        private void LoadData()
        {
            dc = new ofosDataContext();
            KitchenGrid.ItemsSource = dc.Orders;
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Aquamarine;
            Order obj = KitchenGrid.SelectedItem as Order;
            var Order = (from ord in dc.Orders where ord.OrderId == obj.OrderId select e).Single();//create refernce of object
            if (obj.Status == "delivered")
            {
                //deleting the record
                dc.Orders.DeleteOnSubmit(obj);
                MessageBox.Show("Order Deleted Successfully", "orderstatus");
            }
            else if (obj.Status == "placed || prepared ")
            {
                MessageBox.Show("Order Cannot be Deleted ", "orderstatus");
            }
            else
            {
                MessageBox.Show("Order Cannot be Deleted ", "orderstatus");
            }

            dc.SubmitChanges();//commit data
            LoadData();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Aquamarine;
            this.Close();
        }

        private void OrdersPlacedbutton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Aquamarine;
            //getting orders from orders table by status 'placed'
            KitchenGrid.ItemsSource = from ord in dc.Orders where ord.Status == "placed" select ord;
            ReloadButton.Visibility = Visibility.Visible;
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Aquamarine;
            KitchenGrid.ItemsSource = from ord in dc.Orders select ord;
        }
    }
}


