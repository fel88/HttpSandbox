using AutoDialog.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpSandbox
{
    public partial class FiltersEditor : Form
    {
        public FiltersEditor()
        {
            InitializeComponent();
        }

        MockHttpResponse response;

        internal void Init(MockHttpResponse obj)
        {
            response = obj;
            UpdateList();
        }

        public void UpdateList()
        {
            listView1.Items.Clear();
            foreach (var item in response.Filters)
            {
                listView1.Items.Add(new ListViewItem(new string[] {
                    item.GetType()
                    .Name
                })
                { Tag = item });
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void containsTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            response.Filters.Add(new ContainsTextHttpFilter());
            UpdateList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            listView1.SelectedItems[0].Tag.EditWithAutoDialog();
            UpdateList();
        }
    }
}
