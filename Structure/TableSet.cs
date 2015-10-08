using System;
using System.Collections.Generic;
using System.Linq;

namespace Structure
{
    [Serializable]
    public class TableSet : BaseSet, ICloneable
    {
        public int ID { get; set; }
        public String TableName { get; set; }
        public List<FilterSet> Filters { get; set; }
        public List<ColumnSet> Columns { get; set; }
        public List<ForeignKeySet> ForeignKeys { get; set; }
        public List<IndexSet> Indices { get; set; }
        public List<SubSet> Subs { get; set; }
        public List<ListSet> Lists { get; set; }
        public TableSetType SetType { get; set; }
        public Boolean HasIndexes { get; set; }
        public Boolean HasForeignKeys { get; set; }
        public Boolean HasPrimaryKey { get; set; }
        public Boolean HasIdentity { get; set; }
        public String SchemaName { get; set; }
        public MetaInfo MetaData { get; set; }



        public TableSet()
        {
            TableName = "";
            SchemaName = "";
            ExtProp = new List<ExtPropSet>();
            Checked = false;
            Filters = new List<FilterSet>();
            Columns = new List<ColumnSet>();
            ForeignKeys = new List<ForeignKeySet>();
            Subs = new List<SubSet>();
            Lists = new List<ListSet>();
            SetType = TableSetType.Table;
            Indices = new List<IndexSet>();
            HasIndexes = false;
            HasForeignKeys = false;
            HasPrimaryKey = false;
            HasIdentity = false;
            MetaData = new MetaInfo();
        }

        public TableSet(string pName, bool pChecked, List<ColumnSet> pColumns)
        {
            TableName = pName;
            Checked = pChecked;
            Columns = pColumns;
            MetaData = new MetaInfo();
        }

        public static TableSet ObjectTableSet(string pName, bool pChecked)
        {
            TableSet ts = new TableSet();
            ts.TableName = pName;
            ts.Checked = pChecked;
            ts.SetType = TableSetType.Object;
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

        // Needs to be implmented
        public void MergHelp(String HelpDir, Gen AtiveSet) { }


    }



}
