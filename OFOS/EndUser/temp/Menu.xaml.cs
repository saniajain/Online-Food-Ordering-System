using System;
using System.Collections;
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

namespace OFOS.EndUser
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    /// 
    //Author of Menu functionalities --> Sania Jain
    public partial class Menu : Window
    {
        int CustomerId;
        decimal totalCost = 0;
        //Initial loading and displaying the customer id assigned by database--> Sania Jain
        public Menu(String Id)
        {
            CustomerId = int.Parse(Id);
            InitializeComponent();
            CustomerLabel.Content = "Welcome to the shop, your customer Id is:" + Id;
        }


        //class to define item name and price to bind to the xaml elements --> Sania Jain
        public class ItemPriceClass
        {
            public string ItemName { get; set; }
            public decimal ItemPrice { get; set; }

        }

        public class Order
        {
            public string ItemName { get; set; }

            public decimal ItemPrice { get; set; }

            public int ItemQuantity { get; set; }

        }

        List<Order> ord = new List<Order>();
        //function to view menu items from databse for coffee --> Sania Jain
        public void coffeeSelected(object sender, RoutedEventArgs e)
        {
            ofosDataContext dc = new ofosDataContext();
            CheckBox chkBox = (CheckBox)sender;
            if (chkBox.IsChecked == true)
            {
                coffeeListBox.IsEnabled = true;

                var Coffee = new List<ItemPriceClass>();

                ISingleResult<FoodItem_FetchResult> Coffeeresults = dc.FoodItem_Fetch(chkBox.Content.ToString());
                foreach(FoodItem_FetchResult res in Coffeeresults)
                {
                    Console.WriteLine(res);
                    Coffee.Add(new ItemPriceClass { ItemName = res.FoodItemName, ItemPrice = (decimal)res.FoodItemPrice});
                }
                coffeeListBox.ItemsSource = Coffee;
            }
            else
            {
                coffeeListBox.IsEnabled = false;
                OrderListView.Items.Clear();
            }
        }

        //function to handle checkbox selection for coffee --> Sania Jain
        public void coffeeChecked(object sender, RoutedEventArgs e)
        {
            CheckBox coffeeType = (CheckBox)sender;
            if(coffeeType.IsChecked == true)
            {
                ord.Add(new Order { ItemName = coffeeType.Content.ToString(),
                                    ItemPrice = (decimal)coffeeType.Tag,
                                    ItemQuantity = 1
                });
                //OrderListView.Items.Add("coffee Type: " + coffeeType.Content + "Price" + coffeeType.Tag);
                
            }
            else
            {
                foreach(Order item in ord)
                {
                    if(item.ItemName == coffeeType.Content.ToString())
                    {
                        int i = ord.IndexOf(item);
                        ord.RemoveAt(i);
                        break;
                    }
                }
                //int idx = OrderListView.Items.IndexOf("coffee Type: " + coffeeType.Content + "Price" + coffeeType.Tag);
                //OrderListView.Items.RemoveAt(idx);
            }
        }

        public void coffeeQuantityChanged(object sender, TextChangedEventArgs e)
        {
            TextBox coffeeQty = (TextBox)sender;
            foreach (Order item in ord)
            {
                if (item.ItemName == coffeeQty.Tag.ToString())
                {
                    item.ItemQuantity = coffeeQty.Text != "" ? int.Parse(coffeeQty.Text) : 0;
                    Console.WriteLine(item.ItemName, item.ItemPrice, item.ItemQuantity);
                    break;
                }
            }
        }

        //function to view menu items from databse for cake --> Sania Jain
        public void CakeSelected(object sender, RoutedEventArgs e)
        {
            ofosDataContext dc = new ofosDataContext();
            CheckBox chkBox = (CheckBox)sender;
            if (chkBox.IsChecked == true)
            {

                cakeListBox.IsEnabled = true;

                var Cakes = new List<ItemPriceClass>();
                ISingleResult<FoodItem_FetchResult> Coffeeresults = dc.FoodItem_Fetch(chkBox.Content.ToString());
                foreach (FoodItem_FetchResult res in Coffeeresults)
                {
                    Console.WriteLine(res);
                    Cakes.Add(new ItemPriceClass { ItemName = res.FoodItemName, ItemPrice = (decimal)res.FoodItemPrice });
                }
                cakeListBox.ItemsSource = Cakes;
            }
            else
            {
                cakeListBox.IsEnabled = false;
            }
        }

        //function to handle checkbox selection for cake --> Sania Jain
        public void CakeChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cakeType = (CheckBox)sender;
            if (cakeType.IsChecked == true)
            {
                ord.Add(new Order
                {
                    ItemName = cakeType.Content.ToString(),
                    ItemPrice = (decimal)cakeType.Tag,
                    ItemQuantity = 1
                });
                //OrderListView.Items.Add("Cake Type: " + cakeType.Content + "Price" + cakeType.Tag);
            }
            else
            {
                foreach (Order item in ord)
                {
                    if (item.ItemName == cakeType.Content.ToString())
                    {
                        int i = ord.IndexOf(item);
                        ord.RemoveAt(i);
                        break;
                    }
                }
                //int idx = OrderListView.Items.IndexOf("Cake Type: " + cakeType.Content + "Price" + cakeType.Tag);
                //OrderListView.Items.RemoveAt(idx);
            }
        }


        //function to view menu items from databse for cake --> Sania Jain
        public void CookieSelected(object sender, RoutedEventArgs e)
        {
            ofosDataContext dc = new ofosDataContext();
            CheckBox chkBox = (CheckBox)sender;
            if (chkBox.IsChecked == true)
            {

                cookieListBox.IsEnabled = true;

                var Cookies = new List<ItemPriceClass>();

                ISingleResult<FoodItem_FetchResult> Coffeeresults = dc.FoodItem_Fetch(chkBox.Content.ToString());
                foreach (FoodItem_FetchResult res in Coffeeresults)
                {
                    Console.WriteLine(res);
                    Cookies.Add(new ItemPriceClass { ItemName = res.FoodItemName, ItemPrice = (decimal)res.FoodItemPrice });
                    cookieListBox.ItemsSource = Cookies;
                }
            }
            else
            {
                cookieListBox.IsEnabled = false;
            }
        }

        //function to checkbox selection for cookie type --> Sania Jain
        public void CookieChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cookieType = (CheckBox)sender;
            if (cookieType.IsChecked == true)
            {
                ord.Add(new Order
                {
                    ItemName = cookieType.Content.ToString(),
                    ItemPrice = (decimal)cookieType.Tag,
                    ItemQuantity = 1
                });
                //OrderListView.Items.Add("Cookie Type: " + cookieType.Content + "Price" + cookieType.Tag);
            }
            else
            {
                foreach (Order item in ord)
                {
                    if (item.ItemName == cookieType.Content.ToString())
                    {
                        int i = ord.IndexOf(item);
                        ord.RemoveAt(i);
                        break;
                    }
                }
                //int idx = OrderListView.Items.IndexOf("Cookie Type: " + cookieType.Content + "Price" + cookieType.Tag);
                //OrderListView.Items.RemoveAt(idx);
            }
        }

        //function to compute the total on checkout --> Sania Jain
        public void checkoutClick(object sender, RoutedEventArgs e)
        {
            String ordr = "";

            ofosDataContext dc = new ofosDataContext();
            //update foodItem availability
            foreach (Order o in ord)
            {
                int updatedQty = 0;
                //update foodItem table with availability
                ISingleResult<FoodItemAvailable_CheckResult> available = dc.FoodItemAvailable_Check(o.ItemName);
                foreach (FoodItemAvailable_CheckResult res in available)
                {
                    int availableQty = (int)res.FoodItemAvailable;
                    updatedQty = availableQty - o.ItemQuantity;
                }
                    dc.FoodItemAvailability_Update(o.ItemName, updatedQty);

                //insert the order to order table
                string orderStr = o.ItemName + "-" + o.ItemQuantity;
                ordr += orderStr;
            }
            int? orderId = null;
            string Status = "placed";
            dc.Order_Insert(CustomerId, ordr, totalCost, Status, ref orderId);

            // Showing Receipt -- BY HARSHIL
            ShowReceipt receipt = new ShowReceipt(ord, (int)orderId);
            receipt.ShowDialog();

            //MessageBox.Show("Orderid: " + orderId);
            this.Close();
            OrderDisplay orderWindow = new OrderDisplay(orderId.ToString());
            orderWindow.ShowDialog();
        }


        //function to manage cart button and add items to cart --> Sania Jain
        public void ManageCart( object sender, RoutedEventArgs e)
        {
            ofosDataContext dc = new ofosDataContext();
            bool error = false;
            totalCost = 0;
            List<string> itm = new List<string>();
            foreach (Order o in ord)
            {
                //fetch availability values from db before checkout
                ISingleResult<FoodItemAvailable_CheckResult> itemAvailableRes = dc.FoodItemAvailable_Check(o.ItemName.ToString());
                foreach (FoodItemAvailable_CheckResult res in itemAvailableRes)
                {
                    Console.WriteLine(res);
                    if (!error)
                    {
                        if (res.FoodItemAvailable < o.ItemQuantity)
                        {
                            errorLabel.Content = "Added quantity of " + o.ItemName + " is not available.";
                            error = true;
                            if(itm.Count > 0)
                            {
                                itm.Clear();
                            }
                            break;
                        }
                        else
                        {
                            errorLabel.Content = "";
                            itm.Add(o.ItemName + "---" + o.ItemPrice + "---" + o.ItemQuantity);
                            //totalCost += decimal.Parse(o.ItemPrice * o.ItemQuantity);
                            totalCost += Decimal.Multiply(o.ItemPrice, o.ItemQuantity);
                        }
                    }
                }
            }

            foreach (string s in itm)
            {
                OrderListView.Items.Add(s);
            }
            checkout.IsEnabled = true;
        }
    }
}
