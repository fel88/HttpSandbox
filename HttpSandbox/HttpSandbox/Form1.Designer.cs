namespace HttpSandbox
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStrip1 = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            helloWorldToolStripMenuItem = new ToolStripMenuItem();
            helloWorld2ToolStripMenuItem = new ToolStripMenuItem();
            fileDownloadToolStripMenuItem = new ToolStripMenuItem();
            toolStripButton3 = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            contextMenuStrip2 = new ContextMenuStrip(components);
            contentToolStripMenuItem = new ToolStripMenuItem();
            saveToFileToolStripMenuItem = new ToolStripMenuItem();
            parseMultipToolStripMenuItem = new ToolStripMenuItem();
            updateToolStripMenuItem = new ToolStripMenuItem();
            richTextBox1 = new RichTextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            mocksListView = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            addToolStripMenuItem = new ToolStripMenuItem();
            staticHtmlPageResponseToolStripMenuItem = new ToolStripMenuItem();
            status200ToolStripMenuItem = new ToolStripMenuItem();
            htmlFilePageResponseToolStripMenuItem = new ToolStripMenuItem();
            fileResponseToolStripMenuItem = new ToolStripMenuItem();
            staticJsonFileToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            filtersToolStripMenuItem = new ToolStripMenuItem();
            moveToolStripMenuItem = new ToolStripMenuItem();
            upToolStripMenuItem = new ToolStripMenuItem();
            downToolStripMenuItem = new ToolStripMenuItem();
            commandsToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            dynamicJsonToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripButton1, toolStripButton2, toolStripDropDownButton2, toolStripButton3, toolStripButton4 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(980, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem });
            toolStripDropDownButton1.Image = Properties.Resources.database;
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(67, 22);
            toolStripDropDownButton1.Text = "server";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = Properties.Resources.disk;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(97, 22);
            saveToolStripMenuItem.Text = "save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Image = Properties.Resources.folder_open;
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(97, 22);
            loadToolStripMenuItem.Text = "load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.Image = Properties.Resources.control;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(45, 22);
            toolStripButton1.Text = "run";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.Image = Properties.Resources.gear;
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(68, 22);
            toolStripButton2.Text = "settings";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripDropDownButton2
            // 
            toolStripDropDownButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { helloWorldToolStripMenuItem, helloWorld2ToolStripMenuItem, fileDownloadToolStripMenuItem });
            toolStripDropDownButton2.Image = (Image)resources.GetObject("toolStripDropDownButton2.Image");
            toolStripDropDownButton2.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            toolStripDropDownButton2.Size = new Size(57, 22);
            toolStripDropDownButton2.Text = "presets";
            // 
            // helloWorldToolStripMenuItem
            // 
            helloWorldToolStripMenuItem.Name = "helloWorldToolStripMenuItem";
            helloWorldToolStripMenuItem.Size = new Size(153, 22);
            helloWorldToolStripMenuItem.Text = "hello world";
            helloWorldToolStripMenuItem.Click += helloWorldToolStripMenuItem_Click;
            // 
            // helloWorld2ToolStripMenuItem
            // 
            helloWorld2ToolStripMenuItem.Name = "helloWorld2ToolStripMenuItem";
            helloWorld2ToolStripMenuItem.Size = new Size(153, 22);
            helloWorld2ToolStripMenuItem.Text = "file single page";
            helloWorld2ToolStripMenuItem.Click += helloWorld2ToolStripMenuItem_Click;
            // 
            // fileDownloadToolStripMenuItem
            // 
            fileDownloadToolStripMenuItem.Name = "fileDownloadToolStripMenuItem";
            fileDownloadToolStripMenuItem.Size = new Size(153, 22);
            fileDownloadToolStripMenuItem.Text = "file download";
            fileDownloadToolStripMenuItem.Click += fileDownloadToolStripMenuItem_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.Alignment = ToolStripItemAlignment.Right;
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = Properties.Resources.question;
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(23, 22);
            toolStripButton3.Text = "toolStripButton3";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripButton4
            // 
            toolStripButton4.Image = Properties.Resources.globe_network;
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(113, 22);
            toolStripButton4.Text = "show in browser";
            toolStripButton4.Click += toolStripButton4_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(listView1, 0, 0);
            tableLayoutPanel1.Controls.Add(richTextBox1, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(966, 369);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader7 });
            listView1.ContextMenuStrip = contextMenuStrip2;
            listView1.Dock = DockStyle.Fill;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Location = new Point(3, 3);
            listView1.Name = "listView1";
            listView1.Size = new Size(477, 363);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Timestamp";
            columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "URL";
            columnHeader2.Width = 160;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Data size";
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { contentToolStripMenuItem, updateToolStripMenuItem });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(116, 48);
            // 
            // contentToolStripMenuItem
            // 
            contentToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToFileToolStripMenuItem, parseMultipToolStripMenuItem });
            contentToolStripMenuItem.Name = "contentToolStripMenuItem";
            contentToolStripMenuItem.Size = new Size(115, 22);
            contentToolStripMenuItem.Text = "content";
            // 
            // saveToFileToolStripMenuItem
            // 
            saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            saveToFileToolStripMenuItem.Size = new Size(182, 22);
            saveToFileToolStripMenuItem.Text = "save raw body to file";
            saveToFileToolStripMenuItem.Click += saveToFileToolStripMenuItem_Click;
            // 
            // parseMultipToolStripMenuItem
            // 
            parseMultipToolStripMenuItem.Name = "parseMultipToolStripMenuItem";
            parseMultipToolStripMenuItem.Size = new Size(182, 22);
            parseMultipToolStripMenuItem.Text = "save file";
            parseMultipToolStripMenuItem.Click += parseMultipToolStripMenuItem_Click;
            // 
            // updateToolStripMenuItem
            // 
            updateToolStripMenuItem.Image = Properties.Resources.arrow_circle;
            updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            updateToolStripMenuItem.Size = new Size(115, 22);
            updateToolStripMenuItem.Text = "update";
            updateToolStripMenuItem.Click += updateToolStripMenuItem_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new Point(486, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(477, 363);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 25);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(980, 403);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(972, 375);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Requests";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(mocksListView);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(972, 375);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Handlers";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // mocksListView
            // 
            mocksListView.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader6, columnHeader8 });
            mocksListView.ContextMenuStrip = contextMenuStrip1;
            mocksListView.Dock = DockStyle.Fill;
            mocksListView.FullRowSelect = true;
            mocksListView.GridLines = true;
            mocksListView.Location = new Point(3, 3);
            mocksListView.Name = "mocksListView";
            mocksListView.Size = new Size(966, 369);
            mocksListView.TabIndex = 0;
            mocksListView.UseCompatibleStateImageBehavior = false;
            mocksListView.View = View.Details;
            mocksListView.MouseDoubleClick += mocksListView_MouseDoubleClick;
            // 
            // columnHeader4
            // 
            columnHeader4.Width = 220;
            // 
            // columnHeader5
            // 
            columnHeader5.Width = 220;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Priority";
            columnHeader8.Width = 120;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { addToolStripMenuItem, deleteToolStripMenuItem, editToolStripMenuItem, filtersToolStripMenuItem, moveToolStripMenuItem, commandsToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(181, 158);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { staticHtmlPageResponseToolStripMenuItem, status200ToolStripMenuItem, htmlFilePageResponseToolStripMenuItem, fileResponseToolStripMenuItem, staticJsonFileToolStripMenuItem, dynamicJsonToolStripMenuItem });
            addToolStripMenuItem.Image = Properties.Resources.plus;
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(180, 22);
            addToolStripMenuItem.Text = "add";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // staticHtmlPageResponseToolStripMenuItem
            // 
            staticHtmlPageResponseToolStripMenuItem.Name = "staticHtmlPageResponseToolStripMenuItem";
            staticHtmlPageResponseToolStripMenuItem.Size = new Size(209, 22);
            staticHtmlPageResponseToolStripMenuItem.Text = "static html page response";
            staticHtmlPageResponseToolStripMenuItem.Click += staticHtmlPageResponseToolStripMenuItem_Click;
            // 
            // status200ToolStripMenuItem
            // 
            status200ToolStripMenuItem.Name = "status200ToolStripMenuItem";
            status200ToolStripMenuItem.Size = new Size(209, 22);
            status200ToolStripMenuItem.Text = "status 200";
            status200ToolStripMenuItem.Click += status200ToolStripMenuItem_Click;
            // 
            // htmlFilePageResponseToolStripMenuItem
            // 
            htmlFilePageResponseToolStripMenuItem.Name = "htmlFilePageResponseToolStripMenuItem";
            htmlFilePageResponseToolStripMenuItem.Size = new Size(209, 22);
            htmlFilePageResponseToolStripMenuItem.Text = "html file page response";
            htmlFilePageResponseToolStripMenuItem.Click += htmlFilePageResponseToolStripMenuItem_Click;
            // 
            // fileResponseToolStripMenuItem
            // 
            fileResponseToolStripMenuItem.Name = "fileResponseToolStripMenuItem";
            fileResponseToolStripMenuItem.Size = new Size(209, 22);
            fileResponseToolStripMenuItem.Text = "file response";
            fileResponseToolStripMenuItem.Click += fileResponseToolStripMenuItem_Click;
            // 
            // staticJsonFileToolStripMenuItem
            // 
            staticJsonFileToolStripMenuItem.Name = "staticJsonFileToolStripMenuItem";
            staticJsonFileToolStripMenuItem.Size = new Size(209, 22);
            staticJsonFileToolStripMenuItem.Text = "static json file";
            staticJsonFileToolStripMenuItem.Click += staticJsonFileToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Image = Properties.Resources.cross;
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(180, 22);
            deleteToolStripMenuItem.Text = "delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Image = Properties.Resources.pencil;
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(180, 22);
            editToolStripMenuItem.Text = "edit";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // filtersToolStripMenuItem
            // 
            filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            filtersToolStripMenuItem.Size = new Size(180, 22);
            filtersToolStripMenuItem.Text = "filters";
            filtersToolStripMenuItem.Click += filtersToolStripMenuItem_Click;
            // 
            // moveToolStripMenuItem
            // 
            moveToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { upToolStripMenuItem, downToolStripMenuItem });
            moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            moveToolStripMenuItem.Size = new Size(180, 22);
            moveToolStripMenuItem.Text = "move";
            // 
            // upToolStripMenuItem
            // 
            upToolStripMenuItem.Name = "upToolStripMenuItem";
            upToolStripMenuItem.Size = new Size(104, 22);
            upToolStripMenuItem.Text = "up";
            upToolStripMenuItem.Click += upToolStripMenuItem_Click;
            // 
            // downToolStripMenuItem
            // 
            downToolStripMenuItem.Name = "downToolStripMenuItem";
            downToolStripMenuItem.Size = new Size(104, 22);
            downToolStripMenuItem.Text = "down";
            // 
            // commandsToolStripMenuItem
            // 
            commandsToolStripMenuItem.Name = "commandsToolStripMenuItem";
            commandsToolStripMenuItem.Size = new Size(180, 22);
            commandsToolStripMenuItem.Text = "commands";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(980, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // dynamicJsonToolStripMenuItem
            // 
            dynamicJsonToolStripMenuItem.Name = "dynamicJsonToolStripMenuItem";
            dynamicJsonToolStripMenuItem.Size = new Size(209, 22);
            dynamicJsonToolStripMenuItem.Text = "dynamic json";
            dynamicJsonToolStripMenuItem.Click += dynamicJsonToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 450);
            Controls.Add(tabControl1);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Http sandbox";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            contextMenuStrip2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private TableLayoutPanel tableLayoutPanel1;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private RichTextBox richTextBox1;
        private ToolStripButton toolStripButton2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ListView mocksListView;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem staticHtmlPageResponseToolStripMenuItem;
        private ToolStripMenuItem filtersToolStripMenuItem;
        private ToolStripMenuItem moveToolStripMenuItem;
        private ToolStripMenuItem upToolStripMenuItem;
        private ToolStripMenuItem downToolStripMenuItem;
        private ToolStripMenuItem status200ToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem helloWorldToolStripMenuItem;
        private ToolStripMenuItem commandsToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem contentToolStripMenuItem;
        private ToolStripMenuItem saveToFileToolStripMenuItem;
        private ToolStripMenuItem parseMultipToolStripMenuItem;
        private ColumnHeader columnHeader7;
        private ToolStripMenuItem updateToolStripMenuItem;
        private ToolStripButton toolStripButton3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem helloWorld2ToolStripMenuItem;
        private ToolStripMenuItem htmlFilePageResponseToolStripMenuItem;
        private ToolStripButton toolStripButton4;
        private ToolStripMenuItem fileDownloadToolStripMenuItem;
        private ColumnHeader columnHeader8;
        private ToolStripMenuItem fileResponseToolStripMenuItem;
        private ToolStripMenuItem staticJsonFileToolStripMenuItem;
        private ToolStripMenuItem dynamicJsonToolStripMenuItem;
    }
}
