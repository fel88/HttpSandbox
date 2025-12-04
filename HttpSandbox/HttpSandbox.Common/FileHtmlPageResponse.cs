
using System.Xml.Linq;

namespace HttpSandbox
{
    public class FileHtmlPageResponse : MockHttpResponse
    {
        public FileHtmlPageResponse()
        {

        }

        public FileHtmlPageResponse(XElement item) : base(item)
        {
            Path = item.Element("path").Value;

        }

        public string Path { get; set; }

        public override string GetResponse(HttpRequestInfo request)
        {
            var text = File.ReadAllText(Path);
            var resp = "HTTP/1.1 200 OK\r\n" +
                "Content-Type: text/html; charset=UTF-8" +
                "\r\nDate: Fri, 21 Jun 2025 14:18:33 GMT" +
                "\r\nLast-Modified: Thu, 17 Oct 2024 07:18:26 GMT" +
                $"\r\nContent-Length: {text.Length}\r\n\r\n{text}";

            return resp;
        }

  
        public override XElement ToXml()
        {
            XElement ret = new XElement("mock");
            ret.Add(new XAttribute("kind", nameof(FileHtmlPageResponse)));
            ret.Add(new XElement("path", new XCData(Path)));
            UpdateXmlNode(ret);

            return ret;
        }
    }
}
