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
        }
    }
}
