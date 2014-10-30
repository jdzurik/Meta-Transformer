using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Structure;
using System.Xml.Xsl;
using System.Reflection;
using System.IO;

namespace GenForm
{
  public partial class FilterUC : UserControl
  {
    public TableSet ActiveTable;
    public FilterSet ActiveFilter;
    public TreeNode ActiveTreenode;
    public bool IsNew = true;
    public FilterUC()
    {
      InitializeComponent();
    }


    private void txtSql_TextChanged(object sender, EventArgs e)
    {
      ActiveFilter.FilterQuery = txtSql.Text;
    }

    public void SetForm()
    {
      txtTableName.Text = ActiveTable.TableName;
      bisColumns.DataSource = ActiveTable.Columns;
      bisParms.DataSource = ActiveFilter.Parms;
      bisWhere.DataSource = ActiveFilter.Where;
      txtAdvInnerQuery.Text = ActiveFilter.FilterAdvanced;
      cbxIsAdvanced.Checked = ActiveFilter.IsAdvanced;
      extPropMangerUC1.ExtProp = ActiveFilter.ExtProp;
      extPropMangerUC1.DisplayOnGroup = "Filter";
      extPropMangerUC1.SetExtProps();

      if (ActiveFilter.IsAdvanced) 
        tabCtrlBasicAdvanced.SelectedTab = tabAdvanced;

      //extPropMangerUC1.extPropDel += UpdateExtProp;
      txtFilterName.Text = ActiveFilter.Name;
      lblFilter.Text = ActiveFilter.Name;
    }

    

    private void txtFilterName_TextChanged(object sender, EventArgs e)
    {

      ActiveFilter.Name = Gen.NameFormat(txtFilterName.Text);
      UpdateSQL();
    }

    private void UpdateSQL()
    {
      if (ActiveTable != null && ActiveFilter != null)
      {
        string str = ActiveTable.ToXml();
        XsltArgumentList xal = new XsltArgumentList();
        if (!ActiveFilter.IsAdvanced)
        {
          xal.AddParam("FilterGuid", "", ActiveFilter.ItemGuid.ToString());
          txtSql.Text = ActiveTable.TranslateTo(
              Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
              + "\\Resources\\FilterBaseProcSQL.xslt",
              xal);
          ActiveFilter.FilterQuery = txtSql.Text;
          ActiveFilter.FilterQueryInner = ActiveTable.TranslateTo(
              Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
              + "\\Resources\\FilterBaseProcSQLInner1.xslt", xal);
        }
        else
        {
          xal.AddParam("FilterGuid", "", ActiveFilter.ItemGuid.ToString());
          ActiveFilter.FilterQueryInner = txtAdvInnerQuery.Text.Trim();
          txtSql.Text = ActiveTable.TranslateTo(
              Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
              + "\\Resources\\FilterBaseProcSQL.xslt",
              xal);
          ActiveFilter.FilterQuery = txtSql.Text;
        }
      }
    }

    private void btnConvertAdv_Click(object sender, EventArgs e)
    {
      ConvertToAdvanced();
    }

    private void dgvParms_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
      if (e.Control is DataGridViewTextBoxEditingControl)
      {
        DataGridViewTextBoxEditingControl te = (DataGridViewTextBoxEditingControl)e.Control;
        te.AutoCompleteMode = AutoCompleteMode.Suggest;
        te.AutoCompleteSource = AutoCompleteSource.CustomSource;
        if (dgvParms.CurrentCell.ColumnIndex == 1)
        {
          foreach (ColumnSet cs in ActiveTable.Columns)
          {
            te.AutoCompleteCustomSource.Add(cs.ColumnName);
          }
        }
      }
    }


    private void dgvParms_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex == 0 && ActiveTable != null)
      {
        foreach (ColumnSet cs in ActiveTable.Columns)
        {
          if (dgvParms.CurrentCell != null)
          {
            if (dgvParms.CurrentRow.Cells[0].Value != null && dgvParms.CurrentRow.Cells[0].Value.ToString() == cs.ColumnName)
            {
              dgvParms.CurrentRow.Cells[1].Value = cs.ColumnDataType.Name;
              dgvParms.CurrentRow.Cells[2].Value = cs.ColumnDataType.MaximumLength.ToString();

              ActiveFilter.Where.Add(new FilterWhereSet(cs.ColumnName, "@" + cs.ColumnName));
              SetListFields();
              bisWhere.ResetBindings(false);

              dgvWhere.DataSource = null;
              dgvWhere.DataSource = bisWhere;


            }
          }

        }
      }

      UpdateSQL();

    }

    private void dgvWhere_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      UpdateSQL();
    }

    private void tabCtrlBasicAdvanced_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (tabCtrlBasicAdvanced.SelectedTab == tabBasic && ActiveFilter.IsAdvanced)
        tabCtrlBasicAdvanced.SelectedTab = tabAdvanced;
      if (tabCtrlBasicAdvanced.SelectedTab == tabAdvanced && !ActiveFilter.IsAdvanced)
        ConvertToAdvanced();

    }

    private void ConvertToAdvanced()
    {
      DialogResult dr = MessageBox.Show("Are You sure you would like to convert this to advanced mode? \nYou will loose the ability to edit this in basic mode! \n\nThanks... John Dzurik master of the universe. ", "Confirm your desire to convert.", MessageBoxButtons.OKCancel);
      if (dr == DialogResult.OK)
      {
        SetAdvancedFields();
        ActiveFilter.IsAdvanced = true;
        tabCtrlBasicAdvanced.SelectedTab = tabAdvanced;
      }
      else
      {
        tabCtrlBasicAdvanced.SelectedTab = tabBasic;
        SetBasicFields();
      }

    }

    private void SetAdvancedFields()
    {
      txtSql.Text = ActiveFilter.FilterQuery;
      SetListFields();
    }

    private void SetBasicFields()
    {
      txtFilterName.Text = ActiveFilter.Name;
      SetListFields();
    }

    private void SetListFields()
    {
      bisParms.DataSource = ActiveFilter.Parms;
      bisWhere.DataSource = ActiveFilter.Where;
    }

   

    private void txtFilterName_Leave(object sender, EventArgs e)
    {
      txtFilterName.Text = Gen.NameFormat(txtFilterName.Text);
    }

    


    private void btnDeleteFilter_Click(object sender, EventArgs e)
    {
      DialogResult dr = MessageBox.Show("Are You sure you would like to delete this Filter?", "Confirm your desire to delete.", MessageBoxButtons.OKCancel);
      if (dr == DialogResult.OK)
      {
        ActiveTable.Filters.Remove(ActiveFilter);
        ActiveTreenode.Remove();
        splitTblContainer.Visible = false;
        splitTblContainer.Enabled = false;
      }
    }

    private void EnableDisableBasicTab(bool state)
    {
      if (this.tabBasic.HasChildren)
      {
        foreach (Control c in tabBasic.Controls)
        {
          c.Enabled = state;
        }
      }
    }

    private void btnPreviewData_Click(object sender, EventArgs e)
    {
      //dgvPreview.DataSource = null;
      //SetParams pform = new SetParams(this.ActiveFilter);
      //pform.ShowDialog();

      //System.Data.SqlClient.SqlConnection con = null;

      //try
      //{
      //  Server server = new Server(((Form1)ParentForm).activeSet.ServerName);
      //  server.ConnectionContext.DatabaseName = ((Form1)ParentForm).activeSet.DatabaseName;

      //  if (!((Form1)ParentForm).server.ConnectionContext.ConnectionString.Contains("password"))
      //  {
      //    //server.ConnectionContext.ConnectionString = ((Form1)ParentForm).server.ConnectionContext.ConnectionString;
      //    string cons = ((Form1)ParentForm).server.ConnectionContext.ConnectionString + ";database='" + ((Form1)ParentForm).activeSet.DatabaseName + "'";
      //    server.ConnectionContext.ConnectionString = cons;
      //  }
      //  else
      //  {
      //    string cons = ((Form1)ParentForm).server.ConnectionContext.ConnectionString + ";database='" + ((Form1)ParentForm).activeSet.DatabaseName + "'";
      //    server.ConnectionContext.ConnectionString = cons;
      //  }

      //  con = new System.Data.SqlClient.SqlConnection(server.ConnectionContext.ConnectionString);

      //  //USE THIS TO EXUCUTE AGAINST THE DB!!
      //  //server.Databases[""].ExecuteNonQuery();
      //  con = new System.Data.SqlClient.SqlConnection(server.ConnectionContext.ConnectionString);
      //  int s = ActiveFilter.FilterQuery.IndexOf("--Start");
      //  int en = ActiveFilter.FilterQuery.IndexOf("--Finish");
      //  int tot = en - s;
      //  string sql = ActiveFilter.FilterQuery.Substring(s, tot);

      //  SqlCommand com = new SqlCommand(sql, con);
      //  foreach (FilterParameterSet par in ActiveFilter.Parms)
      //  {
      //    DbType t;
      //    SqlParameter p = new SqlParameter();
      //    ParmValues pv = pform.SetVals.SetParameterValues.Find(delegate(ParmValues pv1) { return pv1.ParameterName == par.ParameterName; });

      //    switch (par.ParameterDataType.ToLower())
      //    {
      //      case "uniqueidentifier":
      //        t = DbType.Guid;
      //        p.Value = new Guid(pv.ParameterValue);
      //        break;
      //      case "bit":
      //        t = DbType.Boolean;
      //        p.Value = bool.Parse(pv.ParameterValue);
      //        break;
      //      case "varchar":
      //        t = DbType.String;
      //        p.Value = pv.ParameterValue;
      //        break;
      //      case "nvarchar":
      //        t = DbType.String;
      //        p.Value = pv.ParameterValue;
      //        break;
      //      case "nchar":
      //        t = DbType.String;
      //        p.Value = pv.ParameterValue;
      //        break;
      //      case "char":
      //        t = DbType.String;
      //        p.Value = pv.ParameterValue;
      //        break;
      //      case "text":
      //        t = DbType.String;
      //        p.Value = pv.ParameterValue;
      //        break;
      //      case "ntext":
      //        t = DbType.String;
      //        p.Value = pv.ParameterValue;
      //        break;
      //      case "int":
      //        t = DbType.Int32;
      //        p.Value = int.Parse(pv.ParameterValue);
      //        break;
      //      case "tinyint":
      //        t = DbType.Int32;
      //        p.Value = int.Parse(pv.ParameterValue);
      //        break;
      //      case "real":
      //        t = DbType.Decimal;
      //        p.Value = float.Parse(pv.ParameterValue);
      //        break;
      //      case "float":
      //        t = DbType.Decimal;
      //        p.Value = float.Parse(pv.ParameterValue);
      //        break;
      //      case "decimal":
      //        t = DbType.Decimal;
      //        p.Value = float.Parse(pv.ParameterValue);
      //        break;
      //      case "datetime":
      //        t = DbType.DateTime;
      //        p.Value = DateTime.Parse(pv.ParameterValue);
      //        break;
      //      case "date":
      //        t = DbType.Date;
      //        p.Value = DateTime.Parse(pv.ParameterValue);
      //        break;
      //      case "smalldate":
      //        t = DbType.Date;
      //        p.Value = DateTime.Parse(pv.ParameterValue);
      //        break;
      //      case "xml":
      //        t = DbType.Xml;
      //        p.Value = pv.ParameterValue;
      //        break;
      //      default:
      //        t = (DbType)Enum.Parse(typeof(DbType), par.ParameterDataType);
      //        break;
      //    }

      //    p.DbType = t;
      //    p.Direction = ParameterDirection.Input;
      //    p.Size = par.Size == String.Empty ? 0 : int.Parse(par.Size);
      //    p.ParameterName = par.ParameterName;
      //    com.Parameters.Add(p);
      //  }

      //  //DataSet ds = server.Databases[((Form1)ParentForm).activeSet.DatabaseName].ExecuteWithResults(com.ToString());
      //  DataSet ds = new DataSet();
      //  SqlDataAdapter ad = new SqlDataAdapter(com);
      //  ad.Fill(ds);
      //  dgvPreview.DataMember = "Table";
      //  dgvPreview.DataSource = ds;

      //  com.Dispose();
      //  pform.Dispose();

      //  #region .: Old Datareader code :.
      //  //using (SqlDataReader rd = com.ExecuteReader())
      //  //{
      //  //    while (rd.Read())
      //  //    {
      //  //        System.Diagnostics.Debug.WriteLine(rd[0].ToString());
      //  //    }

      //  //    dgvPreview.DataSource = com.ExecuteReader();

      //  //    rd.Close();
      //  //    con.Close();
      //  //    rd.Dispose();
      //  //    com.Dispose();
      //  //}
      //  #endregion
      //}
      //catch (Exception ex)
      //{
      //  MessageBox.Show(ex.Message);
      //  if (con.State == ConnectionState.Connecting || con.State == ConnectionState.Open || con.State == ConnectionState.Fetching)
      //  {
      //    con.Close();
      //  }
      //}
    }

    private void btnPreviewTableData_Click(object sender, EventArgs e)
    {
      //System.Data.SqlClient.SqlConnection con = null;

      //try
      //{
      //  Server server = new Server(((Form1)ParentForm).activeSet.ServerName);
      //  server.ConnectionContext.DatabaseName = ((Form1)ParentForm).activeSet.DatabaseName;
      //  if (!((Form1)ParentForm).server.ConnectionContext.ConnectionString.Contains("password"))
      //  {
      //    //server.ConnectionContext.ConnectionString = ((Form1)ParentForm).server.ConnectionContext.ConnectionString;
      //    string cons = ((Form1)ParentForm).server.ConnectionContext.ConnectionString + ";database='" + ((Form1)ParentForm).activeSet.DatabaseName + "'";
      //    server.ConnectionContext.ConnectionString = cons;
      //  }
      //  else
      //  {
      //    string cons = ((Form1)ParentForm).server.ConnectionContext.ConnectionString + ";database='" + ((Form1)ParentForm).activeSet.DatabaseName + "'";
      //    server.ConnectionContext.ConnectionString = cons;
      //  }

      //  con = new System.Data.SqlClient.SqlConnection(server.ConnectionContext.ConnectionString);
      //  string sql = "SELECT TOP " + txtMaxRows.Text + " * FROM " + ActiveTable.TableName;

      //  SqlCommand com = new SqlCommand(sql, con);

      //  DataSet ds = new DataSet();
      //  SqlDataAdapter ad = new SqlDataAdapter(com);
      //  ad.Fill(ds);
      //  dgvPreview.DataMember = "Table";
      //  dgvPreview.DataSource = ds;

      //  com.Dispose();
      //  ds.Dispose();
      //}
      //catch (Exception ex)
      //{
      //  MessageBox.Show(ex.Message);
      //  if (con.State == ConnectionState.Connecting || con.State == ConnectionState.Open || con.State == ConnectionState.Fetching)
      //  {
      //    con.Close();
      //  }
      //}
    }

    private void UpdateExtProp(String Name, String Value, String OriginalName)
    {
      //try
      //{
      //  TableSet t = ((Form1)ParentForm).activeSet
      //    .Databases[((Form1)ParentForm).activeSet.DatabaseName]
      //    .Tables[ActiveTable.TableName];
      //  ExtPropAutoCompleteList epc = t.ExtendedProperties;
      //  if (OriginalName == "" && Name != "")
      //  {
      //    ExtendedProperty ep = new ExtendedProperty(t, Name, Value);
      //    ep.Create();
      //  }
      //  if (OriginalName != Name && !epc.Contains(Name) && Name != "")
      //  {
      //    epc[OriginalName].Drop();
      //    ExtendedProperty ep = new ExtendedProperty(t, Name, Value);
      //    ep.Create();
      //  }
      //  if (Name != "" && epc.Contains(Name) && epc[Name].Value.ToString() != Value)
      //  {
      //    epc[Name].Value = Value;
      //    epc[Name].Alter();
      //  }
      //  if (Name == "" && Value == "" && epc.Contains(OriginalName))
      //  {
      //    epc[OriginalName].Drop();
      //  }
      //}
      //catch (Exception ex)
      //{
      //  MessageBox.Show("Error occured modifying extended property." + ex.Message);
      //}
    }

    private void txtAdvInnerQuery_TextChanged(object sender, EventArgs e)
    {
      UpdateSQL();
    }

    private void tableInfoAndProps_Paint(object sender, PaintEventArgs e)
    {

    }

    private void lblNewFilter_Click(object sender, EventArgs e)
    {

    }

    private void extPropMangerUC1_Load(object sender, EventArgs e)
    {

    }

    private void label5_Click(object sender, EventArgs e)
    {

    }

    private void dgvWhere_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
      if (e.Control is DataGridViewTextBoxEditingControl)
      {
        DataGridViewTextBoxEditingControl te = (DataGridViewTextBoxEditingControl)e.Control;
        te.AutoCompleteMode = AutoCompleteMode.Suggest;
        te.AutoCompleteSource = AutoCompleteSource.CustomSource;
        if (dgvParms.CurrentCell.ColumnIndex == 0)
        {
          foreach (ColumnSet cs in ActiveTable.Columns)
          {
            te.AutoCompleteCustomSource.Add(cs.ColumnName);
          }
        }
      }
    }

    private void txtAdvInnerQuery_TextChanged_1(object sender, EventArgs e)
    {
      ActiveFilter.FilterAdvanced = txtAdvInnerQuery.Text;
      UpdateSQL();
    }

    private void cbxIsAdvanced_CheckedChanged(object sender, EventArgs e)
    {
      ActiveFilter.IsAdvanced = cbxIsAdvanced.Checked;
    }



  }
}
