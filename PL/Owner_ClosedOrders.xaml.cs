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
    /// Interaction logic for Owner_ClosedOrders.xaml
    /// </summary>
    public partial class Owner_ClosedOrders : Page
    {
        IBL myBL;
        List<Order> myOrders;
            
        public Owner_ClosedOrders()
        {
            InitializeComponent();
            myBL = factoryBL.getBL();
            myOrders= myBL.getOrders(ord => ord.Status == Enums.OrderStatus.Closed);
            dg_orderDataGrid.ItemsSource= myOrders;//all closed orders
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)//resets date if changed

        {
            if (sender is DatePicker && dg_orderDataGrid.SelectedItem != null)
            {
                (sender as DatePicker).SelectedDate = (dg_orderDataGrid.SelectedItem as Order).CreateDate;

            }
        }

        private void Tb_SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                myOrders=myBL.searchOrders(dp_requestDate.SelectedDate, tb_SearchTextBox.Text, Enums.FunctionSender.Owner);
                dg_orderDataGrid.ItemsSource = myOrders;//updates data source
            }
            catch
            {
                MessageBox.Show("invalid query");
            }
        }

        private void searchDatePicker_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Tb_SearchTextBox_TextChanged(sender, null);//calls searchtextbox function

        }

        //when press clear button: dp_requestDate.SelectedDate = null;
        private void Dg_orderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
