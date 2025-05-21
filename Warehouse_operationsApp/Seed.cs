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
            new Product { Name = "Сварочный аппарат", vendor_code = "TL005", Price = 7000, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Шайбы", vendor_code = "KT006", Price = 2000, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Резак", vendor_code = "TL007", Price = 6000, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Труба", vendor_code = "MT008", Price = 12000, Product_type = productTypes[0], Unit = units[1] },
            new Product { Name = "Кабель", vendor_code = "KT009", Price = 3000, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Молоток", vendor_code = "TL010", Price = 3500, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Профиль", vendor_code = "MT011", Price = 22000, Product_type = productTypes[0], Unit = units[1] },
            new Product { Name = "Шурупы", vendor_code = "KT012", Price = 1300, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Отвертка", vendor_code = "TL013", Price = 2800, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Шестигранник", vendor_code = "TL014", Price = 2500, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Заклепки", vendor_code = "KT015", Price = 1000, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Фланец", vendor_code = "MT016", Price = 17000, Product_type = productTypes[0], Unit = units[1] },
            new Product { Name = "Уровень", vendor_code = "TL017", Price = 4000, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Крепеж", vendor_code = "KT018", Price = 9000, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Кронштейн", vendor_code = "MT019", Price = 19500, Product_type = productTypes[0], Unit = units[0] },
            new Product { Name = "Сверло", vendor_code = "TL020", Price = 3200, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Гайковерт", vendor_code = "TL021", Price = 9000, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Пластина", vendor_code = "MT022", Price = 11000, Product_type = productTypes[0], Unit = units[1] },
            new Product { Name = "Шпилька", vendor_code = "KT023", Price = 2700, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Ножницы по металлу", vendor_code = "TL024", Price = 8000, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Хомут", vendor_code = "KT025", Price = 1500, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Кронштейн крепежный", vendor_code = "MT026", Price = 21000, Product_type = productTypes[0], Unit = units[0] },
            new Product { Name = "Заклепочник", vendor_code = "TL027", Price = 5000, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Профлист", vendor_code = "MT028", Price = 18000, Product_type = productTypes[0], Unit = units[1] },
            new Product { Name = "Гайка усиленная", vendor_code = "KT029", Price = 7000, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Плоскогубцы", vendor_code = "TL030", Price = 6500, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Опора малая", vendor_code = "MT031", Price = 15000, Product_type = productTypes[0], Unit = units[0] },
            new Product { Name = "Болт анкерный", vendor_code = "KT032", Price = 9200, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Дрель", vendor_code = "TL033", Price = 11000, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Подложка", vendor_code = "KT034", Price = 500, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Рама", vendor_code = "MT035", Price = 87000, Product_type = productTypes[0], Unit = units[0] },
            new Product { Name = "Ключ гаечный", vendor_code = "TL036", Price = 3600, Product_type = productTypes[2], Unit = units[0] },
            new Product { Name = "Пластиковая заглушка", vendor_code = "KT037", Price = 450, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Металлическая крышка", vendor_code = "MT038", Price = 24000, Product_type = productTypes[0], Unit = units[0] },
            new Product { Name = "Резиновая втулка", vendor_code = "KT039", Price = 800, Product_type = productTypes[1], Unit = units[1] },
            new Product { Name = "Паяльник", vendor_code = "TL040", Price = 9900, Product_type = productTypes[2], Unit = units[0] }
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

                var documents = new List<Receipt_and_expense_documents>();
                for (int i = 0; i < 40; i++)
                {
                    documents.Add(new Receipt_and_expense_documents { date = DateTime.Now.AddDays(-i), ReceiptAndexpense_documents = i % 2 == 0, Users = users[i % 3] });
                }
                dataContext.Receipt_And_Expense_Documentss.AddRange(documents);
                dataContext.SaveChanges();

                var informationAboutDocuments = new List<Information_about_documents>();
                for (int i = 0; i < 40; i++)
                {
                    informationAboutDocuments.Add(new Information_about_documents { Product = products[i], Quanity = 10 + i, Receipt_and_expense_documents = documents[i], Suppliers = suppliers[i % 2], Cost = products[i].Price });
                }
                dataContext.Information_About_Documentss.AddRange(informationAboutDocuments);
                dataContext.SaveChanges();

                var inventory = new List<Ostatki>();
                for (int i = 0; i < 40; i++)
                {
                    inventory.Add(new Ostatki { Product = products[i], Warehouses = warehouses[i % 2], Quantity_Ostatki = 5 + i });
                }
                dataContext.Ostatkis.AddRange(inventory);
                dataContext.SaveChanges();
            }
        }

    }
}