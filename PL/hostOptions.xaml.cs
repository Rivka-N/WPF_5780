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

namespace PL
{
    /// <summary>
    /// Interaction logic for hostOptions.xaml
    /// </summary>
    public partial class hostOptions : Window
    {
        public hostOptions()
        {
            InitializeComponent();
        }

        private void pb_order_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void Pb_addUnit_Click(object sender, RoutedEventArgs e)
        {
            addUnitOptions addUnitWindow = new addUnitOptions();
            addUnitWindow.Show();
            //add window and unit adding control
        }

        private void Pb_changeUnit_Click(object sender, RoutedEventArgs e)
        {
            hostingUnitsControl hostingWindows = new hostingUnitsControl();
            hostingWindows.Show();
        }
    }
}
