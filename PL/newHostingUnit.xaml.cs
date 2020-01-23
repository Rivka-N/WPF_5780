//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using System.Text.RegularExpressions;
//using BE;
//using BL;


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
using System.Text.RegularExpressions;
using BE;
using BL;


namespace PL
{
    /// <summary>
    /// Interaction logic for newHostingUnit.xaml
    /// </summary>
    public partial class newHostingUnit : Window
    {
        public IBL bL;
        public HostingUnit hosting = new HostingUnit();


        public newHostingUnit()
        {

            InitializeComponent();
            bL = factoryBL.getBL();
            cb_hostingUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();
            cb_meal.ItemsSource = Enum.GetValues(typeof(Enums.MealType)).Cast<Enums.MealType>();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]

            System.Windows.Data.CollectionViewSource bankAccountViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("bankAccountViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // bankAccountViewSource.Source = [generic data source]
        }

        #region buttonAdd
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cb_hostingUnitType.SelectedIndex == -1)//check if unit type wasn't selected
                {
                    cb_hostingUnitType.BorderBrush = Brushes.Red;
                    return;
                }
                if (cb_area.SelectedIndex == -1)//check if area wasn't selected
                {
                    cb_area.Background = Brushes.Red;
                    return;
                }
                bL.addHostingUnit(hosting);
                Close();
            }
            catch (Exception ex)
            {
                MessageBoxResult mbr = MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region numbers
        private void numAdultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int text = 0;
            if (Int32.TryParse(numAdultTextBox.Text, out text))
            {
                if (text < 0)
                {
                    numAdultTextBox.BorderBrush = Brushes.Red;
                    numAdultTextBox.Text = "";
                }
                else
                {
                    hosting.NumAdult = text;
                    numAdultTextBox.BorderBrush = Brushes.Red;
                }
            }
            else
            {

                numAdultTextBox.BorderBrush = Brushes.Red;
                numAdultTextBox.Text = "";
            }
        }


        private void numChildrenTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            int text = 0;
            if (Int32.TryParse(numChildrenTextBox.Text, out text))
            {
                if (text < 0)
                {
                    numChildrenTextBox.BorderBrush = Brushes.Red;
                    numChildrenTextBox.Text = "";
                }

                else
                {
                    hosting.NumChildren = text;
                    numChildrenTextBox.BorderBrush = Brushes.White;
                }
            }
            else
            {
                numChildrenTextBox.BorderBrush = Brushes.Red;

                numChildrenTextBox.Text = "";
            }
        }
        #endregion

        #region names
        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(lastNameTextBox.Text, @"^[a-zA-Z]+$"))
            {
                hosting.Host.LastName = lastNameTextBox.Text;
                lastNameTextBox.BorderBrush = Brushes.White;

            }
            else
            {
                lastNameTextBox.Text = "";
                lastNameTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(nameTextBox.Text, @"^[a-zA-Z]+$"))//if contains only letters
            {
                hosting.Host.Name = nameTextBox.Text;
                nameTextBox.BorderBrush = Brushes.Black;
            }
            else
            {
                nameTextBox.Text = "";
                nameTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void hostingUnitNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hosting.HostingUnitName = hostingUnitNameTextBox.Text;

        }
        #endregion

        #region comboBox
        private void hostingUnitTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_hostingUnitType.Background = Brushes.White;
            hosting.HostingUnitType = (Enums.HostingUnitType)(cb_hostingUnitType.SelectedIndex);
        }

        private void areaVacationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_area.Background = Brushes.White;
            hosting.AreaVacation = (Enums.Area)(cb_area.SelectedIndex);
        }

        private void mealComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_meal.Background = Brushes.White;
            hosting.Meal = (Enums.MealType)(cb_meal.SelectedIndex);
        }

        #endregion

        #region checkBox
        private void poolCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (poolCheckBox.IsChecked == true)//changed to true
                hosting.Pool = Enums.Preference.Yes;
            else
            {
                if (poolCheckBox.IsChecked == false)//changed to false
                    hosting.Pool = Enums.Preference.No;
                else
                    hosting.Pool = Enums.Preference.Maybe;//otherwise it's the third state
            }
        }

        private void jacuzziCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (jacuzziCheckBox.IsChecked == true)//changed to true
                hosting.Jacuzzi = Enums.Preference.Yes;
            else
            {
                if (jacuzziCheckBox.IsChecked == false)//changed to false
                    hosting.Jacuzzi = Enums.Preference.No;
                else
                    hosting.Jacuzzi = Enums.Preference.Maybe;//otherwise it's the third state
            }
        }

        private void gardenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (gardenCheckBox.IsChecked == true)//changed to true
                hosting.Garden = Enums.Preference.Yes;
            else
            {
                if (gardenCheckBox.IsChecked == false)//changed to false
                    hosting.Garden = Enums.Preference.No;
                else
                    hosting.Garden = Enums.Preference.Maybe;//otherwise it's the third state
            }
        }
        #endregion

        private void phoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int text = 0;
            if (Int32.TryParse(phoneTextBox.Text, out text))
            {
                if (text < 0)
                {
                    phoneTextBox.BorderBrush = Brushes.Red;
                    phoneTextBox.Text = "";
                }
                else
                {
                    hosting.Host.Phone = text;
                    phoneTextBox.BorderBrush = Brushes.Black;
                }
            }
            else
            {
                phoneTextBox.BorderBrush = Brushes.Red;
                phoneTextBox.Text = "";
            }
            //if (Regex.Match(phoneTextBox.Text, @"^(\+[0-9]{9})$").Success)
            // {
            //     int text = 0;
            //     if (Int32.TryParse(phoneTextBox.Text, out text))
            //     {
            //         phoneTextBox.BorderBrush = Brushes.Black;
            //     }
            //     else
            //     {
            //         phoneTextBox.BorderBrush = Brushes.Red;
            //     }
            // }
        }

        private void hostingUnitKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cb_hostingUnitType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void bankAcountNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int text = 0;
            if (Int32.TryParse(bankAcountNumberTextBox.Text, out text))
            {
                if (text < 0)
                {
                    bankAcountNumberTextBox.BorderBrush = Brushes.Red;
                    bankAcountNumberTextBox.Text = "";
                }
                else
                {
                    hosting.Host.Bank.BankAcountNumber = text;
                    bankAcountNumberTextBox.BorderBrush = Brushes.Black;
                }
            }
            else
            {
                bankAcountNumberTextBox.BorderBrush = Brushes.Red;
                bankAcountNumberTextBox.Text = "";
            }
        }

        private void bankNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int text = 0;
            if (Int32.TryParse(bankNumberTextBox.Text, out text))
            {
                if (text < 0)
                {
                    bankNumberTextBox.BorderBrush = Brushes.Red;
                    bankNumberTextBox.Text = "";
                }
                else
                {
                    hosting.Host.Bank.BankNumber = text;
                    bankNumberTextBox.BorderBrush = Brushes.Black;
                }
            }
            else
            {
                bankNumberTextBox.BorderBrush = Brushes.Red;
                bankNumberTextBox.Text = "";
            }
        }

        private void bankNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}





//namespace PL
//{
//    /// <summary>
//    /// Interaction logic for newHostingUnit.xaml
//    /// </summary>
//    public partial class newHostingUnit : Window
//    {
//        private IBL bL;
//        public HostingUnit hosting = new HostingUnit();


//        public newHostingUnit()
//        {

//            InitializeComponent();
//            bL = factoryBL.getBL();
//            cb_hostingUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
//            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();
//            cb_meal.ItemsSource = Enum.GetValues(typeof(Enums.MealType)).Cast<Enums.MealType>();

//        }

//        private void Window_Loaded(object sender, RoutedEventArgs e)
//        {

//            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
//            // Load data by setting the CollectionViewSource.Source property:
//            // hostingUnitViewSource.Source = [generic data source]

//            System.Windows.Data.CollectionViewSource bankAccountViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("bankAccountViewSource")));
//            // Load data by setting the CollectionViewSource.Source property:
//            // bankAccountViewSource.Source = [generic data source]
//        }

//        #region buttonAdd
//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                if (cb_hostingUnitType.SelectedIndex == -1)//check if unit type wasn't selected
//                {
//                    cb_hostingUnitType.Background = Brushes.Red;
//                    return;
//                }
//                if (cb_area.SelectedIndex == -1)//check if area wasn't selected
//                {
//                    cb_area.Background = Brushes.Red;
//                    return;
//                }
//                bL.addHostingUnit(hosting);
//                Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBoxResult mbr = MessageBox.Show(ex.Message);
//            }

//        }
//        #endregion

//        #region numbers
//        private void numAdultTextBox_TextChanged(object sender, TextChangedEventArgs e)
//        {
//            int text = 0;
//            if (Int32.TryParse(numAdultTextBox.Text, out text))
//            {
//                if (text < 0)
//                {
//                    numAdultTextBox.BorderBrush = Brushes.Red;
//                    numAdultTextBox.Text = "";
//                }
//                else
//                {
//                    hosting.NumAdult = text;
//                    numAdultTextBox.BorderBrush = Brushes.Red;
//                }
//            }
//            else
//            {

//                numAdultTextBox.Background = Brushes.OrangeRed;
//                numAdultTextBox.Text = "";
//            }
//        }
//    }
//}