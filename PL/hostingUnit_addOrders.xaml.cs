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
    /// Interaction logic for hostingUnit_addOrders.xaml
    /// </summary>
    public partial class hostingUnit_addOrders : Page
    {
        IBL myBL;
        List<Order> myOrders;

        public hostingUnit_addOrders()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            string Name = "esti";
            myOrders = myBL.getOrders(ord => ord.HostName == Name);//bring only this hosting unit's closed orders
            orderDataGrid.ItemsSource = myOrders;
            foreach(Order ord in myOrders)
            {
                if(ord.Status == Enums.OrderStatus.Started)
                {
                    
                }
            }

           // myOrders = myBL.getOrders(ord => ord.HostName == this.Name);



        }
     
        private void Pb_back_Click(object sender, RoutedEventArgs e)
        {

        }

        
        private void pb_addOrder_Click(object sender, RoutedEventArgs e)
        {
            if (orderDataGrid.SelectedItem != null && orderDataGrid.SelectedItem is Order)
                new hostingUnitTabs((HostingUnit)orderDataGrid.SelectedItem).Show();
            else MessageBox.Show("error! Please try again", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            //new window(send current unit)
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }//sends to unit information with data of current row to bind to



        private void orderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orderDataGrid.CurrentItem != null)//something was selected
            {
                Order row = (Order)orderDataGrid.SelectedItem;
                if (row.Status == Enums.OrderStatus.Started)
                    pb_sendMail.IsEnabled = true;
                if (row.Status == Enums.OrderStatus.Mailed)
                    pb_addOrder.IsEnabled = true;
            }
        }

        private void pb_sendMail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
