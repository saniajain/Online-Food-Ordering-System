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
using System.Text.RegularExpressions;

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
            CustomerLabel.Content = "Welcome to the shop, your customer Id is: " + Id;
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

        List<CheckBox> selectedCategory = new List<CheckBox>();

        //function to validate that quantity textboxes only takes digits --> Sania Jain
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //function to clear the cart --> Sania Jain
        public void clearCartClick(object sender, RoutedEventArgs e)
        {
            ord.Clear();
            OrderListView.Items.Clear();
            //fetching all the checkboxes for menuGrid with isChecked as true
            var checkedList = this.MenuGrid.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
            //unchecking the checkboxes and resetting values
            foreach (CheckBox cb in checkedList)
            {
                cb.IsChecked = false;
            }

            clearCart.IsEnabled = false;
            checkout.IsEnabled = false;
            addToCart.IsEnabled = false;
        }

        //function to enable add to cart button only if user has selected atleast one category --> Sania Jain
        public void enableCart()
        {
            if (selectedCategory.Count == 0)
            {
                addToCart.IsEnabled = false;
            }
            else
            {
                addToCart.IsEnabled = true;
            }
        }

        //function to view menu items from databse for coffee --> Sania Jain
        public void coffeeSelected(object sender, RoutedEventArgs e)
        {
            ofosDataContext dc = new ofosDataContext();
            CheckBox chkBox = (CheckBox)sender;
            if (chkBox.IsChecked == true)
            {
                //enabling the coffeList element
                coffeeListBox.IsEnabled = true;
                selectedCategory.Add(chkBox);
                var Coffee = new List<ItemPriceClass>();

                //fetching coffee options from FoodItems table using storedProcedure FoodItem_Fetch
                ISingleResult<FoodItem_FetchResult> Coffeeresults = dc.FoodItem_Fetch(chkBox.Content.ToString());

                //add each coffee item values in ItemPrice list
                foreach (FoodItem_FetchResult res in Coffeeresults)
                {
                    Console.WriteLine(res);
                    Coffee.Add(new ItemPriceClass { ItemName = res.FoodItemName, ItemPrice = (decimal)res.FoodItemPrice });
                }
                //populate the listBox view with fetched coffee items
                coffeeListBox.ItemsSource = Coffee;
            }
            else
            {
                //remove all coffee items from order and disable the listView
                selectedCategory.Remove(chkBox);
                coffeeListBox.IsEnabled = false;
                OrderListView.Items.Clear();
            }
            //calling function to enable the addtocart button
            enableCart();
        }

        //function to handle checkbox selection for item selection --> Sania Jain
        public void itemChecked(object sender, RoutedEventArgs e)
        {
            CheckBox itemType = (CheckBox)sender;
            //check if the item is checked then add it to the order class
            if (itemType.IsChecked == true)
            {
                ord.Add(new Order
                {
                    ItemName = itemType.Content.ToString(),
                    ItemPrice = (decimal)itemType.Tag,
                    ItemQuantity = 1
                });
            }
            else
            {
                //if item is unchecked, remove it from the order class
                foreach (Order item in ord)
                {
                    if (item.ItemName == itemType.Content.ToString())
                    {
                        int i = ord.IndexOf(item);
                        ord.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        //function to handle the item quantity --> Sania Jain
        public void itemQuantityChanged(object sender, TextChangedEventArgs e)
        {
            TextBox itemQty = (TextBox)sender;
            //update the order object with new item quantity against the item name
            foreach (Order item in ord)
            {
                if (item.ItemName == itemQty.Tag.ToString())
                {
                    item.ItemQuantity = itemQty.Text != "" ? int.Parse(itemQty.Text) : 0;
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
                selectedCategory.Add(chkBox);

                var Cakes = new List<ItemPriceClass>();
                ISingleResult<FoodItem_FetchResult> Cakeresults = dc.FoodItem_Fetch(chkBox.Content.ToString());
                foreach (FoodItem_FetchResult res in Cakeresults)
                {
                    Console.WriteLine(res);
                    Cakes.Add(new ItemPriceClass { ItemName = res.FoodItemName, ItemPrice = (decimal)res.FoodItemPrice });
                }
                cakeListBox.ItemsSource = Cakes;
            }
            else
            {
                cakeListBox.IsEnabled = false;
                selectedCategory.Remove(chkBox);
            }
            enableCart();
        }


        //function to view menu items from databse for cake --> Sania Jain
        public void CookieSelected(object sender, RoutedEventArgs e)
        {
            ofosDataContext dc = new ofosDataContext();
            CheckBox chkBox = (CheckBox)sender;
            if (chkBox.IsChecked == true)
            {

                cookieListBox.IsEnabled = true;
                selectedCategory.Add(chkBox);

                var Cookies = new List<ItemPriceClass>();

                ISingleResult<FoodItem_FetchResult> Cookieresults = dc.FoodItem_Fetch(chkBox.Content.ToString());
                foreach (FoodItem_FetchResult res in Cookieresults)
                {
                    Console.WriteLine(res);
                    Cookies.Add(new ItemPriceClass { ItemName = res.FoodItemName, ItemPrice = (decimal)res.FoodItemPrice });
                    cookieListBox.ItemsSource = Cookies;
                }
            }
            else
            {
                selectedCategory.Remove(chkBox);
                cookieListBox.IsEnabled = false;
            }
            enableCart();
        }

        //function to compute the total on checkout --> Sania Jain
        public void checkoutClick(object sender, RoutedEventArgs e)
        {
            String ordr = "";

            ofosDataContext dc = new ofosDataContext();
            foreach (Order o in ord)
            {
                int updatedQty = 0;
                //update foodItem table with availability
                ISingleResult<FoodItemAvailable_CheckResult> available = dc.FoodItemAvailable_Check(o.ItemName);
                foreach (FoodItemAvailable_CheckResult res in available)
                {
                    //calculate the updated availability
                    int availableQty = (int)res.FoodItemAvailable;
                    updatedQty = availableQty - o.ItemQuantity;
                }
                //calling the stored procedure to update the food item availability
                dc.FoodItemAvailability_Update(o.ItemName, updatedQty);

                //creating order string for order table
                string orderStr = o.ItemName + "-" + o.ItemQuantity + " ";
                ordr += orderStr;
            }
            int? orderId = null;
            string Status = "placed";
            //calling stored procedure to insert the order in the order table and get order id
            dc.Order_Insert(CustomerId, ordr, totalCost, Status, ref orderId);
            //display the order id
            //MessageBox.Show("Orderid: " + orderId);

            // Showing Receipt -- BY Geet
            ShowReceipt receipt = new ShowReceipt(ord, (int)orderId);
            receipt.ShowDialog();

            this.Close();
            OrderDisplay orderWindow = new OrderDisplay(orderId.ToString());
            orderWindow.ShowDialog();
        }


        //function to manage cart button and add items to cart --> Sania Jain
        public void ManageCart(object sender, RoutedEventArgs e)
        {
            ofosDataContext dc = new ofosDataContext();
            bool error = false;
            totalCost = 0;
            List<string> itm = new List<string>();
            //check if the order object is having values then populate the cart
            if (ord.Count != 0)
            {
                OrderListView.Items.Clear();
                foreach (Order o in ord)
                {
                    //fetch item availability values from db before checkout
                    ISingleResult<FoodItemAvailable_CheckResult> itemAvailableRes = dc.FoodItemAvailable_Check(o.ItemName.ToString());
                    foreach (FoodItemAvailable_CheckResult res in itemAvailableRes)
                    {
                        Console.WriteLine(res);
                        if (!error)
                        {
                            //check if quantity added by user is not available, prompt to modify order
                            if (res.FoodItemAvailable < o.ItemQuantity)
                            {
                                errorLabel.Content = "Added quantity of " + o.ItemName + " is not available.";
                                error = true;
                                if (itm.Count > 0)
                                {
                                    itm.Clear();
                                }
                                break;
                            }
                            else
                            {
                                //if everything is available, populate the cart
                                errorLabel.Content = "";
                                error = false;
                                itm.Add(o.ItemName + "---" + o.ItemPrice + "---" + o.ItemQuantity);
                                //calculating the total cost of the order
                                totalCost += Decimal.Multiply(o.ItemPrice, o.ItemQuantity);
                            }
                        }
                    }
                }

                foreach (string s in itm)
                {
                    //displaying order items in the cart box
                    OrderListView.Items.Add(s);
                }
                checkout.IsEnabled = true;
                clearCart.IsEnabled = true;
            }
            //if nothing is selected, ask user to pick food items first
            else
            {
                errorLabel.Content = "Please add atleast one food item to cart for checkout.";
            }
        }
    }
}
