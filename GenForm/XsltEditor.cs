using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Structure;
using System.IO;
using System.Xml.Xsl;
using System.Xml;

namespace GenForm
{
  public partial class XsltEditor : UserControl
  {
    public XsltSet activeXslt { get; set; }
    public Gen activeSet { get; set; }
    public XsltEditor()
    {
      InitializeComponent();
    }
    public void setXslt()
    {
      xslEditor1.LoadFile(activeXslt.XsltPath); 
      Cursor.Current = Cursors.Default;
    }
    private void btnRefreshPreview_Click(object sender, EventArgs e)
    {
       rtbOutPreview.Text = "Loading "+ activeSet.ActiveTable.Name +" Date:" +DateTime.Now.ToString(); 
      using (XmlReader xr = XmlReader.Create(new StringReader(activeSet.ToXml())))
      {
        using (XmlReader xsltr = XmlReader.Create(new StringReader(xslEditor1.Text)))
        {
          using (StringWriter sw = new StringWriter())
          {
            try
            {
              XslCompiledTransform xslt = new XslCompiledTransform(true);
              xslt.Load(xsltr, XsltSettings.TrustedXslt, new XmlUrlResolver());
              XsltArgumentList xslal = new XsltArgumentList();
              xslal.AddParam("ObjectName", "", activeSet.ActiveTable.Name);
              xslal.AddParam("NameSpace", "", activeSet.ProjectName);
              xslt.Transform(xr, xslal, sw);
              rtbOutPreview.Text = sw.ToString();
            }
            catch (Exception ex)
            {
              rtbOutPreview.Text = "Error: " + ex.Message;
              if (ex.InnerException != null)
                rtbOutPreview.Text = rtbOutPreview.Text + "\n \n Details: " + ex.InnerException.Message;
            }
            finally 
            {

              sw.Close();
              sw.Dispose();
              xr.Close();
              xsltr.Close();
            }
          }
        }
      }
    }
  }
}
