
#region Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
#endregion

namespace Common.Library.Controls
{
    public delegate void FilenameChangedHandler();
    public delegate void ContentChangedHandler();

    public class SyntaxRichTextBox : RichTextBox, INotifyPropertyChanged
    {
        //*********************************************************************
        //* DECLARATIONS & PROPERTIES
        //*********************************************************************

        #region Declarations
        //public events
        public event FilenameChangedHandler FilenameChanged;
        public event ContentChangedHandler ContentChanged;

        //private declarations
        private SyntaxSettings _syntaxSettings = new SyntaxSettings();
        private string _filename = string.Empty;
        private bool _dirty = false;
        protected static bool m_bPaint = true;
        private int m_nLineEnd = 0;
        private int m_nContentLength = 0;
        private bool skipOnTextChanged = false;
        
        private const int FIRST_VISIBLE_LINE = 206;
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;

        //protected
        protected int m_nLineLength = 0;
        protected int m_nLineStart = 0;        
        protected string m_strKeywords = "";
        protected int m_nCurSelection = 0;
        protected string m_strLine = "";
        
        #endregion

        [DllImport("user32")]
        public static extern int LockWindowUpdate(IntPtr hwndLock);

        #region Printing Declarations
        //Convert the unit used by the .NET framework (1/100 inch) 
        //and the unit used by Win32 API calls (twips 1/1440 inch)
        private const double anInch = 14.4;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE
        {
            public int cpMin;         //First character of range (0 for start of doc)
            public int cpMax;           //Last character of range (-1 for end of doc)
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE
        {
            public IntPtr hdc;             //Actual DC to draw on
            public IntPtr hdcTarget;       //Target DC for determining text formatting
            public RECT rc;                //Region of the DC to draw to (in twips)
            public RECT rcPage;            //Region of the whole DC (page size) (in twips)
            public CHARRANGE chrg;         //Range of text to draw (see earlier declaration)
        }

        private const int WM_USER = 0x0400;
        private const int EM_FORMATRANGE = WM_USER + 57;
        
        [DllImport("USER32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        #endregion

        #region Public Properties
        [Browsable(false)]
        public int ColumnNumber
        {
            get
            {
                Point p = new Point();
                p.X = this.GetLineFromCharIndex(this.SelectionStart);
                p.Y = this.SelectionStart - this.GetFirstCharIndexFromLine(p.X);
                return p.Y + 1;
            }
        }

        [Browsable(false)]
        public bool Dirty
        {
            get { return _dirty; }
            set
            {
                _dirty = value;
                NotifyPropertyChanged("Dirty");
            }
        }

        /// <summary>
        /// Gets first visible line in richtextbox
        /// </summary>
        /// <remarks>Too much trouble to be included in RichTextBox?</remarks>
        [Browsable(false)]
        public int FirstVisibleLine
        {
            get
            {
                return (int) SendMessage(this.Handle, FIRST_VISIBLE_LINE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Gets last visible line in richtextbox
        /// </summary>
        /// <remarks>Too much trouble to be included in RichTextBox?</remarks>
        [Browsable(false)]       
        public int LastVisibleLine
        {
            get
            {
                int lastVisibleLine = FirstVisibleLine + (this.Height / this.Font.Height);
                if (lastVisibleLine > this.Lines.Length || 0 == lastVisibleLine)
                {
                    lastVisibleLine = this.Lines.Length;
                }
                return lastVisibleLine;
            }
        }

        [Browsable(false)]
        public string Filename
        {
            get { return _filename; }
            protected set
            {
                _filename = value;
                OnFilenameChanged();
                NotifyPropertyChanged("Filename");
            }
        }
        
        [Browsable(false)]
        public int LineNumber
        {
            get
            {
                Point p = new Point();
                p.X = this.GetLineFromCharIndex(this.SelectionStart);
                return p.X + 1;
            }
        }

        public new string[] Lines
        {
            get { return base.Lines; }
            set
            {
                skipOnTextChanged = true;

                base.Lines = value;
                OnContentChanged();
                ProcessAllLines();

                skipOnTextChanged = false;
            }
        }

        public string CurrentLine
        {
            get
            {
                int curSelStart = SelectionStart;
                // Find the start of the current line.
                int lineStart = curSelStart;
                while ((lineStart > 0) && (Text[lineStart - 1] != '\n'))
                    lineStart--;
                // Find the end of the current line.
                int lineEnd = curSelStart;
                while ((lineEnd < Text.Length) && (Text[lineEnd] != '\n'))
                    lineEnd++;
                // Calculate the length of the line.
                int lineLength = lineEnd - lineStart;
                // Get the current line.
                string line = Text.Substring(lineStart, lineLength);
                return (line);
            }
        }

        public string CurrentSyntaxLine
        {
            get { return m_strLine; }
        }

        /// <summary>
        /// Gets or sets the settings for syntax highlighting
        /// </summary>
        [Category("Appearance"),
        Description("Gets or sets the settings for syntax highlighting"),
        TypeConverter(typeof(ExpandableObjectConverter)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SyntaxSettings SyntaxSettings
        {
            get { return _syntaxSettings; }
            //set needed for proper design-time interaction
            set { _syntaxSettings = value; }
        }

        public new string Text
        {
            get { return base.Text; }
            set
            {
                skipOnTextChanged = true;

                base.Text = value;
                OnContentChanged();
                ProcessAllLines();

                skipOnTextChanged = false;
            }
        }
        #endregion

        #region Protected Properties
        protected bool ShouldPaint
        {
            get { return m_bPaint; }
            set { m_bPaint = value; }
        }
        #endregion Protected Properties

        //*********************************************************************
        //* METHODS
        //*********************************************************************

        #region File Related Methods
        public void NewFile()
        {
            this.Clear();
            this.Filename = string.Empty;
        }

        public new void LoadFile(string path)
        {
            skipOnTextChanged = true;

            base.LoadFile(path);
            OnContentChanged();
            this.Filename = path;

            ProcessAllLines();

            skipOnTextChanged = false;
            //Dirty = false;
        }

        public new void LoadFile(string path, RichTextBoxStreamType fileType)
        {
            skipOnTextChanged = true;

            base.LoadFile(path, fileType);
            OnContentChanged();
            this.Filename = path;

            ProcessAllLines();

            skipOnTextChanged = false;
            Dirty = false;
        }

        public new void LoadFile(Stream data, RichTextBoxStreamType fileType)
        {
            skipOnTextChanged = true;

            base.LoadFile(data, fileType);
            OnContentChanged();

            ProcessAllLines();

            skipOnTextChanged = false;
            Dirty = false;
        }

        public new void SaveFile(string path)
        {
            try
            {
                base.SaveFile(path);
                this.Filename = path;
                Dirty = false;
            }
            catch (UnauthorizedAccessException uae)
            {
                MessageBox.Show("No access to save file. Verify file is not read-only.",
                    "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public new void SaveFile(string path, RichTextBoxStreamType fileType)
        {
            try
            {
                base.SaveFile(path, fileType);
                this.Filename = path;
                Dirty = false;
            }
            catch (UnauthorizedAccessException uae)
            {
                MessageBox.Show("No access to save file. Verify file is not read-only.",
                    "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Display Related Methods
        public new void Refresh()
        {
            ShouldPaint = true;
            base.Refresh();
        }
        #endregion
        
        #region Printing Methods
        // Render the contents of the RichTextBox for printing
        //	Return the last character printed + 1 (printing start from this point for next page)
        public int Print(int charFrom, int charTo, PrintPageEventArgs e)
        {
            //Calculate the area to render and print
            RECT rectToPrint;
            rectToPrint.Top = (int)(e.MarginBounds.Top * anInch);
            rectToPrint.Bottom = (int)(e.MarginBounds.Bottom * anInch);
            rectToPrint.Left = (int)(e.MarginBounds.Left * anInch);
            rectToPrint.Right = (int)(e.MarginBounds.Right * anInch);

            //Calculate the size of the page
            RECT rectPage;
            rectPage.Top = (int)(e.PageBounds.Top * anInch);
            rectPage.Bottom = (int)(e.PageBounds.Bottom * anInch);
            rectPage.Left = (int)(e.PageBounds.Left * anInch);
            rectPage.Right = (int)(e.PageBounds.Right * anInch);

            IntPtr hdc = e.Graphics.GetHdc();

            FORMATRANGE fmtRange;
            fmtRange.chrg.cpMax = charTo;				//Indicate character from to character to 
            fmtRange.chrg.cpMin = charFrom;
            fmtRange.hdc = hdc;                    //Use the same DC for measuring and rendering
            fmtRange.hdcTarget = hdc;              //Point at printer hDC
            fmtRange.rc = rectToPrint;             //Indicate the area on page to print
            fmtRange.rcPage = rectPage;            //Indicate size of page

            IntPtr res = IntPtr.Zero;

            IntPtr wparam = IntPtr.Zero;
            wparam = new IntPtr(1);

            //Get the pointer to the FORMATRANGE structure in memory
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lparam, false);

            //Send the rendered data for printing 
            res = SendMessage(Handle, EM_FORMATRANGE, wparam, lparam);

            //Free the block of memory allocated
            Marshal.FreeCoTaskMem(lparam);

            //Release the device context handle obtained by a previous call
            e.Graphics.ReleaseHdc(hdc);

            //Return last + 1 character printer
            return res.ToInt32();
        }
        #endregion
        
        #region Keyword Syntax Related Methods
        /// <summary>
        /// "Compiles" the keywords as a regular expression
        /// (appends keywords to single keyword string for regex operations)
        /// </summary>
        public void CompileKeywords()
        {
            for (int i = 0; i < SyntaxSettings.Keywords.Length; i++)
            {
                string strKeyword = SyntaxSettings.Keywords[i];

                if (i == SyntaxSettings.Keywords.Length - 1)
                    m_strKeywords += "\\b" + strKeyword + "\\b";

                else
                    m_strKeywords += "\\b" + strKeyword + "\\b|";
            }
        }

        /// <summary>
        /// Processes all lines for syntax highlighting
        /// </summary>
        protected virtual void ProcessAllLines()
        {
            ShouldPaint = false;
            LockWindowUpdate(this.Handle);

            m_strLine = this.Text;
            m_nLineStart = 0;
            m_nLineEnd = m_nLineStart + m_strLine.Length;

            ProcessLine();

            // ProcessLine loop removed (line set to entire text)

            ShouldPaint = true;
            LockWindowUpdate(IntPtr.Zero);
        }

        /// <summary>
        /// Processes a line for syntax highlighting.
        /// In the case of some operations (load etc.) the line may
        /// be the entire contents of the richtextbox
        /// </summary> 
        protected virtual void ProcessLine()
        {
            // Save the position and make the whole line black
            int nPosition = SelectionStart;
            SelectionStart = m_nLineStart;
            SelectionLength = m_nLineLength;
            SelectionColor = Color.Black;

            // Process the keywords
            ProcessRegex(m_strKeywords,SyntaxSettings.KeywordColor);
            // Process numbers
            if (SyntaxSettings.EnableIntegers)
                ProcessRegex("\\b(?:[0-9]*\\.)?[0-9]+\\b", SyntaxSettings.IntegerColor);
            // Process strings
            if (SyntaxSettings.EnableStrings)
                ProcessRegex("\"[^\"\\\\\\r\\n]*(?:\\\\.[^\"\\\\\\r\\n]*)*\"", SyntaxSettings.StringColor);
            // Process comments
            if (SyntaxSettings.EnableComments && !String.IsNullOrEmpty(SyntaxSettings.CommentPattern))
                ProcessRegex(SyntaxSettings.CommentPattern + ".*", SyntaxSettings.CommentColor); 

            SelectionStart = nPosition;
            SelectionLength = 0;
            SelectionColor = Color.Black;

            m_nCurSelection = nPosition;
        }

        /// <summary>
        /// Processes a regular expression for syntax highlighting
        /// </summary>
        /// <param name="strRegex">Regular expression string pattern</param>
        /// <param name="color">color to make the result of matches</param>
        protected virtual void ProcessRegex(string strRegex, Color color)
        {
            skipOnTextChanged = true;

            Regex regKeywords = new Regex(strRegex, RegexOptions.IgnoreCase); //| RegexOptions.Compiled);

            int nStart, nLenght;

            for (Match regMatch = regKeywords.Match(m_strLine); regMatch.Success; regMatch = regMatch.NextMatch())
            {
                // Process the words
                nStart = m_nLineStart + regMatch.Index;
                nLenght = regMatch.Length;
                SelectionStart = nStart;
                SelectionLength = nLenght;
                SelectionColor = color;
            }

            skipOnTextChanged = false;
        }

        protected virtual void ResumeOnTextChanged()
        {
            skipOnTextChanged = false;
        }

        protected virtual void SuspendOnTextChanged()
        {
            skipOnTextChanged = true;
        }
        #endregion
        

        //*********************************************************************
        //* EVENTS
        //*********************************************************************

        #region Events and Event Related Methods
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.KeyUp += new KeyEventHandler(OnKeyUp);
        }

        protected virtual void OnFilenameChanged()
        {
            if (FilenameChanged != null)
            {
                FilenameChanged();
            }
        }

        void OnKeyUp(object sender, KeyEventArgs e)
        {
            // For some reason the RichTextBox doesn't call Paste() directly on
            // Ctrl+V or Ctrl+Insert
            if (e.Control && (e.KeyCode == Keys.V || e.KeyCode == Keys.Insert))
            {
                Paste();
                //this.SelectedText = Clipboard.GetText(TextDataFormat.Text);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// 
        /// WndProc
        /// 
        /// 
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            try
            {
                if (_logMsg) Debug.WriteLine(m.ToString());

                if (m.Msg == 0x00f)
                {
                    if (ShouldPaint)
                        base.WndProc(ref m);
                    else
                        m.Result = IntPtr.Zero;
                }                
                else if (m.Msg == 0x0302) // WM_PASTE
                {
                    //base.WndProc(ref m);
                    //m.Result = IntPtr.Zero;
                    //ProcessAllLines(); // process new lines
                }                
                else
                    base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
                throw;
            }
        }

        protected bool _logMsg = false;
        /// 
        /// OnTextChanged
        /// 
        /// 
        protected override void OnTextChanged(EventArgs e)
        {
            //_logMsg = true;
            if (skipOnTextChanged)
                return;

            // Calculate stuff here.
            m_nContentLength = this.TextLength;

            int nCurrentSelectionStart = SelectionStart;
            int nCurrentSelectionLength = SelectionLength;

            //this.Enabled = false;
            ShouldPaint = false;

            //helps reduce flickering when we move the cursor during coloring w/scrollbar changes
            LockWindowUpdate(this.Handle);  

            // Find the start of the current line.
            m_nLineStart = nCurrentSelectionStart;
            while ((m_nLineStart > 0) && (Text[m_nLineStart - 1] != '\n'))
                m_nLineStart--;
            // Find the end of the current line.
            m_nLineEnd = nCurrentSelectionStart;
            while ((m_nLineEnd < Text.Length) && (Text[m_nLineEnd] != '\n'))
                m_nLineEnd++;
            // Calculate the length of the line.
            m_nLineLength = m_nLineEnd - m_nLineStart;
            // Get the current line.
            m_strLine = Text.Substring(m_nLineStart, m_nLineLength);

            // Process this line.
            ProcessLine();
                        
            //this.Enabled = true;            
            //this.Focus();
            ShouldPaint = true;
            LockWindowUpdate(IntPtr.Zero);

            OnContentChanged();
            //_logMsg = false;
        }

        protected virtual void OnContentChanged()
        {
            if (this.ContentChanged != null)
            {
                this.ContentChanged();
                Dirty = true;
            }
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        
    } // end class SyntaxRichTextBox    
}
