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
    /// Interaction logic for FoodItemSummary.xaml
    /// </summary>
    public partial class FoodItemSummary : Window
    {
        private CategorizedFood categorized;
        public FoodItemSummary()
        {
            InitializeComponent();
        }

        // An Overload for class constructor
        public FoodItemSummary(CategorizedFood Summary)
        {
            InitializeComponent();
            
            // Keeping for further use
            categorized = Summary;

            // Showing the summary inside the text box
            TxtSummary.Text = Summary.ToString();
        }

        private void BtnSortAscending_Click(object sender, RoutedEventArgs e)
        {
            // Sorting the list
            categorized.SortCategories(ListSortDirection.Ascending);

            // Showing the summary inside the text box
            TxtSummary.Text = categorized.ToString();
        }

        private void BtnSortDescending_Click(object sender, RoutedEventArgs e)
        {
            // Sorting the list
            categorized.SortCategories(ListSortDirection.Descending);

            // Showing the summary inside the text box
            TxtSummary.Text = categorized.ToString();
        }
    }
}
