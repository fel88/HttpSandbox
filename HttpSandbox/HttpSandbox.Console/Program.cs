using HttpSandbox.Common;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace HttpSandbox.Console
{
    internal class Program
    {
        static (string, string) ParseArg(string str)
        {
            var spl = str.Split("=");
            var ret = spl[1];
            return (spl[0], ret);
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("HttpSandbox server....");
            //args parse --xml=<path>

            int port = 8888;
            if (!args.Any(z => z.StartsWith("--xml=")))
            {
                Usage();
                return;
            }
            var parsedArgs = args.Select(ParseArg);
            if (parsedArgs.Any(z => z.Item1 == "--port"))
            {
                port = int.Parse(parsedArgs.First(z => z.Item1 == "--port").Item2);
            }
            var fr = args.First(z => z.StartsWith("--xml="));
            var spl = fr.Split("=");
            var xmlPath = spl[1];

            var doc = XDocument.Load(xmlPath);
            var server = new Server(doc.Root);

            server.RequestAdded = (r) =>
            {
                System.Console.Write($"request added ");
                var array = new string[]{r.Timestamp.ToString(),
                        r.Raw, r.Raw.Length.ToString(),r.Data != null ? r.Data.Length.ToString() : "-" };
                foreach (var item in array)
                {
                    System.Console.Write($" {item.Replace("\n", "").Replace("\r", "")}");
                }
                System.Console.WriteLine();
            };

            server.RequestUpdated = (r) =>
            {
                /*

                   listView1.Invoke(() =>
                   {
                       for (int i = 0; i < listView1.Items.Count; i++)
                       {
                           if (listView1.Items[i].Tag != r)
                               continue;
                           listView1.Items[i].SubItems[2].Text = r.Raw.Length.ToString();
                           listView1.Items[i].SubItems[3].Text = r.Data != null ? FormatFileSize(r.Data.Length) : "-";
                       }
                   });*/

            };
            System.Console.WriteLine($"init server port: {port}");

            server.InitTcp(IPAddress.Any, port);
            server.th.Join();

        }

        private static void Usage()
        {
            System.Console.WriteLine("usage: --xml=<path>");
        }
    }
}
