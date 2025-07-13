using System.IO;
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
        public Action<HttpRequestInfo> RequestUpdated;
        public List<MockHttpResponse> Mocks = new List<MockHttpResponse>();

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
            BinaryReader reader = new BinaryReader(stream);
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
                        var request = ParseHTTPRequest(line, stream, reader);
                        //response ok
                        if (request != null)
                        {
                            var fr = Mocks.FirstOrDefault(z => z.IsApplicable(request));
                            if (fr != null)
                            {
                                var resp = fr.GetResponse();
                                writer.WriteLine(resp);
                                writer.Flush();
                            }
                        }
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

        public static int UploadBufferSize = 16 * 1024;
        public static int UploadBufferDelay = 0;
        private HttpRequestInfo ParseHTTPRequest(string startLine, NetworkStream stream, BinaryReader reader)
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
                        if (request.ContentLength > 0)
                        {

                            List<char> buf = new List<char>();


                            int remains = request.ContentLength;
                            List<byte> bts2 = new List<byte>();

                            while (remains > 0)
                            {
                                if (UploadBufferDelay > 0)                                
                                    Thread.Sleep(UploadBufferDelay);
                                
                                var chunk = reader.ReadBytes(Math.Min(UploadBufferSize, remains));
                                bts2.AddRange(chunk);
                                remains -= chunk.Length;
                            }


                            var bts = bts2.ToArray();
                            request.Data = bts;
                            request.RawData = buf.Select(z => (byte)z).ToArray();
                            request.Raw += new string(buf.ToArray());
                        }
                        RequestUpdated?.Invoke(request);
                        return request;
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
            return null;
        }
    }
}
