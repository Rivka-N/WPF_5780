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
    /// Interaction logic for Owner_HostingUnits.xaml
    /// </summary>
    public partial class Owner_HostingUnits : Page
    {
        IBL myBL;
        List<HostingUnit> unitsList;
        public Owner_HostingUnits()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            unitsList = myBL.getAllHostingUnits();
            dg_hostingUnitDataGrid.ItemsSource = unitsList;//binds to hosting units
        }

        private void tb_SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            unitsList = myBL.searchUnits(tb_SearchTextBox.Text);
            dg_hostingUnitDataGrid.ItemsSource = unitsList;
        }
    }
}
