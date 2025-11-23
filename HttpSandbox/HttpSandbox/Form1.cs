using AutoDialog.Extensions;
using HttpMultipartParser;
using HttpSandbox.Common;
using HttpSandbox.Editors;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Windows.Input;
using System.Xml.Linq;

namespace HttpSandbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Server server = new Server();
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var d = AutoDialog.DialogHelpers.StartDialog();
            d.AddIntegerNumericField("port", "Port", 8888, max: int.MaxValue);

            var topMost = TopMost;
            TopMost = false;

            if (d.ShowDialog(this) != DialogResult.OK)
                return;

            TopMost = topMost;

            var port = d.GetIntegerNumericField("port");
            server.RequestAdded = (r) =>
            {
                listView1.Invoke(() =>
                {
                    listView1.Items.Add(new ListViewItem([r.Timestamp.ToString(),
                        r.Raw, r.Raw.Length.ToString(),r.Data!=null? r.Data.Length.ToString():"-"])
                    { Tag = r });
                });

            };

            server.RequestUpdated = (r) =>
            {
                listView1.Invoke(() =>
                {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        if (listView1.Items[i].Tag != r)
                            continue;
                        listView1.Items[i].SubItems[2].Text = r.Raw.Length.ToString();
                        listView1.Items[i].SubItems[3].Text = r.Data != null ? FormatFileSize(r.Data.Length) : "-";
                    }
                });

            };
            server.InitTcp(IPAddress.Any, port);
            toolStripButton1.Enabled = false;
            Text = $"Server port: {port}";
        }

        public static string FormatFileSize(long bytes)
        {
            var unit = 1024;
            if (bytes < unit)
                return $"{bytes} B";

            var exp = (int)(Math.Log(bytes) / Math.Log(unit));
            return $"{bytes / Math.Pow(unit, exp):F2} {("KMGTPE")[exp - 1]}B";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            richTextBox1.Text = (listView1.SelectedItems[0].Tag as HttpRequestInfo).Raw;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var d = AutoDialog.DialogHelpers.StartDialog();
            d.AddBoolField("topmost", "Top most", TopMost);

            d.AddIntegerNumericField("uploadBufferSize", "upload packet size ", Server.UploadBufferSize, min: 1024, max: 1024 * 1024 * 128);
            d.AddIntegerNumericField("uploadBufferDelay", "upload packet delay", Server.UploadBufferDelay, min: 0, max: 10000);

            d.Shown += (s, e) =>
            {

                var c1 = d.Controls[0] as TableLayoutPanel;
                int max = 0;
                for (int i = 0; i < c1.Controls.Count; i++)
                {
                    var cntr = c1.Controls[i];
                    if (cntr is Label label)
                    {
                        max = Math.Max(max, label.Width);
                        label.Width *= 2;
                    }
                }
                d.Width += max;

            };

            d.TopMost = TopMost;
            if (!d.ShowDialog())
                return;

            TopMost = d.GetBoolField("topmost");
            Server.UploadBufferSize = d.GetIntegerNumericField("uploadBufferSize");
            Server.UploadBufferDelay = d.GetIntegerNumericField("uploadBufferDelay");

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void staticHtmlPageResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Mocks.Add(new StaticHtmlPageResponse());
            UpdateMocksList();
        }

        private void UpdateMocksList()
        {
            mocksListView.Items.Clear();
            foreach (var item in server.Mocks)
            {
                mocksListView.Items.Add(new ListViewItem(new string[] { item.Name,
                    item.GetType().Name,
                    item.IsEnabled.ToString() ,
                    item.Priority.ToString()
                })
                { Tag = item });
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mocksListView.SelectedItems.Count == 0)
                return;

            mocksListView.SelectedItems[0].Tag.EditWithAutoDialog();
            UpdateMocksList();
        }

        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mocksListView.SelectedItems.Count == 0)
                return;

            var obj = mocksListView.SelectedItems[0].Tag as MockHttpResponse;
            var ind = server.Mocks.IndexOf(obj);
            if (ind > 0)
            {
                server.Mocks.Remove(obj);
                server.Mocks.Insert(ind--, obj);
                UpdateMocksList();
            }
        }

        private void status200ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Mocks.Add(new Status200Response());
            UpdateMocksList();
        }

        private void filtersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mocksListView.SelectedItems.Count == 0)
                return;

            var obj = mocksListView.SelectedItems[0].Tag as MockHttpResponse;
            FiltersEditor fed = new FiltersEditor();
            fed.Init(obj);
            fed.ShowDialog();
        }

        private void helloWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Mocks.Clear();
            server.Mocks.Add(new StaticHtmlPageResponse());
            server.Mocks[0].Filters.Add(new ContainsTextHttpFilter() { Filter = "GET" });

            server.Mocks.Add(new Status200Response());
            server.Mocks[1].Filters.Add(new ContainsTextHttpFilter() { Filter = "POST" });
            UpdateMocksList();
        }

        public Command[] GetCommands(MockHttpResponse mock)
        {
            switch (mock)
            {
                case StaticHtmlPageResponse shpr:
                    {
                        var c = new Command()
                        {
                            Name = "load HTML from file",
                            Perform = (z) =>
                            {
                                OpenFileDialog ofd = new OpenFileDialog();
                                if (ofd.ShowDialog() != DialogResult.OK)
                                    return;

                                shpr.Html = File.ReadAllText(ofd.FileName);

                            }
                        };
                        var c2 = new Command()
                        {
                            Name = "edit HTML",
                            Perform = (z) =>
                            {

                                TextEditor editor = new TextEditor();
                                editor.MdiParent = mdi.Instance;
                                editor.Init(shpr.Html);
                                editor.Show();
                                editor.Save += () =>
                                {
                                    shpr.Html = editor.Editor.Text;
                                };

                                editor.FormClosing += (s, e) =>
                                {
                                    shpr.Html = editor.Editor.Text;
                                };
                            }
                        };
                        return [c2, c];
                    }
                case EmbeddedJsonFileResponse ejfr:
                    {
                        var c = new Command()
                        {
                            Name = "load from file",
                            Perform = (z) =>
                            {
                                OpenFileDialog ofd = new OpenFileDialog();
                                if (ofd.ShowDialog() != DialogResult.OK)
                                    return;

                                ejfr.Json = File.ReadAllText(ofd.FileName);

                            }
                        };
                        var c2 = new Command()
                        {
                            Name = "edit",
                            Perform = (z) =>
                            {

                                TextEditor editor = new TextEditor();
                                editor.MdiParent = mdi.Instance;

                                editor.Editor.SetHighlighting("Json");
                                editor.Init(ejfr.Json);
                                editor.Show();
                                editor.Save += () =>
                                {
                                    ejfr.Json = editor.Editor.Text;
                                };
                                editor.FormClosing += (s, e) =>
                                {
                                    ejfr.Json = editor.Editor.Text;
                                };


                            }
                        };
                        return [c2, c];
                    }
                case DynamicJsonResponse djr:
                    {

                        var c2 = new Command()
                        {
                            Name = "edit",
                            Perform = (z) =>
                            {
                                SharpCodeEditor editor = new SharpCodeEditor();
                                editor.MdiParent = mdi.Instance;

                                editor.Init(djr.Program);
                                editor.Show();
                                editor.Save += () =>
                                {
                                    djr.Program = editor.Editor.Text;
                                };
                                /*editor.FormClosing += (s, e) =>
                                {
                                    djr.Program = editor.Editor.Text;
                                };*/


                            }
                        };
                        return [c2];
                    }
                case FileHtmlPageResponse:
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
                case ImgFileResponse:
                    {
                        Command c = new Command()
                        {
                            Name = "Set image",
                            Perform = (z) =>
                            {
                                OpenFileDialog ofd = new OpenFileDialog();
                                if (ofd.ShowDialog() != DialogResult.OK)
                                    return;

                                (z as ImgFileResponse).Path = ofd.FileName;

                            }
                        };
                        return [c];
                    }
                case FileResponse:
                    {
                        Command c = new Command()
                        {
                            Name = "Set file path",
                            Perform = (z) =>
                            {
                                OpenFileDialog ofd = new OpenFileDialog();
                                if (ofd.ShowDialog() != DialogResult.OK)
                                    return;

                                (z as FileResponse).Path = ofd.FileName;

                            }
                        };
                        return [c];
                    }
            }

            return [];
        }



        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            commandsToolStripMenuItem.DropDownItems.Clear();


            if (mocksListView.SelectedItems.Count == 0)
                return;

            var obj = mocksListView.SelectedItems[0].Tag as MockHttpResponse;
            var cmds = GetCommands(obj);
            foreach (var item in cmds)
            {
                ToolStripMenuItem tsp = new ToolStripMenuItem();
                tsp.Text = item.Name;
                tsp.Click += (sender, e) =>
                {
                    item.Perform(obj);
                };
                commandsToolStripMenuItem.DropDownItems.Add(tsp);

            }
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var req = (listView1.SelectedItems[0].Tag as HttpRequestInfo);
            File.WriteAllBytes(sfd.FileName, req.Data);
        }

        private async void parseMultipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;


            var req = (listView1.SelectedItems[0].Tag as HttpRequestInfo);

            var ms = new MemoryStream(req.Data);

            List<byte[]> blocks = new List<byte[]>();
            List<byte> accum = new List<byte>();

            var parser = MultipartFormDataParser.Parse(ms);

            foreach (var file in parser.Files)
            {
                Stream data = file.Data;

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = file.FileName;
                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                MemoryStream ms2 = new MemoryStream();
                data.CopyTo(ms2);

                File.WriteAllBytes(sfd.FileName, ms2.ToArray());

            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.TopMost = TopMost;
            about.Show(this);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Server XML (*.sxml)|*.sxml";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            File.WriteAllText(sfd.FileName, server.ToXml().ToString());
        }

        private void helloWorld2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Mocks.Clear();
            server.Mocks.Add(new FileHtmlPageResponse());
            server.Mocks[0].Filters.Add(new ContainsTextHttpFilter() { Filter = "GET" });

            server.Mocks.Add(new Status200Response());
            server.Mocks[1].Filters.Add(new ContainsTextHttpFilter() { Filter = "POST" });
            UpdateMocksList();
        }

        private void htmlFilePageResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Mocks.Add(new FileHtmlPageResponse());
            UpdateMocksList();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Server XML (*.sxml)|*.sxml";

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var doc = XDocument.Load(ofd.FileName);
            server = new Server(doc.Root);


            UpdateMocksList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mocksListView.SelectedItems.Count == 0)
                return;

            var item = mocksListView.SelectedItems[0].Tag as MockHttpResponse;
            server.Mocks.Remove(item);
            UpdateMocksList();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string url = $"http://localhost:{server.Port}";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

        }

        private void fileDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Mocks.Clear();
            var s1 = new StaticHtmlPageResponse();

            s1.Html = "<!doctype html>\r\n<!-- HTML content follows -->" +
                                    "<html><header></header><body>" +
                                    "Hello world!<p><a href=\"file1.png\" download>\r\n    Download File\r\n</a></p><p><a href=\"file1.png\" >\r\n    Show File\r\n</a></p></body></html>";
            server.Mocks.Add(s1);
            server.Mocks[0].Filters.Add(new ContainsTextHttpFilter() { Filter = "GET" });

            server.Mocks.Add(new Status200Response());
            server.Mocks[1].Filters.Add(new ContainsTextHttpFilter() { Filter = "POST" });

            Bitmap bmp = new Bitmap(200, 30);
            var gr = Graphics.FromImage(bmp);
            gr.Clear(Color.LightGreen);
            var text = "Hello world!";
            var font = new Font("Consolas", 18);
            var mss = gr.MeasureString(text, font);
            gr.DrawString(text, font, Brushes.White, bmp.Width / 2 - mss.Width / 2, bmp.Height / 2 - mss.Height / 2);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            server.Mocks.Add(new EmbeddedImgFileResponse() { Data = ms.ToArray(), Priority = 10 });
            server.Mocks[2].Filters.Add(new ContainsTextHttpFilter() { Filter = "GET " });
            server.Mocks[2].Filters.Add(new ContainsTextHttpFilter() { Filter = "file1.png" });
            UpdateMocksList();
        }

        private void fileResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Mocks.Add(new FileResponse());
            UpdateMocksList();
        }

        private void staticJsonFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Mocks.Add(new EmbeddedJsonFileResponse());
            UpdateMocksList();
        }

        private void mocksListView_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (mocksListView.SelectedItems.Count == 0)
                return;

            var item = mocksListView.SelectedItems[0].Tag as MockHttpResponse;
            var cmnds = GetCommands(item);
            if (cmnds != null && cmnds.Any())
            {
                cmnds[0].Perform(item);
            }
        }

        private void dynamicJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Mocks.Add(new DynamicJsonResponse());
            UpdateMocksList();
        }

        private void staticJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Mocks.Clear();
            var s1 = new StaticHtmlPageResponse();

            s1.Html = "<!doctype html>\r\n<!-- HTML content follows -->" +
                                    "<html><header></header><body>" +
                                    "Hello world!<p><a href=\"data.json\" download>\r\n    Download File\r\n</a></p>" +
                                    "<a href=\"data.json\" >\r\n    Show File\r\n</a>" +
                                    "</body></html>";
            server.Mocks.Add(s1);
            server.Mocks[0].Filters.Add(new ContainsTextHttpFilter() { Filter = "GET" });

            server.Mocks.Add(new DynamicJsonResponse() { Program = JsonTemplates.GenerateSample1, Priority = 10 });
            server.Mocks[1].Filters.Add(new ContainsTextHttpFilter() { Filter = "data.json" });

            UpdateMocksList();
        }

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mocksListView.SelectedItems.Count == 0)
                return;

            var m = mocksListView.SelectedItems[0].Tag as MockHttpResponse;
            server.Mocks.Add(m.Clone());
            UpdateMocksList();
        }
    }

}
