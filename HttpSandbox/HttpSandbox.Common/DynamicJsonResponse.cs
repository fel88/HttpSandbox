using System.Reflection;
using System.Xml.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace HttpSandbox
{
    public class DynamicJsonResponse : MockHttpResponse
    {
        public DynamicJsonResponse()
        {

        }

        public DynamicJsonResponse(XElement item) : base(item)
        {
            Program = item.Element("data").Value;
        }

        public string Program { get; set; }

        IResponseGenerator Generator { get; set; }
        string LastGeneratedProgramHash;
        private string GenerateJson()
        {
            var hash1 = Program.MD5Hash();
            if (Generator != null && hash1 == LastGeneratedProgramHash)
            {
                try
                {
                    var res = Generator.Generate();
                    return res;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
            Generator = null;
            var results = Compiler.Compile(Program);

            if (results.Errors.Any())
            {
                
                foreach (var item in results.Errors)
                {

                }
                return string.Join(",", results.Errors.Select(z => $"{z.Line}: {z.Text}"));
            }

            try
            {
                Assembly asm = results.Assembly;

                Type[] allTypes = asm.GetTypes();

                foreach (Type t in allTypes.Take(1))
                {
                    var inst = Generator = Activator.CreateInstance(t) as IResponseGenerator;
                    //dynamic v = inst;                    
                    LastGeneratedProgramHash = Program.MD5Hash();

                    if (Generator != null)
                    {
                        try
                        {
                            var res = Generator.Generate();
                            return res;
                        }
                        catch (Exception ex)
                        {
                            return ex.Message;
                        }
                    }
                    else
                    {
                        return "";
                    }

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return "";
            }
            return "";
        }
        public override string GetResponse()
        {
            var json = GenerateJson();
            var resp = "HTTP/1.1 200 OK\r\n" +
                "Content-Type: application/json" +
                $"\r\nDate: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}" +
                $"\r\nLast-Modified: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}" +
                $"\r\nContent-Length: {json.Length}\r\n\r\n{json}";

            return resp;
        }

        public override XElement ToXml()
        {
            XElement ret = new XElement("mock");
            ret.Add(new XAttribute("kind", nameof(DynamicJsonResponse)));
            ret.Add(new XElement("data", new XCData(Program)));
            UpdateXmlNode(ret);
            return ret;
        }
    }
}
