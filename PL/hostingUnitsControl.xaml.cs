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
        int unit;

        public hostingUnitsControl()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            hostingUnit1 = new HostingUnit();
            unit = -1;
//bind enums to comboboxes
            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();
            cb_hostingUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
        }
        #region button clicks
        private void Pb_delete_Click(object sender, RoutedEventArgs e)
        {
            //int unit;
            try
            {
                if (unit != -1)//unit number was entered
                {
                    myBL.deleteUnit(unit);
                    MessageBoxResult mb = MessageBox.Show("יחידה נמחקה");//confirms delete

                }
                
                else
                {
                    tb_unitNumber_txt.Background = Brushes.Red;
                }
            }
            catch (Exception exc)
            {
                MessageBoxResult mb = MessageBox.Show(exc.Message);//prints exception
                tb_unitNumber_txt.Background= Brushes.Red;
            }
          
        }

        private void Pb_pastOrder_Click(object sender, RoutedEventArgs e)//shows all orders for this hosting unit
        {
            try
            { if (unit != -1)

                {
                    String s = myBL.printOrdersByUnit(unit);
                    if (s == "")//no units added
                    {
                        MessageBoxResult mb = MessageBox.Show("אין הזמנות ביחידה זו");//prints all orders for this unit
                    }
                    else
                    {
                        MessageBoxResult mb = MessageBox.Show(s);//prints all orders for this unit
                    }
                }
            else
                    tb_unitNumber_txt.Background = Brushes.Red;

            }
            catch (Exception exc)
            {
                MessageBoxResult mb = MessageBox.Show(exc.Message);//prints exception
                
            }
        }
        #endregion
        #region set details
        private void Tb_unitNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Int32.TryParse(tb_unitNumber.Text, out unit);
                hostingUnit1.HostingUnitKey = unit;
                tb_unitNumber.Background = Brushes.White;
            }
            catch
            {
                tb_unitNumber.Background = Brushes.Red;
            }
           
        }
        #endregion
    }
}
