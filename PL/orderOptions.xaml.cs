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
    /// Interaction logic for orderOptions.xaml
    /// </summary>
    public partial class orderOptions : Window
    {
        Host h1;
        public orderOptions()
        {
            InitializeComponent();
            h1 = new Host();
        }
    }
}
