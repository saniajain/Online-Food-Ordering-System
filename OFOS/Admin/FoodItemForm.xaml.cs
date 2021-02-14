using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for FoodItemForm.xaml
    /// </summary>
    public partial class FoodItemForm : Window
    {
        public FoodItemForm()
        {
            // Initializing and showing data in grid - By Mohsen
            InitializeComponent();
            ofosDataContext dc = new ofosDataContext();
            FoodItemGrid.ItemsSource = dc.FoodItems;

        }
        //to reload the data grid 
        private void LoadData()
        {
            // Loading data grid view                   -- BY MOHSEN
            ofosDataContext dc = new ofosDataContext();
            FoodItemGrid.ItemsSource = dc.FoodItems;

            /*DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.Header = "Food Name";
            textColumn.Binding = new Binding("FoodItemName");
            FoodItemGrid.Columns.Add(textColumn);*/

        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Insert form and show it as modal - By Mohsen
            FoodItemCU insertform = new FoodItemCU();
            if (insertform.ShowDialog() == true)  // Refresh the grid after insertion
            {
                ofosDataContext dc = new ofosDataContext();
                FoodItemGrid.ItemsSource = dc.FoodItems;
            }
            LoadData();
            //insertform.ShowDialog();

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Getting selected row
                FoodItem selectedItem = (FoodItem)FoodItemGrid.SelectedItem;

                if (selectedItem != null)
                {
                    // Create a new Insert form and show it as modal - By Mohsen
                    FoodItemCU updateform = new FoodItemCU(selectedItem.Id, selectedItem.FoodItemName, selectedItem.FoodItemImage, selectedItem.FoodItemPrice, selectedItem.FoodItemAvailable, selectedItem.FoodItemCategory);
                    if (updateform.ShowDialog() == true)  // Refresh the grid after insertion
                    {
                        ofosDataContext dc = new ofosDataContext();
                        FoodItemGrid.ItemsSource = dc.FoodItems;
                    }
                    LoadData();
                    //insertform.ShowDialog();
                }
            }
            catch { }
        }

        private void BtnInventory_Click(object sender, RoutedEventArgs e)
        {
            // Creating an instance of data context
            ofosDataContext dc = new ofosDataContext();
            // an instance of categorized food to generate a summary
            CategorizedFood categorize = new CategorizedFood();

            // Adding each item to category summary (it keeps distinct values and total only)
            foreach (FoodItem item in dc.FoodItems)
            {
                categorize.Add(new FoodCategory() { CategoryName = item.FoodItemCategory, Amount = (int)item.FoodItemAvailable });
            }

            FoodItemSummary fis = new FoodItemSummary(categorize);
            fis.ShowDialog();
        }

        private void BtnIngredients_Click(object sender, RoutedEventArgs e)
        {
            // Open an instance of Food Ingredients form  -- By Harshil Tanwar
            this.Close();
            new FoodIngredients().Show();
        }

        private void FoodItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FoodItem selectedItem = (FoodItem)FoodItemGrid.SelectedItem;
                if (selectedItem != null)
                    BtnEdit.IsEnabled = BtnDelete.IsEnabled = true;
                else
                    BtnEdit.IsEnabled = BtnDelete.IsEnabled = false;
            }
            catch
            {

            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Getting selected row
                FoodItem selectedItem = (FoodItem)FoodItemGrid.SelectedItem;

                if (selectedItem != null)
                    if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        ofosDataContext dc = new ofosDataContext();
                        // Delete the row
                        dc.FoodItemDelete(selectedItem.Id);

                        LoadData();
                    }
            }
            catch { }
        }
    }

    // This class is to keep a category of food with a total of all its availabilities  -- BY MOHSEN
    public class FoodCategory : IComparable<FoodCategory>
    {
        public string CategoryName;
        public int Amount;

        public int CompareTo(FoodCategory other)
        {
            if (this.Amount > other.Amount)
                return 1;
            else if (this.Amount < other.Amount)
                return -1;
            else
                return 0;
        }
    }

    // This is an interface for categorized food class
    public interface ICategorizedFood
    {
        List<FoodCategory> Categories { get; set; }
        void Add(FoodCategory category);
        string ToString();
    }

    public class CategorizedFood : ICategorizedFood
    {
        // Private value for storing FoodCategories
        List<FoodCategory> _Categories = new List<FoodCategory>();

        // Public method to access FoodCategories
        public List<FoodCategory> Categories {
            get { return _Categories; }
            set => throw new Exception("You can not set this property.");
        }

        // This methods tries to add a category to the list without duplicating that category name (keeping distinct values only)
        public void Add(FoodCategory category)
        {
            bool foundCategory = false;

            foreach (FoodCategory cat in _Categories)
            {
                if (cat.CategoryName == category.CategoryName)
                {
                    cat.Amount += category.Amount;        // Add the value of 
                    foundCategory = true;
                }
            }

            if (foundCategory == false)                         // If the category name was not found, add it to the list
                _Categories.Add(category);
        }


        // An override method for ToString() that makes a summary to display
        public override string ToString()
        {
            string result = "";

            foreach (FoodCategory cat in _Categories)
            {
                result += cat.CategoryName + " .............. " + cat.Amount.ToString() + "\n";
            }

            return result;
        }

        // Sorts the items in the categories list
        public void SortCategories(ListSortDirection Direction)
        {
            _Categories.Sort();         // First sort the list

            if (Direction == ListSortDirection.Descending)      // If the direction is descending, reverse the items order
                _Categories.Reverse();
        }

    }
}
