using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SpectrAgency.Database
{
    public class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // ========== КЛИЕНТЫ ==========
        public static List<dynamic> GetClients()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.Query("SELECT * FROM clients").ToList();
            }
        }

        public static void AddClient(string fullName, string phone, string email, string address)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("INSERT INTO clients (full_name, phone, email, address) VALUES (@fullName, @phone, @email, @address)",
                    new { fullName, phone, email, address });
            }
        }

        public static void UpdateClient(int id, string fullName, string phone, string email, string address)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE clients SET full_name=@fullName, phone=@phone, email=@email, address=@address WHERE id=@id",
                    new { id, fullName, phone, email, address });
            }
        }

        public static void DeleteClient(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("DELETE FROM clients WHERE id=@id", new { id });
            }
        }

        // ========== ТОВАРЫ ==========
        public static List<dynamic> GetProducts()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.Query("SELECT * FROM products").ToList();
            }
        }

        public static void AddProduct(string name, string category, string unit, decimal price, decimal stockQuantity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("INSERT INTO products (name, category, unit, price, stock_quantity) VALUES (@name, @category, @unit, @price, @stockQuantity)",
                    new { name, category, unit, price, stockQuantity });
            }
        }

        public static void UpdateProduct(int id, string name, string category, string unit, decimal price, decimal stockQuantity)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE products SET name=@name, category=@category, unit=@unit, price=@price, stock_quantity=@stockQuantity WHERE id=@id",
                    new { id, name, category, unit, price, stockQuantity });
            }
        }

        public static void DeleteProduct(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("DELETE FROM products WHERE id=@id", new { id });
            }
        }

        // ========== СОТРУДНИКИ ==========
        public static List<dynamic> GetEmployees()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.Query("SELECT * FROM employees").ToList();
            }
        }

        public static void AddEmployee(string fullName, string position, string phone, DateTime hireDate, decimal salary)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("INSERT INTO employees (full_name, position, phone, hire_date, salary) VALUES (@fullName, @position, @phone, @hireDate, @salary)",
                    new { fullName, position, phone, hireDate, salary });
            }
        }

        public static void UpdateEmployee(int id, string fullName, string position, string phone, DateTime hireDate, decimal salary)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE employees SET full_name=@fullName, position=@position, phone=@phone, hire_date=@hireDate, salary=@salary WHERE id=@id",
                    new { id, fullName, position, phone, hireDate, salary });
            }
        }

        public static void DeleteEmployee(int id)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("DELETE FROM employees WHERE id=@id", new { id });
            }
        }

        // ========== ЗАКАЗЫ ==========
        public static List<dynamic> GetOrders()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.Query(@"SELECT o.*, c.full_name as client_name 
                                 FROM orders o
                                 JOIN clients c ON o.client_id = c.id
                                 ORDER BY o.order_date DESC").ToList();
            }
        }

        public static void UpdateOrderStatus(int id, string status)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE orders SET status=@status WHERE id=@id", new { id, status });
            }
        }

        // ========== ОТЧЁТЫ ==========
        public static decimal GetTotalRevenue()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QuerySingleOrDefault<decimal>("SELECT ISNULL(SUM(total_amount), 0) FROM orders WHERE status='завершен'");
            }
        }

        public static int GetOrdersCount()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QuerySingleOrDefault<int>("SELECT COUNT(*) FROM orders");
            }
        }

        public static int GetClientsCount()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.QuerySingleOrDefault<int>("SELECT COUNT(*) FROM clients");
            }
        }

        public static List<dynamic> GetPopularProducts()
        {
            using (var db = new SqlConnection(connectionString))
            {
                return db.Query(@"SELECT TOP 5 p.name, SUM(op.quantity) as total_sold
                                 FROM order_products op
                                 JOIN products p ON op.product_id = p.id
                                 GROUP BY p.name
                                 ORDER BY total_sold DESC").ToList();
            }
        }
    }
}