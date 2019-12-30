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
    /// Interaction logic for orderOptions.xaml
    /// </summary>
    public partial class orderOptions : Window
    {
        Host h1;
        int unitKey;
        int hostsKey;
        HostingUnit hu1;
        private IBL bL;
        GuestRequest g1;
        GuestRequest FoundGuest;
        public orderOptions()
        {
            InitializeComponent();
            h1 = new Host();
            hu1 = new HostingUnit();
            unitKey = -1;
            hostsKey = -1;
            bL = factoryBL.getBL();
            g1 = new GuestRequest();
            FoundGuest = null;
        }
        #region host and unit checks
        private void Tb_hostNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bL.addHostNum(tb_hostNum.Text, out hostsKey);
                h1.HostKey = hostsKey;
                if (unitKey!=-1)//if also set
                {
                    if (!bL.sameUnit(hu1, hostsKey))//not same as unit
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
                bL.addHostingUnitNum(tb_hostingUnit.Text, out unitKey);
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
           
        }


        #endregion
        #region guest fields
        private void Tb_guestLast_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(tb_guestLast.Text, @"^[a-zA-Z]+$"))
            {
                g1.LastName = tb_guestLast.Text;
                if (FoundGuest != null)
                {
                    if (FoundGuest.LastName != g1.LastName)
                    {
                        tb_guestLast.Background = Brushes.OrangeRed;
                        tb_guestLast.Text = "";
                    }

                }
            }
            else
            {
                tb_guestLast.Background = Brushes.OrangeRed;
                tb_guestLast.Text = "";
            }

        }

        private void Tb_guestFirst_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(tb_guestFirst.Text, @"^[a-zA-Z]+$"))
            {
                g1.Name = tb_guestFirst.Text;
                if (FoundGuest != null)
                {
                    if (FoundGuest.Name != g1.Name)
                    {
                        tb_guestFirst.Background = Brushes.OrangeRed;
                        tb_guestFirst.Text = "";
                    }

                }
            }
            else
            {
                tb_guestFirst.Background = Brushes.OrangeRed;
                tb_guestFirst.Text = "";
            }

        }

        private void Tb_guestKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                FoundGuest= bL.findGuest(g1, tb_guestKey.Text);
                if (FoundGuest==null)//wasn't found
                {
                    tb_guestKey_txt.Text = " מספר אורח לא תקין\n";
                }
                else
                {
                    tb_guestKey_txt.Text = "מספר אורח";
                }
            }
            catch
            {
                tb_guestKey_txt.Text = " מספר אורח לא תקין\n";
            }
        }
        #endregion
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.checkOrder(h1, hu1, g1, FoundGuest);
                MessageBoxResult mbr = MessageBox.Show("הזמנה הוספה");
                Close();
            }
            catch(Exception ex)
            {
                MessageBoxResult mbr =  MessageBox.Show(ex.Message);

            }
        }
    }
}