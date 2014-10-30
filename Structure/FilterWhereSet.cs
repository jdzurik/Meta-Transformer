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
  public class FilterWhereSet : BaseSet
  {
    public String Pre { get; set; }
    public String ColumnName { get; set; }
    public String Operator { get; set; }
    public String WhereValue { get; set; }
    public String Post { get; set; }

    public FilterWhereSet()
    {
      Pre = "(";
      ColumnName = "";
      Operator = " = ";
      WhereValue = "";
      Post = ")";

    }

    public FilterWhereSet(String pColumnName, String pWhereValue)
    {
      Pre = "(";
      ColumnName = pColumnName;
      Operator = " = ";
      WhereValue = pWhereValue;
      Post = ")";

    }

    public FilterWhereSet(String pPre, String pColumnName, String pOperator, String pWhereValue, String pPost)
    {
      Pre = pPre;
      ColumnName = pColumnName;
      Operator = pOperator;
      WhereValue = pWhereValue;
      Post = pPost;
    }

  }

}
