using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Structure
{
    [Serializable]
    public class OraConnect
    {

        public String Owner { get; set; }
        [XmlIgnore]
        public BackgroundWorker bkwGetTables { get; set; }

        public OraConnect()
        {

        }

        public OraConnect(BackgroundWorker pbkwGetTables)
        {
            bkwGetTables = pbkwGetTables;
        }

        public void GetOraTables(ref Gen activeSet)
        {

            using (OracleConnection conn = new OracleConnection(activeSet.DBConnection))
            {
                conn.Open();
                Double numTables = 0.0;
                Double numTablesDone = 0.0;

                using (OracleCommand cmd = new OracleCommand("select count(distinct Table_name) from all_tables where owner = '" + Owner + "' and Num_rows > 0"))
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    numTables = Convert.ToDouble(cmd.ExecuteScalar());
                }


                using (OracleCommand cmd = new OracleCommand("select * from all_tables where owner = '" + Owner + "' and Num_rows > 0"))
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            try
                            {

                                String tName = reader["TABLE_NAME"].ToString();
                                String action = "Update";
                                //String TblID = Convert.ToInt32(reader["TABLESPACE_NAME"].ToString());
                                TableSet ts = activeSet.Tables
                                    .Where(w => w.TableName == tName)
                                    .DefaultIfEmpty(new TableSet())
                                    .First();
                                ts.TableName = tName;
                                //ts.ID = TblID;
                                GetColumns(conn, ts);
                                GetIndices(conn, ts);
                                if (activeSet.Tables
                                    .Where(w => w.TableName == tName)
                                    .FirstOrDefault<TableSet>() == null)
                                {
                                    ts.Name = tName;
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
                        reader.Close();
                    }


                }
                conn.Close();
            }
        }
        private void GetColumns(OracleConnection conn, TableSet t)
        {
            if (t.Columns == null)
            {
                t.Columns = new List<ColumnSet>();
            }
            using (OracleCommand cmd = new OracleCommand("select * from all_tab_Columns where owner = '" + Owner + "' and  table_name = '" + t.TableName + "'"))
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        try
                        {
                            String cname = reader["COLUMN_NAME"].ToString();
                            ColumnSet c = t.GetColumnByColumnName(cname);

                            DataTypeSet dt = new DataTypeSet();
                            if (!String.IsNullOrEmpty(reader["DATA_PRECISION"].ToString()))
                                dt.NumericPrecision = Int32.Parse(reader["DATA_PRECISION"].ToString());
                            if (!String.IsNullOrEmpty(reader["DATA_LENGTH"].ToString()))
                                dt.MaximumLength = Int32.Parse(reader["DATA_LENGTH"].ToString());
                            if (!String.IsNullOrEmpty(reader["DATA_SCALE"].ToString()))
                                dt.NumericScale = Int32.Parse(reader["DATA_SCALE"].ToString());
                            dt.SqlDataType = reader["DATA_TYPE"].ToString();
                            c.ColumnDataType = dt;
                            if (!String.IsNullOrEmpty(reader["DATA_LENGTH"].ToString()))
                                c.Size = Int32.Parse(reader["DATA_LENGTH"].ToString());
                            if (!String.IsNullOrEmpty(reader["COLUMN_ID"].ToString()))
                                c.ID = Int32.Parse(reader["COLUMN_ID"].ToString());
                            if (reader["NULLABLE"].ToString() == "Y") c.IsNullable = true;
                            else c.IsNullable = false;
                            if (c.ColumnName != cname)
                            {
                                c.ColumnName = cname;
                                c.Name = cname;
                                t.Columns.Add(c);
                            }
                        }
                        catch (Exception ex)
                        {
                            bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nColumn Error: " + ex.Message.ToString() + " \n ");
                        }

                    }
                    reader.Close();
                }


            }

        }

        private void GetIndices(OracleConnection conn, TableSet t)
        {
            if (t.Columns == null)
            {
                t.Columns = new List<ColumnSet>();
            }
            using (OracleCommand cmd = new OracleCommand("select Index_Name, UNIQUENESS from all_Indexes where owner = '" + Owner + "' and  table_name = '" + t.Name + "'"))
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IndexSet i = new IndexSet();
                        try
                        {
                            i.Name = reader["INDEX_NAME"].ToString();
                            i.IndexName = reader["INDEX_NAME"].ToString();
                            if (reader["UNIQUENESS"].ToString() == "UNIQUE")
                                i.IsUnique = true;
                            else
                                i.IsUnique = false;

                            GetIndexColumns(conn, i);
                            t.Indices.Add(i);

                        }
                        catch (Exception ex)
                        {
                            bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nIndex Error: " + ex.Message.ToString() + " \n ");
                        }

                    }
                    reader.Close();
                }


            }

        }

        private void GetIndexColumns(OracleConnection conn, IndexSet i)
        {

            using (OracleCommand cmd = new OracleCommand("select * from SYS.ALL_IND_COLUMNS where  table_owner = '" + Owner + "' and Index_name = '" + i.Name + "'"))
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IndexColumnSet ic = new IndexColumnSet();
                        try
                        {
                            if (!String.IsNullOrEmpty(reader["COLUMN_POSITION"].ToString()))
                                ic.ID = Int32.Parse(reader["COLUMN_POSITION"].ToString());
                            ic.Name = reader["COLUMN_NAME"].ToString();
                            ic.ColumnName = reader["COLUMN_NAME"].ToString();
                            i.Columns.Add(ic);
                        }
                        catch (Exception ex)
                        {
                            bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nIndex Error: " + ex.Message.ToString() + " \n ");
                        }

                    }
                    reader.Close();
                }

            }

        }



    }

}
