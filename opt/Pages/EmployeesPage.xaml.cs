using System;
using System.Windows;
using System.Windows.Controls;
using SpectrAgency.Database;

namespace SpectrAgency.Pages
{
    public partial class EmployeesPage : Page
    {
        private dynamic selectedEmployee;

        public EmployeesPage()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                EmployeesGrid.ItemsSource = DatabaseHelper.GetEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (FullNameBox.Text == "ФИО" || string.IsNullOrWhiteSpace(FullNameBox.Text))
            {
                MessageBox.Show("Введите ФИО");
                return;
            }

            decimal salary = 0;
            decimal.TryParse(SalaryBox.Text, out salary);

            DatabaseHelper.AddEmployee(FullNameBox.Text, PositionBox.Text, PhoneBox.Text, DateTime.Now, salary);
            LoadEmployees();
            ClearForm();
        }

        private void UpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee == null)
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }

            decimal salary = 0;
            decimal.TryParse(SalaryBox.Text, out salary);

            DatabaseHelper.UpdateEmployee(selectedEmployee.id, FullNameBox.Text, PositionBox.Text, PhoneBox.Text, DateTime.Now, salary);
            LoadEmployees();
            ClearForm();
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee == null)
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }

            if (MessageBox.Show($"Удалить {selectedEmployee.full_name}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DatabaseHelper.DeleteEmployee(selectedEmployee.id);
                LoadEmployees();
                ClearForm();
            }
        }

        private void EmployeesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem != null)
            {
                selectedEmployee = EmployeesGrid.SelectedItem;
                FullNameBox.Text = selectedEmployee.full_name;
                PositionBox.Text = selectedEmployee.position;
                PhoneBox.Text = selectedEmployee.phone;
                SalaryBox.Text = selectedEmployee.salary.ToString();
            }
        }

        private void ClearForm()
        {
            selectedEmployee = null;
            FullNameBox.Text = "ФИО";
            PositionBox.Text = "Должность";
            PhoneBox.Text = "Телефон";
            SalaryBox.Text = "Зарплата";
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == "ФИО" || tb.Text == "Должность" || tb.Text == "Телефон" || tb.Text == "Зарплата")
            {
                tb.Text = "";
            }
        }
    }
}