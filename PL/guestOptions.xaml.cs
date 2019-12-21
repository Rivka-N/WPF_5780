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

namespace PL
{
    /// <summary>
    /// Interaction logic for guestOptions.xaml
    /// </summary>
    public partial class guestOptions : Window
    {
        GuestRequest g1 = new GuestRequest();

        public guestOptions()
        {
            InitializeComponent();
            
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

        private void CheckBox_jaccuziChecked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
