using System.Reflection;
using System.Xml.Linq;

namespace HttpSandbox
{
    public class DynamicFileResponse : MockHttpResponse
    {
        public DynamicFileResponse()
        {

        }

        public DynamicFileResponse(XElement item) : base(item)
        {
            ContentType = item.Element("contentType").Value;
            Program = item.Element("data").Value;

        }
        public string Program { get; set; }

        IDynamicResponseGenerator Generator { get; set; }
        string LastGeneratedProgramHash;

        private void Compile()
        {
            var hash1 = Program.MD5Hash();
            if (Generator != null && hash1 == LastGeneratedProgramHash)            
                return;
            
            Generator = null;
            var results = Compiler.Compile(Program);

            if (results.Errors.Any())
            {

                foreach (var item in results.Errors)
                {

                }
                //  return string.Join(",", results.Errors.Select(z => $"{z.Line}: {z.Text}"));
                return;
            }

            try
            {
                Assembly asm = results.Assembly;

                Type[] allTypes = asm.GetTypes();

                foreach (Type t in allTypes.Take(1))
                {
                    var inst = Generator = Activator.CreateInstance(t) as IDynamicResponseGenerator;
                    //dynamic v = inst;                    
                    LastGeneratedProgramHash = Program.MD5Hash();

                    if (Generator != null)
                    {
                        try
                        {
                            //var res = Generator.Generate();
                           // return res;
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                    else
                    {
                        return ;
                    }

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return ;
            }
            return ;
        }

        public string ContentType { get; set; } = "application/octet-stream";
        public override string GetResponse(HttpRequestInfo request)
        {
            Compile();
            var resp = "HTTP/1.1 200 OK\r\n" +
                $"Content-Type: {Generator.GetMime(request)}" +
                "\r\nDate: Fri, 21 Jun 2025 14:18:33 GMT" +
                $"\r\nLast-Modified: Thu, 17 Oct {DateTime.Now.Year} 07:18:26 GMT" +
                $"\r\nContent-Length: {Generator.GetLength(request)}\r\n";

            return resp;
        }

        public override async void WriteResponse(HttpRequestInfo request, StreamWriter writer)
        {
            writer.WriteLine(GetResponse(request));
            writer.Flush();
            await Generator.WriteToStream(request, writer.BaseStream);            
        }

        public override XElement ToXml()
        {
            XElement ret = new XElement("mock");
            ret.Add(new XAttribute("kind", nameof(DynamicFileResponse)));
            ret.Add(new XElement("data", new XCData(Program)));

            ret.Add(new XElement("contentType", new XCData(ContentType)));
            UpdateXmlNode(ret);

            return ret;
        }
    }
}
