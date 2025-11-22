
using System.Xml.Linq;

namespace HttpSandbox
{
    public class EmbeddedImgFileResponse : MockHttpResponse
    {
        public EmbeddedImgFileResponse()
        {

        }

        public EmbeddedImgFileResponse(XElement item) : base(item)
        {
            Data = Convert.FromBase64String(item.Element("data").Value);
        }

        public byte[] Data { get; set; }

        public override string GetResponse()
        {
            var resp = "HTTP/1.1 200 OK\r\n" +
                "Content-Type: image/png" +
                "\r\nDate: Fri, 21 Jun 2025 14:18:33 GMT" +
                $"\r\nLast-Modified: Thu, 17 Oct {DateTime.Now.Year} 07:18:26 GMT" +
                $"\r\nContent-Length: {Data.Length}\r\n";

            return resp;
        }

        public override void WriteResponse(StreamWriter writer)
        {
            writer.WriteLine(GetResponse());
            writer.Flush();

            writer.BaseStream.Write(Data, 0, Data.Length);
        }

        public override XElement ToXml()
        {
            XElement ret = new XElement("mock");
            ret.Add(new XAttribute("kind", nameof(EmbeddedImgFileResponse)));
            ret.Add(new XElement("data", Convert.ToBase64String(Data)));            
            UpdateXmlNode(ret);
            return ret;
        }
    }
}
