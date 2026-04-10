using System;
using System.Windows;
using System.Windows.Controls;
using SpectrAgency.Database;

namespace SpectrAgency.Pages
{
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
            LoadReports();
        }

        private void LoadReports()
        {
            try
            {
                RevenueText.Text = DatabaseHelper.GetTotalRevenue().ToString("N0") + " ₽";
                OrdersCountText.Text = DatabaseHelper.GetOrdersCount().ToString();
                ClientsCountText.Text = DatabaseHelper.GetClientsCount().ToString();
                PopularProductsGrid.ItemsSource = DatabaseHelper.GetPopularProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void RefreshReports_Click(object sender, RoutedEventArgs e)
        {
            LoadReports();
            MessageBox.Show("Отчёты обновлены", "Успех");
        }
    }
}