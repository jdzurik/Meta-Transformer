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

  public partial class AddForeignKeyUC : UserControl
  {
    public TableSet ActiveTable;
    public TreeNode ActiveTreenode;

    public AddForeignKeyUC()
    {
      InitializeComponent();
    }
    private void AddNewFK()
    {
      if (txtNewFilterName.Text != "")
      {
        ForeignKeySet newKey = new ForeignKeySet();
        newKey.Name = Gen.NameFormat(txtNewFilterName.Text);
        newKey.ForeignKeyName = Gen.NameFormat(txtNewFilterName.Text);

        if (ActiveTable.ForeignKeys == null)
          ActiveTable.ForeignKeys = new List<ForeignKeySet>();
        ActiveTable.ForeignKeys.Add(newKey);

        TreeNode ctn = new TreeNode(newKey.Name);
        ctn.Checked = false;
        ctn.Name = "Foreignkey";
        ctn.Tag = newKey;
        ActiveTreenode.Nodes.Add(ctn);
        ActiveTreenode.Expand();
        ActiveTreenode.TreeView.SelectedNode = ctn;
        //((Form1)ParentForm).LoadAsyncTree();

      }
      else
      {
        MessageBox.Show("Please Enter a new Foreign Key name.");
      }
    }
    private void btnAddNewForeignKey_Click(object sender, EventArgs e)
    {
      AddNewFK();
    }

    private void txtNewFilterName_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == 13)
      {
        AddNewFK();
      }
    }
  }
}
