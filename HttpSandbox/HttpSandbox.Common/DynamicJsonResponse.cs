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

        private string GenerateJson()
        {
            var results = Compiler.compile(Program);

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
                    var inst = Activator.CreateInstance(t);
                    //dynamic v = inst;                    
                    var mf2 = t.GetMethods().FirstOrDefault(z => z.Name.ToLower().Contains("generate"));

                    if (mf2 != null)
                    {
                        try
                        {
                            var res = mf2.Invoke(inst, new object[] { }) as string;
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
