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
        List<GuestRequest> myRequests;
        public Owner_GuestRequests()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            myRequests = new List<GuestRequest>();
            myRequests = myBL.getRequests();//sets binding data to requests
            ds_guestRequestDataGrid.ItemsSource = myRequests;//binds data
            var add = Enum.GetValues(typeof(Enums.OrderStatus));
            cb_status.Items.Add("All");
            foreach (Enums.OrderStatus item in add)
                cb_status.Items.Add(item);//adds all statuses to combobox options
            }
      
        private void Dp_Registration_SelectedDateChanged(object sender, SelectionChangedEventArgs e)//changes date back to original date selected
        {
            if (sender is DatePicker && ds_guestRequestDataGrid.SelectedItem!=null)
            {
                (sender as DatePicker).SelectedDate = (ds_guestRequestDataGrid.SelectedItem as GuestRequest).Registration;

            }
        }

        #region searches
        private void Cb_status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tb_SearchTextBox_TextChanged(sender, null);
        }

        private void Tb_SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cb_status.SelectedIndex == (int)Enums.OrderStatus.Closed + 1)//filter from closed orders
                myRequests = myBL.searchRequests(Enums.OrderStatus.Closed, tb_SearchTextBox.Text, Enums.FunctionSender.Owner);
            else
            {
                if (cb_status.SelectedIndex == (int)Enums.OrderStatus.Mailed + 1)
                    myRequests = myBL.searchRequests(Enums.OrderStatus.Mailed, tb_SearchTextBox.Text, Enums.FunctionSender.Owner);
                else
                {
                    if (cb_status.SelectedIndex == (int)Enums.OrderStatus.Started + 1)
                        myRequests = myBL.searchRequests(Enums.OrderStatus.Started, tb_SearchTextBox.Text, Enums.FunctionSender.Owner);
                    else
                        myRequests = myBL.searchRequests(tb_SearchTextBox.Text, Enums.FunctionSender.Owner);
                }
            }
            ds_guestRequestDataGrid.ItemsSource = myRequests;//why doesn't reload content after erasing searchbox?
        }
        #endregion
    }
}
