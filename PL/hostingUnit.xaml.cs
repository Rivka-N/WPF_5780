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
using System.Text.RegularExpressions;
using BE;
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for hostingUnit.xaml
    /// </summary>
    public partial class hostingUnit : Window
    {
        private IBL myBL;
        HostingUnit hostingUnit1;
        int unit;
        int hostNum;
        public hostingUnit()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            hostingUnit1 = new HostingUnit();
            unit = -1;
            hostNum = -1;
            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();
            cb_hostingUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
        }


        private void Tb_email_TextChanged(object sender, TextChangedEventArgs e)
        {
            myBL.addMail(tb_email.Text, hostingUnit1.Host);

        }



        private void Cb_pool_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_pool.IsChecked == true)//changed to true
                hostingUnit1.Pool = Enums.Preference.Yes;
            else
            {
                hostingUnit1.Pool = Enums.Preference.No;
            }
        }

        private void Cb_garden_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_garden.IsChecked == true)//changed to true
                hostingUnit1.Garden = Enums.Preference.Yes;
            else
            {
                hostingUnit1.Garden = Enums.Preference.No;
            }
        }

        private void CheckBox_jaccuziChecked(object sender, RoutedEventArgs e)
        {
            if (cb_jaccuzi.IsChecked == true)//changed to true
                hostingUnit1.Jacuzzi = Enums.Preference.Yes;
            else
            {
                hostingUnit1.Jacuzzi = Enums.Preference.No;
            }
        }

        private void Cb_area_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hostingUnit1.AreaVacation = (Enums.Area)(cb_area.SelectedIndex);

        }

        private void Cb_hostingUnitType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hostingUnit1.HostingUnitType = (Enums.HostingUnitType)(cb_hostingUnitType.SelectedIndex);
        }

        private void Tb_unitName_TextChanged(object sender, TextChangedEventArgs e)
        {
            hostingUnit1.HostingUnitName = tb_unitName.Text;
        }

        private void water_got(object sender, TextChangedEventArgs e)
        {
            waterMarkedName.Visibility = System.Windows.Visibility.Collapsed;
            tb_name_txt.Visibility = System.Windows.Visibility.Visible;
            tb_name_txt.Focus();
        }


        private void userInput_lost(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_name_txt.Text))
            {
                tb_name_txt.Visibility = System.Windows.Visibility.Collapsed;
                waterMarkedName.Visibility = System.Windows.Visibility.Visible;
            }
        }



        private void tb_name_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            hostingUnit1.Host.Name = tb_name_txt.Text;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hostingUnit1.Host.Name = tb_last.Text;
        }

        private void tb_email_txt(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Tb_Enter_Child_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}