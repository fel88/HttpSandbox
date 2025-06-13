using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HttpSandbox
{
    public partial class Server
    {
        public Thread th;
        public List<ConnectionInfo> streams = new List<ConnectionInfo>();
        public List<HttpRequestInfo> Requests = new List<HttpRequestInfo>();
        public Action<HttpRequestInfo> RequestAdded;
        public void InitTcp(IPAddress ip, int port, Func<object> factory = null)
        {
            server1 = new TcpListener(ip, port);
            server1.Start();


            th = new Thread(() =>
            {
                while (true)
                {
                    var client = server1.AcceptTcpClient();
                    Console.WriteLine("client accepted");
                    lock (streams)
                    {
                        var stream = client.GetStream();
                        var addr = (client.Client.RemoteEndPoint as IPEndPoint).Address;
                        var _port = (client.Client.RemoteEndPoint as IPEndPoint).Port;
                        var obj = factory != null ? factory() : null;
                        var cinf = new ConnectionInfo() { Stream = stream, Client = client, Ip = addr, Port = _port, Tag = obj };

                        streams.Add(cinf);
                        Thread thp = new Thread(() => { ThreadProcessor(stream, cinf); });
                        thp.IsBackground = true;
                        thp.Start();
                    }

                }
            });
            th.IsBackground = true;
            th.Start();
        }

        private TcpListener server1;
        public void ThreadProcessor(NetworkStream stream, object obj)
        {

            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            while (true)
            {
                try
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        break;
                    if (line.Contains(" HTTP/"))//http start
                    {
                        ParseHTTPRequest(line, reader);
                        //response ok
                        var okResp = "HTTP/1.1 200 OK\r\nContent-Type: application/json";

                        writer.WriteLine(okResp);
                        writer.Flush();
                    }

                }
                catch (IOException iex)
                {

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    break;

                }
            }
        }

        private void ParseHTTPRequest(string startLine, StreamReader reader)
        {
            HttpRequestInfo request = new HttpRequestInfo();
            request.Raw += startLine + Environment.NewLine;
        
            request.Timestamp = DateTime.Now; 
            Requests.Add(request);
            RequestAdded?.Invoke(request);
            int clen = 0;
            while (true)
            {
                try
                {
                    var line = reader.ReadLine();
                    request.Raw += line + Environment.NewLine;
                    if (line.ToLower().StartsWith("content-length"))//http start
                    {
                        var spl = line.Split(new char[] { ':' });
                        request.ContentLength = int.Parse(spl[1]);
                    }

                    if (string.IsNullOrEmpty(line.Trim()) || line == "\r\n")
                    {
                        char[] buf = new char[request.ContentLength];
                        reader.Read(buf, 0, request.ContentLength);
                        request.Raw += Encoding.Default.GetString(buf.Select(z => (byte)z).ToArray());

                       
                        return;
                    }

                }
                catch (IOException iex)
                {

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    break;

                }
            }
        }
    }
}
