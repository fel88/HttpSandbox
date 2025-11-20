
using System.Xml.Linq;

namespace HttpSandbox
{
    public class ImgFileResponse : MockHttpResponse
    {
        public ImgFileResponse()
        {

        }

        public ImgFileResponse(XElement item) : base(item)
        {
            Path = item.Element("path").Value;

        }

        public string Path { get; set; }

        public override string GetResponse()
        {
            var text = File.ReadAllBytes(Path);
            var resp = "HTTP/1.1 200 OK\r\n" +
                "Content-Type: image/jpeg; charset=UTF-8" +
                "\r\nDate: Fri, 21 Jun 2025 14:18:33 GMT" +
                "\r\nLast-Modified: Thu, 17 Oct 2024 07:18:26 GMT" +
                $"\r\nContent-Length: {text.Length}\r\n\r\n";

            return resp;
        }

        public override void WriteResponse(StreamWriter writer)
        {
            writer.WriteLine(GetResponse());
            var text = File.ReadAllBytes(Path);

            writer.BaseStream.Write(text, 0, text.Length);
            writer.Flush();
        }

        public override XElement ToXml()
        {

            XElement ret = new XElement("mock");
            ret.Add(new XAttribute("kind", nameof(ImgFileResponse)));
            ret.Add(new XElement("path", new XCData(Path)));
            ret.Add(FiltersToXml());
            return ret;
        }
    }
}
