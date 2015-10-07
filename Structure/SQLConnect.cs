using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Structure
{
    public class SQLConnect
    {
        public String ActiveServer { get; set; }
        public String ActiveDatabase { get; set; }
        public Boolean IsTrusted { get; set; }

        [XmlIgnore]
        public String DBUsername { get; set; }
        [XmlIgnore]
        public String DBPassword { get; set; }
        [XmlIgnore]
        public BackgroundWorker bkwGetTables { get; set; }

        public SQLConnect()
        {

        }

        public SQLConnect(BackgroundWorker pbkwGetTables)
        {
            bkwGetTables = pbkwGetTables;
        }

        public SQLConnect(String pServer, String pDatabase)
        {
            ActiveServer = pServer;
            ActiveDatabase = pDatabase;
            if (DBUsername + DBPassword == "")
            {
                IsTrusted = true;
            }
            else
            {
                IsTrusted = false;
            }
        }

        public List<String> GetDatabaseNames()
        {
            List<String> DBNames = new List<String>();
            if (ActiveServer != null)
            {
                ServerConnection sc;
                if ((DBPassword == null) || IsTrusted)
                {
                    sc = new ServerConnection(ActiveServer);
                }
                else
                {
                    sc = new ServerConnection(ActiveServer, DBUsername, DBPassword);
                }
                Server srv = new Server(sc);
                try
                {
                    srv.ConnectionContext.Connect();
                    if (srv.ConnectionContext.IsOpen)
                    {
                        foreach (Database db in srv.Databases)
                        {
                            DBNames.Add(db.Name);
                        }
                    }
                }
                finally
                {
                    srv.ConnectionContext.Disconnect();
                }
                
               
            }
            return DBNames;


        }

        public SQLConnect(String pServer, String pDatabase, String pUsername, String pPassword)
        {
            ActiveServer = pServer;
            ActiveDatabase = pDatabase;
            DBUsername = pUsername;
            DBPassword = pPassword;
            if (DBUsername + DBPassword == "")
            {
                IsTrusted = true;
            }
            else
            {
                IsTrusted = false;
            }
        }

        public SQLConnect(String pServer, String pDatabase, Boolean pIsTrusted)
        {
            ActiveServer = pServer;
            ActiveDatabase = pDatabase;
            IsTrusted = pIsTrusted;
        }


        public void GetSqlTables(ref Gen activeSet)
        {
            ServerConnection sc;
            if ((DBPassword == null) || IsTrusted)
            {
                sc = new ServerConnection(ActiveServer);
            }
            else
            {
                sc = new ServerConnection(ActiveServer, DBUsername, DBPassword);
            }
            Server srv = new Server(sc);

            Database db = srv.Databases[ActiveDatabase];
            Double numTables = Convert.ToDouble(db.Tables.Count);
            Double numTablesDone = 0.0;

            foreach (Table tbl in db.Tables)
            {
                try
                {
                    String tName = tbl.Name;
                    String tSchema = tbl.Schema;
                    String action = "Update";

                    TableSet ts = activeSet.Tables
                        .Where(w => w.ID == tbl.ID)
                        .First();
                    if (ts != null)
                    {
                        ts.TableName = tName;
                        ts.SchemaName = tSchema;
                        GetColumns(ts, tbl);
                        GetIndices(ts, tbl);
                        action = "Update";
                    }
                    else {
                        ts = new TableSet();
                        String tblName = tName;
                        if (tSchema != "") {
                            tblName = tSchema + "." + tblName;
                        }
                        ts.Name = tblName;
                        ts.TableName = tName;
                        ts.SchemaName = tSchema;
                        ts.ID = tbl.ID;
                        GetColumns(ts, tbl);
                        GetIndices(ts, tbl);
                        activeSet.Tables.Add(ts);
                        action = "Add";
                    }
                        
                    
                    
                    numTablesDone += 1;
                    bkwGetTables.ReportProgress(Convert.ToInt32((numTablesDone / numTables) * 100), "\nTable " + action + ": " + tName + " \n ");
                }
                catch (Exception ex)
                {
                    bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nTable Error: " + ex.Message.ToString() + " \n ");
                }
            }

            foreach (TableSet ats in activeSet.Tables)
            {
                bool remove = true;
                foreach (Table tbl in db.Tables)
                {
                    if (tbl.ID == ats.ID)
                    {
                        remove = false;
                        break;
                    }
                }
                if (remove) {
                    activeSet.Tables.Remove(ats);
                    bkwGetTables.ReportProgress(Convert.ToInt32((numTablesDone / numTables) * 100), "\nTable Removed: " + ats.TableName + " \n ");
                }
            }

        }

        private void GetIndices(TableSet ts, Table tbl)
        {

            foreach (Index idx in tbl.Indexes)
            {
                IndexSet i = ts.Indices.Where(w => w.ID == idx.ID)
                    .DefaultIfEmpty(new IndexSet())
                    .First();
                try
                {
                    String idxName = idx.Name;
                    i.IndexName = idxName;
                    i.IsUnique = idx.IsUnique;
                    i.ID = idx.ID;
                    i.IsXmlIndex = idx.IsXmlIndex;
                    i.IsSystemObject = idx.IsSystemObject;
                    i.IsSpatialIndex = idx.IsSpatialIndex;
                    i.IsClustered = idx.IsClustered;
                    GetIndexColumns(i, idx.IndexedColumns);

                    if (ts.Indices.Where(w => w.ID == idx.ID).FirstOrDefault<IndexSet>() == null)
                    {
                        i.Name = idxName;
                        ts.Indices.Add(i);
                    }
                }
                catch (Exception ex)
                {
                    bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nIndex Error: " + ex.Message.ToString() + " \n ");
                }

            }
        }

        private void GetIndexColumns(IndexSet i, IndexedColumnCollection idxcc)
        {
            foreach (IndexedColumn idxc in idxcc)
            {
                IndexColumnSet ic = i.Columns
                    .Where(w => w.ID == idxc.ID)
                    .DefaultIfEmpty(new IndexColumnSet())
                    .First<IndexColumnSet>();
                try
                {

                    ic.ID = idxc.ID;
                    ic.ColumnName = idxc.Name;
                    ic.IsComputed = idxc.IsComputed;
                    ic.IsDescending = idxc.Descending;
                    ic.IsIncluded = idxc.IsComputed;
                    if (i.Columns
                        .Where(w => w.ID == idxc.ID)
                        .FirstOrDefault<IndexColumnSet>() == null)
                    {
                        ic.Name = idxc.Name;
                        i.Columns.Add(ic);
                    }

                }
                catch (Exception ex)
                {
                    bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nIndex Error: " + ex.Message.ToString() + " \n ");
                }
            }
        }

        private void GetForeignKey(TableSet ts, Table tbl)
        {

            foreach (ForeignKey fk in tbl.ForeignKeys)
            {
                ForeignKeySet fks = ts.ForeignKeys.Where(w => w.ID == fk.ID)
                    .DefaultIfEmpty(new ForeignKeySet())
                    .First();
                try
                {
                    String fkName = fk.Name;
                   // fks.KeyName = fkName;
                    fks.ID = fk.ID;
                   

                    //GetForeignKeyColumns(fks, fk.Columns);

                    if (ts.ForeignKeys.Where(w => w.ID == fk.ID).FirstOrDefault<ForeignKeySet>() == null)
                    {
                        fks.Name = fkName;
                        ts.ForeignKeys.Add(fks);
                    }
                }
                catch (Exception ex)
                {
                    bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nForeign Error: " + ex.Message.ToString() + " \n ");
                }

            }
        }

        //private void GetForeignKeyColumns(ForeignKeySet fks, ForeignKeyColumnCollection foreignKeyColumnCollection)
        //{
        //    ForeignKeyColumnsSet fkcs = fks.ForeignKeysC.Where(w => w.ID == fks.ID)
        //            .DefaultIfEmpty(new ForeignKeyColumnsSet())
        //            .First();
        //    try
        //    {
        //        String fkName = fk.Name;
        //        fks.KeyName = fkName;
        //        fks.ID = fk.ID;

        //        GetForeignKeyColumns(fks, fk.Columns);

        //        if (ts.ForeignKeys.Where(w => w.ID == fk.ID).FirstOrDefault<ForeignKeySet>() == null)
        //        {
        //            fks.Name = fkName;
        //            ts.ForeignKeys.Add(fks);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nForeign Error: " + ex.Message.ToString() + " \n ");
        //    }
        //}

        private void GetColumns(TableSet ts, Table tbl)
        {
            foreach (Column col in tbl.Columns)
            {
                try
                {
                    String cname = col.Name;
                    ColumnSet c = ts.Columns
                        .Where(w => w.ID == col.ID)
                        .DefaultIfEmpty(new ColumnSet())
                        .First();

                    DataTypeSet dt = new DataTypeSet();
                    dt.NumericPrecision = col.DataType.NumericPrecision;
                    dt.MaximumLength = col.DataType.MaximumLength;
                    dt.NumericScale = col.DataType.NumericScale;
                    dt.SqlDataType = col.DataType.Name;
                    c.ColumnDataType = dt;
                    c.Size = col.DataType.NumericScale;
                    c.ID = col.ID;
                    c.IsNullable = col.Nullable;
                    c.IsForeignKey = col.IsForeignKey;
                    c.Identity = col.Identity;
                    c.IsColumnSet = col.IsColumnSet;
                    //c.Urn = col.Urn.;
                    c.ColumnName = cname;

                    if (ts.Columns
                        .Where(w => w.ID == col.ID)
                        .FirstOrDefault() == null)
                    {
                        c.Name = cname;
                        ts.Columns.Add(c);
                    }
                }
                catch (Exception ex)
                {
                    bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nColumn Error: " + ex.Message.ToString() + " \n ");
                }
            }

        }


    }
}
