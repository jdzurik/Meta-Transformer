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
  public class IndexSet : BaseSet
  {
    public int ID { get; set; }
    public String IndexName { get; set; }
    public List<IndexColumnSet> Columns { get; set; }
    public String IndexKeyType { get; set; }
    public Boolean IsUnique { get; set; }
    public Boolean IsXmlIndex { get; set; }
    public Boolean IsSystemObject { get; set; }
    public Boolean IsClustered { get; set; }
    public Boolean IsSpatialIndex { get; set; }


    public IndexSet()
    {
      ID = 0;
      IndexName = "";
      Columns = new List<IndexColumnSet>();
      ExtProp = new List<ExtPropSet>();
    }

  
  }
}
