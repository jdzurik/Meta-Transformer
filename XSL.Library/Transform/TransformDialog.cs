using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XSL.Library
{
    public partial class TransformDialog : Form
    {
        private enum ContentTypes
        {
            Plain = 0, Html = 1, Xml = 2
        }

        private ContentTypes _outputContentType = ContentTypes.Plain;
        private static double? _transparency;
        private static bool? _alwaysOnTop;
        private XSLRichTextBox _xslRTF;
        private TransformResults _results;        

        #region Construcion
        private TransformDialog()
        {
            InitializeComponent();
        }

        public TransformDialog(XSLRichTextBox xslRTF) : this()
        {
            _xslRTF = xslRTF;
            upDownTransparency.ValueChanged += upDownOpacity_ValueChanged;
            checkBoxAlwaysOnTop.CheckedChanged += checkBoxAlwaysOnTop_CheckedChanged;            
            openXmlToolStripButton.Click += openXmlToolStripButton_Click;
            transformToolStripSplitButton.ButtonClick += transformToolStripSplitButton_ButtonClick;
            launchXmlToolStripButton.Click += launchXmlToolStripButton_Click;
            reloadXmlToolStripButton.Click += reloadXmlToolStripButton_Click;

            upDownTransparency.Value = (_transparency.HasValue) ? (decimal)_transparency.Value
                : 0;
            checkBoxAlwaysOnTop.Checked = (_alwaysOnTop.HasValue) ? _alwaysOnTop.Value
                : true;
            openFileDialog.InitialDirectory = GetAppPath();
            webBrowserInput.DocumentCompleted += webBrowserInput_DocumentCompleted;

            transformHtmlToolStripMenuItem.Click += contentTypeMenuItem_Click;
            transformPlainToolStripMenuItem.Click += contentTypeMenuItem_Click;
            transformXmlToolStripMenuItem.Click += contentTypeMenuItem_Click;
            saveOutputToolStripButton.Click += saveOutputToolStripButton_Click;

            SetDefaultContentType();
        }
        #endregion Construction        

        #region Events
        private void checkBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBoxAlwaysOnTop.Checked;
            _alwaysOnTop = checkBoxAlwaysOnTop.Checked;
        }

        private void contentTypeMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            foreach (ToolStripMenuItem tmp in transformToolStripSplitButton.DropDownItems)
            {
                if (tmp != item)
                {
                    tmp.Checked = false;
                }
            }
                        
            item.Checked = !item.Checked;
            _outputContentType = (ContentTypes)Enum.Parse(typeof(ContentTypes), item.Tag.ToString());
            transformToolStripSplitButton.ToolTipText = "Transform (" + item.Text + ")";
        }

        private void launchXmlToolStripButton_Click(object sender, EventArgs e)
        {
            LaunchXmlFile();
        }

        private void openXmlToolStripButton_Click(object sender, EventArgs e)
        {
            BrowseAndOpenXmlFile();
        }

        private void reloadXmlToolStripButton_Click(object sender, EventArgs e)
        {
            OpenXmlFile(textBoxXmlFilename.Text);
        }

        private void saveOutputToolStripButton_Click(object sender, EventArgs e)
        {
            SaveOutput();
        }

        private void transformToolStripSplitButton_ButtonClick(object sender, EventArgs e)
        {
            Transform();
        }

        private void upDownOpacity_ValueChanged(object sender, EventArgs e)
        {
            SetTransparency((double)upDownTransparency.Value);
        }

        private void webBrowserInput_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            SetWebBrowserEditable(webBrowserInput);
        }
        #endregion Events

        #region Methods
        private void BrowseAndOpenXmlFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenXmlFile(openFileDialog.FileName);
            }
        }

        private string GetAppPath()
        {
            DirectoryInfo di = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
            return di.FullName;
        }

        private string GetOutputExtension()
        {
            string ext = "tmp";

            switch (_outputContentType)
            {
                case ContentTypes.Plain:
                    ext = "tmp";
                    break;
                case ContentTypes.Html:
                    ext = "html";
                    break;
                case ContentTypes.Xml:
                    ext = "xml";
                    break;
            }

            return ext;
        }

        private void LaunchXmlFile()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = textBoxXmlFilename.Text;
            p.Start();
        }

        private void OpenXmlFile(string filename)
        {
            textBoxXmlFilename.Text = filename;
            webBrowserInput.Navigate(filename);
            launchXmlToolStripButton.Enabled = true;
            reloadXmlToolStripButton.Enabled = true;
            transformToolStripSplitButton.Enabled = true;
        }

        private void SaveOutput()
        {
            if (_outputContentType == ContentTypes.Plain)
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";                
            }
            else if (_outputContentType == ContentTypes.Html)
            {
                saveFileDialog.Filter = "HTML Files (*.html)|*.html|All Files (*.*)|*.*";
            }
            else if (_outputContentType == ContentTypes.Xml)
            {
                saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            }

            if (_outputContentType == ContentTypes.Plain)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBoxOutput.SaveFile(saveFileDialog.FileName);
                }
            }
            else
            {
                webBrowserOutput.ShowSaveAsDialog();
            }            
        }

        private void SetDefaultContentType()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(_xslRTF.Text);
                XmlNamespaceManager xNamespaceMgr = new XmlNamespaceManager(doc.NameTable);
                xNamespaceMgr.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");
                XmlNode node = doc.SelectSingleNode("xsl:stylesheet/xsl:output", xNamespaceMgr);

                if (null != node)
                {
                    XmlNode attr = node.Attributes.GetNamedItem("method");
                    if (null != attr)
                    {
                        switch (attr.Value.ToLower())
                        {
                            case "text":
                                transformPlainToolStripMenuItem.PerformClick();
                                break;
                            case "xml":
                                transformXmlToolStripMenuItem.PerformClick();
                                break;
                            case "html":
                                transformHtmlToolStripMenuItem.PerformClick();
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void SetTransparency(double level)
        {
            this.Opacity = (100 - level) / 100;
            _transparency = level;
        }

        private void SetWebBrowserEditable(WebBrowser browser)
        {
            HtmlDocument htmldoc = webBrowserInput.Document;
            htmldoc.ExecCommand("EditMode", true, null);
        }

        private void Transform()
        {
            if (null != _results)
            {
                _results.Dispose();
            }

            TransformSettings settings = new TransformSettings();
            settings.ShowParamsOnTop = checkBoxAlwaysOnTop.Checked;
            settings.EnableParamPrompting = checkBoxPromptParams.Checked;

            string xml = webBrowserInput.Document.Body.InnerText;
            xml = xml.Replace("- <", "  <");

            _results = _xslRTF.Transform(xml, settings);
            
            if (!_results.Cancelled)
            {
                _results.WriteOutputToTempFile(GetOutputExtension());
                richTextBoxOutput.Text = _results.Output;
                textBoxXsltOutputFilename.Text = _results.OutputFilename;
                saveOutputToolStripButton.Enabled = true;

                richTextBoxOutput.Visible = (_outputContentType == ContentTypes.Plain);
                webBrowserOutput.Visible = (!richTextBoxOutput.Visible);

                if (_outputContentType == ContentTypes.Html || _outputContentType == ContentTypes.Xml)
                {
                    webBrowserOutput.Navigate(_results.OutputFilename);
                }
            }
        }
        #endregion Methods                

        #region Properties
        public TransformResults Results
        {
            get { return _results; }
            private set { _results = value; }
        }
        #endregion Properties
    }
}