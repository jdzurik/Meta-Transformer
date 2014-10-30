

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
    public String KeyName { get; set; }
    public String KeyDescription { get; set; }
    public List<Guid> Columns { get; set; }
    public Guid ForeignTableID { get; set; }
    public Guid ForeignColumnID { get; set; }
    public Guid ForeignFilterID { get; set; }

    public String ForeignTable { get; set; }
    public String ForeignColumn { get; set; }
    public String ForeignFilter { get; set; }

    public Boolean IsSingleReturn { get; set; }
    public Boolean IsLazyLoaded { get; set; }
    public Boolean IsOverrideColumn { get; set; }
    public Boolean IsStreamReturn { get; set; }


    public ForeignKeySet()
    {
      ID = 0;
      KeyName = "";
      Columns = new List<Guid>();
      ForeignTableID = Guid.Empty;
      ForeignColumnID = Guid.Empty;
      ForeignFilterID = Guid.Empty;
      ExtProp = new List<ExtPropSet>();
    }

   
  }
}
