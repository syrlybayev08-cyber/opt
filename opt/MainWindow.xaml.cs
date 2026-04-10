using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpectrAgency
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // По умолчанию показываем заказы
            MainFrame.Navigate(new Pages.OrdersPage());
            HighlightActiveButton(NavOrders);
            StatusText.Text = "✅ Текущий раздел: Заказы";
        }

        private void NavOrders_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.OrdersPage());
            HighlightActiveButton(NavOrders);
            StatusText.Text = "✅ Текущий раздел: Заказы";
        }

        private void NavProducts_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.ProductsPage());
            HighlightActiveButton(NavProducts);
            StatusText.Text = "✅ Текущий раздел: Товары";
        }

        private void NavClients_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.ClientsPage());
            HighlightActiveButton(NavClients);
            StatusText.Text = "✅ Текущий раздел: Клиенты";
        }

        private void NavEmployees_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.EmployeesPage());
            HighlightActiveButton(NavEmployees);
            StatusText.Text = "✅ Текущий раздел: Сотрудники";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HighlightActiveButton(Button activeButton)
        {
            var buttons = new[] { NavOrders, NavProducts, NavClients, NavEmployees };
            foreach (var btn in buttons)
            {
                btn.Background = Brushes.Transparent;
                btn.Foreground = Brushes.White;
            }
            activeButton.Background = new SolidColorBrush(Color.FromRgb(230, 74, 25));
        }
    }
}