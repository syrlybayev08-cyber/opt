using System;
using System.Windows;
using System.Windows.Controls;
using SpectrAgency.Database;

namespace SpectrAgency.Pages
{
    public partial class OrdersPage : Page
    {
        private dynamic selectedOrder;

        public OrdersPage()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                OrdersGrid.ItemsSource = DatabaseHelper.GetOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void RefreshOrders_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private void CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }

            DatabaseHelper.UpdateOrderStatus(selectedOrder.id, "завершен");
            LoadOrders();
            MessageBox.Show($"Заказ №{selectedOrder.id} завершён");
        }

        private void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }

            DatabaseHelper.UpdateOrderStatus(selectedOrder.id, "отменен");
            LoadOrders();
            MessageBox.Show($"Заказ №{selectedOrder.id} отменён");
        }

        private void OrdersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersGrid.SelectedItem != null)
            {
                selectedOrder = OrdersGrid.SelectedItem;
            }
        }
    }
}