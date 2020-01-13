﻿using System;
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
    /// Interaction logic for hostingUnit_closedOrders.xaml
    /// </summary>
    /// 
  
    public partial class hostingUnit_closedOrders : Page
    {
        IBL myBL;
        List<Order> myOrders;

        public hostingUnit_closedOrders()
        {
            InitializeComponent();
            myBL = factoryBL.getBL();
            myOrders = myBL.getOrders(ord => ord.Status == Enums.OrderStatus.Closed);
            orderDataGrid.ItemsSource = myOrders;//all closed orders
        }
    }
}
