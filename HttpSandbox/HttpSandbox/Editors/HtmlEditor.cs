using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace HttpSandbox.Editors
{
    public partial class HtmlEditor : Form
    {
        public HtmlEditor()
        {
            InitializeComponent();
            ElementHost elementHost = new ElementHost();
            elementHost.Dock = DockStyle.Fill;
            elementHost.Child = Editor = new CodeEditor.CodeEditor();
            Controls.Add(elementHost);
        }

        public void Init(string text)
        {
            Editor.Text = text;
        }
        public CodeEditor.CodeEditor Editor;
    }
}
