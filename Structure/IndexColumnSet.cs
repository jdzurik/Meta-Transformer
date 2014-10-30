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
  public class IndexColumnSet : BaseSet
  {
    public int ID { get; set; }
    public String ColumnName { get; set; }
    public Boolean IsDescending { get; set; }
    public Boolean IsComputed { get; set; }
    public Boolean IsIncluded { get; set; }

    public IndexColumnSet()
    {
      ID = 0;
      ColumnName = "";
      IsDescending = false;
      IsComputed = false;
      ExtProp = new List<ExtPropSet>();
    }
  }
}
