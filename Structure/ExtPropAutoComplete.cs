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
  public class ExtPropAutoComplete
  {
    public String Name { get; set; }
    public Boolean Enabled { get; set; }
    public Boolean SaveValues { get; set; }
    public String DisplayOnGroup { get; set; }
    public List<ExtPropAutoCompleteValue> AvalibalValues { get; set; }

    public ExtPropAutoComplete()
    {
      Name = "";
      Enabled = true;
      SaveValues = true;
      AvalibalValues = new List<ExtPropAutoCompleteValue>();
      DisplayOnGroup = "All";
    }

    public ExtPropAutoComplete(String pName)
    {
      Name = pName;
      Enabled = true;
      SaveValues = true;
      AvalibalValues = new List<ExtPropAutoCompleteValue>();
      DisplayOnGroup = "All";
    }
  }

  [Serializable]
  public class ExtPropAutoCompleteList : List<ExtPropAutoComplete>
  {

    public ExtPropAutoCompleteList() 
    { 
    }
  }

}
