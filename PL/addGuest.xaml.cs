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
    /// Interaction logic for addGuest.xaml
    /// </summary>
    public partial class addGuest : Window
    {
        GuestRequest g1;
        IBL bl;
        #region ctor
        public addGuest()
        {
            InitializeComponent();
            g1 = new GuestRequest();//new guest request
            bl = factoryBL.getBL();//sets bl

            //sets combobox values
            cb_hostingUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();
            cb_meal.ItemsSource = Enum.GetValues(typeof(Enums.MealType)).Cast<Enums.MealType>();
            dg_guest.DataContext = g1;

            //allow dates to choose from
            dp_entryDateDatePicker.DisplayDateStart = DateTime.Today;//only allows to pick dates in the future or today
            dp_releaseDateDatePicker.DisplayDateStart = DateTime.Today;
            dp_entryDateDatePicker.DisplayDateEnd = DateTime.Today.AddYears(1);//last date is a year from now
            dp_releaseDateDatePicker.DisplayDateEnd = DateTime.Today.AddYears(1);

            //set g1 default values of checkboxes and meal
            g1.Meal = Enums.MealType.None;
            g1.Garden = Enums.Preference.Maybe;
            g1.Pool = Enums.Preference.Maybe;
            g1.Jacuzzi = Enums.Preference.Maybe;

        }
        #endregion
        #region window
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MainWindow().Show();
        }
        #endregion

        #region num people

        private void tb_enterAdult_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                g1.NumAdult = addNum(tb_enterAdult.Text);//checks this is a valid number. -1 if not
                tb_enterAdult.BorderBrush = Brushes.Black;
                checkButton();//sends to function to check if add button should be allowed

            }
            catch (Exception ex)
            {
                tb_enterAdult.Text = "";
                tb_enterAdult.BorderBrush = Brushes.Red;//colors border
                if (ex is LargeNumberExceptionPL)
                    MessageBox.Show(ex.Message);//if the number was too big, explains why number wasn't valid

            }
        }

        private void tb_enterChildren_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                g1.NumChildren = addNum(tb_enterChildren.Text);//checks this is a valid number. -1 if not
                tb_enterChildren.BorderBrush = Brushes.Black;
                checkButton();//sends to function to check if add button should be allowed

            }
            catch (Exception ex)
            {
                tb_enterChildren.BorderBrush = Brushes.Red;//colors border
                tb_enterChildren.Text = "";
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
        #region button
        private void Pb_back_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit without placing ordering?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Close();
            //otherwise does nothing
        }


        private void Pb_add_Click(object sender, RoutedEventArgs e)//check fields again
        {
            if (Regex.IsMatch(tb_nameTextBox.Text, @"^[\p{L}]+$"))//first contains only letters
            {
                if (Regex.IsMatch(tb_lastNameTextBox.Text, @"^[\p{L}]+$"))//last contains only letters
                {
                    if (g1.Mail != null)//recieved a mail address
                    {
                        if (g1.EntryDate >= DateTime.Today && g1.ReleaseDate >= DateTime.Today)//dates are chosen
                        {
                            if (cb_area.SelectedIndex != -1)
                            {
                                    if (cb_hostingUnitType.SelectedIndex != -1)
                                    {
                                        if (g1.NumAdult > 0 && g1.NumChildren > 0)//checks there are people
                                            bl.addGuest(g1); //if it's all valid, adsds guest
                                        else MessageBox.Show("Number of people can't be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                    else MessageBox.Show("Select unit type", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else MessageBox.Show("Select vacation area", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                            }

                            else MessageBox.Show("Please enter dates of stay", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("Please enter email address", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            tb_mail.BorderBrush = Brushes.Red;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter last name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        tb_lastNameTextBox.BorderBrush = Brushes.Red;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter first name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tb_nameTextBox.BorderBrush = Brushes.Red;
                }
         }

            #endregion
            #region datepicked
            private void dp_DateSelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                if (dp_entryDateDatePicker.SelectedDate != null && dp_releaseDateDatePicker.SelectedDate != null)//both dates are selected
                {
                    if (dp_entryDateDatePicker.SelectedDate < dp_releaseDateDatePicker.SelectedDate)//if release date is after entry date
                    {
                        g1.EntryDate = (DateTime)dp_entryDateDatePicker.SelectedDate;
                        g1.ReleaseDate = (DateTime)dp_releaseDateDatePicker.SelectedDate;//sets dates
                        checkButton();//checks if it can enable add button
                    }
                    else//invalid dates
                        MessageBox.Show("vacation start must be at least 1 day after vacation end", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        


        #endregion
        #region check allowing add
        private void checkButton()  //checks all fields are valid and allows button

        {
            //if (tb_nameTextBox != null)
            //{
            //    if (tb_lastNameTextBox != null)//name was entered
            //    {
            //        if (g1.Mail != null)//recieved a mail address
            //        {
            //            if (g1.EntryDate >= DateTime.Today && g1.ReleaseDate >= DateTime.Today)//dates are chosen
            //            {
            //                if (cb_area.SelectedIndex != -1 && cb_meal.SelectedIndex != -1 && cb_hostingUnitType.SelectedIndex != -1)

            //                    if (g1.NumAdult > 0 && g1.NumChildren > 0)//checks there are people
            //                {
            //                    pb_add.IsEnabled = true;
            //                }
            //            }
            //        }

            //    }

            //}

        }
        #endregion
        #region check name and email
        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(tb_nameTextBox.Text, @"^[\p{L}]+$"))//contains only letters
            {
                g1.Name = tb_nameTextBox.Text;//sets first name
                tb_nameTextBox.BorderBrush = Brushes.Black;//resets border
            }
            else
            {
                tb_nameTextBox.BorderBrush = Brushes.Red;
            }

        }
        private void Tb_lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(tb_lastNameTextBox.Text, @"^[\p{L}]+$"))//contains only letters
            {
                g1.Name = tb_lastNameTextBox.Text;//sets first name
                tb_lastNameTextBox.BorderBrush = Brushes.Black;//resets border
            }
            else
            {
                tb_lastNameTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void Tb_mail_MouseLeave(object sender, MouseEventArgs e)
        {
            //checks it's valid mail
            if (tb_mail.Text!="")//not an empty textbox
            {
                tb_mail.Text = "";
                tb_mail.BorderBrush = Brushes.Red;
            }
        }

        private void Tb_nameTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!Regex.IsMatch(tb_nameTextBox.Text, @"^[\p{L}]+$"))//not contains only letters
            {
                if (tb_nameTextBox.Text != "")//not an empty textbox 
                {
                    tb_nameTextBox.Text = "";
                    tb_nameTextBox.BorderBrush = Brushes.Red;
                }
            }
        }

        private void Tb_lastNameTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!Regex.IsMatch(tb_lastNameTextBox.Text, @"^[\p{L}]+$"))
            {
                if (tb_lastNameTextBox.Text != "")//not an empty textbox
                {
                    tb_lastNameTextBox.BorderBrush = Brushes.Red;
                    tb_lastNameTextBox.Text = "";
                }
            }
        }

        #endregion
        #region checkboxes
        private void Cb_poolCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_poolCheckBox.IsChecked == true)//checked
                g1.Pool = Enums.Preference.Yes;
            else
            {
                if (cb_poolCheckBox.IsChecked == false)//not checked
                    g1.Pool = Enums.Preference.No;


                else
                    g1.Pool = Enums.Preference.Maybe;
            }


        }

        private void Cb_jacuzziCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_jacuzziCheckBox.IsChecked == true)//checked
                g1.Jacuzzi = Enums.Preference.Yes;
            else
            {
                if (cb_poolCheckBox.IsChecked == false)//checked
                    g1.Jacuzzi = Enums.Preference.No;
                else
                    g1.Jacuzzi = Enums.Preference.Maybe;
            }
        }

        private void Cb_gardenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_gardenCheckBox.IsChecked == true)//checked
                g1.Garden = Enums.Preference.Yes;
            else
            {
                if (cb_gardenCheckBox.IsChecked == false)//checked
                    g1.Garden = Enums.Preference.No;
                else
                    g1.Garden = Enums.Preference.Maybe;
            }
        }
        #endregion

        private void Cb_meal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_area.SelectedIndex = 2;
        }
    }
}

