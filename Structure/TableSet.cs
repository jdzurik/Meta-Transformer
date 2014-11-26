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
        public MetaInfo MetaData { get; set; }



        public TableSet()
        {
            TableName = "";
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
        public void MergHelp(String HelpDir, Gen AtiveSet) { }
        //public void MergHelp(String HelpDir, Gen AtiveSet)
        //{
        //    String HelpFileName = HelpDir + "\\" + TableName + ".htm";
        //    if (File.Exists(HelpFileName))
        //    {
        //        HtmlDocument doc = new HtmlDocument();
        //        doc.Load(HelpFileName);

        //        HtmlNodeCollection hncTableRows = doc.DocumentNode.SelectNodes("//span/html/body/div/table[2]/tbody");
        //        if (hncTableRows != null)
        //        {
        //            MetaData.INI = hncTableRows[0].SelectSingleNode("tr[3]/td[2]").InnerText;
        //            if (hncTableRows[0].SelectSingleNode("tr[4]/td[2]/table/tbody/tr/td/table") != null)
        //                MetaData.Comment = hncTableRows[0].SelectSingleNode("tr[4]/td[2]/table/tbody/tr/td/table").InnerText;
        //        }

        //        HtmlNodeCollection hncTables = doc.DocumentNode.SelectNodes("//span/html/body/div/table");
        //        for (int j = 0; j < hncTables.Count; j++)
        //        {

        //            if (hncTables[j].SelectNodes("tbody/tr/td") != null
        //                && hncTables[j].SelectNodes("tbody/tr/td").First().InnerText == "Column Information")
        //            {
        //                HtmlNodeCollection hncRows = hncTables[j + 1].SelectNodes("tbody/tr");
        //                if (hncRows != null)
        //                {
        //                    bool isFirst = true;
        //                    String OnStyle = "1";
        //                    ColumnSet csHolder = new ColumnSet();
        //                    String NameHolder = "";

        //                    for (int i = 0; i < hncRows.Count; i++)
        //                    {
        //                        try
        //                        {
        //                            if (isFirst)
        //                            {
        //                                OnStyle = hncRows[i].GetAttributeValue("style", "");
        //                                String cIndex = hncRows[i].SelectSingleNode("td[1]").InnerText;
        //                                NameHolder = hncRows[i].SelectSingleNode("td[2]").InnerText;
        //                                String cCIINI = hncRows[i].SelectSingleNode("td[3]/table/tbody/tr/td").InnerText;
        //                                String cCIItem = hncRows[i].SelectSingleNode("td[4]/table/tbody/tr/td").InnerText;
        //                                //String cType = hncRows[i].SelectSingleNode("/td[5]").InnerText;
        //                                //String cSize = hncRows[i].SelectSingleNode("/td[6]").InnerText;
        //                                //String cScale = hncRows[i].SelectSingleNode("/td[7]").InnerText;
        //                                MetaInfo mi = Columns.Single<ColumnSet>(x => x.ColumnName == NameHolder).MetaData;
        //                                if (mi == null)
        //                                {
        //                                    mi = new MetaInfo(cCIINI, cCIItem);
                                            
        //                                }
        //                                else 
        //                                {
        //                                    mi.Description = "";
        //                                    mi.ItemDescription = "";
        //                                    mi.Comment = "";
        //                                }
        //                                //csHolder.MetaInfo = new MetaInfo(cCIINI, cCIItem);
        //                                //csHolder.HtmlNodes.Add(hncRows[i]); 

        //                            }
        //                            else
        //                            {

        //                                Columns.Single<ColumnSet>(x => x.ColumnName == NameHolder).MetaData.Comment += hncRows[i].SelectSingleNode("td[1]/table/tbody/tr/td/table").InnerText;
        //                                //csHolder.HtmlNodes.Add(hncRows[i]); 

        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            MetaData.Errors = ex.Message;

        //                        }

        //                        if (i < hncRows.Count - 1)
        //                        {
        //                            if (hncRows[i + 1].GetAttributeValue("style", "") == OnStyle)
        //                            {
        //                                isFirst = false;
        //                            }
        //                            else
        //                            {
        //                                isFirst = true;
        //                            }
        //                        }

        //                    }
        //                }
        //            }

        //            // load FK from Help file
        //            if (hncTables[j].SelectNodes("tbody/tr/td") != null
        //                && hncTables[j].SelectNodes("tbody/tr/td").First().InnerText == "Foreign Key Information")
        //            {
        //                HtmlNodeCollection hnfkRows = hncTables[j + 1].SelectNodes("tbody/tr");
        //                if (hnfkRows != null)
        //                {
        //                    ForeignKeys.Clear();
        //                    //bool isFirst = true;
        //                    //String OnStyle = "1";
        //                    //ForeignKeySet fksHolder = new ForeignKeySet();
        //                    //String NameHolder = "";

        //                    for (int i = 0; i < hnfkRows.Count; i++)
        //                    {
        //                        if (hnfkRows[i].SelectSingleNode("td[1]").InnerText != "Datalink Validations")
        //                        {
        //                            String fkName = hnfkRows[i].SelectSingleNode("td[1]").InnerText.Replace("*", "");
        //                            ForeignKeySet fk = new ForeignKeySet();
        //                            if (ForeignKeys.SingleOrDefault<ForeignKeySet>(x => x.KeyName == fkName) == null)
        //                            {
        //                                fk.KeyName = fkName;
        //                                fk.Name = fkName;
        //                                ColumnSet c = new ColumnSet();
        //                                String cn = hnfkRows[i].SelectSingleNode("td[2]").InnerText;
        //                                if (!String.IsNullOrEmpty(cn))
        //                                {
        //                                    c = Columns.SingleOrDefault<ColumnSet>(
        //                                       x => x.ColumnName == cn.Replace("*", ""));
        //                                    if (c != null && c.Name != "")
        //                                    {
        //                                        fk.Columns.Add(c.ItemGuid);
        //                                    }
        //                                }

        //                                string ftn = hnfkRows[i].SelectSingleNode("td[3]").InnerText.Replace("*", "");
        //                                if (ftn != "" && AtiveSet.GetTableByName(ftn) != null)
        //                                {
        //                                    TableSet ts = AtiveSet.GetTableByName(ftn);
        //                                    fk.ForeignTableID = ts.ItemGuid;
        //                                    fk.ForeignTable = ts.TableName;

        //                                    string fcn = hnfkRows[i].SelectSingleNode("td[4]").InnerText.Replace("*", "");
        //                                    if (ftn != "" && ts.GetColumnByColumnName(fcn) != null)
        //                                    { 
        //                                        ColumnSet cs = ts.GetColumnByColumnName(fcn);
        //                                        fk.ForeignColumnID = cs.ItemGuid;
        //                                        fk.ForeignColumn = cs.ColumnName;
        //                                        ForeignKeys.Add(fk);
        //                                    }
        //                                }

        //                            }
        //                            else
        //                            {
        //                                fk = ForeignKeys.SingleOrDefault<ForeignKeySet>(x => x.KeyName == fkName);
        //                                ColumnSet c = Columns.Single<ColumnSet>(x => x.ColumnName == hnfkRows[i].SelectSingleNode("td[2]").InnerText.Replace("*", ""));
        //                                if (c != null) fk.Columns.Add(c.ItemGuid);
        //                                fk.ForeignTable = hnfkRows[i].SelectSingleNode("td[3]").InnerText.Replace("*", "");
        //                                fk.ForeignColumn = hnfkRows[i].SelectSingleNode("td[4]").InnerText.Replace("*", "");
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }


        //        //HtmlNodeCollection hncRows = doc.DocumentNode.SelectNodes("tbody/tr");


        //    }

        //}




    }



}
