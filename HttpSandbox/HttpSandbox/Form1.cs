using System.IO;
using System.Net;
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
                    listView1.Items.Add(new ListViewItem(new string[] { r.Timestamp.ToString(), r.Raw }) { Tag = r });
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
    }
}
