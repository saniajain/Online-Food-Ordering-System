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

namespace OFOS.Admin
{
    /// <summary>
    /// BY HARSHIL TANWAR
    /// </summary>
    public partial class FoodIngredientsCU : Window
    {
        public FoodIngredientsCU()
        {
            InitializeComponent();
        }

        public FoodIngredientsCU(int foodItemId, string foodName)
        {
            InitializeComponent();

            // Setting text boxes' values in Insert mode
            txtFoodItemId.Text = foodItemId.ToString();
            txtFoodName.Text = foodName;

        }

        public FoodIngredientsCU(GetIngredientsResult foodIngredient, string foodName)
        {
            InitializeComponent();

            // Setting text boxes' values in Edit mode
            txtId.Text = foodIngredient.Id.ToString();
            txtFoodItemId.Text = foodIngredient.FoodItemId.ToString();
            txtFoodName.Text = foodName;
            txtIngredient.Text = foodIngredient.Ingredient;
            txtAmount.Text = foodIngredient.Amount.ToString();
            ComboUnit.SelectedValue = foodIngredient.Unit;

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Executing UPDATE stored procedure 
                ofosDataContext dc = new ofosDataContext();
                int id = txtId.Text!="" && Convert.ToInt32(txtId.Text) > 0 ? Convert.ToInt32(txtId.Text) : -1;
                dc.FoodIngredientsInsertUpdate(id, Convert.ToInt32(txtFoodItemId.Text), txtIngredient.Text, Convert.ToDouble(txtAmount.Text), ComboUnit.SelectedValue.ToString());

                MessageBox.Show("Operation successfull!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please check your entries and try again!", "Validation problem", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
