
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

  public class ForeignKeyColumnsSet : BaseSet
  {
    public int ID { get; set; }
    public String ColumnName { get; set; }
    public String RefColumnName { get; set; }
    public String KeyColumnDescription { get; set; }

    public ForeignKeyColumnsSet()
    {
      ID = 0;
      ColumnName = "";
      RefColumnName = "";
      ExtProp = new List<ExtPropSet>();
    }
    public ForeignKeyColumnsSet(ColumnSet cs)
    {
      Name = cs.Name;
      ColumnName = cs.ColumnName;
    }
  }

}
