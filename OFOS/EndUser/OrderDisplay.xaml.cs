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

namespace OFOS.EndUser
{
    /// <summary>
    /// Interaction logic for OrderDisplay.xaml
    /// </summary>
    /// 
    //Author Sania Jain
    public partial class OrderDisplay : Window
    {
        //function to display order placed --> Sania Jain
        public OrderDisplay(string orderId)
        {

            InitializeComponent();
            orderIdLabel.Content = "Your order has been placed! Order Id is: " + orderId;
            ofosDataContext dc = new ofosDataContext();
            OrderDisplayGrid.ItemsSource = dc.Orders;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Aquamarine;
            this.Close();
        }
    }
}
