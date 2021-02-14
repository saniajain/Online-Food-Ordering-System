using System;
using System.Collections.Generic;
using System.Data.Linq;
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

/// <summary>
/// By GEET PATEL ------------------------------------------------------------
/// </summary>
namespace OFOS.EndUser
{
    /// <summary>
    /// Interaction logic for ShowReceipt.xaml
    /// </summary>
    public partial class ShowReceipt : Window
    {
        List<OrderAndTax> _ord = new List<OrderAndTax>();

        public ShowReceipt()
        {
            InitializeComponent();
        }

        public ShowReceipt(List<Menu.Order> ord, int OrderId)
        {
            InitializeComponent();

            foreach (Menu.Order item in ord)
            {
                // Calculate Tax for each item
                _ord.Add(new OrderAndTax() { 
                    ItemName = item.ItemName,
                    ItemQuantity = item.ItemQuantity,
                    ItemPrice = item.ItemPrice,
                    Tax = getTax(),
                    TotalTax = getTax() * (double)(item.ItemQuantity * item.ItemPrice)/100,
                    Total = (double)item.ItemPrice + getTax() * (double)(item.ItemQuantity * item.ItemPrice) / 100
                });
            }

            // Setting grid data source
            GridOrders.ItemsSource = _ord;

            // Adding Total - the color of this row will be changed based on ItemName. if it is empty, background color changes automatically
            _ord.Add(new OrderAndTax()
            {
                ItemName = "",
                ItemQuantity = _ord.Sum(l => l.ItemQuantity),
                ItemPrice = _ord.Sum(l => l.ItemPrice),
                Tax=null,
                TotalTax = _ord.Sum(l => l.TotalTax),
                Total = _ord.Sum(l => l.Total)
            });

            LblOrderId.Content = "Thank you for your purchase! Order Id is: " + OrderId.ToString();

        }

        // Returns the Tax  ----- BY GEET
        private double getTax()
        {
            // creating an instance of data context
            ofosDataContext dc = new ofosDataContext();

            // run the GetTax stored procedure and store the result in a list
            List<GetTaxResult> tr = dc.GetTax("Ontario").ToList<GetTaxResult>();

            // Check if there is only 1 row of data, return the tax, otherwise return 0
            if (tr.Count == 1)
                return (double)tr[0].Tax;
            else
                return 0;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class OrderAndTax: Menu.Order
    {
        public double? Tax { get; set; }
        public double TotalTax { get; set; }
        public double Total { get; set; }
    }

}
