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
            //show all hosting units: binding: bL.getAllHostingUnits()
        }

        private void Pb_Orders_Click(object sender, RoutedEventArgs e)
        {
            pb_Orders.BorderBrush = Brushes.DarkMagenta;
            pb_Orders.FontSize = 20;
            pb_Requests.BorderBrush = Brushes.Black;
            pb_Requests.FontSize = 12;
            pb_Units.FontSize = 12;
            pb_Units.BorderBrush = Brushes.Black;
           string s = "";
            var hostings = bL.getAllHostingUnits();
            foreach (var h in hostings)
                s += h.ToString();
            tb_printInfo.Text = "הזמנות" + "\n"+s;
            
            //add applicable toString(). tb_printInfo.Text=
        }

        private void Pb_Requests_Click(object sender, RoutedEventArgs e)
        {
            pb_Orders.BorderBrush = Brushes.Black;
            pb_Orders.FontSize = 12;
            pb_Requests.BorderBrush = Brushes.DarkMagenta;
            pb_Requests.FontSize = 20;
            pb_Units.FontSize = 12;
            pb_Units.BorderBrush = Brushes.Black;
            //applicable text
        }

        private void Pb_Units_Click(object sender, RoutedEventArgs e)
        {
            pb_Orders.BorderBrush = Brushes.Black;
            pb_Orders.FontSize = 12;
            pb_Requests.BorderBrush = Brushes.Black;
            pb_Requests.FontSize = 12;
            pb_Units.FontSize = 20;
            pb_Units.BorderBrush = Brushes.DarkMagenta;
            //applicable text

        }
    }
}
