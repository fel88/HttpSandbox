using AutoDialog.Extensions;
using HttpMultipartParser;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Security.Permissions;
using System.Text;
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
                    item.IsEnabled.ToString() })
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

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            commandsToolStripMenuItem.DropDownItems.Clear();


            if (mocksListView.SelectedItems.Count == 0)
                return;

            var obj = mocksListView.SelectedItems[0].Tag as MockHttpResponse;
            var cmds = obj.GetCommands();
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

        }
    }

}
