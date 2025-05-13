using Microsoft.EntityFrameworkCore;
using Warehouse_operationsApp.Models;

namespace Warehouse_operationsApp.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Doljnosti> Doljnostis { get; set; }
        public DbSet<Information_about_documents> Information_About_Documentss { get; set; }
        public DbSet<Ostatki> Ostatkis { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_type> Product_Types { get; set; }
        public DbSet<Receipt_and_expense_documents> Receipt_And_Expense_Documentss { get; set; }
        public DbSet<Suppliers> Supplierss { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Users> Userss { get; set; }
        public DbSet<Warehouses> Warehousess { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product_type - Product (one-to-many)
            modelBuilder.Entity<Product_type>()
                .HasMany(pt => pt.Product)
                .WithOne(p => p.Product_type)
                .HasForeignKey(p => p.id_product_type);

            // Unit - Product (one-to-many)
            modelBuilder.Entity<Unit>()
                .HasMany(u => u.Product)
                .WithOne(p => p.Unit)
                .HasForeignKey(p => p.id_unit);

            // Product - Information_about_documents (one-to-one)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Information_about_documents)
                .WithOne(iad => iad.Product)
                .HasForeignKey<Information_about_documents>(iad => iad.id_Product);

            // Product - Ostatki (one-to-many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Ostatki)
                .WithOne(o => o.Product)
                .HasForeignKey(o => o.id_Product);

            // Receipt_and_expense_documents - Information_about_documents (one-to-many)
            modelBuilder.Entity<Receipt_and_expense_documents>()
                .HasMany(rad => rad.Information_about_documents)
                .WithOne(iad => iad.Receipt_and_expense_documents)
                .HasForeignKey(iad => iad.id_doc);

            // Suppliers - Information_about_documents (one-to-many)
            modelBuilder.Entity<Suppliers>()
                .HasMany(s => s.Information_about_documents)
                .WithOne(iad => iad.Suppliers)
                .HasForeignKey(iad => iad.id_suppliers);

            // Users - Receipt_and_expense_documents (one-to-many)
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Receipt_and_expense_documents)
                .WithOne(rad => rad.Users)
                .HasForeignKey(rad => rad.id_users);

            // Users - Warehouses (one-to-many)
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Warehouses)
                .WithOne(w => w.Users)
                .HasForeignKey(w => w.id_users);

            // Warehouses - Ostatki (one-to-many)
            modelBuilder.Entity<Warehouses>()
                .HasMany(w => w.Ostatki)
                .WithOne(o => o.Warehouses)
                .HasForeignKey(o => o.id_warehouses);

            // Doljnosti - Users (one-to-many)
            modelBuilder.Entity<Doljnosti>()
                .HasMany(d => d.Users)
                .WithOne(u => u.Doljnosti)
                .HasForeignKey(u => u.id_doljnosti);

            modelBuilder.Entity<Information_about_documents>()
            .Property(i => i.Price)
            .HasComputedColumnSql("[Quanity] * [Cost]", stored: true);

        }
    }
}
