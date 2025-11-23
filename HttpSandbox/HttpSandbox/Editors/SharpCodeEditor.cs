using HttpSandbox.CodeEditor;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace HttpSandbox.Editors
{
    public partial class SharpCodeEditor : Form
    {
        public SharpCodeEditor()
        {
            InitializeComponent();

            ElementHost elementHost = new ElementHost();
            elementHost.Dock = DockStyle.Fill;
            elementHost.Child = Editor = new CodeEditor.CodeEditor();
            Editor.SetHighlighting("C#");

            panel1.Controls.Add(elementHost);
            InitErrorPanel();
        }
        private void InitErrorPanel()
        {
            lv = new ListView();
            lv.GridLines = true;
            lv.FullRowSelect = true;
            lv.View = View.Details;
            lv.Columns.Add("Line", 100);
            lv.Columns.Add("b", 100);
            lv.Columns.Add("c", 100);
            errorPanel.Controls.Add(lv, 0, 0);
            lv.Dock = DockStyle.Fill;
            panel1.Controls.Add(errorPanel);
            errorPanel.Dock = DockStyle.Bottom;
            errorPanel.Visible = false;
            Editor.TextEditor.TextChanged += TextEditor_TextChanged;
        }

        private void TextEditor_TextChanged(object? sender, EventArgs e)
        {
            Save?.Invoke();
        }

        public void Init(string text)
        {
            Editor.Text = text;
        }
        public CodeEditor.CodeEditor Editor;

        public event Action Save;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Save?.Invoke();
        }
        public static string NormalizeCodeWithRoslyn(string csCode)
        {
            var tree = CSharpSyntaxTree.ParseText(csCode);
            var root = tree.GetRoot().NormalizeWhitespace();
            return root.ToFullString();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Editor.Text = NormalizeCodeWithRoslyn(Editor.Text);
        }

        private void jsonGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Text = JsonTemplates.GenerateSample1;
        }

        TableLayoutPanel errorPanel = new TableLayoutPanel();
        ListView lv;

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                var results = Compiler.Compile(Editor.Text);
                errorPanel.Visible = false;

                lv.Items.Clear();
                foreach (var item in results.Errors)
                {
                    errorPanel.Visible = true;
                    lv.Items.Add(new ListViewItem([$"{item.Line}", $"{item.Line}: {item.Text}"]) { Tag = item, BackColor = Color.Pink, ForeColor = Color.White });
                }
                if (results.Assembly != null)
                    MessageBox.Show("No errors", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var offset = Editor.TextEditor.CaretOffset;
            Editor.TextEditor.Document.Insert(offset, $"Debugger.Launch();{Environment.NewLine}");
        }

        private void drapperFetchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.Text = @"using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Npgsql;
using Dapper;
using System.Threading.Tasks;
using HttpSandbox;

class Program : IResponseGenerator
{
    public class Product
    {
        public int Id { get; set; } // Primary Key (by convention)
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public async Task<IEnumerable<T>> QueryRawSqlAsync<T>(string _connectionString, string sql, object parameters = null)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var results = await connection.QueryAsync<T>(sql, parameters);
            return results;
        }
    }

    public string Generate()
    {
        var connection = ""Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=12345"";
        var query = ""select * from public.\""Products\"""";
        var result = QueryRawSqlAsync<Product>(connection, query);
        result.Wait();
        foreach (var product in result.Result)
        {
        //make json here
        }

        return $""[{{'id':'1','count':'{result.Result.Count()}'}}]"".Replace(""'"", ""\"""");
    }
}";
        }
        public static async Task RunTaskWithTimeoutAsync(Action action)
        {
            var longRunningTask = Task.Run(async () =>
            {
                action();
            });

            try
            {
                // Wait for the task to complete, with a 2 second timeout
                await longRunningTask.WaitAsync(TimeSpan.FromSeconds(5));
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The task timed out.");
                // The original task is still running in the background, 
                // but the flow of control has handled the timeout.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        int RunTimeoutSec = 10;//todo make configurable with AutoDialog
        private async void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                var results = Compiler.Compile(Editor.Text);
                errorPanel.Visible = false;

                lv.Items.Clear();
                foreach (var item in results.Errors)
                {
                    errorPanel.Visible = true;
                    lv.Items.Add(new ListViewItem([$"{item.Line}", $"{item.Line}: {item.Text}"]) { Tag = item, BackColor = Color.Pink, ForeColor = Color.White });
                }

                Assembly asm = results.Assembly;

                Type[] allTypes = asm.GetTypes();

                foreach (Type t in allTypes.Take(1))
                {
                    var inst = Activator.CreateInstance(t) as IResponseGenerator;
                    //dynamic v = inst;
                    string res = string.Empty;
                    await Task.Run(() =>
                    {
                        res = inst.Generate();

                    }).WaitAsync(TimeSpan.FromSeconds(RunTimeoutSec)); 


                    MessageBox.Show(res, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
