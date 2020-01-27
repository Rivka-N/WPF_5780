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
using BE;
namespace PL
{
    /// <summary>
    /// Interaction logic for Owner_Data.xaml
    /// </summary>
    public partial class Owner_Data : Page
    {
        IBL myBL;
        public Owner_Data()
        {
            myBL = factoryBL.getBL();
            InitializeComponent();
            int sum = myBL.TotalSumCollectedFromUnits();
            tb_moneyCollected.Text = sum.ToString();//text is sum collected
            try
            {
                var reqs = myBL.getRequests();
                tb_guests.Text = (reqs != null) ? reqs.Count.ToString() : "0";//total requests
                var us = myBL.getAllHostingUnits();
                tb_units.Text = (us != null) ? us.Count.ToString() : "0";//total units
                var gs = myBL.getRequests(req => req.Status == Enums.OrderStatus.Closed);
                tb_finalguests.Text = gs != null ? gs.Count.ToString() : "0";//all closed requests
            }
            catch
            {
                tb_units.Text = "invalid data";
                tb_guests.Text= "invalid data";
                tb_finalguests.Text= "invalid data";
            }
        }
    }
}
