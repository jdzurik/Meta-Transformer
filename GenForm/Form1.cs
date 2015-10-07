using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Structure;
using System.IO;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;
using System.Collections;
using System.Configuration;
using System.Xml.Xsl;
using System.Xml.XPath;
using RelatedObjects.Storage;
using html = HtmlAgilityPack;
using System.Security.Cryptography;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;




namespace GenForm
{

    public partial class Form1 : Form
    {
        public Gen activeSet = new Gen();
        public String activeProjectLocation { get; set; }
        public String AutoLoadFile;
        public bool AutoSave = true;
        public bool AutoSaveRecover = true;
        public List<TableSet> activeTree { get; set; }
        public TreeNode activeNode { get; set; }
        public List<XsltSet> activeXsltTree { get; set; }
        public FileSystemWatcher dWatch = new FileSystemWatcher();
        public int LoadSize = 100;
        public int OnLoadNum = 0;

        public Form1()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            if (Properties.Settings.Default.ExtPropAutoComplete == null)
            {
                Properties.Settings.Default.ExtPropAutoComplete = new ExtPropAutoCompleteList();
            }
            bisExtProp.DataSource = Properties.Settings.Default.ExtPropAutoComplete;
            dWatch.Filter = "*.xslt";
            //dWatch.NotifyFilter = NotifyFilters.LastAccess |
            //                 NotifyFilters.LastWrite |
            //                 NotifyFilters.FileName |
            //                 NotifyFilters.DirectoryName;
            dWatch.IncludeSubdirectories = false;
            dWatch.Changed += new FileSystemEventHandler(OnXsltChanged);
            dWatch.Created += new FileSystemEventHandler(OnXsltChanged);
            dWatch.Deleted += new FileSystemEventHandler(OnXsltChanged);
            dWatch.Renamed += new RenamedEventHandler(OnXsltRenamed);

            dWatch.EnableRaisingEvents = false;

        }


        private void tsbRunTransform_Click(object sender, EventArgs e)
        {
            if (!bkwRunTemplates.IsBusy)
            {
                activeSet.GenType = GenEnum.All;
                tssProgressLable.Text = "Transforming All Checked:";
                bkwRunTemplates.RunWorkerAsync();
            }
        }

        public void RunTransform()
        {
            if (!bkwRunTemplates.IsBusy)
            {
                activeSet.GenType = GenEnum.All;
                bkwRunTemplates.RunWorkerAsync();
            }
        }

        private void btnHelpFolder_Click(object sender, EventArgs e)
        {
            DialogResult dr = fbdSettings.ShowDialog();
            if (dr == DialogResult.OK)
            {
                activeSet.DirHelpSource = fbdSettings.SelectedPath;
                //txtHelpDir.Text = activeSet.DirHelpSource;

                DirectoryInfo di = new DirectoryInfo(activeSet.DirHelpSource);
                FileInfo[] xf = di.GetFiles("*.html");

                DialogResult dr2 = new DialogResult();
                if (activeSet.HelpFiles.Count > 0)
                    dr2 = MessageBox.Show("The current " + activeSet.HelpFiles.Count.ToString() + " help files will be removed and then replaced is that ok?", "Replace Files?", MessageBoxButtons.OKCancel);

                if (activeSet.HelpFiles.Count == 0 || dr2 == DialogResult.OK)
                {
                    activeSet.HelpFiles.Clear();

                    for (int i = 0; i < xf.Length; i++)
                    {

                        activeSet.HelpFiles.Add(xf[i].FullName);

                    }
                }

            }
        }

        private void btnExcelFile_Click(object sender, EventArgs e)
        {

            ofdSettings.DefaultExt = "xls";
            ofdSettings.Filter = "Excel files (*.xls; *.xlsx)|*.xls; *.xlsx";
            ofdSettings.RestoreDirectory = true;
            ofdSettings.FileName = "";
            DialogResult dr = ofdSettings.ShowDialog();

            if (dr == DialogResult.OK)
            {
                activeSet.ExcelFile = ofdSettings.FileName;
                //txtExcelFile.Text = activeSet.ExcelFile;

            }
        }

        private void btnTemplates_Click(object sender, EventArgs e)
        {
            DialogResult dr = fbdSettings.ShowDialog();
            if (dr == DialogResult.OK)
            {
                activeSet.DirTransforms = fbdSettings.SelectedPath;
                txtTemplateDir.Text = activeSet.DirTransforms;


                DirectoryInfo di = new DirectoryInfo(activeSet.DirTransforms);
                FileInfo[] xf = di.GetFiles("*.xslt");

                DialogResult dr2 = new DialogResult();
                if (activeSet.XsltFiles.Count > 0)
                    dr2 = MessageBox.Show("The current " + activeSet.XsltFiles.Count.ToString() + " template files will remain and only new templates will be added is that ok?", "Add template Files?", MessageBoxButtons.OKCancel);

                if (activeSet.XsltFiles.Count == 0 || dr2 == DialogResult.OK)
                {
                    //activeSet.XsltFiles.Clear();
                    for (int i = 0; i < xf.Length; i++)
                    {
                        try
                        {
                            activeSet.XsltFiles.Single<XsltSet>(x => x.XsltPath == xf[i].FullName);
                        }
                        catch
                        {
                            XsltSet xs = new XsltSet(xf[i].FullName, activeSet.DirOutput);
                            activeSet.XsltFiles.Add(xs);
                        }
                    }
                    LoadTemplates();
                }
            }



        }

        private void LoadTemplates()
        {
            flpTemplates.Controls.Clear();
            if (cbxViewCheckedTemplates.Checked)
            {
                foreach (XsltSet xs in activeSet.XsltFiles)
                {
                    if (xs.Use)
                    {
                        TemplateUC tc = new TemplateUC();
                        tc.activeSet = activeSet;
                        tc.xsltData = xs;
                        tc.Dock = DockStyle.Top;
                        tc.TeplateUCLoad();
                        flpTemplates.Controls.Add(tc);
                    }
                }
            }
            else
            {
                foreach (XsltSet xs in activeSet.XsltFiles)
                {
                    TemplateUC tc = new TemplateUC();
                    tc.activeSet = activeSet;
                    tc.xsltData = xs;
                    tc.Dock = DockStyle.Top;
                    tc.TeplateUCLoad();
                    flpTemplates.Controls.Add(tc);
                }

            }


        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            DialogResult dr = fbdSettings.ShowDialog();
            if (dr == DialogResult.OK)
            {
                activeSet.DirOutput = fbdSettings.SelectedPath;
                txtOutputDir.Text = activeSet.DirOutput;
            }
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            activeSet.ProjectName = txtProjectName.Text;
        }


        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            activeSet.DBUser = txtUsername.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(txtPassword.Text);
            byte[] encrypted = ProtectedData.Protect(
            plainTextBytes,
            null,
            DataProtectionScope.CurrentUser
            );

            activeSet.DBPass = encrypted;
            //activeSet.DBPass = txtPassword.Text;

        }

        private void txtOrcaleCn_TextChanged(object sender, EventArgs e)
        {
            activeSet.DBConnectString = txtOrcaleCn.Text;
        }



        private void txtOutputDir_TextChanged(object sender, EventArgs e)
        {
            activeSet.DirOutput = txtOutputDir.Text;
        }

        private void txtTemplateDir_TextChanged(object sender, EventArgs e)
        {

        }

        private void tsbSaveProject_Click(object sender, EventArgs e)
        {

            SaveToFile();

        }

        private void SaveToFile()
        {
            if (!string.IsNullOrEmpty(activeProjectLocation))
            {
                if (!string.IsNullOrEmpty(activeSet.ProjectName))
                {
                    SaveActiveProject();
                }
                else
                {
                    MessageBox.Show("Project name can not be left blank.");
                }
            }
            else
            {
                SaveWithDialog();
            }
        }

        public void SaveWithDialog()
        {
            if (!string.IsNullOrEmpty(activeSet.ProjectName))
            {
                sfdSettings.CreatePrompt = true;
                sfdSettings.OverwritePrompt = true;

                sfdSettings.FileName = activeSet.ProjectName;
                sfdSettings.DefaultExt = "xcg";
                sfdSettings.Filter = "Xml Code Gen files (*.xcg)|*.xcg";
                sfdSettings.RestoreDirectory = true;
                sfdSettings.FileName = "";

                DialogResult dr = sfdSettings.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    activeSet.AvailableExtProps = Properties.Settings.Default.ExtPropAutoComplete;
                    try
                    {
                        Stream fs = sfdSettings.OpenFile();
                        XmlSerializer serializer = new XmlSerializer(typeof(Gen));
                        serializer.Serialize(fs, activeSet);
                        fs.Close();
                        activeProjectLocation = sfdSettings.FileName;
                        activeSet.ActiveLocation = activeProjectLocation;
                        tssLbl1.Text = "Active File: " + activeProjectLocation;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Can't save File " + ex.Message);
                    }

                }
            }
            else
            {
                MessageBox.Show("Project name can not be left blank.");
            }

        }

        public void SaveActiveProject()
        {
            if (!bkwSaveFile.IsBusy)
                bkwSaveFile.RunWorkerAsync();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            bool cancel = false;
            if (activeProjectLocation != null)
            {
                DialogResult drs = MessageBox.Show("Would you like to save your current project first?", "Save current?", MessageBoxButtons.YesNoCancel);
                if (drs == DialogResult.Yes)
                {
                    SaveToFile();
                }
             
                if (drs == DialogResult.Cancel) {
                    cancel = true;
                }
            }
            if (!cancel)
            {
                OpenFileDialog();
            }
        }

        private void OpenFileDialog()
        {
                    ofdSettings.DefaultExt = "xcg";
                    ofdSettings.Filter = "Xml Code Gen files (*.xcg)|*.xcg";
                    ofdSettings.RestoreDirectory = true;

                    DialogResult dr = ofdSettings.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        try
                        {
                            if (!bkwOpenFile.IsBusy)
                                bkwOpenFile.RunWorkerAsync(ofdSettings.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Invalid File Format." + ex.Message);

                        }
                    }
        }

        public void OpenGenFile(String FileName)
        {
            try
            {
                if (!bkwOpenFile.IsBusy)
                    bkwOpenFile.RunWorkerAsync(FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid File Format." + ex.Message);

            }
        }

        private void bkwOpenFile_DoWork(object sender, DoWorkEventArgs e)
        {
            String Sp = (String)e.Argument;
            FileInfo fi = new FileInfo(Sp);
            if (fi.Exists)
            {
                using (Stream fs = File.Open(Sp, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Gen));
                    activeSet = (Gen)serializer.Deserialize(fs);
                    fs.Close();
                }
            }
        }

        private void bkwOpenFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                SetFormValues();
                activeProjectLocation = ofdSettings.FileName;

                tssLbl1.Text = " Active File: " + activeProjectLocation;

                if (Properties.Settings.Default.ExtPropAutoComplete == null)
                    Properties.Settings.Default.ExtPropAutoComplete = new ExtPropAutoCompleteList();

                Properties.Settings.Default.ExtPropAutoComplete.Clear();
                foreach (ExtPropAutoComplete item in activeSet.AvailableExtProps)
                {
                    Properties.Settings.Default.ExtPropAutoComplete.Add(item);
                }
                Properties.Settings.Default.Save();
                LoadTemplates();
                LoadAsyncTree();
                bisLogging.DataSource = activeSet.Logging;
                activeSet.ActiveLocation = activeProjectLocation;
                dWatch.Path = activeSet.DirTransforms + "\\";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Loading file and setting defalt values \n" + ex.Message);
            }

        }

        public void SetFormValues()
        {

            txtHelpFile.Text = activeSet.HelpFile;
            txtUsername.Text = activeSet.DBUser;
            try
            {
                if (activeSet.DBPass != null && activeSet.DBPass.Length > 0)
                {
                    byte[] unencrypted = ProtectedData.Unprotect(
                    activeSet.DBPass,
                    null,
                    DataProtectionScope.CurrentUser
                    );
                    string stp = Encoding.Default.GetString(unencrypted).Replace("\0", "");
                    txtPassword.Text = stp;
                }
            }
            catch
            {
                MessageBox.Show("Error with security decrypting Password. Make sure you are logged in as the user that set the password.");
            }

            txtOrcaleCn.Text = activeSet.DBConnectString;
            txtOwner.Text = activeSet.OracleConnection.Owner;
            txtOutputDir.Text = activeSet.DirOutput;
            txtTemplateDir.Text = activeSet.DirTransforms;
            txtProjectName.Text = activeSet.ProjectName;
            bisExtProp.DataSource = activeSet.AvailableExtProps;
            cbxSQLServer.Text = activeSet.SQLConnection.ActiveServer;
            cbxSQLDatabase.Text = activeSet.SQLConnection.ActiveDatabase;
            cbxSQLTrustedConnection.Checked = activeSet.SQLConnection.IsTrusted;
            //tviTables

        }

        private void dgvExtPropNames_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                if (dgvExtPropNames.CurrentCell.ColumnIndex == 3)
                {
                    DataGridViewTextBoxEditingControl te = (DataGridViewTextBoxEditingControl)e.Control;
                    te.AutoCompleteMode = AutoCompleteMode.Suggest;
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    te.AutoCompleteCustomSource.Add("All");
                    te.AutoCompleteCustomSource.Add("Table");
                    te.AutoCompleteCustomSource.Add("Column");
                    te.AutoCompleteCustomSource.Add("Filter");
                    te.AutoCompleteCustomSource.Add("Foreignkey");
                    te.AutoCompleteCustomSource.Add("List");
                    te.AutoCompleteCustomSource.Add("Sub");

                }
            }
        }

        private void dgvExtPropNames_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (activeSet.AvailableExtProps.Count > 0)
                bisExtPropValues.DataSource = activeSet.AvailableExtProps[e.RowIndex].AvalibalValues;

        }

        private void tsbRefreshTables_Click(object sender, EventArgs e)
        {
            TableRebuild();
        }

        #region OldOracle

        //private void GetOraTables()
        //{

        //  using (OracleConnection conn = new OracleConnection(activeSet.DBConnection))
        //  {
        //    conn.Open();
        //    Double numTables = 0.0;
        //    Double numTablesDone = 0.0;

        //    using (OracleCommand cmd = new OracleCommand("select count(distinct Table_name) from all_tables where owner = 'LAWSON' and Num_rows > 0"))
        //    {
        //      cmd.Connection = conn;
        //      cmd.CommandType = CommandType.Text;
        //      numTables = Convert.ToDouble(cmd.ExecuteScalar());
        //    }


        //    using (OracleCommand cmd = new OracleCommand("select * from all_tables where owner = 'LAWSON' and Num_rows > 0"))
        //    {
        //      cmd.Connection = conn;
        //      cmd.CommandType = CommandType.Text;

        //      using (OracleDataReader reader = cmd.ExecuteReader())
        //      {
        //        while (reader.Read())
        //        {

        //          try
        //          {
        //            String tName = reader["TABLE_NAME"].ToString();
        //            String action = "Update";
        //            TableSet t = activeSet.GetTableByTableName(tName);
        //            GetColumns(conn, t);
        //            GetIndices(conn, t);
        //            if (t.TableName != tName)
        //            {
        //              t.Name = tName;
        //              t.TableName = tName;
        //              activeSet.Tables.Add(t);
        //              action = "Add";
        //            }

        //            numTablesDone += 1;
        //            bkwGetTables.ReportProgress(Convert.ToInt32((numTablesDone / numTables) * 100), "\nTable " + action + ": " + tName + " \n ");
        //          }
        //          catch (Exception ex)
        //          {
        //            bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nTable Error: " + ex.Message.ToString() + " \n ");
        //          }

        //        }
        //        reader.Close();
        //      }


        //    }
        //    conn.Close();
        //  }
        //}
        //private void GetColumns(OracleConnection conn, TableSet t)
        //{
        //  if (t.Columns == null)
        //  {
        //    t.Columns = new List<ColumnSet>();
        //  }
        //  using (OracleCommand cmd = new OracleCommand("select * from all_tab_Columns where owner = 'LAWSON' and  table_name = '" + t.TableName + "'"))
        //  {
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.Text;

        //    using (OracleDataReader reader = cmd.ExecuteReader())
        //    {
        //      while (reader.Read())
        //      {

        //        try
        //        {
        //          String cname = reader["COLUMN_NAME"].ToString();
        //          ColumnSet c = t.GetColumnByColumnName(cname);

        //          DataTypeSet dt = new DataTypeSet();
        //          if (!String.IsNullOrEmpty(reader["DATA_PRECISION"].ToString()))
        //            dt.NumericPrecision = Int32.Parse(reader["DATA_PRECISION"].ToString());
        //          if (!String.IsNullOrEmpty(reader["DATA_LENGTH"].ToString()))
        //            dt.MaximumLength = Int32.Parse(reader["DATA_LENGTH"].ToString());
        //          if (!String.IsNullOrEmpty(reader["DATA_SCALE"].ToString()))
        //            dt.NumericScale = Int32.Parse(reader["DATA_SCALE"].ToString());
        //          dt.SqlDataType = reader["DATA_TYPE"].ToString();
        //          c.ColumnDataType = dt;
        //          if (!String.IsNullOrEmpty(reader["DATA_LENGTH"].ToString()))
        //            c.Size = Int32.Parse(reader["DATA_LENGTH"].ToString());
        //          if (!String.IsNullOrEmpty(reader["COLUMN_ID"].ToString()))
        //            c.ID = Int32.Parse(reader["COLUMN_ID"].ToString());
        //          if (reader["NULLABLE"].ToString() == "Y") c.IsNullable = true;
        //          else c.IsNullable = false;
        //          if (c.ColumnName != cname)
        //          {
        //            c.ColumnName = cname;
        //            c.Name = cname;
        //            t.Columns.Add(c);
        //          }
        //        }
        //        catch (Exception ex)
        //        {
        //          bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nColumn Error: " + ex.Message.ToString() + " \n ");
        //        }

        //      }
        //      reader.Close();
        //    }


        //  }

        //}
        //private void GetIndices(OracleConnection conn, TableSet t)
        //{
        //  if (t.Columns == null)
        //  {
        //    t.Columns = new List<ColumnSet>();
        //  }
        //  using (OracleCommand cmd = new OracleCommand("select Index_Name, UNIQUENESS from all_Indexes where owner = 'LAWSON' and  table_name = '" + t.Name + "'"))
        //  {
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.Text;

        //    using (OracleDataReader reader = cmd.ExecuteReader())
        //    {
        //      while (reader.Read())
        //      {
        //        IndexSet i = new IndexSet();
        //        try
        //        {
        //          i.Name = reader["INDEX_NAME"].ToString();
        //          i.IndexName = reader["INDEX_NAME"].ToString();
        //          if (reader["UNIQUENESS"].ToString() == "UNIQUE")
        //            i.IsUnique = true;
        //          else
        //            i.IsUnique = false;

        //          GetIndexColumns(conn, i);
        //          t.Indices.Add(i);

        //        }
        //        catch (Exception ex)
        //        {
        //          bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nIndex Error: " + ex.Message.ToString() + " \n ");
        //        }

        //      }
        //      reader.Close();
        //    }


        //  }

        //}
        //private void GetIndexColumns(OracleConnection conn, IndexSet i)
        //{

        //  using (OracleCommand cmd = new OracleCommand("select * from SYS.ALL_IND_COLUMNS where  table_owner = 'LAWSON' and Index_name = '" + i.Name + "'"))
        //  {
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.Text;

        //    using (OracleDataReader reader = cmd.ExecuteReader())
        //    {
        //      while (reader.Read())
        //      {
        //        IndexColumnSet ic = new IndexColumnSet();
        //        try
        //        {
        //          if (!String.IsNullOrEmpty(reader["COLUMN_POSITION"].ToString()))
        //            ic.ID = Int32.Parse(reader["COLUMN_POSITION"].ToString());
        //          ic.Name = reader["COLUMN_NAME"].ToString();
        //          ic.ColumnName = reader["COLUMN_NAME"].ToString();
        //          i.Columns.Add(ic);
        //        }
        //        catch (Exception ex)
        //        {
        //          bkwGetTables.ReportProgress(Convert.ToInt32(0), "\nIndex Error: " + ex.Message.ToString() + " \n ");
        //        }

        //      }
        //      reader.Close();
        //    }

        //  }

        //}

        #endregion OldOracle

        private void bkwGetTables_DoWork(object sender, DoWorkEventArgs e)
        {
            if (activeSet.DataBaseType == "tabOra")
            {
                activeSet.OracleConnection = new OraConnect((BackgroundWorker)sender);
                activeSet.OracleConnection.Owner = txtOwner.Text;
                activeSet.OracleConnection.GetOraTables(ref activeSet);
            }
            else
            {
                activeSet.SQLConnection.bkwGetTables = (BackgroundWorker)sender;
                activeSet.SQLConnection.GetSqlTables(ref activeSet);
            }

        }

        private void GetSQLTables()
        {

        }

        private void bkwGetTables_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            rtbOutput.Text = e.UserState.ToString() + rtbOutput.Text;
            tsProgress1.Value = e.ProgressPercentage;
        }

        private void bkwGetTables_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadAsyncTree();
            tsProgress1.Value = 0;
            tssProgressLable.Text = "Tables Complete:";
        }

        private void tsbMergHelp_Click(object sender, EventArgs e)
        {
            if (!bkwMergeHelp.IsBusy)
            {
                rtbOutput.Text = "";
                tssProgressLable.Text = "Creating and Merging Help:";
                bkwMergeHelp.RunWorkerAsync(true);

            }
        }

        private void tsbMergHTML_Click(object sender, EventArgs e)
        {
            if (!bkwMergeHelp.IsBusy)
            {
                rtbOutput.Text = "";
                tssProgressLable.Text = "Merging HTML Help:";
                bkwMergeHelp.RunWorkerAsync(false);

            }
        }

        private void bkwMergeHelp_DoWork(object sender, DoWorkEventArgs e)
        {

            if (!String.IsNullOrEmpty(activeSet.HelpFile) && !String.IsNullOrEmpty(activeSet.DirHelpSource))
            {
                Double numTables = 0.0;
                Double numTablesDone = 0.0;
                if ((Boolean)(e.Argument))
                {
                    ITStorageWrapper iw = new ITStorageWrapper(activeSet.HelpFile);
                    numTables = iw.foCollection.Count;
                    foreach (IBaseStorageWrapper.FileObjects.FileObject fileObject in iw.foCollection)
                    {
                        if (fileObject.CanRead)
                        {
                            if (fileObject.FileName.EndsWith(".htm"))
                            {
                                html.HtmlDocument doc = new html.HtmlDocument();
                                doc.LoadHtml(fileObject.ReadFromFile());
                                doc.OptionOutputOptimizeAttributeValues = true;
                                doc.OptionOutputAsXml = true;
                                doc.OptionFixNestedTags = true;
                                doc.OptionUseIdAttribute = true;
                                doc.Save(activeSet.DirHelpSource + "\\" + fileObject.FileName);
                                numTablesDone += 1;
                                int percentdone = Convert.ToInt32((numTablesDone / numTables) * 100);
                                bkwMergeHelp.ReportProgress(percentdone, "\n HTML File Created: " + fileObject.FileName + " \n ");
                            }
                        }
                    }
                    bkwMergeHelp.ReportProgress(0, "\n Files Created ... Starting Merged: \n ");
                }

                numTables = Convert.ToDouble(activeSet.Tables.Count);
                numTablesDone = 0.0;

                foreach (TableSet tbl in activeSet.Tables)
                {
                    tbl.MergHelp(activeSet.DirHelpSource, activeSet);
                    numTablesDone += 1;
                    int percentdone = Convert.ToInt32((numTablesDone / numTables) * 100);
                    bkwMergeHelp.ReportProgress(percentdone, "\nHTML Meta Data Merged: " + tbl.Name + " \n ");
                }
                GC.Collect();
            }
            else
            {
                MessageBox.Show("A .chm Help file or output location is not selected.");
            }
        }

        private void bkwMergeHelp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            rtbOutput.Text = e.UserState.ToString() + rtbOutput.Text;
            tsProgress1.Value = e.ProgressPercentage;
        }

        private void bkwMergeHelp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tsProgress1.Value = 0;
            tssProgressLable.Text = "Help File Merge Complete: ";
        }



        private void tsbLoadTree_Click(object sender, EventArgs e)
        {
            LoadAsyncTree();
        }





        private void btnFilter_Click(object sender, EventArgs e)
        {
            //cbxShowChecked.Checked = false;
            LoadAsyncTree();
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                //cbxShowChecked.Checked = false;
                LoadAsyncTree();
            }
        }

        //public void SearchTree()
        //{
        //  if (!bkwLoadTree.IsBusy)
        //  {
        //    if (txtFilter.Text == "")
        //    {
        //      LoadAsyncTree();
        //    }
        //    else
        //    {
        //      rtbOutput.Text = "";
        //      btnFilter.Enabled = false;
        //      if (cbxShowChecked.Checked)
        //      {
        //        activeTree = (from tbl
        //                    in activeSet.Tables
        //                      where tbl.Checked && tbl.Name.ToLower().Contains(txtFilter.Text.ToLower()) || (tbl.Columns.Exists(x => x.Name.ToLower().Contains(txtFilter.Text.ToLower())))
        //                      select tbl
        //              ).OrderBy(x => x.Name).ToList<TableSet>();
        //      }
        //      else
        //      {
        //        activeTree = (from tbl
        //                    in activeSet.Tables
        //                      where tbl.Name.ToLower().Contains(txtFilter.Text.ToLower())
        //                      || (tbl.Columns.Exists(x => x.Name.ToLower().Contains(txtFilter.Text.ToLower())))
        //                      select tbl
        //              ).OrderBy(x => x.Name).ToList<TableSet>();
        //      }
        //      tssProgressLable.Text = "Loading Search Tree: ";

        //      bkwLoadTree.RunWorkerAsync();

        //    }
        //  }
        //}

        private void tviTables_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == "Table")
            {
                TableSet ao = (TableSet)e.Node.Tag;
                ao.Checked = e.Node.Checked;
            }

            if (e.Node.Name == "Column")
            {
                ColumnSet ao = (ColumnSet)e.Node.Tag;
                ao.Checked = e.Node.Checked;
            }

            if (e.Node.Name == "Filter")
            {
                FilterSet ao = (FilterSet)e.Node.Tag;
                ao.Checked = e.Node.Checked;
            }

            if (e.Node.Name == "Foreignkey")
            {
                ForeignKeySet ao = (ForeignKeySet)e.Node.Tag;
                ao.Checked = e.Node.Checked;
            }


        }

        private void tviTables_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Node.Name == "Table")
                {
                    TableSet ao = (TableSet)e.Node.Tag;
                    ao.Name = e.Label;
                }

                if (e.Node.Name == "Column")
                {
                    ColumnSet ao = (ColumnSet)e.Node.Tag;
                    ao.Name = e.Label;
                }

                if (e.Node.Name == "Filter")
                {
                    FilterSet ao = (FilterSet)e.Node.Tag;
                    ao.Name = e.Label;
                }

                if (e.Node.Name == "Foreignkey")
                {
                    ForeignKeySet ao = (ForeignKeySet)e.Node.Tag;
                    ao.Name = e.Label;
                }
            }
        }

        private void tviTables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == "Table")
            {
                TableUC tuc = new TableUC();
                tuc.Dock = DockStyle.Fill;
                tuc.ActiveTable = (TableSet)e.Node.Tag;
                tuc.SetForm();
                pnlControlContainer.Controls.Clear();
                pnlControlContainer.Controls.Add(tuc);
                activeSet.ActiveTable = (TableSet)e.Node.Tag;

            }
            if (e.Node.Name == "Column")
            {
                ColumnUC tuc = new ColumnUC();
                tuc.Dock = DockStyle.Fill;
                tuc.ActiveColumn = (ColumnSet)e.Node.Tag;
                tuc.ActiveTable = activeSet.ActiveTable;
                tuc.ActiveTreeNode = e.Node;
                tuc.SetForm();
                pnlControlContainer.Controls.Clear();
                pnlControlContainer.Controls.Add(tuc);

            }

            if (e.Node.Name == "Index")
            {
                IndexUC tuc = new IndexUC();
                tuc.Dock = DockStyle.Fill;
                tuc.ActiveIndex = (IndexSet)e.Node.Tag;
                tuc.SetForm();
                pnlControlContainer.Controls.Clear();
                pnlControlContainer.Controls.Add(tuc);

            }


            if (e.Node.Name == "AddFilter")
            {
                activeSet.ActiveTable = (TableSet)e.Node.Parent.Tag;
                AddFilterUC tuc = new AddFilterUC();
                tuc.Dock = DockStyle.Fill;
                tuc.ActiveTable = activeSet.ActiveTable;
                tuc.ActiveTreenode = e.Node;
                pnlControlContainer.Controls.Clear();
                pnlControlContainer.Controls.Add(tuc);

            }

            if (e.Node.Name == "Filter")
            {
                activeSet.ActiveTable = (TableSet)e.Node.Parent.Parent.Tag;
                FilterUC tuc = new FilterUC();
                tuc.Dock = DockStyle.Fill;
                tuc.ActiveFilter = (FilterSet)e.Node.Tag;
                tuc.ActiveTable = activeSet.ActiveTable;
                tuc.ActiveTreenode = e.Node;
                tuc.SetForm();
                pnlControlContainer.Controls.Clear();
                pnlControlContainer.Controls.Add(tuc);

            }


            if (e.Node.Name == "AddForeignkey")
            {
                activeSet.ActiveTable = (TableSet)e.Node.Parent.Tag;
                AddForeignKeyUC tuc = new AddForeignKeyUC();
                tuc.Dock = DockStyle.Fill;
                tuc.ActiveTable = activeSet.ActiveTable;
                tuc.ActiveTreenode = e.Node;
                pnlControlContainer.Controls.Clear();
                pnlControlContainer.Controls.Add(tuc);

            }

            if (e.Node.Name == "Foreignkey")
            {
                activeSet.ActiveTable = (TableSet)e.Node.Parent.Parent.Tag;
                ForeignKeyUC tuc = new ForeignKeyUC();
                tuc.Dock = DockStyle.Fill;
                tuc.ActiveFK = (ForeignKeySet)e.Node.Tag;
                tuc.ActiveTable = activeSet.ActiveTable;
                tuc.ActiveTreenode = e.Node;
                tuc.ActiveSet = activeSet;
                tuc.SetForm();
                pnlControlContainer.Controls.Clear();
                pnlControlContainer.Controls.Add(tuc);

            }

        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabTables")
            {
                if (tviTables.Nodes == null || tviTables.Nodes.Count == 0)
                {
                    LoadAsyncTree();
                }
            }
            if (tabControl1.SelectedTab.Name == "tabTransformers")
            {
                if (tviXslts.Nodes == null || tviXslts.Nodes.Count == 0)
                {

                }
                LoadAsyncTreeXslt(true);
                if (dWatch.Path != "")
                {
                    dWatch.EnableRaisingEvents = true;
                }
                else
                {
                    dWatch.EnableRaisingEvents = false;
                }
            }
            else
            {
                dWatch.EnableRaisingEvents = false;
            }
        }

        public void LoadAsyncTreeXslt(Boolean IsAsync)
        {
            if (cbxViewCheckedTemplates.Checked)
            {
                //btnFilter.Enabled = false;
                if (txtFilterXslt.Text != "")
                {
                    activeXsltTree = (from tbl
                                in activeSet.XsltFiles
                                      where tbl.Use
                                      && tbl.Name.ToLower().Contains(txtFilterXslt.Text.ToLower())
                                      select tbl
                          ).OrderBy(x => x.Name).ToList<XsltSet>();
                }
                else
                {
                    activeXsltTree = (from tbl
                                in activeSet.XsltFiles
                                      where tbl.Use
                                      select tbl
                          ).OrderBy(x => x.Name).ToList<XsltSet>();
                }
            }
            else
            {
                if (rtbOutput.InvokeRequired)
                {
                    rtbOutput.Invoke(new MethodInvoker(delegate { rtbOutput.Text = ""; }));
                }
                //btnFilterXslt.Enabled = false;
                if (txtFilterXslt.Text != "")
                {
                    activeXsltTree = activeSet.XsltFiles.Where(c => c.Name.ToLower().Contains(txtFilterXslt.Text.ToLower())).OrderBy(x => x.Name).ToList<XsltSet>();
                }
                else
                {
                    activeXsltTree = activeSet.XsltFiles.OrderBy(x => x.Name).ToList<XsltSet>();
                }

            }
            if (IsAsync && !bkwLoadXsltTree.IsBusy)
            {
                tssProgressLable.Text = "Loading Xslt Tree:";
                bkwLoadXsltTree.RunWorkerAsync();
            }
            else
            {
                LoadXsltTree(IsAsync);
            }

        }

        public void LoadAsyncTree()
        {
            if (cbxShowChecked.Checked)
            {
                btnFilter.Enabled = false;
                if (txtFilter.Text == "")
                {
                    activeTree = (from tbl
                            in activeSet.Tables
                                  where tbl.Checked
                                  select tbl
                      ).OrderBy(x => x.Name).ToList<TableSet>();
                    tssProgressLable.Text = "Loading Checked Tables: ";
                }
                else
                {
                    activeTree = (from tbl
                                  in activeSet.Tables
                                  where tbl.Checked
                                  && (tbl.Name.ToLower().Contains(txtFilter.Text.ToLower())
                                  || (tbl.Columns.Exists(
                                        x => x.Name.ToLower().Contains(txtFilter.Text.ToLower()))))
                                  select tbl
                                  ).OrderBy(x => x.Name).ToList<TableSet>();
                    tssProgressLable.Text = "Loading Checked Tables Search: ";
                }
            }
            else
            {
                rtbOutput.Text = "";
                btnFilter.Enabled = false;
                if (txtFilter.Text == "")
                {
                    activeTree = activeSet.Tables.OrderBy(x => x.Name).ToList<TableSet>();
                    tssProgressLable.Text = "Loading Tables:";
                }
                else
                {
                    activeTree = (from tbl
                                  in activeSet.Tables
                                  where (tbl.Name.ToLower().Contains(txtFilter.Text.ToLower())
                                  || (tbl.Columns.Exists(
                                        x => x.Name.ToLower().Contains(txtFilter.Text.ToLower()))))
                                  select tbl
                                  ).OrderBy(x => x.Name).ToList<TableSet>();
                    tssProgressLable.Text = "Loading Tables Search:";
                }
            }
            tviTables.Nodes.Clear();
            bkwLoadTree.RunWorkerAsync();
        }

        private void bkwLoadTree_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadTree();
        }

        private void bkwLoadTree_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tsProgress1.Value = e.ProgressPercentage;
        }

        private void bkwLoadTree_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tsProgress1.Value = 0;
            tssProgressLable.Text = "Tree Loaded: ";
            //tabControl1.SelectTab("tabTables");
            btnFilter.Enabled = true;

            if (OnLoadNum < activeTree.Count)
            {
                bkwLoadTree.RunWorkerAsync();
            }
            else
            {
                tabControl1.SelectTab("tabTables");
                OnLoadNum = 0;
            }

        }

        private void LoadTree()
        {


            Double numTables = Convert.ToDouble(activeSet.Tables.Count);
            Double numTablesDone = 0.0;
            //tviTables.Invoke(
            //     (MethodInvoker)delegate()
            //     {
            //       tviTables.Nodes.Clear();
            //     });
            //foreach (TableSet tbl in activeTree)
            //{

            int LoadTo = OnLoadNum + LoadSize;
            if (LoadTo > activeTree.Count)
            {
                LoadTo = activeTree.Count;
            }
            for (int j = OnLoadNum; j < LoadTo; j++)
            {
                numTablesDone = Convert.ToDouble(OnLoadNum);
                TableSet tbl = activeTree[j];
                int percentdone = Convert.ToInt32((numTablesDone / numTables) * 100);
                bkwLoadTree.ReportProgress(percentdone, "Loading Table: " + tbl.TableName);
                //if (tbl.Name == "")
                //{
                //   tbl.Name = tbl.TableName;
                //}
                TreeNode ttn = new TreeNode(tbl.Name);
                ttn.ContextMenuStrip = cmsTableTree;
                ttn.Checked = tbl.Checked;

                ttn.Name = "Table";
                ttn.Tag = tbl;
                TreeNode ttnc = new TreeNode("Columns");
                for (int i = 0; i < tbl.Columns.Count; i++)
                {

                    TreeNode ctn = new TreeNode(tbl.Columns[i].Name);
                    ctn.Checked = tbl.Columns[i].Checked;
                    ctn.Name = "Column";
                    ctn.Tag = tbl.Columns[i];
                    ttnc.Nodes.Add(ctn);
                }
                ttn.Nodes.Add(ttnc);

                TreeNode ttni = new TreeNode("Indices");
                for (int i = 0; i < tbl.Indices.Count; i++)
                {
                    TreeNode ctn = new TreeNode(tbl.Indices[i].Name);
                    ctn.Checked = tbl.Indices[i].Checked;
                    ctn.Name = "Index";
                    ctn.Tag = tbl.Indices[i];
                    ttni.Nodes.Add(ctn);
                }
                ttn.Nodes.Add(ttni);

                TreeNode ttnf = new TreeNode("Filters");
                ttnf.Name = "AddFilter";
                for (int i = 0; i < tbl.Filters.Count; i++)
                {
                    TreeNode ctn = new TreeNode(tbl.Filters[i].Name);
                    ctn.Checked = tbl.Filters[i].Checked;
                    ctn.Name = "Filter";
                    ctn.Tag = tbl.Filters[i];
                    ttnf.Nodes.Add(ctn);
                }
                ttn.Nodes.Add(ttnf);

                TreeNode ttnfk = new TreeNode("Foreign Keys");
                ttnfk.Name = "AddForeignkey";
                for (int i = 0; i < tbl.ForeignKeys.Count; i++)
                {
                    TreeNode ctn = new TreeNode(tbl.ForeignKeys[i].Name);
                    ctn.Checked = tbl.ForeignKeys[i].Checked;
                    ctn.Tag = tbl.ForeignKeys[i];
                    ctn.Name = "Foreignkey";
                    ttnfk.Nodes.Add(ctn);
                }
                ttn.Nodes.Add(ttnfk);

                tviTables.Invoke(
                   (MethodInvoker)delegate()
                   {
                       tviTables.Nodes.Add(ttn);
                   });
                //numTablesDone += 1;

                OnLoadNum += 1;

            }
        }

        private void cbxShowChecked_CheckedChanged(object sender, EventArgs e)
        {
            LoadAsyncTree();
        }

        private void bkwSaveFile_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bkwSaveFile.ReportProgress(50, "Saving ...");
                Stream fs = File.Open(activeProjectLocation, FileMode.Create, FileAccess.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(typeof(Gen));
                serializer.Serialize(fs, activeSet);
                fs.Close();
                bkwSaveFile.ReportProgress(100, "Complete! ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't Save File! " + ex.Message);
            }
        }

        private void bkwSaveFile_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tsProgress1.Value = e.ProgressPercentage;
            tssProgressLable.Text = e.UserState.ToString();
        }

        private void bkwSaveFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tsProgress1.Value = 0;
            tssProgressLable.Text = "File Saved! ";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void cbxViewCheckedTemplates_CheckedChanged(object sender, EventArgs e)
        {
            LoadAsyncTreeXslt(true);
        }

        private void bkwRunTemplates_DoWork(object sender, DoWorkEventArgs e)
        {
            activeSet.Run(bkwRunTemplates);
        }

        private void bkwRunTemplates_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if (tabsOutput.SelectedTab.Name != "tabLogging")
            //  tabsOutput.SelectTab("tabLogging");

            LoggingItem li = (LoggingItem)e.UserState;
            if (li.LogType == LoggingEnum.Progress)
            {
                tsProgress1.Value = li.PercentComplete;
            }

            //dgvLogging.Sort = "StartTime";
            dgvLogging.DataSource = activeSet.Logging;

        }

        private void bkwRunTemplates_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tsProgress1.Value = 0;
            tssProgressLable.Text = "Transformation Complete!";
        }

        private void runTemplatesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!bkwRunTemplates.IsBusy)
            {
                activeSet.GenType = GenEnum.Individual;
                bkwRunTemplates.RunWorkerAsync();
            }

        }

        private void dgvLogging_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //if ()
            int rit = e.RowIndex;
        }

        private void tviTables_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name == "Table")
            {
                activeNode = e.Node;
                activeSet.ActiveTable = (TableSet)activeNode.Tag;
            }

            if (e.Node.Name == "Column")
            {
                //ColumnSet ao = (ColumnSet)e.Node.Tag;
                //ao.Name = e.Label;
            }
        }

        private void tsbExcelMerg_Click(object sender, EventArgs e)
        {
            if (!bkwMergeExcel.IsBusy)
            {
                tssProgressLable.Text = "Excel Merg Started: ";
                bkwMergeExcel.RunWorkerAsync();
            }

        }

        private void bkwMergeExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            activeSet.MergExcelHelp(bkwMergeExcel);
        }

        private void bkwMergeExcel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tsProgress1.Value = e.ProgressPercentage;
            rtbOutput.Text = e.UserState.ToString() + rtbOutput.Text;
        }

        private void bkwMergeExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tsProgress1.Value = 0;
            tssProgressLable.Text = "Excel Merg Complete! ";
            rtbOutput.Text = "";
        }

        private void tmrSave_Tick(object sender, EventArgs e)
        {
            if (AutoSave && !String.IsNullOrEmpty(activeProjectLocation))
            {
                if (!bkwSaveBackUp.IsBusy)
                    bkwSaveBackUp.RunWorkerAsync();
            }
        }

        private void tslAutoSave_Click(object sender, EventArgs e)
        {
            if (AutoSave)
            {
                AutoSave = false;
                tslAutoSave.Text = "Auto Save OFF";
            }
            else
            {
                AutoSave = true;
                tslAutoSave.Text = "Auto Save ON";
            }
        }

        private void bkwSaveBackUp_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bkwSaveBackUp.ReportProgress(86, "Saving ...");
                Stream fs = File.Open(activeProjectLocation + ".bck", FileMode.Create, FileAccess.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(typeof(Gen));
                serializer.Serialize(fs, activeSet);
                fs.Close();
                bkwSaveBackUp.ReportProgress(100, "Complete! ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't Back Up File! " + ex.Message);
            }
        }

        private void bkwSaveBackUp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tsProgress1.Value = e.ProgressPercentage;
            tssProgressLable.Text = e.UserState.ToString();
        }

        private void bkwSaveBackUp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tsProgress1.Value = 0;
            tssProgressLable.Text = "File Backed Up! ";
        }

        private void btnHelpFile_Click(object sender, EventArgs e)
        {
            ofdSettings.DefaultExt = "chm";
            ofdSettings.Filter = "Help files (*.chm)|*.chm";
            ofdSettings.RestoreDirectory = true;
            ofdSettings.FileName = "";
            DialogResult dr = ofdSettings.ShowDialog();

            if (dr == DialogResult.OK)
            {
                activeSet.HelpFile = ofdSettings.FileName;
                txtHelpFile.Text = activeSet.HelpFile;

            }
        }

        private void txtHelpFile_TextChanged(object sender, EventArgs e)
        {
            activeSet.HelpFile = txtHelpFile.Text;
        }

        private void bkwLoadXsltTree_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadXsltTree(true);
        }



        private void bkwLoadXsltTree_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tsProgress1.Value = e.ProgressPercentage;
            tssProgressLable.Text = e.UserState.ToString();
        }

        private void bkwLoadXsltTree_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tsProgress1.Value = 0;
            tssProgressLable.Text = "XSLT Files loaded";
        }

        private void LoadXsltTree(Boolean isAsync)
        {
            Double numTables = Convert.ToDouble(activeSet.XsltFiles.Count);
            Double numTablesDone = 0.0;
            tviXslts.Invoke(
                 (MethodInvoker)delegate()
                 {
                     tviXslts.Nodes.Clear();
                 });
            foreach (XsltSet xsl in activeXsltTree)
            {
                int percentdone = Convert.ToInt32((numTablesDone / numTables) * 100);
                if (isAsync)
                {
                    bkwLoadXsltTree.ReportProgress(percentdone, "Loading Xslt directories: " + xsl.Name);
                }
                TreeNode ttn = new TreeNode(xsl.Name);
                ttn.ContextMenuStrip = cmsXslTab;
                ttn.Tag = xsl;
                //ttn.Checked = xsl.Checked;
                ttn.Checked = xsl.Use;

                foreach (XsltSet xsli in xsl.Imports)
                {
                    TreeNode ttni = new TreeNode(xsli.Name);
                    ttni.Tag = xsli;
                    ttni.Checked = false;
                    ttn.Nodes.Add(ttni);
                }

                tviXslts.Invoke(
                   (MethodInvoker)delegate()
                   {
                       tviXslts.Nodes.Add(ttn);
                   });
                numTablesDone += 1;

            }

        }


        private void OnXsltRenamed(object sender, RenamedEventArgs e)
        {
            try
            {
                XsltSet xs = activeSet.XsltFiles.Single(x => x.XsltPath == e.OldFullPath);
                xs.XsltPath = e.FullPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Original file not found. \n" + ex.Message);
                activeSet.XsltFiles.Add(new XsltSet(e.FullPath, activeSet.DirOutput));
            }
            LoadAsyncTreeXslt(false);
        }

        private void OnXsltChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                switch (e.ChangeType)
                {
                    case WatcherChangeTypes.Changed:
                        activeSet.XsltFiles.Single(x => x.XsltPath == e.FullPath).LoadImports();
                        break;
                    case WatcherChangeTypes.Created:
                        XsltSet xsc = new XsltSet(e.FullPath, activeSet.DirOutput);
                        activeSet.XsltFiles.Add(xsc);
                        break;
                    case WatcherChangeTypes.Deleted:
                        activeSet.XsltFiles.Remove(activeSet.XsltFiles.Single(x => x.XsltPath == e.FullPath));
                        break;

                }
            }
            catch (Exception ex)
            {
                switch (e.ChangeType)
                {
                    case WatcherChangeTypes.Changed:
                        MessageBox.Show("Error Changing file: " + ex.Message);
                        break;
                    case WatcherChangeTypes.Created:
                        MessageBox.Show("Error Creating file: " + ex.Message);
                        break;
                    case WatcherChangeTypes.Deleted:
                        MessageBox.Show("Error Deleting file: " + ex.Message);
                        break;

                }
            }


            LoadAsyncTreeXslt(false);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tviXslts_AfterSelect(object sender, TreeViewEventArgs e)
        {

            XsltSet xs = (XsltSet)e.Node.Tag;


            TabPage tp = new TabPage(xs.Name);
            tp.Tag = xs;
            tp.Name = xs.Name;
            XsltEditor xe = new XsltEditor();
            xe.activeSet = activeSet;
            xe.activeXslt = xs;
            xe.setXslt();
            xe.Dock = DockStyle.Fill;
            tp.Controls.Add(xe);
            tp.ContextMenuStrip = cmsXslTab;
            if (!tabsTransView.TabPages.ContainsKey(xs.Name))
            {
                tabsTransView.TabPages.Add(tp);
                //tabsTransView.Selected += tabsTransView_Selected;
                //tabsTransView.MouseClick +=tabsTransView_MouseClick;
                tabsTransView.SelectedTab = tp;
            }
            else
            {
                tabsTransView.SelectedIndex = tabsTransView.TabPages.IndexOfKey(xs.Name);
            }
        }

        //private void tabsTransView_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //    }
        //}

        private void tabsTransView_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Tag != null)
            {
                tabsOutput.TabPages[2].Controls.Clear();
                TemplateUC tc = new TemplateUC();
                tc.activeSet = activeSet;
                tc.xsltData = (XsltSet)e.TabPage.Tag;
                tc.TeplateUCLoad();
                tabsOutput.TabPages[2].Controls.Add(tc);
                tabsOutput.SelectedTab = tabsOutput.TabPages[2];
            }
        }
        void ToolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripButton t = (ToolStripButton)(sender);
            ToolStrip ts = t.Owner;
            TabPage tb = (TabPage)ts.Parent;
            tabsTransView.TabPages.Remove(tb);
        }
        private void flpTemplates_Paint(object sender, PaintEventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabsTransView.TabPages.Remove(tabsTransView.SelectedTab);
        }

        private void tviXslts_AfterCheck(object sender, TreeViewEventArgs e)
        {
            XsltSet xs = (XsltSet)e.Node.Tag;
            xs.Checked = e.Node.Checked;
            xs.Use = e.Node.Checked;

        }

        private void tabsTransView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFilterXslt_Click(object sender, EventArgs e)
        {
            LoadAsyncTreeXslt(true);
        }

        private void checkAllColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (TreeNode tnc in activeNode.Nodes)
            {
                if (tnc.Text == "Columns")
                {
                    foreach (TreeNode tnci in tnc.Nodes)
                    {
                        tnci.Checked = true;
                    }
                }
            }

        }



        private void tabConnectStrings_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeSet.DataBaseType = tabConnectStrings.SelectedTab.Name;
            //TableRebuild();
        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCheckCon_Click(object sender, EventArgs e)
        {
            String sm = "";
            try
            {
                using (OracleConnection conn = new OracleConnection(activeSet.DBConnection))
                {
                    conn.Open();
                    Double numTables = 0.0;

                    using (OracleCommand cmd = new OracleCommand("select count(distinct Table_name) from all_tables where owner = '" + Owner + "' and Num_rows > 0"))
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        numTables = Convert.ToDouble(cmd.ExecuteScalar());

                        sm = "Connection Success! " + numTables + " tables available.";
                    }
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                sm = ex.Message;
            }

            MessageBox.Show(sm);

        }

        private void cbxSQLTrustedConnection_CheckedChanged(object sender, EventArgs e)
        {
            activeSet.SQLConnection.IsTrusted = cbxSQLTrustedConnection.Checked;
        }

        private void btnSearchNetwork_Click(object sender, EventArgs e)
        {
            DataTable dataTable = SmoApplication.EnumAvailableSqlServers(true);
            cbxSQLServer.ValueMember = "Name";
            cbxSQLServer.DataSource = dataTable;
        }

        private void cbxSQLServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeSet.SQLConnection.ActiveServer = cbxSQLServer.Text;
            if (activeSet.SQLConnection.ActiveServer != null)
            {
                cbxSQLDatabase.DataSource = activeSet.SQLConnection.GetDatabaseNames();
                cbxSQLDatabase.SelectedValue = activeSet.SQLConnection.ActiveDatabase;
            }
        }

        private void cbxSQLDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {

            activeSet.SQLConnection.ActiveDatabase = cbxSQLDatabase.Text;
        }

        private void TableRebuild()
        {
            DialogResult dr = MessageBox.Show("Are you sure you would like rebuild all existing tables and Columns?", "Confirm your desire to rebuild.", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK && !bkwGetTables.IsBusy)
            {
                tssProgressLable.Text = "Loading Tables:";
                bkwGetTables.RunWorkerAsync();
            }
        }

        private void cbxSQLServer_TextChanged(object sender, EventArgs e)
        {
            activeSet.SQLConnection.ActiveServer = cbxSQLServer.Text;
            if (activeSet.SQLConnection.ActiveServer != null)
            {
                string dbname = activeSet.SQLConnection.ActiveDatabase;
                cbxSQLDatabase.DataSource = activeSet.SQLConnection.GetDatabaseNames();
                cbxSQLDatabase.Text = dbname;
            }
        }

        private void cbxSQLDatabase_SelectedValueChanged(object sender, EventArgs e)
        {
            activeSet.SQLConnection.ActiveDatabase = cbxSQLDatabase.Text;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            bool cancel = false;
            if (activeProjectLocation != null)
            {
                DialogResult drs = MessageBox.Show("Would you like to save your current project first?", "Save current?", MessageBoxButtons.YesNoCancel);
                if (drs == DialogResult.Yes)
                {
                    SaveToFile();
                }

                if (drs == DialogResult.Cancel)
                {
                    cancel = true;
                }
            }
            if (!cancel)
            {
                activeSet = new Gen();
                SetFormValues();
                LoadAsyncTree();
            }

        }



    }



}
