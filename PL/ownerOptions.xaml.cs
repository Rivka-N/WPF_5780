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
using BL;
namespace PL
{
    /// <summary>
    /// Interaction logic for ownerOptions.xaml
    /// </summary>
    public partial class ownerOptions : Window
    {
        private IBL bL; 
        public ownerOptions()
        {
            InitializeComponent();
            bL=factoryBL.getBL();
        }
        #region button clicks
        private void Pb_Orders_Click(object sender, RoutedEventArgs e)
        {
            pb_Orders.BorderBrush = Brushes.DarkMagenta;
            pb_Orders.FontSize = 20;
            pb_Requests.BorderBrush = Brushes.Black;
            pb_Requests.FontSize = 12;
            pb_Units.FontSize = 12;
            pb_Units.BorderBrush = Brushes.Black;
            tb_printInfo.Text = "הזמנות" + "\n";

            List<BE.Order> orders = bL.getAllOrders();
            foreach(var ord in orders)
            {
                tb_printInfo.Text += ord+"\n";
            }
            
        }

        private void Pb_Requests_Click(object sender, RoutedEventArgs e)
        {
            pb_Orders.BorderBrush = Brushes.Black;
            pb_Orders.FontSize = 12;
            pb_Requests.BorderBrush = Brushes.DarkMagenta;
            pb_Requests.FontSize = 20;
            pb_Units.FontSize = 12;
            pb_Units.BorderBrush = Brushes.Black;
            tb_printInfo.Text = "בקשות אירוח" + "\n";
            var gRequests = bL.getRequests();
            foreach (var g in gRequests)
                tb_printInfo.Text += g+"\n";
        }

        private void Pb_Units_Click(object sender, RoutedEventArgs e)
        {
            pb_Orders.BorderBrush = Brushes.Black;
            pb_Orders.FontSize = 12;
            pb_Requests.BorderBrush = Brushes.Black;
            pb_Requests.FontSize = 12;
            pb_Units.FontSize = 20;
            pb_Units.BorderBrush = Brushes.DarkMagenta;
            //string s = "";
            var hostings = bL.getAllHostingUnits();
            //foreach (var h in hostings)
            //    s += h.ToString()+"\n";
            //tb_printInfo.Text = "יחידות אירוח" + "\n"+s;
            guestRequestListView.DataContext = hostings;
        }
        #endregion

        private void tb_noOrderReq_Click(object sender, RoutedEventArgs e)
        {
            pb_Orders.BorderBrush = Brushes.Black;
            pb_Orders.FontSize = 12;
            pb_Requests.BorderBrush = Brushes.Black;
            pb_Requests.FontSize = 12;
            pb_Units.FontSize = 12;
            pb_Units.BorderBrush = Brushes.DarkMagenta;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }

        private void guestRequestListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
