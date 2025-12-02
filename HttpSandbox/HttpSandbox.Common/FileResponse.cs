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
            ContentType = item.Element("contentType").Value;
        }

        public string Path { get; set; }
        public string ContentType { get; set; } = "application/octet-stream";
        public override string GetResponse()
        {
            var finfo = new FileInfo(Path);            
            
            var resp = "HTTP/1.1 200 OK\r\n" +
                $"Content-Type: {ContentType}" +
                "\r\nDate: Fri, 21 Jun 2025 14:18:33 GMT" +
                $"\r\nLast-Modified: Thu, 17 Oct {DateTime.Now.Year} 07:18:26 GMT" +
                $"\r\nContent-Length: {finfo.Length}\r\n";

            return resp;
        }

        public override async void WriteResponse(StreamWriter writer)
        {
            writer.WriteLine(GetResponse());
            writer.Flush();
            using FileStream fs = new FileStream(Path, FileMode.Open);
            await fs.CopyToAsync(writer.BaseStream);            
        }

        public override XElement ToXml()
        {
            XElement ret = new XElement("mock");
            ret.Add(new XAttribute("kind", nameof(FileResponse)));
            ret.Add(new XElement("path", new XCData(Path)));
            ret.Add(new XElement("contentType", new XCData(ContentType)));
            UpdateXmlNode(ret);

            return ret;
        }
    }
}
