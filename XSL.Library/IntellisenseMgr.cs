
#region Using Directives
using Common.Library.Controls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using XSL.Library.RegularExpressions;

#endregion Using Directives

namespace XSL.Library
{
    //TODO: add <!-- comment to tag list
    //TODO: add </closing tag to tag list
    //TODO: add xsl functions
    //TODO: add xsl attributes
    //TODO: owner-drawn listbox w/graphic icons

    public class IntellisenseMgr
    {
        #region Private Declarations
        private bool _enableIntellisense = true;
        private bool _inAttributeValue = false;
        private string _currentTagText = string.Empty;
        private string _currentTagName = string.Empty;
        private bool _isContextInfoCurrent = false;
        private int _currentTagLineStartPos = 0;
        
        private List<XSLTag> _xslTags;
        private List<string> _xslFunctions;

        private XSLRichTextBox _xslRichTextBox;
        private ListBox _intellisenseListBox;
        private ToolTip _intellisenseTooltip;

        private KeyPressEventArgs _lastKeyPress = null;
        private string _typed = string.Empty;
        private bool _wordMatched = false;
        
        private enum DataSourceModes
        {
            Undefined,
            Tag,
            Attribute
        }
        #endregion

        #region Constructors
        public IntellisenseMgr(XSLRichTextBox xslRichTextBox)
        {
            _xslRichTextBox = xslRichTextBox;            
            _xslRichTextBox.KeyPress += new KeyPressEventHandler(_xslRichTextBox_KeyPress);
            //_xslRichTextBox.TextChanged += new EventHandler(_xslRichTextBox_TextChanged);
            _xslRichTextBox.ContentChanged += new ContentChangedHandler(_xslRichTextBox_ContentChanged);
        }
        #endregion Constructors

        //*********************************************************************
        //* PROPERTIES
        //*********************************************************************
                                
        #region Private Properties
        private DataSourceModes DataSourceMode
        {
            get
            {
                string typeName = _intellisenseListBox.DataSource.GetType().FullName;

                if (typeName.Contains("XSLTag"))
                    return DataSourceModes.Tag;
                else if (typeName.Contains("XSLAttribute"))
                    return DataSourceModes.Attribute;
                else
                    return DataSourceModes.Undefined;
            }
        }

        private bool IsContextInfoCurrent
        {
            get
            {
                return _isContextInfoCurrent;
                //return null != _lastKeyPress;
            }
            set
            {
                _isContextInfoCurrent = value;
            }
        }
        #endregion Private Properties

        #region Public Virtual Properties
        public virtual string CurrentTagText
        {
            get
            {
                if (!IsContextInfoCurrent) _currentTagText = GetCurrentTagText();
                return _currentTagText;
            }
        }

        /// <summary>
        /// Returns the name of the current tag at textbox position
        /// If cursor is not within a tag, returns empty string
        /// </summary>
        public virtual string CurrentTagName
        {
            get
            {
                if (!IsContextInfoCurrent) _currentTagName = GetCurrentTagName();
                return _currentTagName;
            }
        }

        public virtual bool InAttributeValue
        {
            get
            {
                if (!IsContextInfoCurrent) _inAttributeValue = GetInAttributeValue();
                return _inAttributeValue;
            }
        }
        #endregion Public Virtual Properties

        #region Public Properties
        public XSLTag CurrentListTag
        {
            get
            {
                return FindTagByName(_intellisenseListBox.Text);
            }
        }

        public XSLAttribute CurrentListAttribute
        {
            get
            {
                return FindAttributeByName(_intellisenseListBox.Text);
            }
        }

        public XSLTag CurrentTag
        {
            get
            {
                string curTagName = this.CurrentTagName;
                return (0 == curTagName.Length) ? null : FindTagByName(curTagName);
            }
        }

        public bool IntellisenseInitialized
        {
            get
            {
                return _xslTags != null && _xslTags.Count > 0
                    && _intellisenseListBox != null;
            }
        }

        public bool EnableIntellisense
        {
            get { return _enableIntellisense; }
            set { _enableIntellisense = value; }
        }

        public List<string> XslFunctions
        {
            get { return _xslFunctions; }
            protected set { _xslFunctions = value; }
        }

        public List<XSLTag> XslTags
        {
            get { return _xslTags; }
            protected set { _xslTags = value; }
        }
        #endregion Public Properties

        //*********************************************************************
        //* METHODS
        //*********************************************************************

        #region Private Initialization Methods
        private void AddXSLTags()
        {
            _xslTags = XSLTags.GetXSLTags();
        }

        private void AddXSLFunctions()
        {
            _xslFunctions = XSLFunctions.GetXslFunctions();
        }

        private void InitializeIntellisense()
        {
            AddXSLTags();
            AddXSLFunctions();

            _intellisenseListBox = new ListBox();
            _intellisenseListBox.Name = "_intellisenseListBox";
            _intellisenseListBox.Size = new Size(250, 100);
            _intellisenseListBox.Visible = false;
            _intellisenseListBox.DataSource = _xslTags;
            _intellisenseListBox.DisplayMember = "TagName";
            _intellisenseListBox.Leave += new EventHandler(_intellisenseListBox_Leave);
            _intellisenseListBox.KeyDown += new KeyEventHandler(_intellisenseListBox_KeyDown);
            _intellisenseListBox.DoubleClick += new EventHandler(_intellisenseListBox_DoubleClick);
            _intellisenseListBox.SelectedValueChanged += new EventHandler(
                _intellisenseListBox_SelectedValueChanged);
            _intellisenseTooltip = new ToolTip();
            _intellisenseListBox.Cursor = Cursors.Arrow;
            _intellisenseListBox.Sorted = true;
            
            _xslRichTextBox.Controls.Add(_intellisenseListBox);
        }

        private bool PrepareIntellisense(string charPressed)
        {
            //ensure one-time, initial setup has been performed
            if (!IntellisenseInitialized) InitializeIntellisense();

            this.IsContextInfoCurrent = false;

            if (" " == charPressed)
            {
                XSLTag curTag = this.CurrentTag;

                if (null != curTag)
                {                    
                    if (!InAttributeValue)
                    {
                        _intellisenseListBox.DataSource = curTag.Attributes;
                        _intellisenseListBox.DisplayMember = "Name";
                        _intellisenseListBox.SelectedIndex = -1;
                    }
                    else
                    {
                        //TODO: functions(), variable list etc.
                        return false; // for now until ready
                    }
                }
                else
                {
                    return false;
                }
            }
            else if ("<" == charPressed)
            {
                _intellisenseListBox.DataSource = _xslTags;
                _intellisenseListBox.DisplayMember = "TagName";
            }

            this.IsContextInfoCurrent = true;
            return true;
        }
        #endregion

        #region Private Prep Methods
        private string GetCurrentTagName()
        {
            string tagName = string.Empty;
            string currentTagText = this.CurrentTagText;

            if (currentTagText.Length > 0)
            {
                int endPos = currentTagText.IndexOf(" ", 1);

                if (endPos == -1)
                {
                    endPos = currentTagText.Length - 1;
                }

                tagName = currentTagText.Substring(1, endPos - 1);
            }

            return tagName;
        }

        private string GetCurrentTagText()
        {
            string currentLine = _xslRichTextBox.CurrentLine;

            string tagText = string.Empty;
            int selStart = _xslRichTextBox.ColumnNumber - 1;

            if (selStart > currentLine.Length) return string.Empty;

            int tagStart = currentLine.LastIndexOf("<", (selStart > 0) ? selStart - 1 : selStart);

            if (tagStart > -1 && tagStart <= selStart)
            {
                int tagEnd = currentLine.IndexOf(">", tagStart);
                string buffer = string.Empty;

                if (tagEnd > tagStart)
                {
                    buffer = currentLine.Substring(tagStart, tagEnd - tagStart + 1);
                    int otherTagStart = buffer.LastIndexOf("<");

                    if (-1 != otherTagStart && otherTagStart > tagStart)
                    {
                        tagEnd = otherTagStart;
                    }

                    if (selStart > tagEnd)
                    {
                        tagText = string.Empty;
                    }
                    else
                    {
                        tagText = currentLine.Substring(tagStart, tagEnd - tagStart + 1);
                    }
                }
                else if (-1 == tagEnd)
                {
                    int otherTagStart = currentLine.IndexOf("<", tagStart + 1);

                    if (-1 != otherTagStart)
                    {
                        tagEnd = otherTagStart;
                    }
                    else
                    {
                        tagEnd = currentLine.Length - 1;
                    }

                    if (tagEnd > tagStart)
                    {
                        tagText = currentLine.Substring(tagStart, tagEnd - tagStart + 1);
                    }
                }
            }

            _currentTagLineStartPos = tagStart;
            return tagText;
        }

        private bool GetInAttributeValue()
        {            
            AttributeNameValueRegex attrRegEx = new AttributeNameValueRegex();
            string curTagText = this.CurrentTagText;

            for (Match attrMatch = attrRegEx.Match(curTagText);
                attrMatch.Success; attrMatch = attrMatch.NextMatch())
            {
                int quoteStart = attrMatch.Groups[0].Value.IndexOf("\"");
                int quoteEnd = attrMatch.Groups[0].Value.IndexOf("\"", quoteStart + 1);

                quoteStart += attrMatch.Index;
                quoteEnd += attrMatch.Index;

                if (quoteStart > -1 && quoteEnd > -1 && quoteEnd > quoteStart)
                {
                    int colPos = _xslRichTextBox.ColumnNumber -1;
                    if (colPos > quoteStart && colPos < quoteEnd)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region Private Show/Hide Methods
        private void HideIntellisense()
        {
            _intellisenseListBox.Visible = false;
            _typed = string.Empty;
            _wordMatched = false;
            HideIntellisenseToolTip();
            _xslRichTextBox.Focus();
        }

        private void HideIntellisenseToolTip()
        {
            _intellisenseTooltip.Hide(_xslRichTextBox);
        }

        private void ShowIntellisense(string charPressed)
        {
            const int MARGIN = 5;
            if (!PrepareIntellisense(charPressed)) return;

            Point intellisensePos = _xslRichTextBox.GetPositionFromCharIndex(
                _xslRichTextBox.SelectionStart);
            intellisensePos.Y = intellisensePos.Y + _xslRichTextBox.Font.Height + MARGIN;

            //calculate whether proposed intellisense position will be 
            //off-screen beneath the last visible line
            int lastVisibleLineStart = _xslRichTextBox.GetFirstCharIndexFromLine(
                _xslRichTextBox.LastVisibleLine);
            Point lastVisibleLineStartPos = _xslRichTextBox.GetPositionFromCharIndex(
                lastVisibleLineStart);

            if (intellisensePos.Y + _intellisenseListBox.Height > lastVisibleLineStartPos.Y)
            {
                // just show intellisense on top of text if at bottom
                // (scrolling down then showing beneath does not appear to work)
                intellisensePos.Y -= _intellisenseListBox.Height + MARGIN + _xslRichTextBox.Font.Height;
            }

            // see if intellisense x pos will put it off screen to the right
            if (intellisensePos.X + _intellisenseListBox.Width > _xslRichTextBox.Width)
            {
                int diff = (intellisensePos.X + _intellisenseListBox.Width) - _xslRichTextBox.Width;
                intellisensePos.X -= diff + 25;
            }

            _intellisenseListBox.Location = intellisensePos;
            _intellisenseListBox.Visible = true;
            _intellisenseListBox.Focus();
        }

        private string GetIntellisenseToolTip()
        {
            if (DataSourceMode == DataSourceModes.Tag)
            {
                XSLTag tag = CurrentListTag;
                return (null == tag) ? string.Empty : tag.ToolTip;
            }
            else if (DataSourceMode == DataSourceModes.Attribute)
            {
                XSLAttribute attr = CurrentListAttribute;
                return (null == attr) ? string.Empty : attr.ToolTip;
            }

            return string.Empty;
        }

        private void ShowIntellisenseToolTip()
        {
            int xPos = _intellisenseListBox.Location.X + _intellisenseListBox.Width;
            int yPos = _intellisenseListBox.Location.Y;
            int duration = 30 * 1000;

            _intellisenseTooltip.Show(GetIntellisenseToolTip(), _xslRichTextBox, 
                xPos, yPos, duration);
        }
        #endregion

        #region Private Methods
        private XSLTag FindTagByName(string tagName)
        {
            XSLTag tag = _xslTags.Find(
                delegate(XSLTag t)
                {
                    return 0 == string.Compare(t.TagName, tagName, true);
                });
            return tag;
        }

        private XSLAttribute FindAttributeByName(string name)
        {
            XSLAttribute attr = CurrentTag.Attributes.Find(
                delegate(XSLAttribute a)
                {
                    return 0 == string.Compare(a.Name, name, true);
                });
            return attr;
        }

        private void InsertIntellisenseText(Keys keyPressed)
        {
            bool hide = true;
            XSLTag currentTag = CurrentListTag;

            //OemPeriod = ">" w/shift (assumed)
            //OemQuestion = "/"
            if (keyPressed == Keys.OemPeriod || keyPressed == Keys.Enter)
            {
              if (currentTag != null && currentTag.AllowsContent)
              {
                _xslRichTextBox.SelectedText = _intellisenseListBox.Text +
                    "></" + _intellisenseListBox.Text + ">";
                _xslRichTextBox.SelectionStart -= _intellisenseListBox.Text.Length + 3;
              }
              else
              {
                _xslRichTextBox.SelectedText = _intellisenseListBox.Text + "/>";
              }
              
            }
            else if (keyPressed == Keys.OemQuestion) // "/"
            {
                _xslRichTextBox.SelectedText = _intellisenseListBox.Text + "/>";
            }
            else if (keyPressed == Keys.Tab || keyPressed == Keys.Space)
            {
                _xslRichTextBox.SelectedText = _intellisenseListBox.Text + " ";
                ShowIntellisense(" ");
                hide = false;
            }

            if (hide)
            {
                HideIntellisense();
            }
            //TODO: check tag and determine if <this/> or <this></this>
            //TODO: tag attributes?
        }                
        #endregion

        //*********************************************************************
        //* EVENTS
        //*********************************************************************

        #region XSLRichTextBox Events
        void _xslRichTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (EnableIntellisense)
            {
                //TODO: tag help/description?
                //TODO: tag attribute intellisense?
                if ("<" == e.KeyChar.ToString())
                {
                    //delay showing intellisense until after keypress and text changed
                    _lastKeyPress = e;
                    //ShowIntellisense("<");
                }
                else if (" " == e.KeyChar.ToString())
                {
                    //delay showing intellisense until after keypress and text changed
                    _lastKeyPress = e;
                    //ShowIntellisense(" "); //TODO: revisit soon
                }
                else
                {
                    _lastKeyPress = null;
                }
            }
        }

        /// <summary>
        /// Basically OnTextChanged but this is here b/c syntax rich textbox
        /// turns off text changed events in many cases
        /// </summary>
        void _xslRichTextBox_ContentChanged()
        {
            if (EnableIntellisense && null != _lastKeyPress)
            {
                ShowIntellisense(_lastKeyPress.KeyChar.ToString());
                _lastKeyPress = null;
            }
        }        
        #endregion

        #region Intellisense events
        void _intellisenseListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowIntellisenseToolTip();
        }

        void _intellisenseListBox_KeyDown(object sender, KeyEventArgs e)
        {
            //Debug.WriteLine(e.KeyCode);
            Debug.WriteLine(_typed);

            switch (e.KeyCode)
            {
                case Keys.Back:
                    if (_typed.Length > 0)
                    {
                        _typed = _typed.Substring(0, _typed.Length - 1);
                    }
                    break;
                case Keys.Delete:
                    _typed = string.Empty;
                    break;
                case Keys.Escape:
                    HideIntellisense();
                    break;
                case Keys.Enter:
                    InsertIntellisenseText(e.KeyCode);
                    break;
                case Keys.Tab:
                    e.Handled = true;
                    InsertIntellisenseText(e.KeyCode);
                    break;
                case Keys.Space:
                    InsertIntellisenseText(e.KeyCode);
                    break;
                case Keys.OemQuestion: // "/"
                    InsertIntellisenseText(e.KeyCode);
                    break;
                case Keys.OemPeriod:
                    if (e.Shift) // ">"
                    {
                        InsertIntellisenseText(Keys.OemPeriod);
                    }
                    break;
                case Keys.Oem1:
                    if (e.Shift) // ":"
                    {
                        _typed += ":";
                    }
                    break;
                default:                    

                    if (Char.IsLetterOrDigit((char)e.KeyValue))
                    {
                        char val = (char)e.KeyValue;
                        _typed += val;

                        _wordMatched = false;

                        // Loop through all the items in the listview, looking
                        // for one that starts with the letters typed
                        for (int i = 0; i < _intellisenseListBox.Items.Count; i++)
                        {
                            string itemText = _intellisenseListBox.GetItemText(_intellisenseListBox.Items[i]);
                            
                            if (itemText.ToLower().StartsWith(_typed.ToLower()))
                            {
                                _wordMatched = true;
                                _intellisenseListBox.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    //System.Diagnostics.Debug.WriteLine(e.KeyCode);
                    break;
            }            
        }

        void _intellisenseListBox_Leave(object sender, EventArgs e)
        {
            HideIntellisense();
        }

        void _intellisenseListBox_DoubleClick(object sender, EventArgs e)
        {
            InsertIntellisenseText(Keys.Enter);
        }
        #endregion
    }
}
