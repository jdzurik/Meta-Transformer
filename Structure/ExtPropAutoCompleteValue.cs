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
  public class ExtPropAutoCompleteValue
  {
    public String Value { get; set; }
    public Boolean Enabled { get; set; }

    public ExtPropAutoCompleteValue()
    {
      Value = "";
      Enabled = true;
    }

    public ExtPropAutoCompleteValue(String pValue)
    {
      Value = pValue;
      Enabled = true;
    }
  }

}
