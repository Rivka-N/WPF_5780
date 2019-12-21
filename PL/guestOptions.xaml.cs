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
        public guestOptions()
        {
            InitializeComponent();
            GuestRequest g1 = new GuestRequest();
            
        }

        private void cl_addStartDate(object sender, SelectionChangedEventArgs e)
        {
            #region endDate
            //cl_endDate starts from the day after start date
            //updates the statdate in guest
            //add field to guest to update date only if it's not set yet.
            //cl_EndDate.DisplayDateStart = tomorrow.AddDays(1);//sets the start date to tomorrow so the stay will be at least one day long
            cl_EndDate.Visibility = Visibility.Visible;//makes enddate visible to choose from now
            tb_EndDate.Visibility = Visibility.Visible;
            #endregion
            
        }
    }
}
