using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Linq;

namespace Program
{

    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public ushort UnitsInStock { get; set; }
        public ushort? UnitsOnOrder { get; set; }
        public ushort? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        protected bool Equals(Product other) => Equals(ProductID, other.ProductID);
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return ReferenceEquals(this, obj)
                ? true
                : obj.GetType() == GetType()
                   && Equals((Product)obj);
        }

        public override int GetHashCode() => ProductID.GetHashCode();

        public override string ToString() => "Product " + ProductID;
    }

    public class MostExpensiveProduct
    {
        public string TenMostExpensiveProducts { get; set; }

        public decimal? UnitPrice { get; set; }
    }

    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public NorthwindContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\BABUSQL;Database=Customer;Trusted_Connection=True;");


            LoggerFactory program_logger = new LoggerFactory();

            // Console logger: Logs to Console 
            ConsoleLoggerOptions console_options = new ConsoleLoggerOptions();
            console_options.DisableColors = false;
            console_options.IncludeScopes = false;

            ConsoleLoggerProvider console_logger = new ConsoleLoggerProvider((category, level) => level >= LogLevel.Trace, true, false);
            program_logger.AddProvider(console_logger);

            DebugLoggerProvider debug_logger = new DebugLoggerProvider();
            program_logger.AddProvider(debug_logger);

            optionsBuilder.UseLoggerFactory(program_logger).EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(
                b =>
                {
                    b.ToTable("Ｐroducts");
                    b.Property(o => o.ProductID).HasColumnName("ＰroductID");
                    b.Property(o => o.ProductName).HasColumnName("ＰroductName");
                    b.Property(o => o.SupplierID).HasColumnName("ＳupplierID");
                    b.Property(o => o.CategoryID).HasColumnName("ＣategoryID");
                    b.Property(o => o.QuantityPerUnit).HasColumnName("ＱuantityPerUnit");
                    b.Property(o => o.UnitPrice).HasColumnName("ＵnitPrice");
                    b.Property(o => o.UnitsInStock).HasColumnName("ＵnitsInStock");
                    b.Property(o => o.UnitsOnOrder).HasColumnName("ＵnitsOnOrder");
                    b.Property(o => o.ReorderLevel).HasColumnName("ＲeorderLevel");
                    b.Property(o => o.Discontinued).HasColumnName("Ｄiscontinued");

                    b.Ignore(p => p.CategoryID);
                    b.Ignore(p => p.QuantityPerUnit);
                    b.Ignore(p => p.ReorderLevel);
                    b.Ignore(p => p.UnitsOnOrder);

                });

            modelBuilder.Entity<MostExpensiveProduct>(
                b =>
                {
                    b.ToTable("ＭostExpensiveProducts");
                    b.Property(c => c.UnitPrice).HasColumnName("ＵnitPrice");
                    b.Property(c => c.TenMostExpensiveProducts).HasColumnName("ＴenMostExpensiveProducts");
                    b.HasKey(mep => mep.TenMostExpensiveProducts);
                });
        }


        public static void Main(String[] arg)
        {
            var _context = new NorthwindContext();

            var actual = _context
                    .Set<MostExpensiveProduct>()
                    .FromSql(TenMostExpensiveProductsSproc, GetTenMostExpensiveProductsParameters())
                    .Select(
                        mep =>
                            new MostExpensiveProduct { TenMostExpensiveProducts = "Foo", UnitPrice = mep.UnitPrice }).ToList();

            Console.WriteLine(actual.Count());
        }
        protected static string OpenDelimeter => "\"";

        protected static string CloseDelimeter => "\"";
        protected static string TenMostExpensiveProductsSproc => "[dbo].[Ｔen Most Expensive Products]";
        public static object Northwind { get; private set; }

        public static string NormalizeDelimeters(string sql)
            => sql.Replace("[", OpenDelimeter).Replace("]", CloseDelimeter);

        protected static object[] GetTenMostExpensiveProductsParameters()
            => Array.Empty<object>();


    }
}