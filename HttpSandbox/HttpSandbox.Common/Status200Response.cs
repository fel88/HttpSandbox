using System.Xml.Linq;

namespace HttpSandbox
{
    public class Status200Response : MockHttpResponse
    {
        public Status200Response() { }
        public Status200Response(XElement elem) : base(elem) { }

        public override string GetResponse()
        {
            return "HTTP/1.1 200 OK\r\nContent-Type: application/json\r\nContent-Length: 20\r\n\r\n{\"success\":\"true\"}";
            //HTTP/1.1 200 OK\r\nContent-Type: application/json\r\n\r\n           
        }

        public override XElement ToXml()
        {
            XElement ret = new XElement("mock");
            ret.Add(new XAttribute("kind", nameof(Status200Response)));
            ret.Add(FiltersToXml());

            return ret;
        }
    }
}
