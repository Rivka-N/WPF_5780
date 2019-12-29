using BL;
using BE;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBL bL;
        
        public MainWindow()
        {
            InitializeComponent();
            bL = factoryBL.getBL();
           string hostingUnitName = "Nave Mahmad";
           bL.addHostingUnit(new HostingUnit(new Host(), hostingUnitName, Enums.HostingUnitType.Zimmer));
           bL.addOrder(new BE.Order(DateTime.Now));
            
            //bL.addHostingUnit (new HostingUnit(new Host(12, "shomo", "tenen", "shlomo@sh.com", 123, new BankAccount(), true), "neve shlomo", Enums.HostingUnitType.Camping, Enums.Area.Center));
            //hostingUnits = bL.getAllHostingUnits();
        }

        private void pb_owner_Click(object sender, RoutedEventArgs e)
        {
            ownerOptions ownerWindow = new ownerOptions();
            ownerWindow.Show();
        }

        private void pb_guest_Click(object sender, RoutedEventArgs e)
        {
            guestOptions guestWindow = new guestOptions();
            guestWindow.Show();
        }

        private void pb_host_Click(object sender, RoutedEventArgs e)
        {
            hostOptions hostWinows = new hostOptions();
            hostWinows.Show();
        }
    }
}
