using System.Diagnostics.Metrics;
using Warehouse_operationsApp.Data;
using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Userss.Any())
            {
                var positions = new List<Doljnosti>
            {
                new Doljnosti { Post = "Менеджер" },
                new Doljnosti { Post = "Рабочий склада" },
                new Doljnosti { Post = "Бухгалтер" }
            };
                dataContext.Doljnostis.AddRange(positions);
                dataContext.SaveChanges();

                var users = new List<Users>
            {
                new Users { FIO = "Иван Иванов", Login = "ivanov", password = "12345", Doljnosti = positions[0] },
                new Users { FIO = "Петр Петров", Login = "petrov", password = "54321", Doljnosti = positions[1] },
                new Users { FIO = "Сергей Сергеев", Login = "sergeev", password = "67890", Doljnosti = positions[2] }
            };
                dataContext.Userss.AddRange(users);
                dataContext.SaveChanges();

                var warehouses = new List<Warehouses>
            {
                new Warehouses { Name = "Центральный склад", address = "Улица Ленина, 1", Users = users[0] },
                new Warehouses { Name = "Запасной склад", address = "Улица Пушкина, 5", Users = users[1] }
            };
                dataContext.Warehousess.AddRange(warehouses);
                dataContext.SaveChanges();

                var productTypes = new List<Product_type>
            {
                new Product_type { Name = "Металлоконструкции" },
                new Product_type { Name = "Комплектующие" },
                new Product_type { Name = "Инструменты" }
            };
                dataContext.Product_Types.AddRange(productTypes);
                dataContext.SaveChanges();

                var units = new List<Unit>
            {
                new Unit { Name = "Штука" },
                new Unit { Name = "Килограмм" }
            };
                dataContext.Units.AddRange(units);
                dataContext.SaveChanges();

                var products = new List<Product>
            {
                new Product { Name = "Каркас", vendor_code = "MT001", Price = 80000, Product_type = productTypes[0], Unit = units[0] },
                new Product { Name = "Опора", vendor_code = "MT002", Price = 50000, Product_type = productTypes[0], Unit = units[0] },
                new Product { Name = "Болты", vendor_code = "KT003", Price = 15000, Product_type = productTypes[1], Unit = units[1] },
                new Product { Name = "Гайки", vendor_code = "KT004", Price = 5000, Product_type = productTypes[1], Unit = units[1] },
                new Product { Name = "Сварочный аппарат", vendor_code = "TL005", Price = 7000, Product_type = productTypes[2], Unit = units[0] }
            };
                dataContext.Products.AddRange(products);
                dataContext.SaveChanges();

                var suppliers = new List<Suppliers>
            {
                new Suppliers { Name = "Поставщик стали", Contact_Information = "123-456" },
                new Suppliers { Name = "Поставщик инструментов", Contact_Information = "789-012" }
            };
                dataContext.Supplierss.AddRange(suppliers);
                dataContext.SaveChanges();

                var documents = new List<Receipt_and_expense_documents>
            {
                new Receipt_and_expense_documents { date = DateTime.Now, ReceiptAndexpense_documents = true, Users = users[0] },
                new Receipt_and_expense_documents { date = DateTime.Now, ReceiptAndexpense_documents = false, Users = users[1] }
            };
                dataContext.Receipt_And_Expense_Documentss.AddRange(documents);
                dataContext.SaveChanges();

                var informationAboutDocuments = new List<Information_about_documents>
            {
                new Information_about_documents { Product = products[0], Quanity = 10, Receipt_and_expense_documents = documents[0], Suppliers = suppliers[0], Cost = 75000 },
                new Information_about_documents { Product = products[1], Quanity = 5, Receipt_and_expense_documents = documents[1], Suppliers = suppliers[0], Cost = 48000 }
            };
                dataContext.Information_About_Documentss.AddRange(informationAboutDocuments);
                dataContext.SaveChanges();

                var inventory = new List<Ostatki>
            {
                new Ostatki { Product = products[0], Warehouses = warehouses[0], Quantity_Ostatki = 10 },
                new Ostatki { Product = products[1], Warehouses = warehouses[1], Quantity_Ostatki = 5 }
            };
                dataContext.Ostatkis.AddRange(inventory);
                dataContext.SaveChanges();
            }
        }
    }
}