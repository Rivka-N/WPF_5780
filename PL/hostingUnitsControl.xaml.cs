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
    /// Interaction logic for hostingUnitsControl.xaml
    /// </summary>
    public partial class hostingUnitsControl : Window
    {
        HostingUnit hostingUnit1; 
        public hostingUnitsControl()
        {
            InitializeComponent();
            hostingUnit1 = new HostingUnit();
        }

        private void Pb_delete_Click(object sender, RoutedEventArgs e)
        {
            //check no open orders connected to it
        }
    }
}
