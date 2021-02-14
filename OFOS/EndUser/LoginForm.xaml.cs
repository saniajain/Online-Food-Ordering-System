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
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    /// 
    //Author of LoginForm functionalities --> Sania Jain
    public partial class LoginForm : Window
    {
        ofosDataContext dc;
        public LoginForm()
        {
            InitializeComponent();
            dc = new ofosDataContext();
        }

        //function to validate the phone number field has only numbers
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //function to handle customer login --> Sania Jain
        private void AddCustomer_click(object sender, RoutedEventArgs e)
        {
            //check if phone number and name entered is not empty. If so, show error.

            if (phoneTextbox.Text == "" || unameTextbox.Text == "")
            {
                errorLabel.Content = "Please fill in all the fields before login.";
            }
            else
            {
                //Data maping object to our database
                ofosDataContext clContext = new ofosDataContext();
                //check if the customer name and phone exists in db then return the customer id
                //if its a new customer then insert and return the customer id
                //this is checked by stored procedure Customer_Insert
                int? Id = null;
                //calling the stored procedure to insert customer information and generate a customer id
                clContext.Customer_Insert(unameTextbox.Text, phoneTextbox.Text, ref Id);
                //MessageBox.Show("Your customer id is: " + Id.ToString());
                Menu menuWindow = new Menu(Id.ToString());
                menuWindow.ShowDialog();
            }
        }

        //function to handle staff login (Admin and Kitchen) --> Sania Jain
        private void staffLogin_Click(object sender, RoutedEventArgs e)
        {
            ofosDataContext dc = new ofosDataContext();
            //fetching all values from staffLogin table
            ISingleResult<StaffLogin_CheckResult> tab = dc.StaffLogin_Check(UserNameTextBox.Text, passwordTextBox.Password);
            Console.WriteLine(tab);
            var lst = tab.ToList();
            //check if the db has no record of staff show error of incorrect credentials.
            if (lst.Count == 0)
            {
                staffLoginError.Content = "Incorrect Credentials";
            }
            else
            {
                staffLoginError.Content = "";
                foreach (var val in lst)
                {
                    //check the type of staff - Kitchen or Admin
                    if (val.Type != null)
                    {
                        if (val.Type == "Admin")
                        {
                            //redirect to Admin code flow
                            Admin.FoodItemForm fi = new Admin.FoodItemForm();
                            fi.ShowDialog();
                        }
                        else if (val.Type == "Kitchen")
                        {
                            //redirect to Kitchen code flow
                            KitchenStaff.Kitchen ki = new KitchenStaff.Kitchen();
                            ki.ReloadButton.Visibility = Visibility.Hidden;
                            ki.ShowDialog();

                        }
                    }
                }
            }
        }
    }
}
