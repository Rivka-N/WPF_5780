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
    /// Interaction logic for hostingUnitTabs.xaml
    /// </summary>
    public partial class hostingUnitTabs : Window
    {
        IBL myBL;
        HostingUnit unit;
        List<Order> myOrders;
        List<Order> addOrders;
        public hostingUnitTabs(HostingUnit hosting)
        {
            myBL = BL.factoryBL.getBL();
            InitializeComponent();
            unit = hosting;
            //sets closed orders data grid source
            myOrders = myBL.getOrders(ord => ord.Status == Enums.OrderStatus.Closed && unit.HostingUnitKey==ord.HostingUnitKey);//sets source for orders
            dg_orderDataGrid.ItemsSource = myOrders;//all closed orders from this host

            //sets addOrders data grid source
            addOrders = myBL.getOrders(ord => ord.HostingUnitKey == unit.HostingUnitKey && (ord.Status == Enums.OrderStatus.Mailed || ord.Status == Enums.OrderStatus.Started));
            dg_addOrder.ItemsSource = addOrders;
        }
        #region windows events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new AllUnitsList().Show();//opens previous window again
        }
        #endregion
        #region tabs
        private void Tab_addOrders_Selected(object sender, RoutedEventArgs e)
        {
            //change closed orders
        }
        #endregion
        #region add order tab
        private void pb_addOrder_Click(object sender, RoutedEventArgs e)//what does this do?
        {
            try
            {
                if (dg_addOrder.SelectedItem != null && dg_addOrder.SelectedItem is Order)
                    new hostingUnitTabs((HostingUnit)dg_addOrder.SelectedItem).Show();
                else MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                //new window(send current unit)
            }
            catch
            {
                MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }//sends to unit information with data of current row to bind to



        private void orderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_addOrder.CurrentItem != null)//something was selected
            {
                Order row = (Order)dg_addOrder.SelectedItem;
                if (row.Status == Enums.OrderStatus.Started)
                    pb_sendMail.IsEnabled = true;
                if (row.Status == Enums.OrderStatus.Mailed)
                    pb_addOrder.IsEnabled = true;
            }
        }

        private void pb_sendMail_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        private void Pb_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();//closes window and returns to other window
        }
    }
}

