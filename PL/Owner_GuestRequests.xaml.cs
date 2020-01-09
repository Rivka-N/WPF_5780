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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;
namespace PL
{
    /// <summary>
    /// Interaction logic for Owner_GuestRequests.xaml
    /// </summary>
    public partial class Owner_GuestRequests : Page
    {
        IBL myBL;
        public Owner_GuestRequests()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            ds_guestRequestDataGrid.ItemsSource = myBL.getRequests();//binds data
            
        }

      
        private void Dp_Registration_SelectedDateChanged(object sender, SelectionChangedEventArgs e)//changes date back to original date selected
        {
            if (sender is DatePicker && ds_guestRequestDataGrid.SelectedItem!=null)
            {
                (sender as DatePicker).SelectedDate = (ds_guestRequestDataGrid.SelectedItem as GuestRequest).Registration;

            }
        }
    }
}
