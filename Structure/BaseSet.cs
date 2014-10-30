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
  public class BaseSet
  {
    public Guid ItemGuid { get; set; }
    public List<ExtPropSet> ExtProp { get; set; }
    public String Name { get; set; }
    public Boolean Checked { get; set; }

    public BaseSet()
    {
      ItemGuid = Guid.NewGuid();
      Name = "";
      Checked = false;
      ExtProp = new List<ExtPropSet>();
    }

    public String GetExtPropValueByName(String Name)
    {
      String pval = "";
      foreach (ExtPropSet item in ExtProp)
      {
        if (Name == item.Name)
          pval = item.Value;
      }
      return pval;
    }
  }
}
