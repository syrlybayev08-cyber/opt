using System;
using System.Windows;
using System.Windows.Controls;
using SpectrAgency.Database;

namespace SpectrAgency.Pages
{
    public partial class ProductsPage : Page
    {
        private dynamic selectedProduct;

        public ProductsPage()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                var products = DatabaseHelper.GetProducts();  // ← ТУТ ТОЧКА ОСТАНОВА
                ProductsGrid.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "Название" || string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите название");
                return;
            }

            decimal price = 0;
            decimal stock = 0;
            decimal.TryParse(PriceBox.Text, out price);
            decimal.TryParse(StockBox.Text, out stock);

            DatabaseHelper.AddProduct(NameBox.Text, CategoryBox.Text, "шт", price, stock);
            LoadProducts();
            ClearForm();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите товар");
                return;
            }

            decimal price = 0;
            decimal stock = 0;
            decimal.TryParse(PriceBox.Text, out price);
            decimal.TryParse(StockBox.Text, out stock);

            DatabaseHelper.UpdateProduct(selectedProduct.id, NameBox.Text, CategoryBox.Text, "шт", price, stock);
            LoadProducts();
            ClearForm();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите товар");
                return;
            }

            if (MessageBox.Show($"Удалить {selectedProduct.name}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DatabaseHelper.DeleteProduct(selectedProduct.id);
                LoadProducts();
                ClearForm();
            }
        }

        private void ProductsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                selectedProduct = ProductsGrid.SelectedItem;
                NameBox.Text = selectedProduct.name;
                CategoryBox.Text = selectedProduct.category;
                PriceBox.Text = selectedProduct.price.ToString();
                StockBox.Text = selectedProduct.stock_quantity.ToString();
            }
        }

        private void ClearForm()
        {
            selectedProduct = null;
            NameBox.Text = "Название";
            CategoryBox.Text = "Категория";
            PriceBox.Text = "Цена";
            StockBox.Text = "Остаток";
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == "Название" || tb.Text == "Категория" || tb.Text == "Цена" || tb.Text == "Остаток")
            {
                tb.Text = "";
            }
        }
    }
}