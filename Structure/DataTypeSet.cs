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
  public class DataTypeSet : BaseSet
  {

    public String SqlDataType { get; set; }
    public String Schema { get; set; }
    public int NumericPrecision { get; set; }
    public int NumericScale { get; set; }
    public int MaximumLength { get; set; }

    public DataTypeSet()
    {
      Name = "";
      SqlDataType = "";
    }

   
  }
}
