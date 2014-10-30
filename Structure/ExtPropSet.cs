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
  public class ExtPropSet
  {
    public String Name { get; set; }
    public String Value { get; set; }
    public Boolean Checked { get; set; }

    public ExtPropSet()
    {
      Name = "";
      Value = "";
      Checked = false;
    }

  }
}
