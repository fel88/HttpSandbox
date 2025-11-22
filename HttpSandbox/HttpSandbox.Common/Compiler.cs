using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Npgsql;
using System.Reflection;

namespace HttpSandbox
{
    public class Compiler
    {
        public static RoslynCompilerResults compile(string program)
        {
            List<MetadataReference> References = new List<MetadataReference>();

            List<string> assembliesToBind =
            [
                "Microsoft.CSharp.dll",
                "System.Core.dll",
                "System.Runtime.dll",
                "System.Data.Common.dll",
                "System.ComponentModel.Primitives.dll",
                "System.Collections.dll",
                "System.Linq.dll",
                "System.Windows.Forms.dll",
                Assembly.GetAssembly(typeof(System.Dynamic.DynamicObject)).FullName,
                Assembly.GetAssembly(typeof(System.Attribute)).FullName,
                Assembly.GetAssembly(typeof(NpgsqlConnection)).FullName,
                Assembly.GetAssembly(typeof(Compiler)).FullName,
                Assembly.GetAssembly(typeof(Task)).FullName,
                Assembly.GetAssembly(typeof(Dapper.SqlMapper)).FullName,
            ];

            foreach (var item in assembliesToBind)
            {
                if (File.Exists(item))
                {
                    References.Add(MetadataReference.CreateFromFile(item));
                }
                else
                {
                    Assembly assm = null;
                    try
                    {
                        assm = Assembly.Load(item);
                    }
                    catch (Exception ex)
                    {

                    }
                    if (assm == null)
                    {
                        assm = TryGetAssemblyFromGAC(item);
                    }
                    References.Add(MetadataReference.CreateFromFile(assm.Location));

                }
            }

            var syntaxTree = CSharpSyntaxTree.ParseText(program);
            Random rand = new Random();
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            CSharpCompilation compilation = CSharpCompilation.Create($"assembly_{rand.Next()}_{DateTime.Now.Microsecond}",
                new[] { syntaxTree }, references: References,
                options);
            bool debug = true;
            var res = new RoslynCompilerResults(compilation, debug);


            return res;
        }



        private static Assembly TryGetAssemblyFromGAC(string path)
        {
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (item.IsDynamic)
                    continue;

                var d = new DirectoryInfo(Path.GetDirectoryName(item.Location));
                foreach (var ff in d.GetFiles())
                {
                    if (ff.Name.ToLower() == path.ToLower())
                    {
                        return Assembly.LoadFrom(ff.FullName);
                    }
                }
            }
            return null;
        }
    }
}
