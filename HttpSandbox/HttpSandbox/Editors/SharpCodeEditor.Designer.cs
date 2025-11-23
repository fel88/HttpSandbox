namespace HttpSandbox.Editors
{
    partial class SharpCodeEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharpCodeEditor));
            toolStrip1 = new ToolStrip();
            toolStripButton4 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripButton5 = new ToolStripButton();
            toolStripButton3 = new ToolStripDropDownButton();
            jsonGenerationToolStripMenuItem = new ToolStripMenuItem();
            drapperFetchToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            toolStripButton1 = new ToolStripButton();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton4, toolStripButton1, toolStripButton2, toolStripButton5, toolStripButton3 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton4
            // 
            toolStripButton4.Image = Properties.Resources.compile;
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(70, 22);
            toolStripButton4.Text = "compile";
            toolStripButton4.Click += toolStripButton4_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.Image = Properties.Resources.edit_outdent_rtl;
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(63, 22);
            toolStripButton2.Text = "format";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripButton5
            // 
            toolStripButton5.Image = Properties.Resources.bug__plus;
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(86, 22);
            toolStripButton5.Text = "+debugger";
            toolStripButton5.Click += toolStripButton5_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton3.DropDownItems.AddRange(new ToolStripItem[] { jsonGenerationToolStripMenuItem, drapperFetchToolStripMenuItem });
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(63, 22);
            toolStripButton3.Text = "samples";
            // 
            // jsonGenerationToolStripMenuItem
            // 
            jsonGenerationToolStripMenuItem.Name = "jsonGenerationToolStripMenuItem";
            jsonGenerationToolStripMenuItem.Size = new Size(159, 22);
            jsonGenerationToolStripMenuItem.Text = "json generation ";
            jsonGenerationToolStripMenuItem.Click += jsonGenerationToolStripMenuItem_Click;
            // 
            // drapperFetchToolStripMenuItem
            // 
            drapperFetchToolStripMenuItem.Name = "drapperFetchToolStripMenuItem";
            drapperFetchToolStripMenuItem.Size = new Size(159, 22);
            drapperFetchToolStripMenuItem.Text = "drapper fetch";
            drapperFetchToolStripMenuItem.Click += drapperFetchToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 425);
            panel1.TabIndex = 1;
            // 
            // toolStripButton1
            // 
            toolStripButton1.Image = Properties.Resources.compile_error;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(102, 22);
            toolStripButton1.Text = "compile + run";
            toolStripButton1.Click += toolStripButton1_Click_1;
            // 
            // SharpCodeEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Name = "SharpCodeEditor";
            Text = "SharpCodeEditor";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton2;
        private Panel panel1;
        private ToolStripDropDownButton toolStripButton3;
        private ToolStripMenuItem jsonGenerationToolStripMenuItem;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripMenuItem drapperFetchToolStripMenuItem;
        private ToolStripButton toolStripButton1;
    }
}