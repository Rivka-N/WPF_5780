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
            #region sets combobox
            var addArea = Enum.GetValues(typeof(Enums.Area));
            var addType = Enum.GetValues(typeof(Enums.MealType));
            cb_area.Items.Add("All");
            cb_unitType.Items.Add("All");
            foreach (Enums.Area item in addArea)
                cb_area.Items.Add(item);//adds all statuses to combobox options 
            foreach (Enums.HostingUnitType item in addType)
                cb_unitType.Items.Add(item);
            #endregion
        }
  
        #region buttons
        private void Pb_changeUnit_Click(object sender, RoutedEventArgs e)//needs to show change unit tab with data of current unit
        {
            //new window(send current unit)?
            
        }

        private void Pb_bankInfo_Click(object sender, RoutedEventArgs e)
            //bank page tab with option to change information or update collection clearance. send data of current unit to bind to
        {

        }

        private void AddUnit_Click(object sender, RoutedEventArgs e)//make sure unit gets added to list
        {
            new hostingUnit().ShowDialog();
        }

        private void Pb_back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
        #region click events
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }//sends to unit information with data of current row to bind to

        private void Dg_hostingUnitDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_hostingUnitDataGrid.CurrentItem != null)//something was selected
            {
                pb_changeUnit.IsEnabled = true;
                pb_bankInfo.IsEnabled = true;//allows button clicks
            }
            else
            {
                pb_bankInfo.IsEnabled = false;
                pb_changeUnit.IsEnabled = false;//otherwise disables them
            }
        }
        #endregion
        #region searches
        private void search()//filters items in list
        {
            try
            {
                dg_hostingUnitDataGrid.ItemsSource = myBL.searchUnits(tb_search.Text, cb_unitType.SelectedIndex - 1, cb_area.SelectedIndex - 1, Enums.FunctionSender.HostList);
                //sets items to new selection
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Cb_unitType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            search();//sends to search function
        }

        private void Cb_area_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            search();
        }
        private void Pb_reset_Click(object sender, RoutedEventArgs e)
        {
            cb_unitType.SelectedIndex = 0;
            cb_area.SelectedIndex = 0;
            tb_search.Text = "";
            search();//refilters items
        }

        private void Tb_SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            search();
        }
        #endregion
        #region window

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MainWindow().Show();//opens main window again
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }
        #endregion

       
    }
}
