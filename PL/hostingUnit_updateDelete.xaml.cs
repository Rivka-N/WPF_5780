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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

using BE;
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for hostingUnit_updateDelete.xaml
    /// </summary>
    /// 
    
    public partial class hostingUnit_updateDelete : Page
    {
        IBL myBl;
        

        public hostingUnit_updateDelete()
        {
            InitializeComponent();
            

        }

        private void hostingUnitListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dalete_click(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void hostingUnitDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string message = "Are you sure you want to delete this unit?";
            string caption = "Confirmation";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
            {
                // OK code here
                int code=0;
                myBl.deleteUnit(code);
            }
            else
            {
                // Cancel code here  
            }

        }
        #region textBox
        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            
        }

        private void hostingUnitKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        private void hostingUnitNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(hostingUnitNameTextBox.Text, @"^[a-zA-Z]+$"))
            {
                g1.LastName = hostingUnitNameTextBox.Text;//needs to know how i get the hostingunit
                hostingUnitNameTextBox.Background = Brushes.White;

            }
            else
            {
                hostingUnitNameTextBox.Text = "";
                hostingUnitNameTextBox.Background = Brushes.Red;
            }
        }
        #endregion

        #region comboBox
        private void hostingUnitTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

             //(Enums.HostingUnitType)(hostingUnitTypeComboBox.SelectedIndex);
        }

        private void mealComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //(Enums.MealType)(mealComboBox.SelectedIndex)
        }
        #endregion

        #region numbers
        private void numChildrenTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int text = 0;
            if (Int32.TryParse(numChildrenTextBox.Text, out text))
            {
                if (text < 0)
                {
                    numChildrenTextBox.Background = Brushes.OrangeRed;
                    numChildrenTextBox.Text = "";
                }
                else
                {
                    g1.NumAdult = text;
                    numChildrenTextBox.Background = Brushes.White;
                }
            }
            else
            {

                numChildrenTextBox.Background = Brushes.OrangeRed;
                numChildrenTextBox.Text = "";
            }
        }

        private void numAdultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int text = 0;
            if (Int32.TryParse(numAdultTextBox.Text, out text))
            {
                if (text < 0)
                {
                    numAdultTextBox.Background = Brushes.OrangeRed;
                    numAdultTextBox.Text = "";
                }
                else
                {
                    g1.NumAdult = text;
                    numAdultTextBox.Background = Brushes.White;
                }
            }
            else
            {

                numAdultTextBox.Background = Brushes.OrangeRed;
                numAdultTextBox.Text = "";
            }
        }
        #endregion

        #region checkBox
        private void jacuzziCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (jacuzziCheckBox.IsChecked == true)//changed to true
                g1.Jacuzzi = Enums.Preference.Yes;
            else
            {
                if (jacuzziCheckBox.IsChecked == false)//changed to false
                    g1.Jacuzzi = Enums.Preference.No;
                else
                    g1.Jacuzzi = Enums.Preference.Maybe;//otherwise it's the third state
            }
        }

        private void poolCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (poolCheckBox.IsChecked == true)//changed to true
                g1.Pool = Enums.Preference.Yes;
            else
            {
                if (poolCheckBox.IsChecked == false)//changed to false
                    g1.Pool = Enums.Preference.No;
                else
                    g1.Pool = Enums.Preference.Maybe;//otherwise it's the third state
            }
        }

        private void gardenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (gardenCheckBox.IsChecked == true)//changed to true
                g1.Garden = Enums.Preference.Yes;
            else
            {
                if (gardenCheckBox.IsChecked == false)//changed to false
                    g1.Garden = Enums.Preference.No;
                else
                    g1.Garden = Enums.Preference.Maybe;//otherwise it's the third state
            }
        }

        #endregion //not finish
    }
}
