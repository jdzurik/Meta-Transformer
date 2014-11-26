using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Structure;
namespace GenForm
{
  public partial class ColumnUC : UserControl
  {
    public ColumnSet ActiveColumn { get; set; }
    public TableSet ActiveTable { get; set; }
    public TreeNode ActiveTreeNode { get; set; }
    public ColumnUC()
    {
      InitializeComponent();
    }

    public void SetForm()
    {
      txtName.Text = ActiveColumn.Name;
      lblTableName.Text = ActiveColumn.ColumnName;
      if (ActiveColumn.MetaData != null)
      {
        lblMetaDataFullname.Text = ActiveColumn.MetaData.FullName;
        txtDataType.Text = ActiveColumn.ColumnDataType.SqlDataType;
        if (!String.IsNullOrEmpty(ActiveColumn.MetaData.Comment))
          txtDescrption.Text = ActiveColumn.MetaData.Comment;
        else
          txtDescrption.Text = ActiveColumn.MetaData.AltDescription;

        rtbExcelDesc.Text = ActiveColumn.MetaData.AltDescription;        
        rtbDesc.Text = ActiveColumn.MetaData.Description;
      }
      extPropSetUC1.dasExtProp.DataSource = ActiveColumn.ExtProp;
    }


    private void txtDescrption_TextChanged(object sender, EventArgs e)
    {
      
    }

    private void txtName_TextChanged(object sender, EventArgs e)
    {
      ActiveColumn.Name = txtName.Text;
    }

    private void rtbDesc_TextChanged(object sender, EventArgs e)
    {
      ActiveColumn.MetaData.Description = rtbDesc.Text ;
    }

    private void btnAddFK_Click(object sender, EventArgs e)
    {
      if (txtFKname.Text != "")
      {
        ForeignKeySet newKey = new ForeignKeySet();
        newKey.Name = Gen.NameFormat(txtFKname.Text);
        newKey.ForeignKeyName = Gen.NameFormat(txtFKname.Text);
        //newKey.Columns.Add(ActiveColumn.ItemGuid);

        if (ActiveTable.ForeignKeys == null)
          ActiveTable.ForeignKeys = new List<ForeignKeySet>();
        ActiveTable.ForeignKeys.Add(newKey);

        TreeNode ctn = new TreeNode(newKey.Name);
        ctn.Checked = false;
        ctn.Name = "Foreignkey";
        ctn.Tag = newKey;

        TreeNode tn = ActiveTreeNode.Parent.Parent.LastNode;
        tn.Nodes.Add(ctn);
        tn.Expand();
        tn.TreeView.SelectedNode = ctn;

      }
      else
      {
        MessageBox.Show("Please Enter a new Foreign Key name.");
      }
    }


  }
}
