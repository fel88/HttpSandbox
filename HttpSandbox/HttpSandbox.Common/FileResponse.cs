using System.Xml.Linq;

namespace HttpSandbox
{
    public class FileResponse : MockHttpResponse
    {
        public FileResponse()
        {

        }

        public FileResponse(XElement item) : base(item)
        {
            Path = item.Element("path").Value;
        }

        public string Path { get; set; }

        public override string GetResponse()
        {
            var file = File.ReadAllBytes(Path);
            var resp = "HTTP/1.1 200 OK\r\n" +
                "Content-Type: application/octet-stream" +
                "\r\nDate: Fri, 21 Jun 2025 14:18:33 GMT" +
                $"\r\nLast-Modified: Thu, 17 Oct {DateTime.Now.Year} 07:18:26 GMT" +
                $"\r\nContent-Length: {file.Length}\r\n";

            return resp;
        }

        public override void WriteResponse(StreamWriter writer)
        {
            var file = File.ReadAllBytes(Path);

            writer.WriteLine(GetResponse());
            writer.Flush();

            writer.BaseStream.Write(file, 0, file.Length);
        }

        public override XElement ToXml()
        {
            XElement ret = new XElement("mock");
            ret.Add(new XAttribute("kind", nameof(FileResponse)));
            ret.Add(new XElement("path", new XCData(Path)));
            UpdateXmlNode(ret);

            return ret;
        }
    }
}
