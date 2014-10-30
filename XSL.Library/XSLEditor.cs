
#region Using Directives
using Common.Library.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Windows.Forms;

using Actions = XSL.Library.ActionExecuteEventArgs.Actions;
#endregion

namespace XSL.Library
{    
    public partial class XSLEditor : UserControl
    {
        #region Declarations
        private bool _showToolStrip = true;
        private bool _showStatusStrip = true;
        private bool _autoValidateXsl = true;
        private bool _showValidationResults = true;
        private DateTime _lastValidationDT;
        private Color _validateOkBackColor = Color.PaleGreen;
        private Color _validateOkForeColor = Color.Black;
        private Color _validateErrBackColor = Color.LightPink;
        private Color _validateErrForeColor = Color.Black;
        private int _lastCharPrintedPos = 0;
        private string _transformResults = string.Empty;
        private bool _transformDebuggingEnabled = true;        
        #endregion

        public event ActionExecuteHandler ActionBeforeExecute;
        public event ActionExecuteHandler ActionAfterExecute;
        
        #region Constructors
        public XSLEditor()
        {
            InitializeComponent();

            xslTextBox.EnableIntellisense = false;
            
            xslTextBox.ContentChanged += xslTextBox_ContentChanged;
            xslTextBox.FilenameChanged += xslTextBox_FilenameChanged;
            xslTextBox.KeyDown += xslTextBox_KeyDown;
            xslTextBox.MouseDown += xslTextBox_MouseDown;
            xslTextBox.MouseUp += xslTextBox_MouseUp;
            xslTextBox.PropertyChanged += xslTextBox_PropertyChanged;
            xslTextBox.SelectionChanged += xslTextBox_SelectionChanged;            
            
            // toolbar handlers
            autoValidateToolStripButton.Click += OnToggleAutoValidate;
            copyToolStripButton.Click += OnCopy;
            cutToolStripButton.Click += OnCut;
            enableIntellsenseToolStripButton.Click += OnToggleEnableIntellisense;
            findToolStripButton.Click += OnFindReplace;
            newToolStripButton.Click += OnNew;
            openToolStripButton.Click += OnOpen;
            pageSetupToolStripButton.Click += OnPageSetup;
            pasteToolStripButton.Click += OnPaste;
            printToolStripButton.Click += OnPrint;
            printPreviewToolStripButton.Click += OnPrintPreview;
            saveToolStripButton.Click += OnSave;
            saveAsToolStripButton.Click += OnSaveAs;
            showValidationResultsToolStripButton.Click += OnToggleValidationResults;
            transformXslToolStripButton.Click += OnTransform;
            validateXslToolStripButton.Click += OnValidateXSL;
            wordWrapToolStripButton.Click += OnToggleWordwrap;            
                        
            // context menu handlers
            autoValidateXSLToolStripMenuItem.Click += OnToggleAutoValidate;
            copyToolStripMenuItem.Click += OnCopy;
            cutToolStripMenuItem.Click += OnCut;
            enableIntellisenceToolStripMenuItem.Click += OnToggleEnableIntellisense;
            findToolStripMenuItem.Click += OnFindReplace;
            newToolStripMenuItem.Click += OnNew;
            openToolStripMenuItem.Click += OnOpen;
            pageSetupToolStripMenuItem.Click += OnPageSetup;
            pasteToolStripMenuItem.Click += OnPaste;
            printToolStripMenuItem.Click += OnPrint;
            printPreviewToolStripMenuItem.Click += OnPrintPreview;
            saveToolStripMenuItem.Click += OnSave;
            saveAsToolStripMenuItem.Click += OnSaveAs;
            showStatusBarToolStripMenuItem.Click += OnToggleStatusbar;
            showToolbarToolStripMenuItem.Click += OnToggleToolStrip;
            showValidationResultsToolStripMenuItem.Click += OnToggleValidationResults;
            transformToolStripMenuItem.Click += OnTransform;
            validateToolStripMenuItem.Click += OnValidateXSL;
            wordwrapToolStripMenuItem.Click += OnToggleWordwrap;            
        }        
        #endregion

        //*********************************************************************
        //* PROPERTIES
        //*********************************************************************

        #region Public Appearance Properties
        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets whether default toolstrip will be shown")]
        public bool ShowToolStrip
        {
            get { return _showToolStrip; }
            set
            {
                
                _showToolStrip = value;
                xslToolStrip.Visible = value;
            }
        }

        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets whether default status strip will be shown")]
        public bool ShowStatusStrip
        {
            get { return _showStatusStrip; }
            set
            {
                _showStatusStrip = value;
                statusStrip.Visible = value;
                showStatusBarToolStripMenuItem.Checked = value;
            }
        }

        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets value whether to show XSL validation results")]
        public bool ShowValidationResults
        {
            get { return _showValidationResults; }
            set
            {
                _showValidationResults = value;
                showValidationResultsToolStripButton.Checked = value;
                showValidationResultsToolStripMenuItem.Checked = value;
                validationResultsTextBox.Visible = value;
            }
        }

        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets the title of the control to be shown in toolbar")]
        public string Title
        {
            get { return titleToolStripLabel.Text; }
            set { titleToolStripLabel.Text = value; }
        }

        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets the validation window background color for successful validation")]
        public Color ValidateOkBackColor
        {
            get { return _validateOkBackColor; }
            set
            {
                if (value != _validateOkBackColor && this.DesignMode
                    && xslTextBox.Text.Length == 0)
                {
                    validationResultsTextBox.BackColor = ValidateOkBackColor;
                }

                _validateOkBackColor = value;
            }
        }

        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets the validation window foreground color for successful validation")]
        public Color ValidateOkForeColor
        {
            get { return _validateOkForeColor; }
            set
            {
                _validateOkForeColor = value;
            }
        }

        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets the validation window background color for unsuccessful validation")]
        public Color ValidateErrBackColor
        {
            get { return _validateErrBackColor; }
            set
            {
                _validateErrBackColor = value;
            }
        }

        [Browsable(true),
        Category("Appearance"),
        Description("Gets or sets the validation window foreground color for unsuccessful validation")]
        public Color ValidateErrForeColor
        {
            get { return _validateErrForeColor; }
            set
            {                
                _validateErrForeColor = value;
            }
        }
        #endregion

        #region Public Properties
        [
        Browsable(true),
        Category("Behavior"),
        Description("Gets or sets value indicating if xsl should automatically be validated as text changes")
        ]
        public bool AutoValidateXsl
        {
            get { return _autoValidateXsl; }
            set
            {
                _autoValidateXsl = value;
                autoValidateToolStripButton.Checked = value;
                autoValidateXSLToolStripMenuItem.Checked = value;

                autoValidateToolStripStatusLabel.Text = string.Format("Auto-validate {0}",
                    (true == value) ? "On" : "Off");
            }
        }

        [Browsable(false)]
        public string Filename
        {
            get { return xslTextBox.Filename; }
        }

        public override string Text
        {
            get { return xslTextBox.Text; }
            set { xslTextBox.Text = value; }
        }

        public XSLRichTextBox XslTextBox
        {
            get { return xslTextBox; }
        }
        
        public void LoadFile(string path)
        {
          
          xslTextBox.LoadFile(path, RichTextBoxStreamType.PlainText);
          saveToolStripButton.Enabled = true;
        }                
        
        /// <summary>
        /// True to generate transformation debug information; otherwise false. Setting this to 
        /// true enables you to debug the style sheet with the Microsoft Visual Studio Debugger
        /// </summary>
        [
        Browsable(true),
        Category("Behavior"),
        DefaultValue(true),
        Description("True to generate transformation debug information; otherwise false. Setting this to "
            + "true enables you to debug the style sheet with the Microsoft Visual Studio Debugger")
        ]
        public bool TransformDebuggingEnabled
        {
            get { return _transformDebuggingEnabled; }
            set { _transformDebuggingEnabled = value; }
        }        
        #endregion

        //*********************************************************************
        //* METHODS
        //*********************************************************************

        #region Virtual Methods
        protected virtual void OnCopy(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.Copy)) return;
            xslTextBox.Copy();
            OnActionAfterExecute(Actions.Copy);
        }

        protected virtual void OnCut(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.Cut)) return;
            xslTextBox.Cut();
            OnActionAfterExecute(Actions.Cut);
        }

        protected virtual void OnFindReplace(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.FindReplace)) return;

            FindReplaceDialog findForm = new FindReplaceDialog(xslTextBox);
            findForm.Show();

            OnActionAfterExecute(Actions.FindReplace);
        }

        protected virtual void OnNew(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.New)) return;
            xslTextBox.NewFile();
            xslTextBox.Text = Properties.Resources.NewXSLTemplate;
            OnActionAfterExecute(Actions.New);
        }

        protected virtual void OnOpen(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.Open)) return;
            Open();
            OnActionAfterExecute(Actions.Open);
        }        

        protected virtual void OnPageSetup(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.PageSetup)) return;
            PageSetup();
            OnActionAfterExecute(Actions.PageSetup);
        }

        protected virtual void OnPaste(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.Paste)) return;
            xslTextBox.Paste();
            OnActionAfterExecute(Actions.Paste);
        }

        protected virtual void OnPrint(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.Print)) return;
            Print();
            OnActionAfterExecute(Actions.Print);
        }

        protected virtual void OnPrintPreview(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.PrintPreview)) return;
            PrintPreview();
            OnActionAfterExecute(Actions.PrintPreview);
        }

        protected virtual void OnSave(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.Save)) return;
            Save();
            OnActionAfterExecute(Actions.Save);
        }        

        protected virtual void OnSaveAs(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.SaveAs)) return;
            SaveFileAs();
            OnActionAfterExecute(Actions.SaveAs);
        }

        protected virtual void OnTransform(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.Transform)) return;
            Transform();
            OnActionAfterExecute(Actions.Transform);
        }

        protected virtual void OnToggleAutoValidate(object sender, EventArgs e)
        {
            this.AutoValidateXsl = !this.AutoValidateXsl;
        }

        protected virtual void OnToggleEnableIntellisense(object sender, EventArgs e)
        {
            enableIntellsenseToolStripButton.Checked = !enableIntellsenseToolStripButton.Checked;
            xslTextBox.EnableIntellisense = enableIntellsenseToolStripButton.Checked;
        }

        protected virtual void OnToggleStatusbar(object sender, EventArgs e)
        {
            ShowStatusStrip = !ShowStatusStrip;            
        }

        protected virtual void OnToggleToolStrip(object sender, EventArgs e)
        {
            ShowToolStrip = !ShowToolStrip;
        }

        protected virtual void OnToggleValidationResults(object sender, EventArgs e)
        {
            this.ShowValidationResults = !this.ShowValidationResults;
        }              

        protected virtual void OnValidateXSL(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(Actions.ValidateXSL)) return;

            ValidateXSL();

            // if validate was invoked via user click but validation results are not shown
            // show them so user knows something happened
            if (!validationResultsTextBox.Visible)
            {
                this.ShowValidationResults = true;
            }

            OnActionAfterExecute(Actions.ValidateXSL);
        }

        protected virtual void OnToggleWordwrap(object sender, EventArgs e)
        {
            xslTextBox.WordWrap = !xslTextBox.WordWrap;
            wordWrapToolStripButton.Checked = xslTextBox.WordWrap;
            wordwrapToolStripMenuItem.Checked = xslTextBox.WordWrap;
        }

        protected virtual void TransformDebugClick()
        {
            if (!OnActionBeforeExecute(Actions.TransformDebug)) return;

            OnActionAfterExecute(Actions.TransformDebug);
        }
        #endregion

        #region Public Virtual Methods
        public virtual void Open()
        {
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (openFileDialog.FileName.Length > 0)
                {
                    xslTextBox.LoadFile(openFileDialog.FileName,
                        RichTextBoxStreamType.PlainText);
                }
            }
        }

        public virtual void PageSetup()
        {
            pageSetupDialog.ShowDialog();
        }

        public virtual void Print()
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        public virtual void PrintPreview()
        {
            printPreviewDialog.ShowDialog();
        }

        public virtual void Save()
        {
            if (0 == this.Filename.Length)
            {
                SaveFileAs();
            }
            else
            {
                xslTextBox.SaveFile(this.Filename, RichTextBoxStreamType.PlainText);
            }

            xslTextBox.Refresh();
        }

        public virtual void SaveFileAs()
        {
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (saveFileDialog.FileName.Length > 0)
                {
                    xslTextBox.SaveFile(saveFileDialog.FileName,
                        RichTextBoxStreamType.PlainText);
                }
            }
        }

        public virtual void Transform()
        {
            xslTextBox.ShowTransformDialog();
        }

        public virtual void ValidateXSL()
        {
            try
            {
                XPathDocument xPathDoc = new XPathDocument(new StringReader(xslTextBox.Text));
                validationResultsTextBox.Text = "Syntax valid";
                validationResultsTextBox.BackColor = _validateOkBackColor;
                validationResultsTextBox.ForeColor = _validateOkForeColor;
            }
            catch (XmlException xmlEx)
            {
                validationResultsTextBox.Text = xmlEx.Message;
                validationResultsTextBox.BackColor = _validateErrBackColor;
                validationResultsTextBox.ForeColor = _validateErrForeColor;
            }
            finally
            {
                _lastValidationDT = DateTime.Now;
                toolTip.SetToolTip(validationResultsTextBox,
                    "Last validated at " + _lastValidationDT.ToString("G"));                
            }
        }
        #endregion Public Virtual Methods
        

        #region Private Methods
        private bool OnActionBeforeExecute(Actions action)
        {
            ActionExecuteEventArgs e = new ActionExecuteEventArgs(action);

            if (ActionBeforeExecute != null)
            {
                ActionBeforeExecute(this, e);
            }

            return (!e.Cancel);
        }

        private void OnActionAfterExecute(Actions action)
        {
            ActionExecuteEventArgs e = new ActionExecuteEventArgs(action);

            if (ActionAfterExecute != null)
            {
                ActionAfterExecute(this, e);
            }
        }

        private void UpdateCursorPos()
        {
            lineToolStripStatusLabel.Text = "Line: " + xslTextBox.LineNumber.ToString();
            columnToolStripStatusLabel.Text = "Col: " + xslTextBox.ColumnNumber.ToString();
        }
        #endregion

        //*********************************************************************
        //* EVENTS
        //*********************************************************************

        #region ToolStrip Events
        private void xslDebugToolStripButton_Click(object sender, EventArgs e)
        {
            TransformDebugClick();
        }            
        #endregion

        #region xslTextBox events
        private void xslTextBox_ContentChanged()
        {            
            UpdateCursorPos();

            if (_autoValidateXsl)
            {
                ValidateXSL();
            }            
        }

        private void xslTextBox_FilenameChanged()
        {
            filenameToolStripStatusLabel.Text = (this.xslTextBox.Filename.Length > 0)
                ? this.xslTextBox.Filename : "Untitled";
        }

        private void xslTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // shortcut keys for context menu strip will only fire when context menu is shown
            if (e.KeyCode == Keys.F && e.Control)
            {
                OnFindReplace(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.N && e.Control)
            {
                OnNew(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.O && e.Control)
            {
                OnOpen(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.P && e.Control)
            {
                OnPrint(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.S && e.Control)
            {
                OnSave(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.T && e.Control)
            {
                OnTransform(this, new EventArgs());
            }
        }

        private void xslTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip.Show(xslTextBox, e.Location);
            }
        }           

        private void xslTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateCursorPos();
        }

        private void xslTextBox_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Dirty")
            {
                saveToolStripButton.Enabled = xslTextBox.Dirty;
                saveToolStripMenuItem.Enabled = saveToolStripButton.Enabled;
            }
        }

        private void xslTextBox_SelectionChanged(object sender, EventArgs e)
        {
            UpdateCursorPos();
            cutToolStripButton.Enabled = (xslTextBox.SelectionLength > 0);
            copyToolStripButton.Enabled = (xslTextBox.SelectionLength > 0);
            
            cutToolStripMenuItem.Enabled = cutToolStripButton.Enabled;
            copyToolStripMenuItem.Enabled = copyToolStripButton.Enabled;
        }
        #endregion xslTextBox events

        #region StatusStrip Events
        private void autoValidateToolStripStatusLabel_DoubleClick(object sender, EventArgs e)
        {
            this.AutoValidateXsl = !AutoValidateXsl;
        }
        #endregion        
                      
        #region Printing Events
        private void printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _lastCharPrintedPos = 0;
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            _lastCharPrintedPos = xslTextBox.Print(_lastCharPrintedPos, xslTextBox.TextLength, e);

            // Check for more pages
            if (_lastCharPrintedPos < xslTextBox.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }
        
        private void printDocument_EndPrint(object sender, PrintEventArgs e)
        {
            //xslTextBox.WordWrap = false;
        }
        #endregion                
               
    }

    #region Event Related
    public delegate void ActionExecuteHandler(object sender, ActionExecuteEventArgs e);

    public class ActionExecuteEventArgs : System.EventArgs
    {
        public enum Actions
        {
            Copy, Cut, FindReplace, New, Open, PageSetup, Paste, Print, PrintPreview, 
            Save, SaveAs, Transform, TransformDebug, ValidateXSL
        }

        private bool _cancel = false;

        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }
        private Actions _action;

        public Actions Action
        {
            get { return _action; }
            set { _action = value; }
        }

        public ActionExecuteEventArgs(Actions action)
        {
            _action = action;
        }
    }
    #endregion
}
