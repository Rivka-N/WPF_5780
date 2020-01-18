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
using BE;
using BL;
namespace PL
{
    /// <summary>
    /// Interaction logic for hostingUnitTabs.xaml
    /// </summary>
    public partial class hostingUnitTabs : Window
    {
        IBL myBL;
        HostingUnit unit;
        List<Order> myOrders;
        List<Order> addOrders;
        
        public hostingUnitTabs(HostingUnit hosting)
        {
            myBL = BL.factoryBL.getBL();
            InitializeComponent();
            unit = hosting;
            //sets closed orders data grid source
            myOrders = myBL.getOrders(ord => ord.Status == Enums.OrderStatus.Closed && unit.HostingUnitKey==ord.HostingUnitKey);//sets source for orders
            dg_orderDataGrid.ItemsSource = myOrders;//all closed orders from this host

            //sets addOrders data grid source
            addOrders = myBL.getOrders(ord => ord.HostingUnitKey == unit.HostingUnitKey && (ord.Status == Enums.OrderStatus.Mailed || ord.Status == Enums.OrderStatus.Started));
            dg_addOrder.ItemsSource = addOrders;
            //dg_unit
        }
        #region windows events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
       

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new AllUnitsList().Show();//opens previous window again
        }
        #endregion

        #region back button
        private void Pb_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();//closes window and returns to other window
        }
        #endregion
        #region tabs
        private void Tab_addOrders_Selected(object sender, RoutedEventArgs e)
        {
            //change closed orders
        }
        #endregion
        #region add order tab
        private void pb_addOrder_Click(object sender, RoutedEventArgs e)//what does this do?
        {
            try
            {
                if (dg_addOrder.SelectedItem != null && dg_addOrder.SelectedItem is Order)
                    new hostingUnitTabs((HostingUnit)dg_addOrder.SelectedItem).Show();
                else MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                //new window(send current unit)
            }
            catch
            {
                MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }//sends to unit information with data of current row to bind to



        private void orderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_addOrder.CurrentItem != null)//something was selected
            {
                Order row = (Order)dg_addOrder.SelectedItem;
                if (row.Status == Enums.OrderStatus.Started)
                    pb_sendMail.IsEnabled = true;
                if (row.Status == Enums.OrderStatus.Mailed)
                    pb_addOrder.IsEnabled = true;
            }
        }

        private void pb_sendMail_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region delete Unit button 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string message = "Are you sure you want to delete this unit?";
            string caption = "Confirmation";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
            {
                // OK code here
                int code = 0;
                myBL.deleteUnit(code);

            }
            else
            {
                // Cancel code here. nothing needs to happen then
            }

        }
        #endregion

        #region textBox updateUnit
        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void hostingUnitKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void hostingUnitNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //    if (Regex.IsMatch(hostingUnitNameTextBox.Text, @"^[a-zA-Z]+$"))
            //    {
            //        g1.LastName = hostingUnitNameTextBox.Text;//needs to know how i get the hostingunit
            //        hostingUnitNameTextBox.Background = Brushes.White;

            //    }
            //    else
            //    {
            //        hostingUnitNameTextBox.Text = "";
            //        hostingUnitNameTextBox.Background = Brushes.Red;
            //    }
        }
        #endregion

        #region comboBox UpdateUnit
        private void hostingUnitTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //(Enums.HostingUnitType)(hostingUnitTypeComboBox.SelectedIndex);
        }

        private void mealComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //(Enums.MealType)(mealComboBox.SelectedIndex)
        }

        #endregion
        #region numbers UpdateUnit
        private void numChildrenTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //    int text = 0;
            //    if (Int32.TryParse(numChildrenTextBox.Text, out text))
            //    {
            //        if (text < 0)
            //        {
            //            numChildrenTextBox.Background = Brushes.OrangeRed;
            //            numChildrenTextBox.Text = "";
            //        }
            //        else
            //        {
            //            g1.NumAdult = text;
            //            numChildrenTextBox.Background = Brushes.White;
            //        }
            //    }
            //    else
            //    {

            //        numChildrenTextBox.Background = Brushes.OrangeRed;
            //        numChildrenTextBox.Text = "";
            //    }
        }

        private void numAdultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //    int text = 0;
            //    if (Int32.TryParse(numAdultTextBox.Text, out text))
            //    {
            //        if (text < 0)
            //        {
            //            numAdultTextBox.Background = Brushes.OrangeRed;
            //            numAdultTextBox.Text = "";
            //        }
            //        else
            //        {
            //            g1.NumAdult = text;
            //            numAdultTextBox.Background = Brushes.White;
            //        }
            //    }
            //    else
            //    {

            //        numAdultTextBox.Background = Brushes.OrangeRed;
            //        numAdultTextBox.Text = "";
            //    }
        }
        #endregion

        #region checkBox update unit
        private void jacuzziCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //        if (jacuzziCheckBox.IsChecked == true)//changed to true
            //            g1.Jacuzzi = Enums.Preference.Yes;
            //        else
            //        {
            //            if (jacuzziCheckBox.IsChecked == false)//changed to false
            //                g1.Jacuzzi = Enums.Preference.No;
            //            else
            //                g1.Jacuzzi = Enums.Preference.Maybe;//otherwise it's the third state
            //        }
        }

        private void poolCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //        if (poolCheckBox.IsChecked == true)//changed to true
            //            g1.Pool = Enums.Preference.Yes;
            //        else
            //        {
            //            if (poolCheckBox.IsChecked == false)//changed to false
            //                g1.Pool = Enums.Preference.No;
            //            else
            //                g1.Pool = Enums.Preference.Maybe;//otherwise it's the third state
            //        }
            //    }

            //    private void gardenCheckBox_Checked(object sender, RoutedEventArgs e)
            //    {
            //        if (gardenCheckBox.IsChecked == true)//changed to true
            //            g1.Garden = Enums.Preference.Yes;
            //        else
            //        {
            //            if (gardenCheckBox.IsChecked == false)//changed to false
            //                g1.Garden = Enums.Preference.No;
            //            else
            //                g1.Garden = Enums.Preference.Maybe;//otherwise it's the third state
            //        }
        }
        private void gardenCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        #endregion //not finish

       
    }
}

