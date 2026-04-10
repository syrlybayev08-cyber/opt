using System;
using System.Windows;
using System.Windows.Controls;
using SpectrAgency.Database;

namespace SpectrAgency.Pages
{
    public partial class ClientsPage : Page
    {
        private dynamic selectedClient;

        public ClientsPage()
        {
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            try
            {
                ClientsGrid.ItemsSource = DatabaseHelper.GetClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            if (FullNameBox.Text == "ФИО" || string.IsNullOrWhiteSpace(FullNameBox.Text))
            {
                MessageBox.Show("Введите ФИО");
                return;
            }

            DatabaseHelper.AddClient(FullNameBox.Text, PhoneBox.Text, EmailBox.Text, AddressBox.Text);
            LoadClients();
            ClearForm();
        }

        private void UpdateClient_Click(object sender, RoutedEventArgs e)
        {
            if (selectedClient == null)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }

            DatabaseHelper.UpdateClient(selectedClient.id, FullNameBox.Text, PhoneBox.Text, EmailBox.Text, AddressBox.Text);
            LoadClients();
            ClearForm();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (selectedClient == null)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }

            if (MessageBox.Show($"Удалить {selectedClient.full_name}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DatabaseHelper.DeleteClient(selectedClient.id);
                LoadClients();
                ClearForm();
            }
        }

        private void ClientsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsGrid.SelectedItem != null)
            {
                selectedClient = ClientsGrid.SelectedItem;
                FullNameBox.Text = selectedClient.full_name;
                PhoneBox.Text = selectedClient.phone;
                EmailBox.Text = selectedClient.email;
                AddressBox.Text = selectedClient.address;
            }
        }

        private void ClearForm()
        {
            selectedClient = null;
            FullNameBox.Text = "ФИО";
            PhoneBox.Text = "Телефон";
            EmailBox.Text = "Email";
            AddressBox.Text = "Адрес";
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == "ФИО" || tb.Text == "Телефон" || tb.Text == "Email" || tb.Text == "Адрес")
            {
                tb.Text = "";
            }
        }
    }
}