using AbstractLayer;
using DbContextGenerator;
using SampleProject1.Common.Models;
using SampleProject2.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject1.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Assembly> allAssemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (string dll in Directory.GetFiles(path, "*.Common.dll"))
                allAssemblies.Add(Assembly.LoadFile(dll));

            var type = typeof(Entity);
      
            List<Type> types = allAssemblies
             .SelectMany(s => s.GetTypes())
             .Where(p => type.IsAssignableFrom(p)).ToList();

            List<string> entities = new List<string>();
            foreach (var item in types)
            {
                entities.Add(item.Name);
            }

            types.Add(typeof(Entity));

            new ContextGenerator().Generate(entities, types.ToArray());

            var products = Get<Product>().ToList();

            var productTypes = Get<ProductType>().ToList();


            var query = from p in Get<Product>()
                        join pt in Get<ProductType>() on p.ProductTypeId equals pt.Id
                        select new
                        {
                            Id = p.Id,
                            Name = p.Name,
                            ProductType = pt.Name

                        };

            var JoinResult = query.ToList();
        }

        public static System.Data.Entity.DbSet<T> Get<T>() where T : class
        {
            var set = GetDbContextInstance().Set<T>();
            return set;
        }


        static DbContext _dbContext=null;
        public static DbContext GetDbContextInstance()
        {
            if (_dbContext == null)
            {
                string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var dllversionAssm = Assembly.LoadFile(path + "\\ProjectContext.dll");
                Type type = dllversionAssm.GetType("DbContextGenerator.TestContext");
                _dbContext = (DbContext)Activator.CreateInstance(type);
            }
            return _dbContext;
        }
    }
}
