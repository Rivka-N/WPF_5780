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
    /// Interaction logic for orderOptions.xaml
    /// </summary>
    public partial class orderOptions : Window
    {
        Host h1;
        int unitKey;
        int hostsKey;
        HostingUnit hu1;
        private IBL bL;
        public orderOptions()
        {
            InitializeComponent();
            h1 = new Host();
            unitKey = -1;
            hostsKey = -1;
            bL = factoryBL.getBL();
        }

        private void Tb_hostNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bL.addHostNum(tb_hostNum.Text, hostsKey);
                if (unitKey!=-1)//if also set
                {
                    if (!bL.sameUnit(hu1, hostsKey))
                    {
                        tb_hostNum_txt.Text = "קוד מארח לא תואם קוד יחידה. הכנס שוב";
                    }
                    else
                    {
                        tb_hostNum_txt.Text = "קוד מארח";
                    }
                }
            }
            catch (InvalidException iEx)
            {
                tb_hostNum_txt.Text = "הכנס קוד מארח\n " + iEx.Message;
            }
            
        }

        private void Tb_hostingUnit_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bL.addHostingUnitNum(tb_hostingUnit.Text, unitKey);
                hu1 = bL.findUnit(unitKey);
                if (hostsKey!=-1)//already set
                {
                    if (!bL.sameUnit(hu1, hostsKey))
                    {
                        tb_hostingUnit_txt.Text = "קוד יחידה לא תואם קוד מארח. הכנס שוב";
                    }
                    else
                    {
                        tb_hostingUnit_txt.Text = "קוד יחידה";
                    }
                }
            }
            catch(InvalidException iEx)
            {
                tb_hostingUnit_txt.Text = "הכנס קוד יחידת אירוח\n" + iEx.Message;
            }
            //Int32.TryParse(tb_hostingUnit.Text, out unitKey);//add catch?
            //if (unitKey >= 0)
            //{
            //    hu1 = bL.findUnit(unitKey);//check that works
            //    if (hu1 == null)//what then?
            //    {

            //    }
            //    else
            //    {
            //        if (hostsKey>0)
            //        {
            //            if (hu1.host.HostKey!=hostsKey)//if this isn't the same numbers
            //            {
            //                tb_hostingUnit_txt.Text = "קוד משתמש וקוד יחידה לא תואמים. הכנס קוד יחידה";
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    //and in catch-error message
            //}
            
           
        }
    }
}
