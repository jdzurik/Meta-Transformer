using System;
using System.Collections.Generic;

namespace Structure
{
  [Serializable]
  public class ColumnSet : BaseSet
  {
    public int ID { get; set; }
    public string ColumnName { get; set; }
    public bool IsDataMember { get; set; }
    public DataTypeSet ColumnDataType { get; set; }
    public int Size { get; set; }
    public bool InPrimaryKey { get; set; }
    public bool IsForeignKey { get; set; }
    public bool IsUniqueKey { get; set; }
    public bool Identity { get; set; }
    public string Default { get; set; }
    public bool IsLazyLoaded { get; set; }
    public bool IsNullable { get; set; }
    public bool IsColumnSet { get; set; }
    public MetaInfo MetaData { get; set; }
    //public MetaInfo MetaData { get; set; }
    //public List<String> HtmlNodes { get; set; }   

    public ColumnSet()
    {
      ColumnName = "";
      ExtProp = new List<ExtPropSet>();
      InPrimaryKey = false;
      IsForeignKey = false;
      IsLazyLoaded = false;
      IsNullable = false;
      MetaData = new MetaInfo();
      //HtmlNodes = new List<String>();
    }

    public ColumnSet(string pName, bool pChecked)
    {
      ColumnName = pName;
      Checked = pChecked;
      IsLazyLoaded = false;
      IsNullable = false;
      MetaData = new MetaInfo();
      //HtmlNodes = new List<String>();
    }
   
  }

}
