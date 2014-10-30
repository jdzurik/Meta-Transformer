
#region Libraries Used
using Common.Library.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;

using XSL.Library.RegularExpressions;
#endregion

namespace XSL.Library
{
    public class XSLRichTextBox : SyntaxRichTextBox
    {
        #region Declarations
        private IntellisenseMgr _intellisenseMgr = null;
        private XslSyntaxSettings _xslSyntaxSettings = new XslSyntaxSettings();        
        #endregion
        
        #region Constructors
        public XSLRichTextBox()
        {
            //InitializeIntellisense(); // done if/when needed            
            if (!this.DesignMode)
            {
                _intellisenseMgr = new IntellisenseMgr(this);
            }
        }        
        #endregion               
                
        #region Public Properties
        [
        Category("Behavior"),
        Description("Gets or sets value indicating if simple xsl intellisense will be enabled"),
        DefaultValue(true)
        ]
        public bool EnableIntellisense
        {
            get { return _intellisenseMgr.EnableIntellisense; }
            set { _intellisenseMgr.EnableIntellisense = value; }
        }

        [
        Category("Appearance"),
        Description("Gets or sets the settings for syntax highlighting"),
        TypeConverter(typeof(ExpandableObjectConverter)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
        ]
        public new XslSyntaxSettings SyntaxSettings
        {
            get { return _xslSyntaxSettings; }

            //set needed for proper design-time interaction
            set
            {
                _xslSyntaxSettings = value;
            }
        }

        public IntellisenseMgr Intellisense
        {
            get { return _intellisenseMgr; }
        }
        
        [ReadOnly(true),
        Browsable(false)]
        public List<string> XslFunctions
        {
            get { return _intellisenseMgr.XslFunctions; }
        }

        [ReadOnly(true),
        Browsable(false)]
        public List<XSLTag> XslTags
        {
            get { return _intellisenseMgr.XslTags; }
        }
        #endregion
                

        //*********************************************************************
        //* METHODS
        //*********************************************************************
        
        #region Public Syntax Methods
        public void SetDefaultSyntaxSettings()
        {
            //SyntaxSettings.EnableStrings = false;
            //SyntaxSettings.EnableIntegers = false;
            SyntaxSettings.EnableComments = true;
            SyntaxSettings.CommentColor = Color.Gray;

            SyntaxSettings.CharacterEntityColor = Color.Purple;
            SyntaxSettings.AttributeNameColor = Color.Crimson;
            SyntaxSettings.AttributeValueColor = Color.Green;
            SyntaxSettings.XslFunctionsColor = Color.DarkGoldenrod;
        }

        public void SyntaxHighlightAll()
        {
            this.ProcessAllLines();
        }
        #endregion Public Syntax Methods

        #region Public Transform Methods
        public void ShowTransformDialog()
        {
            TransformDialog dlg = new TransformDialog(this);
            dlg.Show();
        }

        public TransformResults Transform()
        {
            TransformResults results = null;

            using (TransformDialog dlg = new TransformDialog(this))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    results = dlg.Results;
                }
            }

            return results;
        }

        public TransformResults Transform(FileInfo xmlFile, TransformSettings settings)
        {
            return TransformUtility.Transform(xmlFile, this.Text, settings);
        }

        public TransformResults Transform(FileInfo xmlFile)
        {
            return TransformUtility.Transform(xmlFile, this.Text);            
        }

        public TransformResults Transform(string xml)
        {
            return TransformUtility.Transform(xml, this.Text);            
        }

        public TransformResults Transform(string xml, TransformSettings settings)
        {
            return TransformUtility.Transform(xml, this.Text, settings);
        }

        public TransformResults Transform(XmlDocument xmlDoc, TransformSettings settings)
        {
            return TransformUtility.Transform(xmlDoc, this.Text, settings);
        }

        public TransformResults Transform(XmlDocument xmlDoc)
        {
            return TransformUtility.Transform(xmlDoc, this.Text);
        }       
        #endregion Public Transform Methods

        #region Protected Overrides
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //SetDefaultSyntaxSettings();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);            
        }        

        protected override void ProcessLine()
        {
            if (0 == m_strLine.Length) return;

            // Save the position and make the whole line black
            int nPosition = SelectionStart;
            //_logMsg = true;
            SelectionStart = m_nLineStart;
            //_logMsg = false;
            SelectionLength = m_nLineLength;
            SelectionColor = Color.Black;

            ProcessXmlTags();

            // Process comments
            if (SyntaxSettings.EnableComments)
                ProcessRegex(@"<!--[\w\W]*?-->", SyntaxSettings.CommentColor); //comments

            ProcessRegex(@"&(?<value>[a-z0-9#]+;)", SyntaxSettings.CharacterEntityColor); //char entities            

            SelectionStart = nPosition;
            SelectionLength = 0;
            SelectionColor = Color.Black;

            m_nCurSelection = nPosition;
        }
        #endregion

        #region Protected Methods
        protected void ProcessXmlTags()
        {
            SuspendOnTextChanged();
            int nStart, nLength;
            KnownXslTagsRegex tagRegEx = new KnownXslTagsRegex();

            for (Match regMatch = tagRegEx.Match(m_strLine); regMatch.Success; regMatch = regMatch.NextMatch())
            {               
                // group 0: full tag
                // group 1: :tagname w/o namespace prefix
                // group 2: all attributes
                // group 3: attrname="attrvalue" (last set)
                // group 4: ="attrvalue" (last set)
                // group 5: ?
                // group 6: tag name
                // group 7: attribute name (last set)
                // group 8: attribute value (last set)
                // group 9: / closing tag

                string fullTag = regMatch.Groups[0].Value;
                string tagName = regMatch.Groups[1].Value;
                nStart = m_nLineStart + regMatch.Index + 1;
                if (fullTag.StartsWith("</")) nStart++;                
                nLength = tagName.Length;

                SelectionStart = nStart;
                SelectionLength = nLength;
                SelectionColor = SyntaxSettings.XslTagColor;

                AttributeNameValueRegex attrRegEx = new AttributeNameValueRegex();
                string attrs = regMatch.Groups[2].Value;
                for (Match attrMatch = attrRegEx.Match(attrs); attrMatch.Success; attrMatch = attrMatch.NextMatch())
                {
                    //group[0] = all (attr=value), group[1] = attrname, group[2] = attrvalue
                    string attrName = attrMatch.Groups[1].Value;
                    string attrValue = attrMatch.Groups[2].Value;

                    SelectionStart = nStart + tagName.Length + attrMatch.Index;
                    SelectionLength = attrName.Length;
                    SelectionColor = SyntaxSettings.AttributeNameColor;

                    SelectionStart += attrName.Length + 2;
                    SelectionLength = attrValue.Length;
                    SelectionColor = SyntaxSettings.AttributeValueColor;

                    KnownXslFunctionsRegex funcRegEx = new KnownXslFunctionsRegex();
                    for (Match funcMatch = funcRegEx.Match(attrValue); funcMatch.Success; funcMatch = funcMatch.NextMatch())
                    {
                        string func = funcMatch.Value;
                        SelectionStart = nStart + tagName.Length + attrMatch.Index 
                            + attrName.Length + 2 + funcMatch.Index;
                        SelectionLength = func.Length;
                        SelectionColor = SyntaxSettings.XslFunctionsColor;

                        int pos = this.Text.IndexOf(")", SelectionStart + 1);
                        SelectionStart = pos;
                        SelectionLength = 1;
                        SelectionColor = SyntaxSettings.XslFunctionsColor;
                    }
                }
            }

            ResumeOnTextChanged();
        }
        #endregion        

        private void InitializeComponent()
        {
      this.SuspendLayout();
      // 
      // XSLRichTextBox
      // 
      this.AutoWordSelection = true;
      this.SyntaxSettings.CommentColor = System.Drawing.Color.Empty;
      this.SyntaxSettings.CommentPattern = null;
      this.SyntaxSettings.EnableComments = false;

      this.SyntaxSettings.IntegerColor = System.Drawing.Color.Empty;
      this.SyntaxSettings.KeywordColor = System.Drawing.Color.Empty;
      this.SyntaxSettings.StringColor = System.Drawing.Color.Empty;
      this.ResumeLayout(false);

        }

    } // end class XSLRichTextBox
        
} // end namespace
