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
  public class SubSet : BaseSet
  {
    public TableSet Type { get; set; }
    public ColumnSet ReferenceField { get; set; }
    public Boolean OverrideProperty { get; set; }
    public Boolean LazyLoad { get; set; }
  }
}
