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
        IEnumerable<IGrouping<int, BankAccount>> bankSource;//bank combobox source
        bool closeProgram;//for exiting window
        #region c-tors
        public hostingUnitTabs(HostingUnit hosting)
        {
            myBL = BL.factoryBL.getBL();
            InitializeComponent();
            unit = hosting;
            originalUnit = myBL.copy(hosting);//to compare unit to see if there were changes. makes copy of hosting
            closeProgram = false;

            //sets closed orders data grid source
            updateOrdersSource();

            //sets addOrders data grid source
            updateGuestsSource();

            //set enums also
            dg_updateUnitGrid.DataContext = unit;
            dg_bank.DataContext = (!(unit.Host == null) && !(unit.Host.Bank == null)) ? unit.Host : null;//equals bank if exists
            //combobox sources
            cb_updateUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
            cb_meal.ItemsSource = Enum.GetValues(typeof(Enums.MealType)).Cast<Enums.MealType>();
            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();


            //addorder binding
            cb_addorder_meal.ItemsSource = Enum.GetValues(typeof(Enums.MealType)).Cast<Enums.MealType>();

            //initialize mail
            tb_mail.Text = originalUnit.Host.Mail.Address;

            //init checkboxes
            cb_gardenCheckBox.IsChecked = unit.Garden == Enums.Preference.Yes ? true : false;
            cb_poolCheckBox.IsChecked = unit.Pool == Enums.Preference.Yes ? true : false;
            cb_jacuzziCheckBox.IsChecked = unit.Jacuzzi == Enums.Preference.Yes ? true : false;

            //bank init
            bankSource = myBL.groupBranchesByBank();//branches grouped by bank
            initBank();//sets banks and comboboxes
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
        private void updateGuestsSource(Func<GuestRequest,bool> sortBy=null)
        {
            if (sortBy == null)
            {            //updates
                addOrders = (unit.Host.CollectionClearance == true) ? myBL.getReleventRequests(unit) : null;
            }
            else//there are conditions to sort by
            {
                addOrders = (unit.Host.CollectionClearance == true) ? myBL.getReleventRequests(unit).Where(g=>sortBy(g)).ToList() : null;
                //if there's a collection clearance brings the items and sorts again. otherwise, doesn't
            }
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
        private void Dp_DateChangedOrders(object sender, SelectionChangedEventArgs e)//changes date back to original date selected
        {
            if (sender is DatePicker)
                if (dg_orderDataGrid.SelectedItem != null)//there's an item selected
                    if (dg_orderDataGrid.SelectedCells != null)//check which one is selected
                    {
                        (sender as DatePicker).SelectedDate = (dg_guestRequestDataGrid.SelectedItem as Order).OrderDate;
                    }
        }

        private void Dp_DateChangedOrderCreated(object sender, SelectionChangedEventArgs e)//changes date back to original date selected
        {
            if (sender is DatePicker)
                if (dg_orderDataGrid.SelectedItem != null)//there's an item selected
                    if (dg_orderDataGrid.SelectedCells != null)//check which one is selected
                    {
                        (sender as DatePicker).SelectedDate = (dg_guestRequestDataGrid.SelectedItem as Order).CreateDate;
                    }
        }

        private void Dp_dateChangedEntry(object sender, SelectionChangedEventArgs e)//changes date back to original date selected
        {
            if (sender is DatePicker)
                if (dg_guestRequestDataGrid.SelectedItem != null)//there's an item selected
                {
                    if (dg_guestRequestDataGrid.SelectedCells != null)//check which one is selected
                        (sender as DatePicker).SelectedDate = (dg_guestRequestDataGrid.SelectedItem as GuestRequest).EntryDate;
                }

        }
        private void Dp_dateChangedRelease(object sender, SelectionChangedEventArgs e)//changes date back to original date selected
        {
            if (sender is DatePicker)
                if (dg_guestRequestDataGrid.SelectedItem != null)//there's an item selected
                {
                    if (dg_guestRequestDataGrid.SelectedCells != null)//check which one is selected
                        (sender as DatePicker).SelectedDate = (dg_guestRequestDataGrid.SelectedItem as GuestRequest).ReleaseDate;
                }
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
                MessageBox.Show("unable to send email! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);

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
                if (!originalUnit.Host.CollectionClearance)//check if was allowed before
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
                if (!(originalUnit.HostingUnitName == tb_unitname.Text))
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
                    if (!(tb_name.Text == originalUnit.Host.Name))//name was changed
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
                if (!(unit.Host.Mail == originalUnit.Host.Mail))
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
                if (!(unit.NumAdult== originalUnit.NumAdult))
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
                if (!(unit.NumChildren == originalUnit.NumChildren))
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
        private void cb_hostingUnitType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!((Enums.HostingUnitType)cb_updateUnitType.SelectedIndex == originalUnit.HostingUnitType))//different unit type
            {
                pb_update.IsEnabled = true;
                unit.HostingUnitType = (Enums.HostingUnitType)cb_updateUnitType.SelectedIndex;
            }
        }

        private void Cb_area_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!((Enums.Area)cb_area.SelectedIndex == originalUnit.AreaVacation))//different unit type
            {
                pb_update.IsEnabled = true;
                unit.AreaVacation = (Enums.Area)cb_area.SelectedIndex;
            }
        }


        private void Cb_meal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!((Enums.MealType)cb_meal.SelectedIndex == originalUnit.Meal))//different unit type
            {
                pb_update.IsEnabled = true;
                unit.Meal = (Enums.MealType)cb_meal.SelectedIndex;
            }
        }
        #endregion
      

        #region checkBox update unit
        private void jacuzziCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_jacuzziCheckBox.IsChecked == true)//changed to true
                unit.Jacuzzi = Enums.Preference.Yes;
            else
            {
                if (cb_jacuzziCheckBox.IsChecked == false)//changed to false
                    unit.Jacuzzi = Enums.Preference.No;
                else
                    unit.Jacuzzi = Enums.Preference.Maybe;//otherwise it's the third state
            }
            if (!(unit.Jacuzzi==originalUnit.Jacuzzi))
                pb_update.IsEnabled = true;

        }

        private void poolCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_poolCheckBox.IsChecked == true)//changed to true
                unit.Pool = Enums.Preference.Yes;
            else
            {
                if (cb_poolCheckBox.IsChecked == false)//changed to false
                    unit.Pool = Enums.Preference.No;
                else
                    unit.Pool = Enums.Preference.Maybe;//otherwise it's the third state
            }
            if (!(unit.Pool==originalUnit.Pool)) 
                pb_update.IsEnabled = true;

        }

        private void gardenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_gardenCheckBox.IsChecked == true)//changed to true
                unit.Garden = Enums.Preference.Yes;
            else
            {
                if (cb_gardenCheckBox.IsChecked == false)//changed to false
                    unit.Garden = Enums.Preference.No;
                else
                    unit.Garden = Enums.Preference.Maybe;//otherwise it's the third state
            }
            if (!(unit.Garden==originalUnit.Garden))
                pb_update.IsEnabled = true;

        }



        #endregion

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
            sortGuests();
        }


        private void Lb_addOrder_jaccuzi_Checked(object sender, RoutedEventArgs e)
        {
            sortGuests();
        }

        private void Lb_addOrder_pool_Checked(object sender, RoutedEventArgs e)
        {
            sortGuests();

        }

        private void Lb_addOrder_garden_Checked(object sender, RoutedEventArgs e)
        {
            sortGuests();

        }


        private void Cb_addorder_meal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sortGuests();
        }

        private void Tb_addOrder_adult_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tb_addOrder_adult.Text != "")//has text
                    addNum(tb_addOrder_adult.Text);//checks it's a valid number
                if (tb_addOrder_child.Text != "")
                    addNum(tb_addOrder_child.Text);//checks it's a valid number
                //if it's a valid number
                sortGuests();//searches. otherwise doesn't
            }
            catch (Exception ex)
            {
                //does nothing. only doesn't search
            }

            //only takes into condieration when it loses focus
        }
        private void Tb_addOrder_numLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_addOrder_adult.Text != "")//has text
                    addNum(tb_addOrder_adult.Text);//checks it's a valid number
                if (tb_addOrder_child.Text != "")
                    addNum(tb_addOrder_child.Text);//checks it's a valid number
                //if it's a valid number
                sortGuests();//searches. otherwise doesn't
            }
            catch (Exception ex)
            {
                if (ex is invalidTypeExceptionPL || ex is negativeNumberExceptionPL || ex is LargeNumberExceptionPL)//invalid number
                    MessageBox.Show("invalid number. Try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            sortGuests();
        }

        
        private void sortGuests()//searches through the guests based on the new selection
        {
            try
            {
                if (tb_addOrder_SearchTextBox != null)
                {
                    var guestList = myBL.GuestSearchQuery
                        (tb_addOrder_SearchTextBox.Text, tb_addOrder_child.Text, tb_addOrder_adult.Text, 
                        cb_addOrder_garden.IsChecked, cb_addOrder_jaccuzi.IsChecked, cb_addOrder_pool.IsChecked, 
                        cb_addorder_meal.SelectedIndex);
                    updateGuestsSource(g => guestList(g));//updates guests showing           
                }
            }
            catch(Exception ex)
            {
                if (! (ex is System.NullReferenceException))
                    MessageBox.Show("invalid search.", "error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }


        #endregion

        #region reset guest searches
        private void Pb_addorderReset_Click(object sender, RoutedEventArgs e)
        {
            //resets selections
            cb_addOrder_garden.IsChecked = null;
            cb_addOrder_jaccuzi.IsChecked = null;
            cb_addorder_meal.SelectedIndex = -1;
            cb_addOrder_pool.IsChecked = null;
            tb_addOrder_adult.Text = "";
            tb_addOrder_child.Text = "";
            tb_addOrder_SearchTextBox.Text = "";
            sortGuests();//resorts guests

        }

        #endregion
        #region bank
        private void initBank()
        {
            foreach (var bank in bankSource)
            {
                cb_bankName.Items.Add(bank.First().BankName);//adds key of each group to list
                cb_bankNumberTextBox.Items.Add(bank.First().BankNumber.ToString());
            }
            //give branches of bank
            foreach (var bank in bankSource)
            {

            }
                cb_bankName.SelectedIndex = bankSource.
                bankSource.Find(b=>unit.Host.Bank.BankNumber==b.First().BankNumber)
        }
        private void BankAcountNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

               if (Regex.IsMatch(bankAcountNumberTextBox.Text, ("^[0-9]$")))//only numbers
            {
                bankAcountNumberTextBox.BorderBrush = Brushes.Gray;
                unit.Host.Bank.BankAcountNumber = Convert.ToInt32(bankAcountNumberTextBox.Text);//sets new bank number
                if (unit.Host.Bank.BankAcountNumber != originalUnit.Host.Bank.BankAcountNumber)//if they're different
                    pb_update.IsEnabled = true;//enables button

            }
              else
            {
                bankAcountNumberTextBox.BorderBrush = Brushes.Red;
                bankAcountNumberTextBox.Text = "";
            }
        }
        private void BankAcountNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(bankAcountNumberTextBox.Text, ("^[0-9]$")))//only numbers
            {
                bankAcountNumberTextBox.BorderBrush = Brushes.Gray;
                
            }
            else
            {
                bankAcountNumberTextBox.BorderBrush = Brushes.Red;
                bankAcountNumberTextBox.Text = "";
            }
        }

        private void Cb_bankName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }


        #endregion
    }
}

