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
    /// Interaction logic for guestOptions.xaml
    /// </summary>
    public partial class guestOptions : Window
    {
        private IBL bL;
        private GuestRequest g1 = new GuestRequest();

        public guestOptions()
        {
            InitializeComponent();
            bL = factoryBL.getBL();
            //cb_hostingUnitType.DataContext = Enum.GetValues(Enums.Area);
            //cb_hostingUnitType.ItemsSource = Enum.GetValues(Enums.Area);            
            //cb_hostingUnitType.DataContext = bL.getAllHostingUnits();
            //cb_area.DataContext = Enums.Area;
        }

        #region dates
        private void cl_addEntryDate(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bL.addEntryDate(cl_EnterDate.SelectedDate, g1);
                //tb_EndDate.Text = "בחר תאריך התחלה";
                tb_StartDate.Text = "תאריך כניסה תקין";
            }
            catch
            {
                //cl_EnterDate.SelectedDate = DateTime.Now;
                tb_StartDate.Text = "תאריך כניסה ויציאה לא תואמים. הכנס תאריכים חדשים";
            }
            //g1.EntryDate = (DateTime)cl_EnterDate.SelectedDate;
            #region endDate
            //cl_endDate starts from the day after start date
            //updates the statdate in guest
            //add field to guest to update date only if it's not set yet.
            //cl_EndDate.DisplayDateStart = tomorrow.AddDays(1);//sets the start date to tomorrow so the stay will be at least one day long

            //change enddate visibility: works
            //cl_EndDate.Visibility = Visibility.Visible;//makes enddate visible to choose from now
            //tb_EndDate.Visibility = Visibility.Visible;
            #endregion

        }
        private void Cl_addEndDate(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bL.addReleaseDate(cl_LeaveDate.SelectedDate, g1);
                tb_EndDate.Text = "תאריך יציאה תקין";

            }
            catch
            {

                tb_EndDate.Text = "תאריך כניסה ויציאה לא תואמים. הכנס תאריכים חדשים";

            }
            //g1.ReleaseDate = (DateTime)cl_LeaveDate.SelectedDate;//check not after start date
        }
        #endregion

        private void Tb_subAreaInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            g1.SubArea = tb_subArea.Text;
            //add check that sub area is in area
        }

        private void Continue_Clicked(object sender, RoutedEventArgs e)
        {
            if (g1.EntryDate < g1.ReleaseDate)//checks that dates are valid. also check that num of people is valid
            {
                bL.addGuest(g1);//adds it as guest
                tb_StartDate.Background = Brushes.White;
                tb_EndDate.Background = Brushes.White;
                //pb_continue.Content = "\n" + "בקשתך התקבלה";
                if (g1.NumAdult == 0 && g1.NumChildren == 0)//no guests
                {
                    tb_Enter_Adults.Background = Brushes.OrangeRed;
                    tb_Enter_Child.Background = Brushes.Orange;


                }
                else
                {
                    var foundUnits = bL.findUnit(bL.getAllHostingUnits(), g1);
                    string s = "";
                    foreach (HostingUnit hu in foundUnits)
                        s += hu.ToString();
                    MessageBoxResult mb_completed = MessageBox.Show("units founds\n" + s);
                    Close();
                }
            }

            if (!(g1.EntryDate < g1.ReleaseDate))
            {
                tb_StartDate.Background = Brushes.Red;
                tb_EndDate.Background = Brushes.Red;
                tb_StartDate.Text += "\n" + "תאריך כניסה ויציאה לא תקין";//fix after they fix
            }


        }
        #region num people
        private void Tb_Enter_Adults_TextChanged(object sender, TextChangedEventArgs e)
        {
            int text = 0;
            if (Int32.TryParse(tb_Enter_Adults.Text, out text))
            {
                if (text < 0)
                {
                    tb_Enter_Adults.Background = Brushes.OrangeRed;
                    tb_Enter_Adults.Text = "";
                }
                else
                {
                    g1.NumAdult = text;
                    tb_Enter_Adults.Background = Brushes.White;
                }
            }
            else
            {

                tb_Enter_Adults.Background = Brushes.OrangeRed;
                tb_Enter_Adults.Text = "";
            }
        }


        private void Tb_Enter_Child_TextChanged(object sender, TextChangedEventArgs e)
        {
            int text = 0;
            if (Int32.TryParse(tb_Enter_Child.Text, out text))
            {
                if (text < 0)
                {
                    tb_Enter_Child.Background = Brushes.OrangeRed;
                    tb_Enter_Child.Text = "";
                }

                else
                {
                    g1.NumChildren = text;
                    tb_Enter_Child.Background = Brushes.White;
                }
            }
            else
            {
                tb_Enter_Child.Background = Brushes.OrangeRed;
                tb_Enter_Child.Text = "";
            }
        }
        #endregion
        #region checkboxes
        private void Cb_pool_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_pool.IsChecked == true)//changed to true
                g1.Pool = Enums.Preference.Yes;
            else
            {
                if (cb_pool.IsChecked == false)//changed to false
                    g1.Pool = Enums.Preference.No;
                else
                    g1.Pool = Enums.Preference.Maybe;//otherwise it's the third state
            }
        }
        private void Cb_garden_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_garden.IsChecked == true)//changed to true
                g1.Garden = Enums.Preference.Yes;
            else
            {
                if (cb_garden.IsChecked == false)//changed to false
                    g1.Garden = Enums.Preference.No;
                else
                    g1.Garden = Enums.Preference.Maybe;//otherwise it's the third state
            }
        }
        private void CheckBox_attractionsChecked(object sender, RoutedEventArgs e)
        {
            if (cb_attractions.IsChecked == true)//changed to true
                g1.ChildrenAttractions = Enums.Preference.Yes;
            else
            {
                if (cb_attractions.IsChecked == false)//changed to false
                    g1.ChildrenAttractions = Enums.Preference.No;
                else
                    g1.ChildrenAttractions = Enums.Preference.Maybe;//otherwise it's the third state
            }
        }
        private void CheckBox_jaccuziChecked(object sender, RoutedEventArgs e)
        {
            if (cb_jaccuzi.IsChecked == true)//changed to true
                g1.Jacuzzi = Enums.Preference.Yes;
            else
            {
                if (cb_jaccuzi.IsChecked == false)//changed to false
                    g1.Jacuzzi = Enums.Preference.No;
                else
                    g1.Jacuzzi = Enums.Preference.Maybe;//otherwise it's the third state
            }
        }
        #endregion

        #region name and email text changed
        private void Tb_email_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bL.addMail(tb_email.Text, g1);
            }
            catch
            {
                tb_email_txt.Text = "כתובת מייל לא תקין.\n הכנס מייל";
            }
        }

        private void Tb_last_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(tb_last.Text, @"^[a-zA-Z]+$"))
            {
                g1.LastName = tb_last.Text;
            }
            else
            { tb_last.Text = "";
                tb_last.Background = Brushes.Red;
            }
        }

        private void Tb_first_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(tb_first.Text, @"^[a-zA-Z]+$"))
            {
                g1.Name = tb_first.Text;
            }
            else
            {
                tb_first.Text = "";
                tb_first.Background = Brushes.Red;
            }
        }
        #endregion
    }
}
