
#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Windows.Forms;
#endregion

namespace XSL.Library
{
    public partial class XSLTemplateParams : Form
    {
        #region Nested Types
        private class ParamItem
        {
            public ParamItem(string name, string defaultValue, Control control, string nameSpace)
            {
                this.Name = name;
                this.DefaultValue = defaultValue;
                this.AssocControl = control;
                this.NameSpace = nameSpace;
            }

            public string Name;
            public string NameSpace;
            public string DefaultValue;
            public Control AssocControl;
            public string Value;
        }

        /// <summary>
        /// Non-standard / custom xsl:param attributes to provide additional
        /// information for more control over xsl parametrs
        /// Contains the names of the custom attributes w/defaults
        /// </summary>
        public class CustomAttributeNames
        {
            public  CustomAttributeNames()
            {
                this.ControlType = "customControlType";
                this.CustomListXPath = "customListXPath";
                this.CustomReadOnly = "customReadOnly";
            }

            /// <summary>
            /// Type of UI control for param; currently can be one of
            /// TextBox or CheckedListBox. Attribute name defaults to
            /// "customControlType"
            /// </summary>
            public string ControlType;
            /// <summary>
            /// XPath expression that returns the list of string values to be
            /// used to populate a list (i.e. ControlType of CheckedListBox)
            /// Attribute name defaults to customListXPath
            /// </summary>
            public string CustomListXPath;
            /// <summary>
            /// "true" if param should be readonly
            /// </summary>
            public string CustomReadOnly;
        }
        #endregion

        #region Declarations
        private List<ParamItem> _paramItems = new List<ParamItem>();
        private const int LEFT_MARGIN = 10;
        private const int PARAM_VERT_SPACING = 10;
        private const int PARAM_HORIZ_SPACING = 30;
        private Point _currentPosition = new Point(LEFT_MARGIN, PARAM_VERT_SPACING);
        private int _paramIndex = 0;
        private XmlDocument _xmlDoc = null;

        public XmlDocument XmlDoc
        {
            get { return _xmlDoc; }
            set { _xmlDoc = value; }
        }

        /// <summary>
        /// Non-standard / custom xsl:param attributes to provide additional
        /// information for more control over xsl parametrs
        /// Contains the names of the custom attributes w/defaults
        /// </summary>
        public CustomAttributeNames CustomAttributes = new CustomAttributeNames();
        #endregion

        #region Constructors
        public XSLTemplateParams()
        {
            InitializeComponent();
            cancelButton.Click += new EventHandler(cancelButton_Click);
        }        

        public XSLTemplateParams(XmlDocument xmlDoc,
                    XPathDocument xslDoc) : this()
        {
            _xmlDoc = xmlDoc;
            AddParams(xslDoc);
        }
        #endregion

        #region Public Methods
        public void AddParams(XPathDocument xslDoc)
        {
            XPathNavigator xslNav = xslDoc.CreateNavigator();

            XmlNamespaceManager xNamespaceMgr = new XmlNamespaceManager(xslNav.NameTable);
            xNamespaceMgr.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");
            xNamespaceMgr.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
            xNamespaceMgr.AddNamespace("fn", "http://www.w3.org/2005/xpath-functions");

            XPathNodeIterator cursor = xslNav.Select("/xsl:stylesheet/xsl:param", xNamespaceMgr);

            _paramIndex = 0;

            while (cursor.MoveNext())
            {
                string paramName = cursor.Current.GetAttribute("name", xNamespaceMgr.DefaultNamespace);

                if (!ParamExists(paramName))
                {
                    bool isHidden = false;
                    string hiddenFlag = cursor.Current.GetAttribute("customHidden", 
                        xNamespaceMgr.DefaultNamespace);

                    if (!string.IsNullOrEmpty(hiddenFlag))                    
                    {
                        hiddenFlag = hiddenFlag.ToUpper();
                        if (hiddenFlag == "1" || hiddenFlag == "Y" || hiddenFlag == "YES"
                            || hiddenFlag == "TRUE")
                        {
                            isHidden = true;
                        }
                        else
                        {
                            isHidden = false;
                        }
                    }

                    if (!isHidden)
                    {
                        _paramIndex++;
                        AddParam(cursor.Current, xNamespaceMgr);
                    }
                }
            }

            buttonOK.Top = _currentPosition.Y;
            cancelButton.Top = buttonOK.Top;
        }
        #endregion

        #region Private Methods
        private bool ParamExists(string paramName)
        {
            foreach (ParamItem param in _paramItems)
            {
                if (0 == string.Compare(param.Name, paramName, true))
                {
                    return true;
                }
            }

            return false;
        }

        private string ParseParamDefaultValue(string paramDefaultValue)
        {
            paramDefaultValue = paramDefaultValue.Replace("{Environment.MachineName}", Environment.MachineName);
            paramDefaultValue = paramDefaultValue.Replace("{Environment.UserName}", Environment.UserName);
            paramDefaultValue = paramDefaultValue.Replace("{DateTime.Now.ToShortDateString()}", DateTime.Now.ToShortDateString());
            return paramDefaultValue;
        }

        private void AddParam(XPathNavigator currentParam, XmlNamespaceManager xNamespaceMgr)
        {
            string paramName = currentParam.GetAttribute("name", xNamespaceMgr.DefaultNamespace);
            string paramDefaultValue = currentParam.GetAttribute("value", xNamespaceMgr.DefaultNamespace);
            paramDefaultValue = ParseParamDefaultValue(paramDefaultValue);

            string tempReadOnly = currentParam.GetAttribute(CustomAttributes.CustomReadOnly,
                xNamespaceMgr.DefaultNamespace);

            bool paramReadOnly = (string.IsNullOrEmpty(tempReadOnly)) ? false : Convert.ToBoolean(tempReadOnly);
            
            string controlType = currentParam.GetAttribute(CustomAttributes.ControlType, xNamespaceMgr.DefaultNamespace);
            if (0 == controlType.Length) controlType = "TextBox";

            Label paramLabel = AddParamLabel(paramName);
            _currentPosition.X = paramLabel.Left + paramLabel.Width + PARAM_HORIZ_SPACING;

            Control ctl = null;
            switch (controlType)
            {
                case "TextBox":
                    ctl = AddParamTextBox(paramName, paramDefaultValue, paramReadOnly);
                    break;
                case "CheckedListBox":
                    string xpathList = currentParam.GetAttribute(CustomAttributes.CustomListXPath,
                        xNamespaceMgr.DefaultNamespace);
                    XmlNodeList xNodes = _xmlDoc.SelectNodes(xpathList);
                    List<string> items = new List<string>();
                    foreach (XmlNode xNode in xNodes)
                    {
                        items.Add(xNode.Value);
                    }
                    ctl = AddParamCheckedListBox(paramName, items);
                    break;
            }

            ParamItem param = new ParamItem(paramName,
                    paramDefaultValue, ctl, xNamespaceMgr.DefaultNamespace);
            _paramItems.Add(param);

            _currentPosition.Y += ctl.Height + PARAM_VERT_SPACING;
            _currentPosition.X = LEFT_MARGIN;
        }

        private Label AddParamLabel(string paramName)
        {
            Label paramLabel = new Label();
            paramLabel.Name = paramName + "Label";
            paramLabel.Text = paramName;

            paramLabel.Location = new Point(LEFT_MARGIN, _currentPosition.Y);

            this.Controls.Add(paramLabel);
            return (paramLabel);
        }

        private TextBox AddParamTextBox(string paramName, string paramDefaultValue, bool readOnly)
        {
            TextBox paramTextBox = new TextBox();
            paramTextBox.Name = paramName + "TextBox";
            paramTextBox.Location = _currentPosition;
            paramTextBox.Size = new Size(200, 30);
            paramTextBox.Text = paramDefaultValue;
            paramTextBox.ReadOnly = readOnly;

            this.Controls.Add(paramTextBox);

            return (paramTextBox);
        }

        private CheckedListBox AddParamCheckedListBox(string paramName, List<string> items)
        {
            CheckedListBox chkListBox = new CheckedListBox();
            chkListBox.Name = paramName + "CheckedListBox";
            
            chkListBox.Location = _currentPosition;
            chkListBox.Size = new Size(250, 85);
            chkListBox.Sorted = true;

            foreach (string item in items)
            {
                chkListBox.Items.Add(item, true);
            }

            this.Controls.Add(chkListBox);
            return chkListBox;
        }
        #endregion

        #region Public Properties
        public XsltArgumentList XsltArgs
        {
            get
            {
                XsltArgumentList xsltArgs = new XsltArgumentList();

                foreach (ParamItem arg in _paramItems)
                {
                    // if the value is empty do not add param; this will allow
                    // stylesheet to choose a default value if desired
                    if (arg.Value.Length > 0)
                    {
                        xsltArgs.AddParam(arg.Name, arg.NameSpace, arg.Value);
                    }
                }

                return xsltArgs;
            }
        }

        public int TemplateArgCount
        {
            get
            {
                return _paramItems.Count;
            }
        }
        #endregion

        #region Control Events
        void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            foreach (ParamItem arg in _paramItems)
            {
                if (arg.AssocControl is TextBox)
                {
                    arg.Value = arg.AssocControl.Text;
                }
                else if (arg.AssocControl is CheckedListBox)
                {
                    CheckedListBox box = arg.AssocControl as CheckedListBox;
                    string val = string.Empty;
                    
                    foreach (object item in box.CheckedItems)
                    {
                        val += item.ToString() + ",";
                    }

                    if (val.EndsWith(","))
                    {
                        val = val.Substring(0, val.Length - 1);
                    }

                    arg.Value = val;
                }
            }

            this.Close();
        }
        #endregion

        #region Overrides
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (null != _paramItems && _paramItems.Count > 0 && null != _paramItems[0].AssocControl)
            {
                _paramItems[0].AssocControl.Focus();
            }

            this.BringToFront();
        }
        #endregion
    }
}