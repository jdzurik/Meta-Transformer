
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
using HtmlAgilityPack;
using RelatedObjects.Storage;

namespace Structure
{
    [Serializable]
    public class ViewSet : BaseSet, ICloneable
    {
        public int ID { get; set; }
        public String ViewName { get; set; }
        public List<FilterSet> Filters { get; set; }
        public List<ColumnSet> Columns { get; set; }
        public List<IndexSet> Indices { get; set; }
        public List<SubSet> Subs { get; set; }
        public List<ListSet> Lists { get; set; }
        public Boolean HasIndexes { get; set; }
        public Boolean HasForeignKeys { get; set; }
        public Boolean HasPrimaryKey { get; set; }
        public Boolean HasIdentity { get; set; }
        public String SchemaName { get; set; }
        public MetaInfo MetaData { get; set; }



        public ViewSet()
        {
            ViewName = "";
            ExtProp = new List<ExtPropSet>();
            Checked = false;
            Filters = new List<FilterSet>();
            Columns = new List<ColumnSet>();

            Subs = new List<SubSet>();
            Lists = new List<ListSet>();
            Indices = new List<IndexSet>();
            HasIndexes = false;
            HasForeignKeys = false;
            HasPrimaryKey = false;
            HasIdentity = false;
            MetaData = new MetaInfo();
        }

        public ViewSet(string pName, bool pChecked, List<ColumnSet> pColumns)
        {
            ViewName = pName;
            Checked = pChecked;
            Columns = pColumns;
            MetaData = new MetaInfo();
        }

        public static ViewSet ObjectViewSet(string pName, bool pChecked)
        {
            ViewSet ts = new ViewSet();
            ts.ViewName = pName;
            ts.Checked = pChecked;
            return ts;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        public TableSet Clone()
        {
            return (TableSet)this.MemberwiseClone();
        }

        public ColumnSet GetColumById(int ID)
        {

            ColumnSet ts = new ColumnSet();
            ts = Columns.Single(x => x.ID == ID);
            return ts;

        }

        public FilterSet GetFilterByGUID(Guid pGuid)
        {
            FilterSet ts = new FilterSet();
            ts = Filters.Single(x => x.ItemGuid == pGuid);
            return ts;
        }

        public ColumnSet GetColumnByGUID(Guid pGuid)
        {
            ColumnSet ts = new ColumnSet();
            ts = Columns.Single(x => x.ItemGuid == pGuid);
            return ts;
        }

        public ColumnSet GetColumnByColumnName(String ColumnName)
        {
            ColumnSet ts = new ColumnSet();
            try
            {
                ts = Columns.Single(x => x.ColumnName == ColumnName);
            }
            catch
            {
            }

            return ts;
        }

        public ColumnSet GetColumnByName(String Name)
        {
            ColumnSet ts = new ColumnSet();
            ts = Columns.Single(x => x.Name == Name);
            return ts;
        }
        public void MergHelp(String HelpDir, Gen AtiveSet) { }
        

    }



}
