using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace HttpSandbox
{
    public partial class mdi : Form
    {
        public mdi()
        {
            InitializeComponent();
            Instance = this;
            Form1 f = new Form1();
            f.MdiParent = this;
            f.Show();
        }

        public static mdi Instance;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.MdiParent = this;
            f.Show();

        }
    }
}
