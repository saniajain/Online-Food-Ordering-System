using Microsoft.Win32;
using OFOS;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
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
    /// Interaction logic for FoodItemCU.xaml
    /// </summary>
    public partial class FoodItemCU : Window
    {

        private bool _isEditMode = false;

        public FoodItemCU()
        {
            InitializeComponent();
        }
        
        public FoodItemCU(int Id, string Name, string Image, decimal? Price, int? Available, string Category)
        {
            this._isEditMode = true;

            InitializeComponent();

            txtId.Text = Id.ToString();
            txtName.Text = Name;
            txtImage.Text = Image;
            ComboCategory.SelectedValue = Category;
            txtPrice.Text = Price.ToString();
            txtAvailability.Text = Available.ToString();

            Uri fileUri = new Uri(Image);
            ImageBox.Source = new BitmapImage(fileUri);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Get user confirmation before closing the dialogue - By Mohsen
            MessageBoxResult rsltMessageBox = MessageBox.Show("Do you want to cancel? all changes will be lost.", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (rsltMessageBox == MessageBoxResult.Yes)
                this.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)      // By MOHSEN
        {
            try
            {
                if (!_isEditMode)       // INSERT COMMAND
                {
                    // Executing INSERT stored procedure -- By Mohsen
                    ofosDataContext dc = new ofosDataContext();
                    dc.FoodItemInsert(txtName.Text, txtImage.Text, Convert.ToDecimal(txtPrice.Text), Convert.ToInt32(txtAvailability.Text), ComboCategory.SelectedValue.ToString());

                    MessageBox.Show("Record inserted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else                    // UPDATE COMMAND
                {
                    // Executing UPDATE stored procedure -- By Mohsen
                    ofosDataContext dc = new ofosDataContext();
                    dc.FoodItemUpdate(Convert.ToInt32(txtId.Text), txtName.Text, txtImage.Text, Convert.ToDecimal(txtPrice.Text), Convert.ToInt32(txtAvailability.Text), ComboCategory.SelectedValue.ToString());

                    MessageBox.Show("Record Updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Please check your entries and try again!", "Validation problem", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)     // By MOHSEN
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtImage.Text = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                ImageBox.Source = new BitmapImage(fileUri);
            }
        }
    }
}
