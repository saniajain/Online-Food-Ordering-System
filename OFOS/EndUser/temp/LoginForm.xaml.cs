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

        //function to handle customer login --> Sania Jain
        private void AddCustomer_click(object sender, RoutedEventArgs e)
        {
            //Data maping object to our database  
            ofosDataContext clContext = new ofosDataContext();
            //check if the customer name and phone exists in db

            int? Id = null;
            //calling the stored procedure to insert customer information and generate a customer if
            clContext.Customer_Insert(textbox1.Text, textBox2.Text, ref Id);
            //MessageBox.Show("Your customer id is: " + ((int)Id).ToString());
            Menu menuWindow = new Menu(Id.ToString());
            menuWindow.ShowDialog();

        }

        //function to handle staff login (Admin and Kitchen) --> Sania Jain
        private void staffLogin_Click(object sender, RoutedEventArgs e)
        {
            ofosDataContext dc = new ofosDataContext();
            ISingleResult<StaffLogin_CheckResult> tab = dc.StaffLogin_Check(UserNameTextBox.Text, passwordTextBox.Text);
            Console.WriteLine(tab);
            foreach (StaffLogin_CheckResult val in tab)
            {
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
                        KitchenStaff.Kitchen ki = new KitchenStaff.Kitchen();
                        ki.ReloadButton.Visibility = Visibility.Hidden;
                        ki.ShowDialog();
                        //redirect to Kitchen code flow

                    }
                }
            }
        }
    }
}
