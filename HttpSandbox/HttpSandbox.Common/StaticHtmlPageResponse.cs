using System.Xml.Linq;

namespace HttpSandbox
{
    public class StaticHtmlPageResponse : MockHttpResponse
    {
        public StaticHtmlPageResponse()
        {

        }

        public StaticHtmlPageResponse(XElement item):base(item)
        {
            Html = item.Element("content").Value;
           
        }

        public string Html { get; set; } = "<!doctype html>\r\n<!-- HTML content follows -->" +
                                    "<html><header></header><body>" +
                                    "Hello world!</body></html>";

        public override string GetResponse()
        {
            var resp = "HTTP/1.1 200 OK\r\n" +
                "Content-Type: text/html; charset=UTF-8" +
                "\r\nDate: Fri, 21 Jun 2025 14:18:33 GMT" +
                "\r\nLast-Modified: Thu, 17 Oct 2024 07:18:26 GMT" +
                $"\r\nContent-Length: {Html.Length}\r\n\r\n{Html}";

            return resp;
        }
                
        public override XElement ToXml()
        {
            XElement ret = new XElement("mock");
            ret.Add(new XAttribute("kind", nameof(StaticHtmlPageResponse)));
            ret.Add(new XElement("content", new XCData(Html)));
            UpdateXmlNode(ret);
            return ret;
        }

        
    }
}
