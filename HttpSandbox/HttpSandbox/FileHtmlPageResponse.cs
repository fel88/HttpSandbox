
using System.Xml.Linq;

namespace HttpSandbox
{
    public class FileHtmlPageResponse : MockHttpResponse
    {
        public string Path { get; set; }

        public override string GetResponse()
        {
            var text = File.ReadAllText(Path);
            var resp = "HTTP/1.1 200 OK\r\n" +
                "Content-Type: text/html; charset=UTF-8" +
                "\r\nDate: Fri, 21 Jun 2025 14:18:33 GMT" +
                "\r\nLast-Modified: Thu, 17 Oct 2024 07:18:26 GMT" +
                $"\r\nContent-Length: {text.Length}\r\n\r\n{text}";

            return resp;
        }

        public override IEnumerable<Command> GetCommands()
        {
            Command c = new Command()
            {
                Name = "Set HTML file",
                Perform = (z) =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    if (ofd.ShowDialog() != DialogResult.OK)
                        return;

                    (z as FileHtmlPageResponse).Path = ofd.FileName;

                }
            };
            return [c];
        }

        internal override XElement ToXml()
        {
            throw new NotImplementedException();
        }
    }
}
