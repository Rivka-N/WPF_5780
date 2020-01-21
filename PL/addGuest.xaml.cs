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
    /// Interaction logic for addGuest.xaml
    /// </summary>
    public partial class addGuest : Window
    {
        GuestRequest g1;
        public addGuest()
        {
            InitializeComponent();
             g1 = new GuestRequest();//new guest request
            cb_hostingUnitType.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType)).Cast<Enums.HostingUnitType>();
            cb_area.ItemsSource = Enum.GetValues(typeof(Enums.Area)).Cast<Enums.Area>();
            cb_meal.ItemsSource = Enum.GetValues(typeof(Enums.MealType)).Cast<Enums.MealType>();


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

        #region num people

        private void tb_enterAdult_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            try
            {
                g1.NumAdult=addNum(tb_enterAdult.Text);//checks this is a valid number. -1 if not
                tb_enterAdult.BorderBrush = Brushes.Black;

            }
            catch(Exception ex)
            {
                tb_enterAdult.BorderBrush = Brushes.Red;//colors border
                tb_enterAdult.Text = "";//resets text
                if (ex is LargeNumberExceptionPL)
                    MessageBox.Show(ex.Message);//if the number was too big, explains why number wasn't valid
            }
        }

        private void tb_enterChildren_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                g1.NumAdult = addNum(tb_enterChildren.Text);//checks this is a valid number. -1 if not
                tb_enterAdult.BorderBrush = Brushes.Black;

            }
            catch (Exception ex)
            {
                tb_enterAdult.BorderBrush = Brushes.Red;//colors border
                tb_enterAdult.Text = "";//resets text
                if (ex is LargeNumberExceptionPL)
                    MessageBox.Show(ex.Message);//if the number was too big, explains why number wasn't valid
            }
        }
        private int addNum(string number)
        {
            int text = 0;
            if (!Int32.TryParse(number, out text))//if it's not a number
                throw new invalidTypeExceptionPL();
            if (text < 0)//not a valid number
                throw new negativeNumberExceptionPL();
            if (text > 1000)
                throw new LargeNumberExceptionPL("Number cannot be over 1000");
            return text;//if it's valid, returns it
        }

       
        #endregion

       
    }
}
