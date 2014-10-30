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
  public class FilterSet : BaseSet, ICloneable
  {

    public string FilterName { get; set; }
    public string FilterQuery { get; set; }
    public string FilterQueryInner { get; set; }
    public List<FilterParameterSet> Parms { get; set; }
    public List<FilterWhereSet> Where { get; set; }
    public string FilterAdvanced { get; set; }
    public Boolean IsAdvanced { get; set; }
    public Boolean IsSingle { get; set; }



    public FilterSet()
    {
      FilterName = "";
      FilterQuery = "";
      Parms = new List<FilterParameterSet>();
      Where = new List<FilterWhereSet>();
      IsAdvanced = false;
      IsSingle = false;
    }
    object ICloneable.Clone()
    {
      return this.Clone();
    }
    public FilterSet Clone()
    {
      return (FilterSet)this.MemberwiseClone();
    }

  }
}
