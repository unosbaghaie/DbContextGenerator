using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextGenerator
{
    public class ContextGenerator
    {
        public void Generate(List<string> entities, params Type[] types)
        {
            StringBuilder code = new StringBuilder();

            code.AppendLine(@"
           using System.Data.Entity;
           using System.Data.Entity.Core.EntityClient;
           using SampleProject1.Common.Models;
           using SampleProject1.Common.Models.Mapping;
           using SampleProject2.Common.Models;
           using SampleProject2.Common.Models.Mapping;

           namespace DbContextGenerator
           {
                public partial class TestContext : DbContext
                {
                    static TestContext()
                    {
                        Database.SetInitializer<TestContext>(null);
                    }

                    public TestContext()
                        : base(""Data Source=.;Initial Catalog=Test;Integrated Security=True;MultipleActiveResultSets=True"")
                    {
                        }
                ");

            var pluralizeHelper = new PluralizeHelper();

            foreach (var entity in entities)
            {
                code.AppendLine($@"public DbSet<{entity}> {pluralizeHelper.Pluralize(entity)} {{ get; set; }}");
            }

            code.AppendLine(@"protected override void OnModelCreating(DbModelBuilder modelBuilder)");
            code.AppendLine(@"{");

            foreach (var entity in entities)
            {
                code.AppendLine($@"modelBuilder.Configurations.Add(new {entity}Map());");
            }
            code.AppendLine(@"}");
            code.AppendLine(@"}");
            code.AppendLine(@"}");
           
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            parameters.ReferencedAssemblies.Add("System.Data.Entity.dll");
            parameters.ReferencedAssemblies.Add("System.ComponentModel.dll");

            foreach (var type in types)
            {
                parameters.ReferencedAssemblies.Add(type.Assembly.Location);
            }

            parameters.ReferencedAssemblies.Add(typeof(DbSet).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(DbContext).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(IQueryable).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(IQueryable<>).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(System.ComponentModel.IListSource).Assembly.Location);

            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = false;
            parameters.OutputAssembly = "ProjectContext.dll";

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code.ToString());

            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                throw new InvalidOperationException(sb.ToString());
            }
        }

    }
}
