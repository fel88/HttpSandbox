using AutoDialog.Extensions;
using System.IO;
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
                    listView1.Items.Add(new ListViewItem(new string[] { r.Timestamp.ToString(), r.Raw,r.Raw.Length.ToString() }) { Tag = r });
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
                    }                    
                });

            };
            server.InitTcp(IPAddress.Any, port);
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

            if (!d.ShowDialog())
                return;

            TopMost = d.GetBoolField("topmost");
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
    }
    
}
