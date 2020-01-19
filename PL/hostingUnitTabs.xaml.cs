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
        List<GuestRequest> addOrders;
        
        public hostingUnitTabs(HostingUnit hosting)
        {
            myBL = BL.factoryBL.getBL();
            InitializeComponent();
            unit = hosting;

            //sets closed orders data grid source
            myOrders = myBL.getOrders(ord => ord.Status == Enums.OrderStatus.Closed && unit.HostingUnitKey==ord.HostingUnitKey);//sets source for orders
            dg_orderDataGrid.ItemsSource = myOrders;//all closed orders from this host

            //sets addOrders data grid source
            addOrders = unit.guestForUnit;
            dg_guestRequestDataGrid.ItemsSource = addOrders;

            //set enums also
            dg_updateUnitGrid.DataContext = unit;
            
           
        }
        public hostingUnitTabs(HostingUnit hosting, int tab): this(hosting)
        {
            tc_mainControl.SelectedIndex = tab;//selects required tab
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
        #region doubleclick
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //same as adding order/mailing with button depending on which is available
        }
        #endregion
        #region back button
        private void Pb_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();//closes window and returns to other window
        }
        #endregion
        #region tabs
       
        private void Tab_updateDeleteUnit_Unselected(object sender, RoutedEventArgs e)
        {
            addOrders = unit.guestForUnit;//make sure it was filtered through again
            dg_guestRequestDataGrid.ItemsSource = addOrders;
            //resets available orders from non- mailed orders based on what's relevent now
        }
        private void Tab_addOrders_Unselected(object sender, RoutedEventArgs e)
        {

            //resets closed orders
            myOrders = myBL.getOrders(ord => ord.Status == Enums.OrderStatus.Closed && unit.HostingUnitKey == ord.HostingUnitKey);//sets source for orders
            dg_orderDataGrid.ItemsSource = myOrders;//all closed orders from this host

            //resets addOrders data grid source
            addOrders = unit.guestForUnit;//sets to all available guests for adding (minus the one already added)
            dg_guestRequestDataGrid.ItemsSource = addOrders;
        }

        #endregion
        #region reset date
        //not done
        private void Dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)//changes date back to original date selected
        {
            if (sender is DatePicker)
                if (tc_mainControl.SelectedItem == tab_addOrders)
                    if (dg_guestRequestDataGrid.SelectedItem != null)//there's an item selected
                    { if (dg_guestRequestDataGrid.SelectedCells != null)
                            (sender as DatePicker).SelectedDate = (dg_guestRequestDataGrid.SelectedItem as GuestRequest).Registration;
                    }
                    //change each date based on what's relevent
                    else
                        if (tc_mainControl.SelectedItem == tab_closedOrders)
                        if (dg_orderDataGrid.SelectedItem != null)
                            (sender as DatePicker).SelectedDate = (dg_orderDataGrid.SelectedItem as Order).CreateDate;//find which date is selected and only change that one?
        }

#endregion

#region add order datagrid

        private void dg_guestRequestDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_guestRequestDataGrid.SelectedItem != null)//something was selected
            {
                GuestRequest row = (GuestRequest)dg_guestRequestDataGrid.SelectedItem;
                var curOrder=myBL.getOrders(ord => ord.GuestRequestKey == row.GuestRequestKey && ord.HostingUnitKey == unit.HostingUnitKey);//finds the applicable order
                { if (curOrder==null)//there is no order existing yet
                    pb_sendMail.IsEnabled = true;//has to send mail first
                 else 
                    { if (curOrder[0].Status == Enums.OrderStatus.Mailed)//already sent mail
                            pb_addOrder.IsEnabled = true;
                        else
                        {
                            pb_sendMail.IsEnabled = false;
                            pb_addOrder.IsEnabled = false;//disables buttons
                            MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                        }
            }
            else//selected item is null
            {
                pb_sendMail.IsEnabled = false;
                pb_addOrder.IsEnabled = false;

            }
        }
        #endregion
        #region button clicks add order
        private void pb_addOrder_Click(object sender, RoutedEventArgs e)//adds final order. deletes from the rest
        {
            try
            {
                if (dg_guestRequestDataGrid.SelectedItem != null && dg_guestRequestDataGrid.SelectedItem is GuestRequest)
                {
                    //update order to closed, change status everywhere and delete other orders with guest...
                }
                else MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void pb_sendMail_Click(object sender, RoutedEventArgs e)
        {//Order newOrder= new Order();//creates new order
         //newOrder.CreateDate = DateTime.Now;
         //newOrder.GuestName=dg_guestRequestDataGrid.SelectedItem
         //myBL.addOrder(newOrder);//adds order
         //add request to orders and update status or update order to closed and status everywhere
         //sends mail and adds order
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

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }

    }
}

