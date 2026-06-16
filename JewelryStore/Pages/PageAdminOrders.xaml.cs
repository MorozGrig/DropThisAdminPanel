using JewelryStore.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JewelryStore.Pages
{
    public partial class PageAdminOrders : Page
    {
        private DropThisDatabaseEntities db;
        public List<Orders> OrdersList { get; set; }
        public List<StatusOrders> StatusList { get; set; }

        public PageAdminOrders()
        {
            InitializeComponent();
            db = AppConnect.model0db; 
            LoadData();
        }

        private void LoadData()
        {
            OrdersList = db.Orders.Include("StatusOrder").ToList();
            StatusList = db.StatusOrders.ToList();
            OrdersGrid.ItemsSource = OrdersList;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            MessageBox.Show("Список обновлён!");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.SaveChanges();
                MessageBox.Show("Статусы заказов обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OrdersGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            SaveBtn.IsEnabled = true; 
        }
    }
}