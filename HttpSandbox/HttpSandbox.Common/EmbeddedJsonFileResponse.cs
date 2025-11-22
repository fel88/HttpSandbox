
using System.Xml.Linq;

namespace HttpSandbox
{
    public class EmbeddedJsonFileResponse : MockHttpResponse
    {
        public EmbeddedJsonFileResponse()
        {

        }

        public EmbeddedJsonFileResponse(XElement item) : base(item)
        {
            Json = item.Element("data").Value;
        }

        public string Json { get; set; }

        public override string GetResponse()
        {
            var resp = "HTTP/1.1 200 OK\r\n" +
                "Content-Type: application/json" +
                $"\r\nDate: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}" +
                $"\r\nLast-Modified: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}" +
                $"\r\nContent-Length: {Json.Length}\r\n\r\n{Json}";

            return resp;
        }
        
        public override XElement ToXml()
        {
            XElement ret = new XElement("mock");
            ret.Add(new XAttribute("kind", nameof(EmbeddedJsonFileResponse)));
            ret.Add(new XElement("data", new XCData(Json)));
            UpdateXmlNode(ret);
            return ret;
        }
    }
}
