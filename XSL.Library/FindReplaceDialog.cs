using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XSL.Library
{
    public partial class FindReplaceDialog : Form
    {
        private XSLRichTextBox _richTextBox = null;
        private bool _origHideSelect;

        public FindReplaceDialog(XSLRichTextBox rtfBox)
        {
            InitializeComponent();
            _richTextBox = rtfBox;
            _origHideSelect = _richTextBox.HideSelection;
            _richTextBox.HideSelection = false;
            
            findButton.Click += new EventHandler(findButton_Click);
            findNextButton.Click += new EventHandler(findNextButton_Click);
            replaceButton.Click += new EventHandler(replaceButton_Click);
            replaceAllButton.Click += new EventHandler(replaceAllButton_Click);
        }

        void replaceAllButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            _richTextBox.Text = _richTextBox.Text.Replace(findTextBox.Text,
                replaceTextBox.Text);
            _richTextBox.SyntaxHighlightAll();
            Cursor.Current = Cursors.Default;
        }

        void replaceButton_Click(object sender, EventArgs e)
        {
            if (_richTextBox.SelectionLength > 0)
            {
                _richTextBox.SelectedText = replaceTextBox.Text;
            }

            DoFind(_richTextBox.SelectionStart + 1);            
        }

        void findNextButton_Click(object sender, EventArgs e)
        {
            DoFind(_richTextBox.SelectionStart + 1);
        }

        void findButton_Click(object sender, EventArgs e)
        {
            DoFind(0);
        }

        private void DoFind(int startPos)
        {
            StringComparison compareType = (matchCaseCheckBox.Checked) ?
                StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;

            startPos = _richTextBox.Text.IndexOf(findTextBox.Text, startPos, compareType);

            if (startPos == -1)
            {
                MessageBox.Show("No matches found.", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _richTextBox.Select(startPos, findTextBox.Text.Length);
            _richTextBox.ScrollToCaret();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _richTextBox.HideSelection = _origHideSelect;
        }
    }
}