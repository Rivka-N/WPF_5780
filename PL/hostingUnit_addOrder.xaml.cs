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
    /// Interaction logic for hostingUnit_addOrder.xaml
    /// </summary>
    public partial class hostingUnit_addOrder : Window
    {
        public hostingUnit_addOrder()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // orderViewSource.Source = [generic data source]
        }

        private void orderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void orderDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void orderDataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
