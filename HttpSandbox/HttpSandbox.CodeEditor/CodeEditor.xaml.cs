using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Search;
using System.Windows.Controls;

namespace HttpSandbox.CodeEditor
{
    /// <summary>
    /// Interaction logic for CodeEditor.xaml
    /// </summary>
    public partial class CodeEditor : UserControl
    {
        public CodeEditor()
        {
            InitializeComponent();
            SearchPanel.Install(textEditor);
        }

        public void SetHighlighting(string name)
        {
            textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(name);
        }

        public TextEditor TextEditor => textEditor;

        public string Text { get => textEditor.Text; set => textEditor.Text = value; }
    }
}
