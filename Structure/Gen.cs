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
using System.ComponentModel;
using System.Diagnostics;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;


namespace Structure
{
    [Serializable]
    public class Gen : BaseSet
    {
        public String ProjectName { get; set; }
        public String DirHelpSource { get; set; }
        public String DirOutput { get; set; }
        public String DirTransforms { get; set; }
        public String ExcelFile { get; set; }
        public String HelpFile { get; set; }
        public String HTMLParser { get; set; }
        public String DataBaseType { get; set; }
        public OraConnect OracleConnection { get; set; }
        public SQLConnect SQLConnection { get; set; }

        private String _DBUser;
        public String DBUser
        {
            get { return _DBUser; }
            set
            {
                _DBUser = value;
                BuildConnection();
            }
        }
        private byte[] _DBPass;
        public byte[] DBPass
        {
            get
            {
                return _DBPass;
            }
            set
            {
                _DBPass = value;
                UnencryptDBPass();
                BuildConnection();
            }
        }
        [XmlIgnore]
        public String DecryptDBPass;

        private String _DBConnectString;

        public String DBConnectString
        {
            get { return _DBConnectString; }
            set
            {
                _DBConnectString = value;
                BuildConnection();
            }
        }
        [XmlIgnore]
        public String DBConnection { get; set; }

        public List<TableSet> Tables { get; set; }
        public List<XsltSet> XsltFiles { get; set; }
        public List<String> HelpFiles { get; set; }
        public ExtPropAutoCompleteList AvailableExtProps { get; set; }
        [XmlIgnore]
        public GenEnum GenType { get; set; }
        [XmlIgnore]
        public TableSet ActiveTable { get; set; }
        [XmlIgnore]
        public LoggingList Logging { get; set; }
        [XmlIgnore]
        private BackgroundWorker bw { get; set; }
        [XmlIgnore]
        public String ActiveLocation { get; set; }


        public TableSet GeTabletByGUID(Guid pGuid)
        {
            TableSet ts = new TableSet();
            ts = Tables.Single(x => x.ItemGuid == pGuid);
            return ts;
        }

        public TableSet GetTableByTableName(String TableName)
        {
            TableSet ts = new TableSet();
            try
            {
                ts = Tables.Single(x => x.TableName.ToUpper() == TableName.ToUpper());
            }
            catch
            {
            }
            return ts;
        }

        public TableSet GetTableByName(String Name)
        {
            TableSet ts = new TableSet();
            try
            {
                ts = Tables.Single(x => x.Name == Name);
            }
            catch
            {
            }

            return ts;
        }

        public void Run(BackgroundWorker pbw)
        {
            bw = pbw;
            Logging.Clear();
            LoggingItem GenLog = AddInfoDate(DateTime.Now, " Starting Transformation.", "All");
            Double OnCount = 0.0;
            switch (GenType)
            {

                case GenEnum.All:
                    //string tempObjName = ActiveTable.Name;
                    LoggingItem ItemLog = AddInfoProgress(Convert.ToDouble((from xsl
                                                    in XsltFiles
                                                                            where xsl.Use
                                                                            select xsl).Count() * (from tb
                                                                            in Tables
                                                                                                   where tb.Checked
                                                                                                   select tb).Count()), " Starting All Progress.", "");


                    foreach (XsltSet x in (from xsl
                                              in XsltFiles
                                           where xsl.Use
                                           select xsl))
                    {
                        int xsltRan = 0;
                        foreach (TableSet tbl in (from tbl
                                                    in Tables
                                                  where tbl.Checked
                                                  select tbl))
                        {

                            string filename = CreateFileName(x, tbl);
                            if (!filename.Contains("NoValueFound"))
                            {
                                if (xsltRan == 0)
                                {
                                    OnCount++;

                                    if (x.CreateOneFile == true)
                                    {
                                        TransformFile(x.OutputDirectory + @"\" + filename,
                                            x.XsltPath,
                                            tbl.Name
                                            );
                                        //, xr);
                                        xsltRan = 1;
                                        UpdateInfoProgress(ItemLog, OnCount);
                                        AddInfoMessage("Single File Created", filename);
                                    }
                                    else
                                    {
                                        TransformFile(x.OutputDirectory + @"\" + filename,
                                            x.XsltPath,
                                            tbl.Name
                                            );
                                        //, xr);
                                        UpdateInfoProgress(ItemLog, OnCount);
                                        // AddInfoMessage("File Created", filename);
                                    }
                                }
                            }
                            else
                            {
                                OnCount++;
                                AddErrorMessage("Error Creating: File could not find folder or file name.", filename);
                            }

                            string res = String.Empty;
                            //if (x.RunOnDatabase)
                            //{
                            //  UpdateErrorMessage("Run On DB: " + tbl.Name + "");
                            //  RunOnDB(ParentForm.activeSet.TranslateTo(x.XsltPath), serv, ParentForm.activeSet.ObjectName);
                            //}

                        }

                    }
                    break;
                case GenEnum.Individual:

                    LoggingItem ItemILog = AddInfoProgress(Convert.ToDouble((from xsl
                                                    in XsltFiles
                                                                             where xsl.Use
                                                                             select xsl).Count()), " Starting Single Progress.", "");
                    foreach (XsltSet x in (from xsl
                                              in XsltFiles
                                           where xsl.Use
                                           select xsl))
                    {
                        string filename = CreateFileName(x, ActiveTable);
                        OnCount++;
                        TransformFile(x.OutputDirectory + @"\" + filename, x.XsltPath, ActiveTable.Name);
                        //, xr);
                        //x.TransformFile(this, x.OutputDirectory + @"\" + filename,  ActiveTable.Name);
                        UpdateInfoProgress(ItemILog, OnCount);
                        //string res = String.Empty;
                        //if (x.RunOnDatabase)
                        //{
                        //  RunOnDB(ParentForm.activeSet.TranslateTo(x.XsltPath), serv, ParentForm.activeSet.ObjectName);
                        //}

                    }
                    break;

            }

            UpdateInfoDate(GenLog, DateTime.Now);

            if (CheckSingleRun())
                AddInfoMessage("Build was successfull!");
        }

        public Gen()
        {
            ProjectName = "";
            DirHelpSource = "";
            DirOutput = "";
            DirTransforms = "";
            ExcelFile = "";
            DBConnection = "";
            XsltFiles = new List<XsltSet>();
            AvailableExtProps = new ExtPropAutoCompleteList();
            HelpFiles = new List<String>();
            Tables = new List<TableSet>();
            Logging = new LoggingList();
            OracleConnection = new OraConnect();
            SQLConnection = new SQLConnect();

        }

        public static string NameFormat(string Name)
        {
            return Name.Replace("/", "_")
                .Replace("*", "")
                .Replace("?", "")
                .Replace("\"", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "")
                .Replace("@", "")
                .Replace("#", "")
                .Replace(" ", "_");
        }

        private string CreateFileName(XsltSet x, TableSet tbl)
        {
            String FileName = "";
            String FolderName = "";
            if (x.NameAsFolder)
            {
                if (String.IsNullOrEmpty(x.FolderAsPropExtName))
                    FolderName = tbl.Name + @"\";
                else
                    FolderName = tbl.GetExtPropValueByName(x.FolderAsPropExtName).Trim() + @"\";
                DirectoryInfo di = new DirectoryInfo(x.OutputDirectory + @"\" + FolderName);
                if (!di.Exists)
                    di.Create();
            }

            if (x.CreateOneFile)
            {
                if (String.IsNullOrEmpty(x.OneFileName))
                    FileName = ProjectName + x.OutputFileExtention;
                else
                    FileName = x.OneFileName + x.OutputFileExtention;
            }
            else
            {
                if (String.IsNullOrEmpty(x.FileAsPropExtName))
                    FileName = tbl.Name + x.OutputFileExtention;
                else
                    FileName = tbl.GetExtPropValueByName(x.FileAsPropExtName).Trim() + x.OutputFileExtention;

            }
            return (FolderName + FileName).ToString();
        }

        private static void CreateDirectory(string sdir, string ndir)
        {
            DirectoryInfo di = new DirectoryInfo(sdir + ndir);
            if (!di.Exists)
            {
                di.Create();
                setAttributesNormal(di);
            }
        }


        private static void DeleteDirectory(string sdir)
        {
            DirectoryInfo di = new DirectoryInfo(sdir);
            if (di.Exists)
            {
                setAttributesNormal(di);
                di.Delete(true);
            }
        }

        static void setAttributesNormal(DirectoryInfo dir)
        {
            // Remove flags from the current directory
            dir.Attributes = FileAttributes.Normal;

            // Remove flags from all files in the current directory
            foreach (FileInfo file in dir.GetFiles())
            {
                file.Attributes = FileAttributes.Normal;
            }

            // Do the same for all subdirectories
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                setAttributesNormal(subDir);
            }
        }



        public void TransformFile(string OutputFilePath, string XsltPath, string objectName)//, XmlReader xr)
        {
            try
            {
                FileInfo f = new FileInfo(OutputFilePath);
                if (!f.IsReadOnly || !f.Exists)
                {
                    using (XmlReader xr = XmlReader.Create(new StringReader(this.ToXml())))
                    {
                        using (FileStream fs = new FileStream(OutputFilePath, FileMode.Create))
                        {
                            using (TextWriter tw = new StreamWriter(fs, new UTF8Encoding()))
                            {
                                XslCompiledTransform xslt = new XslCompiledTransform(true);
                                xslt.Load(XsltPath, XsltSettings.TrustedXslt, new XmlUrlResolver());
                                XsltArgumentList xslal = new XsltArgumentList();
                                xslal.AddParam("ObjectName", "", objectName);
                                xslal.AddParam("NameSpace", "", ProjectName);
                                xslt.Transform(xr, xslal, tw);
                                //xslt.Transform(ActiveLocation, xslal, tw);
                                tw.Close();
                                tw.Dispose();
                                fs.Close();
                                fs.Dispose();

                            }
                        }
                    }
                    AddInfoMessage("Created: " + OutputFilePath, "Creates");
                }
                else
                {
                    AddErrorMessage("This file: " + OutputFilePath + " \nis probably read only or, you don't have the correct permissions.", objectName);
                }
                f = null;
            }
            catch (Exception ex)
            {
                AddErrorMessage("This file: " + OutputFilePath + " errored out on this Template: " + XsltPath + " | Error Message: " + ex.Message, objectName);
                AddInfoMessage("This file: " + OutputFilePath + " \nis probably read only or, you don't have the correct permissions.");
            }
        }


        //public  void RunOnDB(string pSql, Server pServ, String ObjName)
        //{
        //  try
        //  {
        //    pServ.ConnectionContext.ExecuteNonQuery(pSql);
        //    Utils.ErrorLogger.AddError("SQL Complete: " + ObjName, CodeGeneratorForm.Utils.ErrorType.Information, ObjName);
        //  }
        //  catch (Exception ex)
        //  {
        //    Utils.ErrorLogger.AddError(ex.InnerException.Message + "  Table Name: " + ObjName, CodeGeneratorForm.Utils.ErrorType.Error, ObjName);
        //  }
        //}


        public void AddInfoMessage(String Message)
        {
            LoggingItem li = new LoggingItem(Message);
            Logging.Add(li);
            bw.ReportProgress(0, li);
        }

        public void AddInfoMessage(String Message, String Name)
        {
            LoggingItem li = new LoggingItem(Message, Name);
            Logging.Add(li);
            bw.ReportProgress(0, li);
        }

        public LoggingItem AddInfoProgress(Double TotalItems, String Message, String Name)
        {
            LoggingItem li = new LoggingItem(TotalItems, Message, Name, LoggingEnum.Progress);
            Logging.Add(li);
            bw.ReportProgress(0, li);
            return li;
        }

        public void UpdateInfoProgress(LoggingItem li, Double OnItem)
        {
            li.OnItem = OnItem;
            li.CalcPercent();
            bw.ReportProgress(li.PercentComplete, li);
        }

        public LoggingItem AddInfoDate(DateTime StartDate, String Message, String Name)
        {
            LoggingItem li = new LoggingItem(StartDate, Message, Name, LoggingEnum.Duration);
            Logging.Add(li);
            bw.ReportProgress(0, li);
            return li;
        }

        public void UpdateInfoDate(LoggingItem li, DateTime EndDate)
        {
            li.EndTime = EndDate;
            li.CalcTime();
            bw.ReportProgress(0, li);
        }

        public void AddErrorMessage(String Message, String Name)
        {
            LoggingItem li = new LoggingItem(Message, Name, LoggingEnum.Error);
            Logging.Add(li);
            bw.ReportProgress(0, li);
        }

        private bool CheckSingleRun()
        {
            bool wassuccess = true;

            //Check to see if this has an active table to run.
            if (Tables.Count <= 0 || ActiveTable == null && GenType == GenEnum.Individual)
            {
                AddErrorMessage("You must have an active file in order to do a partial build.", "Build Fail");
                wassuccess = false;
            }

            //Check to see if it's assigned a filepath to output files to.
            if (String.IsNullOrEmpty(DirOutput))
            {
                AddErrorMessage("You must choose an output directory for your files!", "Build Fail");
                wassuccess = false;
            }

            //Check to see if we have templates
            if (XsltFiles != null)
            {
                if (XsltFiles.Count == 0)
                {
                    AddErrorMessage("You must choose a directory to pull XSLT's from.", "Build Fail");
                    wassuccess = false;
                }
            }
            else
            {
                AddErrorMessage("You must choose a directory to pull XSLT's from.", "Build Fail");
                wassuccess = false;
            }

            return wassuccess;
        }
        private void UnencryptDBPass()
        {
            try
            {
                byte[] unencrypted = ProtectedData.Unprotect(
                                        _DBPass,
                                        null,
                                        DataProtectionScope.CurrentUser
                                       );
                DecryptDBPass = Encoding.Default.GetString(unencrypted).Replace("\0", "");
            }
            catch (Exception ex)
            {
                DecryptDBPass = "ErrorDecr|" + ex.Message;
            }
        }
        private void BuildConnection()
        {
            StringBuilder sb = new StringBuilder();
            if (!String.IsNullOrEmpty(_DBUser))
                sb.AppendFormat("User Id={0};", _DBUser);

            if (_DBPass != null)
            {
                sb.AppendFormat("Password={0};", DecryptDBPass);
            }
            if (!String.IsNullOrEmpty(_DBConnectString))
                sb.Append(_DBConnectString);
            DBConnection = sb.ToString();
        }

        public void MergExcelHelp(BackgroundWorker pbw)
        {

            Double OnCount = 0.0;
            /*
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
      
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(ExcelFile, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel.Worksheet tabName = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Excel.Worksheet colName = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
            Excel.Worksheet colName2 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);

      
            xlWorkBook.Close(false, Type.Missing, Type.Missing);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            */


            //var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", ExcelFile);
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\";", ExcelFile);


            using (OleDbConnection objConnection = new OleDbConnection(connectionString))
            {
                objConnection.Open();

                DataTable dtSchema = objConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dtSchema.Rows.Count > 2)
                {
                    var tnAdapter = new OleDbDataAdapter("SELECT * FROM [" + dtSchema.Rows[2]["TABLE_NAME"] + "]", connectionString);
                    var tnds = new DataSet();
                    pbw.ReportProgress(0, "Loading Tables ... \n \n");
                    tnAdapter.Fill(tnds, "TableNames");
                    var tnData = tnds.Tables["TableNames"].AsEnumerable();

                    var cnAdapter = new OleDbDataAdapter("Select * From [" + dtSchema.Rows[0]["TABLE_NAME"] + "] UNION SELECT * from [" + dtSchema.Rows[1]["TABLE_NAME"] + "]", connectionString);
                    var cnds = new DataSet();
                    pbw.ReportProgress(0, "Loading Columns ... \n \n");
                    cnAdapter.Fill(cnds, "ColumnNames");
                    var cnData = cnds.Tables["ColumnNames"].AsEnumerable();

                    Double TotalTab = Convert.ToDouble(Tables.Count());

                    foreach (TableSet tbl in Tables)
                    {
                        //if (tbl.Checked)
                        //{
                        var tnq = tnData.Where(x => x.Field<string>("Table Name") == tbl.TableName).Select(x =>
                                        new MetaInfo
                                        {
                                            AltDescription = x.Field<string>("Table description"),
                                            INI = x.Field<string>("Chronicles MF")
                                        });
                        if (tnq.Count<MetaInfo>() > 0)
                        {
                            tbl.MetaData.AltDescription = tnq.First<MetaInfo>().AltDescription;
                            tbl.MetaData.INI = tnq.First<MetaInfo>().INI;
                        }
                        foreach (ColumnSet col in tbl.Columns)
                        {
                            var cnq = cnData.Where(x => x.Field<string>("Table Name") == tbl.TableName
                              && x.Field<string>("Column Name") == col.ColumnName).Select(x =>
                                         new MetaInfo
                                         {
                                             AltDescription = x.Field<string>("Column Description"),
                                             INI = x.Field<string>("INI"),
                                             Item = x.Field<double?>("Item").ToString()
                                         });
                            if (cnq.Count<MetaInfo>() > 0)
                            {
                                col.MetaData.AltDescription = cnq.First<MetaInfo>().AltDescription;
                                col.MetaData.INI = cnq.First<MetaInfo>().INI;
                                col.MetaData.Item = cnq.First<MetaInfo>().Item;

                            }
                        }
                        OnCount++;
                        pbw.ReportProgress(Convert.ToInt32((OnCount / TotalTab) * 100), "Updated Table: " + tbl.TableName + " \n \n");

                        //}
                    }

                    tnAdapter.Dispose();
                    cnAdapter.Dispose();
                }
                dtSchema.Dispose();
            }
        }

    }
}
