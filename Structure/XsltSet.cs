using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace Structure
{

  [Serializable]
  public class XsltSet : BaseSet
  {
    public bool Use { get; set; }

    private String _XsltPath;
    public String XsltPath
    {
      get { return _XsltPath; }
      set
      {
        _XsltPath = value;
        SetXSLTInfo();
      }
    }

    public string OutputFileExtention { get; set; }
    public string FileAsPropExtName { get; set; }
    public string OutputDirectory { get; set; }
    public bool RunOnDatabase { get; set; }
    public bool NameAsFolder { get; set; }
    public string FolderAsPropExtName { get; set; }
    public bool CreateOneFile { get; set; }
    public string OneFileName { get; set; }
    public bool IsImport { get; set; }

    [XmlIgnore]
    public List<XsltSet> Imports { get; set; }
    [XmlIgnore]
    public XmlDocument XmlDoc { get; set; }
    [XmlIgnore]
    public FileInfo XsltInfo { get; set; }
    [XmlIgnore]
    public List<String> Errors { get; set; }

    public XsltSet(String pXsltPath, String pOutputDirectory)
    {
      _XsltPath = pXsltPath;
      OutputDirectory = pOutputDirectory;
      RunOnDatabase = false;
      Use = true;
      CreateOneFile = false;
      IsImport = false;
      Imports = new List<XsltSet>();
      SetXSLTInfo();

    }

    public XsltSet()
    {
      _XsltPath = "";
      OutputFileExtention = "";
      OutputDirectory = "";
      RunOnDatabase = false;
      Use = true;
      CreateOneFile = false;
      IsImport = false;
      Imports = new List<XsltSet>();
    }

    private void SetXSLTInfo()
    {
      if (!String.IsNullOrEmpty(_XsltPath))
      {
        XsltInfo = new FileInfo(_XsltPath);
        if (XsltInfo.Exists)
        {
			try
			{
				XmlDoc = new XmlDocument();
				XmlDoc.Load(_XsltPath);
				this.Name = XsltInfo.Name;
                LoadImports(); 
 			}
			catch(Exception ex) {
                Errors.Add(ex.Message);					 
			}
        }
      }
    }

    public void LoadImports() 
    {
        Imports.Clear();
        foreach (XmlNode xn in XmlDoc.GetElementsByTagName("xsl:import"))
        {
            String xip = xn.Attributes.GetNamedItem("href").InnerText;
            String xp = "";
            if (xip.Contains(Path.VolumeSeparatorChar))
            {
                xp = xip;
            }
            else
            {

                //xp = XsltInfo.Directory.FullName + xip;
                if (xip.Contains("../"))
                {
                    Uri folder = new Uri(XsltInfo.Directory.FullName);
                    Uri file = new Uri(folder, xip);
                    xp = file.ToString().Replace('/', Path.DirectorySeparatorChar).Replace("file:\\\\\\", "");
                }
                else
                {
                    xp = XsltInfo.Directory.FullName +"\\" + xip;
                }
                xp = xp.Replace("/", "\\");
               
            }
            if (File.Exists(xp))
            {
                XsltSet xsim = new XsltSet(xp, "");
                xsim.IsImport = true;
                Imports.Add(xsim);
            }
        }
    }

    //public void TransformFile(Gen g, string OutputFilePath,  string objectName)
    //{
    //  try
    //  {
    //    FileInfo f = new FileInfo(OutputFilePath);
    //    if (!f.IsReadOnly || !f.Exists)
    //    {
    //      string x = g.ToXml();
    //      XmlSerializer s = new XmlSerializer(g.GetType());
    //      using (StringWriter writer = new StringWriter())
    //      {
    //        s.Serialize(writer, g);
    //        x = writer.ToString().Replace("utf-16", "utf-8");
    //      }
    //      using (XmlReader xr = XmlReader.Create(new StringReader(x)))
    //      {
    //        using (FileStream fs = new FileStream(OutputFilePath, FileMode.Create))
    //        {
    //          using (TextWriter tw = new StreamWriter(fs, new UTF8Encoding()))
    //          {
    //            XslCompiledTransform xslt = new XslCompiledTransform(true);
    //            xslt.Load(_XsltPath, XsltSettings.TrustedXslt, new XmlUrlResolver());
    //            XsltArgumentList xslal = new XsltArgumentList();
    //            xslal.AddParam("ObjectName", "", objectName);
    //            //xslt.Transform(xr, xslal, tw);
    //            xslt.Transform(g.ActiveLocation, xslal, tw);
    //            tw.Close();
    //            tw.Dispose();
    //            fs.Close();
    //            fs.Dispose();
    //            xr.Close();
    //          }
    //        }
    //      }
    //      g.AddInfoMessage("Created: " + OutputFilePath, "Creates");
    //    }
    //    else
    //    {
    //      g.AddErrorMessage("This file: " + OutputFilePath + " \nis probably read only or, you don't have the correct permissions.", objectName);
    //    }
    //    f = null;
    //  }
    //  catch (Exception ex)
    //  {
    //    g.AddErrorMessage("This file: " + OutputFilePath + " errored out on this Template: " + _XsltPath + " | Error Message: " + ex.Message, objectName);
    //    g.AddInfoMessage("This file: " + OutputFilePath + " \nis probably read only or, you don't have the correct permissions.");
    //  }
    //}

  }
}
