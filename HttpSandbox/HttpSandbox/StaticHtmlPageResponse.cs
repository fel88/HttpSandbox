
namespace HttpSandbox
{
    public class StaticHtmlPageResponse : MockHttpResponse
    {
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

        public override IEnumerable<Command> GetCommands()
        {
            Command c = new Command()
            {
                Name = "load HTML from file",
                Perform = (z) =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    if (ofd.ShowDialog() != DialogResult.OK)
                        return;

                    (z as StaticHtmlPageResponse).Html = File.ReadAllText(ofd.FileName);

                }
            };
            return new[] { c };
        }
    }
}
