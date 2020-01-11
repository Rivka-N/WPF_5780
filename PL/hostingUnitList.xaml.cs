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
using BE;

namespace PL
{
    /// <summary>
    /// Interaction logic for hostingUnitList.xaml
    /// </summary>
    public partial class hostingUnitList : Window
    {


        IBL myBL;

        public hostingUnitList()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            dataGrid.ItemsSource = myBL.getRequests();//binds data

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]
        }

        private void hostingUnitDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void hostingUnitDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addUnit_Click(object sender, RoutedEventArgs e)
        {
            hostingUnitsControl hostingUnitWindow = new hostingUnitsControl();
            hostingUnitWindow.Show();

        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void hostingUnitDataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
