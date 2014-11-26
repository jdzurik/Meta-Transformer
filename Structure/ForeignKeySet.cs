

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
  public class ForeignKeySet : BaseSet
  {
    public int ID { get; set; }
    public String ForeignKeyName { get; set; }
    public String Description { get; set; }
    public List<ForeignKeyColumnsSet> Columns { get; set; }

    public String ReferencedTable { get; set; }
    public String ReferencedKey { get; set; }

    public Boolean IsCheckedExisting { get; set; }
    public Boolean NotForReplication { get; set; }
    public Boolean IsEnabled { get; set; }

    public String ForeignKeyFilterID { get; set; }

    public Boolean IsSingleReturn { get; set; }
    public Boolean IsLazyLoaded { get; set; }
    public Boolean IsOverrideColumn { get; set; }
    public Boolean IsStreamReturn { get; set; }


    public ForeignKeySet()
    {
      ID = 0;
      ForeignKeyName = "";

      ExtProp = new List<ExtPropSet>();
    }

   
  }
}
