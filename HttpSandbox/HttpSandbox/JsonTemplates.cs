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

    }
}