//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using SampleProject1.Common.Models.Mapping;

//namespace SampleProject1.Common.Models
//{
//    public partial class TestContext : DbContext
//    {
//        static TestContext()
//        {
//            Database.SetInitializer<TestContext>(null);
//        }

//        public TestContext()
//            : base("Name=TestContext")
//        {
//        }

//        public DbSet<Product> Products { get; set; }
//        public DbSet<ProductType> ProductTypes { get; set; }
//        public DbSet<sysdiagram> sysdiagrams { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Configurations.Add(new ProductMap());
//            modelBuilder.Configurations.Add(new ProductTypeMap());
//            modelBuilder.Configurations.Add(new sysdiagramMap());
//        }
//    }
//}
