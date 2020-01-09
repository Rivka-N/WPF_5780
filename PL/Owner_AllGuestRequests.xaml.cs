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
using BL;
namespace PL
{
    /// <summary>
    /// Interaction logic for Owner_AllGuestRequests1.xaml
    /// </summary>
    public partial class Owner_AllGuestRequests1 : Page
    {
        IBL myBL;
        public Owner_AllGuestRequests1()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            ds_guestRequestDataGrid.ItemsSource = myBL.getRequests();
        }

    }
}
