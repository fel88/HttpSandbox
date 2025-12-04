namespace HttpSandbox
{
    public static class JsonTemplates
    {
        public static string GenerateSample1 =>

             @"using HttpSandbox;

class Program : IResponseGenerator
{
    public string Generate()
    {
        return ""[{'id':'1','name':'alex'}]"".Replace(""'"", ""\"""");
    }
}";
        public static string GenerateSample2 =>

           @"using HttpSandbox;
using System.IO;
using System.Threading.Tasks;

class Program : IDynamicResponseGenerator
{
    public string GetMime(HttpRequestInfo request)
    {
        return ""application/octet-stream"";
    }

    public long GetLength(HttpRequestInfo request)
    {
        var f = new FileInfo(""123.dat"");
        return f.Length;
    }

    public async Task WriteToStream(HttpRequestInfo request, Stream stream)
    {
        using FileStream fs = new FileStream(""123.dat"", FileMode.Open);
        await fs.CopyToAsync(stream);
    }
}";

    }
}