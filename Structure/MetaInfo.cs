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
  public class MetaInfo
  {
    public String INI { get; set; }
    public String Item { get; set; }
    public String FullName { get; set; }
    public String ItemDescription { get; set; }
    public String AltDescription { get; set; }
    public String Description { get; set; }
    public String DataType { get; set; }
    public String ResponseType { get; set; }
    public String Time { get; set; }
    public String SQLColumnName { get; set; }
    public String SQLDescription { get; set; }
    public String SQLTableName { get; set; }
    public String HelpText { get; set; }
    public String Comment { get; set; }
    public String Catagory { get; set; }
    public String Errors { get; set; }
    public MetaInfo()
    {
      INI = "";
      Item = "";
      FullName = "";
    }
    public MetaInfo(String ini, String item) 
    {
      INI = ini;
      Item = item;
      FullName = ini +" "+ item;

    }
  }
}
