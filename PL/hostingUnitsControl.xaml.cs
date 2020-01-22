//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using System.Text.RegularExpressions;
//using BE;
//using BL;

//namespace PL
//{
//    /// <summary>
//    /// Interaction logic for hostingUnitsControl.xaml
//    /// </summary>
//    public partial class hostingUnitsControl : Window
//    {
//        private IBL myBL;
//        HostingUnit hostingUnit1;
//        int unit;
//        int hostNum;
//        public hostingUnitsControl()
//        {
//            myBL = factoryBL.getBL();
//            InitializeComponent();
//            hostingUnit1 = new HostingUnit();
//            unit = -1;
//            hostNum = -1;
//            invisible();//hides fields so they can't be changed yet
////bind enums to comboboxes
//            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();
//            cb_hostingUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
//        }
//        #region button clicks
//        private void Pb_delete_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                if (unit != -1)//unit number was entered
//                {
//                    myBL.deleteUnit(unit);
//                    MessageBoxResult mb = MessageBox.Show("יחידה נמחקה");//confirms delete

//                }

//                else
//                {
//                    tb_unitNumber_txt.Background = Brushes.Red;
//                }
//            }
//            catch (Exception exc)
//            {
//                MessageBoxResult mb = MessageBox.Show(exc.Message);//prints exception
//                tb_unitNumber_txt.Background= Brushes.Red;
//            }

//        }

//        private void Pb_pastOrder_Click(object sender, RoutedEventArgs e)//shows all orders for this hosting unit
//        {
//            try
//            { if (unit != -1)

//                {
//                    String s = myBL.printOrdersByUnit(unit);
//                    if (s == "")//no units added
//                    {
//                        MessageBoxResult mb = MessageBox.Show("אין הזמנות ביחידה זו");//prints all orders for this unit
//                    }
//                    else
//                    {
//                        MessageBoxResult mb = MessageBox.Show(s);//prints all orders for this unit
//                    }
//                }
//            else
//                    tb_unitNumber_txt.Background = Brushes.Red;

//            }
//            catch (Exception exc)
//            {
//                MessageBoxResult mb = MessageBox.Show(exc.Message);//prints exception

//            }
//        }

//        private void Pb_update_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                if (unit != -1)
//                {
//                    {
//                        myBL.changeUnit(hostingUnit1);
//                    }

//                }
//                else
//                    tb_unitNumber_txt.Background = Brushes.Red;
//            }
//            catch (Exception exc)
//            {
//                MessageBoxResult mb = MessageBox.Show(exc.Message);//prints exception

//            }
//        }
//        #endregion
//        #region unit and id number

//        private void Tb_unitNumber_TextChanged(object sender, TextChangedEventArgs e)
//        {
//            try
//            {
//                Int32.TryParse(tb_unitNumber.Text, out unit);
//                hostingUnit1 = myBL.findUnit(unit);//sets unit
//                tb_unitNumber.Background = Brushes.White;
//                if (hostNum!=-1)//if hostnum was set
//                {
//                    if (!myBL.sameUnit(hostingUnit1, hostNum))

//                    {
//                        tb_unitNumber_txt.Text = "הכנס מספר יחידת אירוח\n" + "מספר לא תואם לקוד מארח";
//                        tb_unitNumber_txt.Background = Brushes.Red;
//                    }
//                    else
//                    {
//                        visible();//makes other elements visible
//                        tb_unitNumber_txt.Text = "מספר יחידת אירוח";
//                        tb_unitNumber_txt.Background = Brushes.White;
//                        tb_id_txt.Text = "מספר מארח";
//                        tb_id_txt.Background = Brushes.White;
//                    }

//                }
//            }
//            catch(Exception ex)
//            {
//                tb_unitNumber.Background = Brushes.Red;
//                tb_unitNumber_txt.Text = "הכנס מספר יחידה" + ex.Message;
//            }

//        }
//#endregion
//        #region checkboxes
//        private void Cb_pool_Checked(object sender, RoutedEventArgs e)
//        {
//            if (cb_pool.IsChecked == true)//changed to true
//                hostingUnit1.Pool = Enums.Preference.Yes;
//            else
//            {   
//                    hostingUnit1.Pool = Enums.Preference.No;
//             }
//        }
//        private void Cb_garden_Checked(object sender, RoutedEventArgs e)
//        {
//            if (cb_garden.IsChecked == true)//changed to true
//                hostingUnit1.Garden = Enums.Preference.Yes;
//            else
//            {
//                hostingUnit1.Garden = Enums.Preference.No;
//            }
//        }

//        private void CheckBox_jaccuziChecked(object sender, RoutedEventArgs e)
//        {
//            if (cb_jaccuzi.IsChecked == true)//changed to true
//                hostingUnit1.Jacuzzi = Enums.Preference.Yes;
//            else
//            {
//               hostingUnit1.Jacuzzi = Enums.Preference.No;
//            }
//        }
//        #endregion




//        #endregion
//        #region comboboxes
//        private void Cb_area_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            tb_area.Background = Brushes.White;
//            hostingUnit1.AreaVacation = (Enums.Area)(cb_area.SelectedIndex);

//        }

//        private void Cb_hostingUnitType_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            tb_unitType.Background = Brushes.White;
//            hostingUnit1.HostingUnitType = (Enums.HostingUnitType)(cb_hostingUnitType.SelectedIndex);
//        }
//        #endregion
//    }
//}
