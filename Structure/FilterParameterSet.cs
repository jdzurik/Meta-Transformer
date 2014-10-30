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
  public class FilterParameterSet : BaseSet
  {
    public String ParameterName { get; set; }
    public String ParameterDataType { get; set; }
    public String Direction { get; set; }
    public String Size { get; set; }


    public FilterParameterSet()
    {
      ParameterName = "";
      ParameterDataType = "";
      Size = "";
      Direction = "Input";
    }

    public FilterParameterSet(ColumnSet cs)
    {
      ParameterName = cs.ColumnName;
      ParameterDataType = cs.ColumnDataType.Name;
      Size = cs.ColumnDataType.MaximumLength.ToString();
      Direction = "Input";
    }

  }

}
