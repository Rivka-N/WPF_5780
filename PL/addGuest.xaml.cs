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
    /// Interaction logic for addGuest.xaml
    /// </summary>
    public partial class addGuest : Window
    {
        public addGuest()
        {
            InitializeComponent();
            cb_hostingUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();
            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.MealType)).Cast<Enums.MealType>();


        }

        #region window
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MainWindow().Show();
        }
        #endregion
    }
}
