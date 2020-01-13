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
    /// Interaction logic for OwnerTabs.xaml
    /// </summary>
    public partial class OwnerTabs : Window
    {
        public OwnerTabs()
        {
            InitializeComponent();
        }
        #region window loading and closing
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

            private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MainWindow().Show();//opens main window again
        }
        #endregion

        private void Pb_back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
