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
  public static class StringExtensions
  {
    public static string ToXml(this object obj)
    {
      XmlSerializer s = new XmlSerializer(obj.GetType());
      using (StringWriter writer = new StringWriter())
      {
        s.Serialize(writer, obj);
        return writer.ToString().Replace("utf-16", "utf-8");
      }
    }

    public static string TranslateTo(this object obj, string xsltPath)
    {

      String sr = obj.ToXml();
      MemoryStream m = new MemoryStream(Encoding.Default.GetBytes(sr));
      StringWriter sw = new StringWriter();
      XslCompiledTransform transform = new XslCompiledTransform();
      transform.Load(xsltPath, XsltSettings.TrustedXslt, new XmlUrlResolver());
      transform.Transform(XmlReader.Create(m), null, sw);
      return sw.ToString();

    }

    public static string TranslateTo(this object obj, string xsltPath, XsltArgumentList Parms)
    {
      String sr = obj.ToXml();
      MemoryStream m = new MemoryStream(Encoding.Default.GetBytes(sr));
      StringWriter sw = new StringWriter();
      try
      {
        XslCompiledTransform transform = new XslCompiledTransform();
        transform.Load(xsltPath, XsltSettings.TrustedXslt, new XmlUrlResolver());
        transform.Transform(XmlReader.Create(m), Parms, sw);
      }
      catch 
      {
        
        
      }
      return sw.ToString();
    }

  }
}
