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
    public partial class FoodIngredients : Window
    {
        public FoodIngredients()
        {
            InitializeComponent();

            ofosDataContext dc = new ofosDataContext();
            GridFoodItem.ItemsSource = dc.FoodItems;

        }

        private void GridFoodItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FoodItem selectedItem = (FoodItem)GridFoodItem.SelectedItem;
                if (selectedItem != null)
                {
                    // create data context
                    ofosDataContext dc = new ofosDataContext();

                    // get selected id
                    int selectedIndex = ((FoodItem)GridFoodItem.SelectedItem).Id;

                    // fill the ingredients grid
                    GridIngredients.ItemsSource = dc.GetIngredients(selectedIndex);

                    // Enabling New button
                    btnNew.IsEnabled = true;
                }
                else
                    // Disabling New button
                    btnNew.IsEnabled = false;
            }
            catch
            {

            }

        }

        private void GridIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Getting selected row
                GetIngredientsResult selectedItem = (GetIngredientsResult)GridIngredients.SelectedItem;

                // Disable the button if no row is selected
                if (selectedItem == null)
                    BtnEdit.IsEnabled = BtnDelete.IsEnabled = false;
                else
                    BtnEdit.IsEnabled = BtnDelete.IsEnabled = true;
            }
            catch
            { 
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            // Getting selected row
            FoodItem selectedItem = (FoodItem)GridFoodItem.SelectedItem;

            if (selectedItem != null)
            {
                // Open Insert Form
                new FoodIngredientsCU(((FoodItem)GridFoodItem.SelectedItem).Id, ((FoodItem)GridFoodItem.SelectedItem).FoodItemName).ShowDialog();

                // Refresh the grid
                // create data context
                ofosDataContext dc = new ofosDataContext();

                // get selected id
                int selectedIndex = ((FoodItem)GridFoodItem.SelectedItem).Id;

                // fill the ingredients grid
                GridIngredients.ItemsSource = dc.GetIngredients(selectedIndex);

            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Getting selected row
                GetIngredientsResult selectedItem = (GetIngredientsResult)GridIngredients.SelectedItem;

                if (selectedItem != null)
                {
                    FoodIngredientsCU fi = new FoodIngredientsCU((GetIngredientsResult)GridIngredients.SelectedItem, ((FoodItem)GridFoodItem.SelectedItem).FoodItemName);
                    // Open Update Form
                    fi.ShowDialog();

                    // Refresh the grid
                    // create data context
                    ofosDataContext dc = new ofosDataContext();

                    // get selected id
                    int selectedIndex = ((FoodItem)GridFoodItem.SelectedItem).Id;

                    // fill the ingredients grid
                    GridIngredients.ItemsSource = dc.GetIngredients(selectedIndex);

                }
            }
            catch { }

        }

        private void BtnFoodItems_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new FoodItemForm().Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Getting selected row
                GetIngredientsResult selectedItem = (GetIngredientsResult)GridIngredients.SelectedItem;

                if (selectedItem != null)
                    if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        ofosDataContext dc = new ofosDataContext();
                        // Delete the row
                        dc.FoodIngredientDelete(selectedItem.Id);

                        // Refresh the grid after Delete                     
                        // get selected id
                        int selectedIndex = ((FoodItem)GridFoodItem.SelectedItem).Id;

                        // fill the ingredients grid
                        GridIngredients.ItemsSource = dc.GetIngredients(selectedIndex);
                    }
            }
            catch { }
        }
    }

}
