using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml.Serialization;


namespace Structure
{
  [Serializable]
  public class ListSet : BaseSet
  {

    public TableSet Type { get; set; }
    public Boolean LazyLoad { get; set; }
    public Boolean IsManyToMany { get; set; }
  }
}
