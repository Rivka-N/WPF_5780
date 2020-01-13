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
    /// Interaction logic for AllUnitsList.xaml
    /// </summary>
    public partial class AllUnitsList : Window
    {
        private IBL myBL;
        public AllUnitsList()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            dg_hostingUnitDataGrid.ItemsSource = myBL.getAllHostingUnits();
        }
        #region window

      

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MainWindow().Show();//opens main window again
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }
        #endregion
        #region buttons
        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            new hostingUnit().ShowDialog();
        }
        #endregion
    }
}
