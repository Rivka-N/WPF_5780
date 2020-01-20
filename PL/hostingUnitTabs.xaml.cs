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
    /// Interaction logic for hostingUnitTabs.xaml
    /// </summary>
    public partial class hostingUnitTabs : Window
    {
        IBL myBL;
        HostingUnit unit;
        List<Order> myOrders;
        List<GuestRequest> addOrders;
        bool closeProgram;//for exiting window
        #region c-tors
        public hostingUnitTabs(HostingUnit hosting)
        {
            myBL = BL.factoryBL.getBL();
            InitializeComponent();
            unit = hosting;
            closeProgram = false;

            //sets closed orders data grid source
            myOrders = myBL.getOrders(ord => ord.Status == Enums.OrderStatus.Closed && unit.HostingUnitKey==ord.HostingUnitKey);//sets source for orders
            dg_orderDataGrid.ItemsSource = myOrders;//all closed orders from this host

            //sets addOrders data grid source
            addOrders = myBL.getReleventRequests(unit);
            dg_guestRequestDataGrid.ItemsSource = addOrders;

            //set enums also
            dg_updateUnitGrid.DataContext = unit;
            
           
        }
        public hostingUnitTabs(HostingUnit hosting, int tab): this(hosting)
        {
            tc_mainControl.SelectedIndex = tab;//selects required tab
        }
        #endregion
        #region windows events
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!closeProgram)//if doesn't want to close program
               new AllUnitsList().Show();//opens previous window again
        }
        #endregion
        #region doubleclick
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            //same as adding order/mailing with button depending on which is available
        }

        #endregion
        #region cell selected guests
        private void dg_guestRequestDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_guestRequestDataGrid.SelectedItem != null)//something was selected
            {
                GuestRequest row = (GuestRequest)dg_guestRequestDataGrid.SelectedItem;
                var curOrder = myBL.getOrders(ord => ord.GuestRequestKey == row.GuestRequestKey && ord.HostingUnitKey == unit.HostingUnitKey);//finds the applicable order
                {
                    if (curOrder.Count == 0)//there is no order existing yet
                    {
                        pb_sendMail.IsEnabled = true;//has to send mail first
                        pb_addOrder.IsEnabled = false;//disables buttons
                    }
                    else
                    {
                        if (curOrder[0].Status == Enums.OrderStatus.Mailed)//already sent mail
                        {
                            pb_addOrder.IsEnabled = true;
                            pb_sendMail.IsEnabled = false;

                        }
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
        #region back button
        private void Pb_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();//closes window and returns to other window
        }
        #endregion
        #region tabs
       
        private void Tab_updateDeleteUnit_Unselected(object sender, RoutedEventArgs e)
        {
            addOrders = myBL.getReleventRequests(unit);
            dg_guestRequestDataGrid.ItemsSource = addOrders;
            
            //resets available orders from non- mailed orders based on what's relevent now
        }
        private void Tab_addOrders_Unselected(object sender, RoutedEventArgs e)
        {

            //resets closed orders
            myOrders = myBL.getOrders(ord => ord.Status == Enums.OrderStatus.Closed && unit.HostingUnitKey == ord.HostingUnitKey);//sets source for orders
            dg_orderDataGrid.ItemsSource = myOrders;//all closed orders from this host

            //resets addOrders data grid source

            addOrders = myBL.getReleventRequests(unit);//sets to all available guests for adding (minus the one already added)
            dg_guestRequestDataGrid.ItemsSource = addOrders;
        }

        #endregion
        #region reset date
        private void Dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)//changes date back to original date selected
        //not done
        {
            if (sender is DatePicker)
                if (tc_mainControl.SelectedItem == tab_addOrders)
                    if (dg_guestRequestDataGrid.SelectedItem != null)//there's an item selected
                    { if (dg_guestRequestDataGrid.SelectedCells != null)//check which one is selected
                            (sender as DatePicker).SelectedDate = (dg_guestRequestDataGrid.SelectedItem as GuestRequest).Registration;
                    }
                    //change each date based on what's relevent
                    else
                        if (tc_mainControl.SelectedItem == tab_closedOrders)
                        if (dg_orderDataGrid.SelectedItem != null)
                            (sender as DatePicker).SelectedDate = (dg_orderDataGrid.SelectedItem as Order).CreateDate;//find which date is selected and only change that one?
        }

#endregion

        #region button addOrder
        private void pb_addOrder_Click(object sender, RoutedEventArgs e)//adds final order. deletes from the rest
        {
            try
            {
                if (dg_guestRequestDataGrid.SelectedItem != null && dg_guestRequestDataGrid.SelectedItem is GuestRequest)
                {
                    myBL.order(unit, (GuestRequest)dg_guestRequestDataGrid.SelectedItem);
                    
                    //updates lists

                    //sets closed orders data grid source
                    myOrders = myBL.getOrders(ord => ord.Status == Enums.OrderStatus.Closed && unit.HostingUnitKey == ord.HostingUnitKey);//sets source for orders
                    dg_orderDataGrid.ItemsSource = myOrders;//all closed orders from this host

                    //sets addOrders data grid source
                    addOrders = myBL.getReleventRequests(unit);
                    dg_guestRequestDataGrid.ItemsSource = addOrders;
                }
                else MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error! " + ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void pb_sendMail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_guestRequestDataGrid.SelectedItem != null && dg_guestRequestDataGrid.SelectedItem is GuestRequest)
                {
                    myBL.sendGuestMail(unit, (GuestRequest)dg_guestRequestDataGrid.SelectedItem);
                   //needs to send mail, add order, update guest status and add to num suggestions?
                }
                else
                    MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            catch
            {
                MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);

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

            if (Regex.IsMatch(hostingUnitNameTextBox.Text, @"^[a-zA-Z]+$"))
            {
                unit.HostingUnitName = hostingUnitNameTextBox.Text;//set the new name
                hostingUnitNameTextBox.Background = Brushes.White;
                pb_update.IsEnabled = true;

            }
            else
            {
                hostingUnitNameTextBox.Text = "";
                hostingUnitNameTextBox.Background = Brushes.Red;
            }
        }
        #endregion

        #region comboBox UpdateUnit
        private void hostingUnitTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hostingUnitTypeComboBox.Background = Brushes.White;
            unit.HostingUnitType = (Enums.HostingUnitType)(hostingUnitTypeComboBox.SelectedIndex);
            pb_update.IsEnabled = true;

            //(Enums.HostingUnitType)(hostingUnitTypeComboBox.SelectedIndex);
        }

        private void mealComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mealComboBox.Background = Brushes.White;
            unit.Meal = (Enums.MealType)(mealComboBox.SelectedIndex);
            pb_update.IsEnabled = true;

            //(Enums.MealType)(mealComboBox.SelectedIndex)
        }

        #endregion
        #region numbers UpdateUnit
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
                    unit.NumChildren = text;
                    numChildrenTextBox.Background = Brushes.White;
                    pb_update.IsEnabled = true;

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
                    unit.NumAdult = text;
                    numAdultTextBox.Background = Brushes.White;
                    pb_update.IsEnabled = true;

                }
            }
            else
            {

                numAdultTextBox.Background = Brushes.OrangeRed;
                numAdultTextBox.Text = "";
            }
        }
        #endregion


        #region checkBox update unit
        private void jacuzziCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (jacuzziCheckBox.IsChecked == true)//changed to true
                unit.Jacuzzi = Enums.Preference.Yes;
            else
            {
                if (jacuzziCheckBox.IsChecked == false)//changed to false
                    unit.Jacuzzi = Enums.Preference.No;
                else
                    unit.Jacuzzi = Enums.Preference.Maybe;//otherwise it's the third state
            }
            pb_update.IsEnabled = true;

        }

        private void poolCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (poolCheckBox.IsChecked == true)//changed to true
                unit.Pool = Enums.Preference.Yes;
            else
            {
                if (poolCheckBox.IsChecked == false)//changed to false
                    unit.Pool = Enums.Preference.No;
                else
                    unit.Pool = Enums.Preference.Maybe;//otherwise it's the third state
            }
            pb_update.IsEnabled = true;

        }

        private void gardenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (gardenCheckBox.IsChecked == true)//changed to true
                unit.Garden = Enums.Preference.Yes;
            else
            {
                if (gardenCheckBox.IsChecked == false)//changed to false
                    unit.Garden = Enums.Preference.No;
                else
                    unit.Garden = Enums.Preference.Maybe;//otherwise it's the third state
            }
            pb_update.IsEnabled = true;

        }



        #endregion //not finish

        #region unitUpdateTab buttons
        private void pb_update_Click(object sender, RoutedEventArgs e)
        {
            if (myBL.checkUnit(unit))
            {
                myBL.changeUnit(unit);
               MessageBox.Show("The unit updated\n");//prints message
            }
            
            Close();

        }

        private void pb_delete_Click(object sender, RoutedEventArgs e)
        {
           
                string message = "Are you sure you want to delete this unit?";
                string caption = "Confirmation";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
            try { 
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                {
                    // OK code here
                    myBL.deleteUnit(unit.HostingUnitKey);//send to the delete function in bl, there is problem in the remove functios ds
                    if (MessageBox.Show("the unit" + unit.HostingUnitKey + " : " + unit.HostingUnitName + "was removed\n Exit program?", "unit Removed", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        closeProgram = true;//exit all the way
                    Close();
                }
                else
                {
                    // Cancel code here. nothing needs to happen then
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to Delete Unit:" + ex.Message, "error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        #endregion
        #region Closed Order searching

        private void searchOrders()//searches through orders based on criteria
        {
            try
            {
                myOrders = myBL.searchOrders(dp_closed_orderDate.SelectedDate, tb_closed_SearchTextBox.Text, Enums.FunctionSender.Host);
                dg_orderDataGrid.ItemsSource = myOrders;//updates data source
            }
            catch
            {
                MessageBox.Show("invalid query");
            }
        }
        private void Tb_closed_SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchOrders();//refilters orders
        }
        private void Pb_closed_resetDate_Click(object sender, RoutedEventArgs e)
        {
            dp_closed_orderDate.SelectedDate = null;
            searchOrders();//refilters orders
        }
        private void Dp_closed_orderDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            searchOrders();
        }

        #endregion
        #region requests searching
        private void Tb_addOrder_SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var guests = myBL.GuestSearchQuery(null, tb_addOrder_SearchTextBox.Text, Enums.FunctionSender.Host);//sends to function to find relevent orders

            addOrders = myBL.getReleventRequests(unit).Where(g => guests(g)).Select(g => g).ToList();
            dg_guestRequestDataGrid.ItemsSource = addOrders;

        }
        #endregion
    }
}

