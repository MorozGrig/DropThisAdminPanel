using JewelryStore.AppData;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JewelryStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageOrderDetails.xaml
    /// </summary>
    public partial class PageOrderDetails : Page
    {
        public PageOrderDetails(int orderId)
        {
            InitializeComponent();
            LoadOrderDetails(orderId);
        }

        private void LoadOrderDetails(int orderId)
        {
            var db = AppConnect.model0db;

            var order = db.Orders.FirstOrDefault(o => o.IdOrder == orderId);

            if (order != null)
            {
                var orderItems = db.OrderItems
                    .Where(oi => oi.IdOrder == orderId)
                    .Select(oi => new
                    {
                        oi.IdOrderItem,
                        oi.Quantity,
                        oi.UnitPrice,
                        oi.TotalPrice,
                        Jewelry = oi.Jewelries,
                        JewelryTip = oi.Jewelries.JewelryTips
                    })
                    .ToList();

                tbOrderNumber.Text = $"Заказ #{order.IdOrder}";
                listOrderItems.ItemsSource = orderItems;
                tbOrderTotal.Text = $"Итого: {order.TotalPrice:N0} руб.";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
