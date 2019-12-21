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
        }

        private void cl_addEntryDate(object sender, SelectionChangedEventArgs e)
        {
            g1.EntryDate = (DateTime)cl_EnterDate.SelectedDate;
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

        private void Tb_subAreaInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            g1.SubArea = tb_subArea.Text;
            //add check that sub area is in area
        }

        private void Cl_addEndDate(object sender, SelectionChangedEventArgs e)
        {
           g1.ReleaseDate = (DateTime)cl_LeaveDate.SelectedDate;//check not after start date
        }
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
                    g1.Garden= Enums.Preference.Maybe;//otherwise it's the third state
            }
        }
        private void CheckBox_attractionsChecked(object sender, RoutedEventArgs e)
        {
            if ( cb_attractions.IsChecked == true)//changed to true
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
        private void Continue_Clicked(object sender, RoutedEventArgs e)
        {
            if (g1.EntryDate<g1.ReleaseDate)//checks that dates are valid
            {
                bL.addGuest(g1);//adds it as guest
            }
            else
            {
                tb_StartDate.Background = Brushes.Red;
                tb_EndDate.Background = Brushes.Red;
                tb_StartDate.Text += "\n" + "תאריך כניסה ויציאה לא תקין";
            }

        }

     
    }
}
