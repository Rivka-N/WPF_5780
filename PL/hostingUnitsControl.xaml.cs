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
    /// Interaction logic for hostingUnitsControl.xaml
    /// </summary>
    public partial class hostingUnitsControl : Window
    {
        private IBL myBL;
        HostingUnit hostingUnit1; 
        public hostingUnitsControl()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            hostingUnit1 = new HostingUnit();
//bind enums to comboboxes
            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();
            cb_hostingUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
        }

        private void Pb_delete_Click(object sender, RoutedEventArgs e)
        {
            
            //check unit exists
            //check no open orders connected to it
        }

        private void Pb_pastOrder_Click(object sender, RoutedEventArgs e)//shows all orders for this hosting unit
        {
            int unit;
            try
            {
                Int32.TryParse(tb_unitNumber.Text, out unit);
                MessageBoxResult mb=MessageBox.Show(myBL.printOrdersByUnit(unit));//prints all orders for this unit
            }
            catch(Exception exc)
            {
                MessageBoxResult mb = MessageBox.Show(exc.Message);//prints exception
                
            }
        }
    }
}
