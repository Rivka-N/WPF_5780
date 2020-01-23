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
        private readonly HostingUnit originalUnit;//to compare changed one to
        List<Order> myOrders;
        List<GuestRequest> addOrders;
        bool closeProgram;//for exiting window
        #region c-tors
        public hostingUnitTabs(HostingUnit hosting)
        {
            myBL = BL.factoryBL.getBL();
            InitializeComponent();
            unit = hosting;
            originalUnit = hosting;//to compare unit to see if there were changes
            closeProgram = false;

            //sets closed orders data grid source
            updateOrdersSource();

            //sets addOrders data grid source
            updateGuestsSource();

            //set enums also
            dg_updateUnitGrid.DataContext = unit;
            dg_bank.DataContext = (unit.Host != null && unit.Host.Bank != null) ? unit.Host : null;//equals bank if exists
            //combobox sources
            cb_updateUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
            cb_meal.ItemsSource = Enum.GetValues(typeof(Enums.MealType)).Cast<Enums.MealType>();
            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();

            //error textboxes
            //if (myOrders.Count==0 || myOrders==null)


        }
        public hostingUnitTabs(HostingUnit hosting, int tab) : this(hosting)
        {
            tc_mainControl.SelectedIndex = tab;//selects required tab

        }
        #endregion
        #region windows events
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));

            // Load data by setting the CollectionViewSource.Source property:
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closeProgram)//if doesn't want to close program
                new AllUnitsList().Show();//opens previous window again
        }
        #endregion
        #region doubleclick
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)//same as adding order/mailing with button depending on which is available

        {
            //if (pb_addOrder.IsEnabled)//add order

        }

        #endregion
        #region update lists source
        private void updateGuestsSource()
        {
            //updates
            addOrders = (unit.Host.CollectionClearance == true) ? myBL.getReleventRequests(unit) : null;
            dg_guestRequestDataGrid.ItemsSource = addOrders;

            //prints error textbox if empty

            if (addOrders == null || addOrders.Count == 0)//nothing to show
            {
                tb_guest_error.Visibility = Visibility.Visible;//shows textbox
                dg_guestRequestDataGrid.Visibility = Visibility.Collapsed;
                if (!unit.Host.CollectionClearance)//no collection clearance
                    tb_guest_error.Text = "Add Bank Collection Permission";
                else
                    tb_guest_error.Text = "No Relevent Orders";
            }
            else
            {
                tb_guest_error.Visibility = Visibility.Collapsed;//hides textbox
                dg_guestRequestDataGrid.Visibility = Visibility.Visible;//shows datagrid
            }
        }
        private void updateOrdersSource()
        {
            myOrders = myBL.getOrders(ord => ord.Status == Enums.OrderStatus.Closed && unit.HostingUnitKey == ord.HostingUnitKey);//sets source for orders
            dg_orderDataGrid.ItemsSource = myOrders;//all closed orders from this host
                                                    //prints error textbox if empty

            if (myOrders == null || myOrders.Count == 0)//nothing to show
            {
                tb_order_error.Visibility = Visibility.Visible;//shows textbox
                dg_orderDataGrid.Visibility = Visibility.Collapsed;
                tb_order_error.Text = "No relevent orders\n";
                if (!unit.Host.CollectionClearance)//no collection clearance
                    tb_order_error.Text += "Add Bank Collection Permission";
            }
            else
            {
                tb_order_error.Visibility = Visibility.Collapsed;//hides textbox
                dg_orderDataGrid.Visibility = Visibility.Visible;//shows datagrid
            }
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
            Close();//closes window and returns to other window

        }
        private void Pb_UnitBack_Click(object sender, RoutedEventArgs e)
        {
            if (pb_update.IsEnabled)//if there were changes
                if (MessageBox.Show("Are you sure you want to exit without saving changes?", "unsaved changes", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    //wants to exit
                    Close();//closes window
                            //otherwise does nothing
        }
        #endregion
        #region tabs

        private void Tab_updateDeleteUnit_Unselected(object sender, RoutedEventArgs e)
        {
            if (pb_update.IsEnabled)//if there were changes
                if (MessageBox.Show("Are you sure you want to exit without saving changes?", "unsaved changes", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
                {
                    tc_mainControl.SelectedIndex = 0;//selects first tab again
                    return;
                }

            //wants to exit
            //otherwise does nothing
            updateGuestsSource();

            //resets available orders from non- mailed orders based on what's relevent now
        }
        private void Tab_addOrders_Unselected(object sender, RoutedEventArgs e)
        {

            //resets closed orders
            updateOrdersSource();

            //resets addOrders data grid source
            updateGuestsSource();
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
                    updateOrdersSource();

                    //sets addOrders data grid source
                    updateGuestsSource();
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
                    MessageBox.Show("Mail sent. Please wait for response from " + ((GuestRequest)dg_guestRequestDataGrid.SelectedItem).Name + " " + ((GuestRequest)dg_guestRequestDataGrid.SelectedItem).LastName, "Mail Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    updateGuestsSource();
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

        #region check bank and collection clearance
        private void Cb_collectionClearance_Checked(object sender, RoutedEventArgs e)//need to add check other conditions
        {
            if (cb_collectionClearance.IsChecked == false)//set to false
            {
                if (MessageBox.Show("You will not be able to add any more orders. Proceed?", "warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    unit.Host.CollectionClearance = false;
                else
                    cb_collectionClearance.IsChecked = true;//adds check again
            }
            else//checked
                MessageBox.Show("go over to your add Orders tab to start talking to customers", "Unit Allowed", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
     
        #region updateUnit text lost focus
        //checks if item was updated, if it's valid, and if both then updates new host and allows update button
        private void Tb_unitname_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Regex.IsMatch(tb_name.Text, @"[\p{L}]{2,}"))//contains some letters
                    throw new invalidTypeExceptionPL();//not a valid name
                tb_unitname.BorderBrush = Brushes.Gray;
                if (originalUnit.HostingUnitName != tb_unitname.Text)
                {
                    unit.HostingUnitName = tb_unitname.Text;
                    pb_update.IsEnabled = true;
                }
            }
            catch
            {
                MessageBox.Show("unit name must contain at least two letters", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                tb_unitname.BorderBrush = Brushes.Red;
                tb_unitname.Text = originalUnit.HostingUnitName;
            }
        }

        private void Tb_phone_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int text = 0;
                if (!Regex.IsMatch(tb_phone.Text, @"^(\+[0-9]{9,10})$"))//9-10 numbers
                    throw new invalidTypeExceptionPL();
                if (Int32.TryParse(tb_phone.Text, out text))
                {
                    if (text < 0)
                    {
                        throw new invalidTypeExceptionPL();
                    }

                    unit.Host.Phone = text;//sets new number
                    tb_phone.BorderBrush = Brushes.Gray;
                    if (text != originalUnit.Host.Phone)//was changed
                        pb_update.IsEnabled = true;//allows update
                }
                else
                {
                    throw new invalidTypeExceptionPL();//not valid phone number
                }
            }
            catch
            {
                tb_phone.BorderBrush = Brushes.Red;
                tb_phone.Text = originalUnit.Host.Phone.ToString();//resets text
            }

        }

        private void Tb_lastName_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Regex.IsMatch(tb_lastName.Text, @"^[\p{L}]+$"))//contains only letters
                {
                    if (tb_lastName.Text == originalUnit.Host.LastName)//name was changed
                    {
                        unit.Host.LastName = tb_lastName.Text;//sets first name
                        pb_update.IsEnabled = true;
                    }
                    tb_lastName.BorderBrush = Brushes.Gray;//resets border
                }
                else
                    throw new invalidTypeExceptionPL();//not name
            }
            catch
            {
                tb_lastName.BorderBrush = Brushes.Red;
                tb_lastName.Text = originalUnit.Host.LastName;
            }
        }

        private void Tb_name_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Regex.IsMatch(tb_name.Text, @"^[\p{L}]+$"))//contains only letters
                {
                    if (tb_name.Text != originalUnit.Host.Name)//name was changed
                    {
                        unit.Host.Name = tb_name.Text;//sets first name
                        pb_update.IsEnabled = true;
                    }
                    tb_name.BorderBrush = Brushes.Gray;//resets border
                }
                else
                    throw new invalidTypeExceptionPL();//not name
            }
            catch
            {
                tb_name.BorderBrush = Brushes.Red;
                tb_name.Text = originalUnit.Host.Name;
            }
        }

        private void Tb_mail_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                //checks it's valid mail

                    unit.Host.Mail = myBL.checkMail(tb_mail.Text);//checks if it's an email

                    tb_mail.BorderBrush = Brushes.Gray;
                if (unit.Host.Mail != originalUnit.Host.Mail)
                    pb_update.IsEnabled = true;

            }
            catch
            {
                tb_mail.Text = originalUnit.Host.Mail.Address;
                tb_mail.BorderBrush = Brushes.Red;
            }
        }


        #endregion
        #region update unit number checks
        private void Tb_numAdultTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {

                unit.NumAdult = addNum(tb_numAdultTextBox.Text);//checks this is a valid number. 
                tb_numAdultTextBox.BorderBrush = Brushes.Gray;
                if (unit.NumAdult != originalUnit.NumAdult)
                    pb_update.IsEnabled = true;

            }
            catch (Exception ex)
            {
                tb_numAdultTextBox.BorderBrush = Brushes.Red;//colors border
                tb_numAdultTextBox.Text = originalUnit.NumAdult.ToString();
                if (ex is LargeNumberExceptionPL)
                    MessageBox.Show(ex.Message);//if the number was too big, explains why number wasn't valid
            }
        }

        private void Tb_numChildrenTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {

                unit.NumChildren = addNum(tb_numChildrenTextBox.Text);//checks this is a valid number. 
                tb_numChildrenTextBox.BorderBrush = Brushes.Gray;
                if (unit.NumChildren != originalUnit.NumChildren)
                    pb_update.IsEnabled = true;

            }
            catch (Exception ex)
            {
                tb_numChildrenTextBox.BorderBrush = Brushes.Red;//colors border
                tb_numChildrenTextBox.Text = originalUnit.NumChildren.ToString();
                if (ex is LargeNumberExceptionPL)
                    MessageBox.Show(ex.Message);//if the number was too big, explains why number wasn't valid
            }
        }
        private int addNum(string number)
        {
            int text = 0;
            if (!Int32.TryParse(number, out text))//if it's not a number
                throw new invalidTypeExceptionPL();
            if (text < 0)//not a valid number
                throw new negativeNumberExceptionPL();
            if (text > 1000)
                throw new LargeNumberExceptionPL("Number cannot be over 1000");
            return text;//if it's valid, returns it
        }
        #endregion

        #region comboBox UpdateUnit
        private void hostingUnitTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            unit.HostingUnitType = (Enums.HostingUnitType)(cb_updateUnitType.SelectedIndex);
            pb_update.IsEnabled = true;

            //(Enums.HostingUnitType)(hostingUnitTypeComboBox.SelectedIndex);
        }

        private void mealComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_meal.Background = Brushes.White;
            unit.Meal = (Enums.MealType)(cb_meal.SelectedIndex);
            pb_update.IsEnabled = true;

            //(Enums.MealType)(mealComboBox.SelectedIndex)
        }
        private void Cb_area_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_area.SelectedIndex != -1)//if something was selected
                pb_update.IsEnabled = true;

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
                MessageBox.Show("The unit was updated\n", "Unit update", MessageBoxButton.OK, MessageBoxImage.Asterisk);//prints message
                Close();

            }
            else//didn't update anything
                if (MessageBox.Show("No changes were made.\n Exit anyway?\n", "unit update", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)//prints message
                Close();//closes
            //otherwise does nothing
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
            catch (Exception ex)
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
        #region combobox changes
        private void cb_hostingUnitType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Enums.HostingUnitType)cb_updateUnitType.SelectedIndex!=originalUnit.HostingUnitType)//different unit type
            {
                pb_update.IsEnabled = true;
                unit.HostingUnitType = (Enums.HostingUnitType)cb_updateUnitType.SelectedIndex;
            }
        }
    }
}

