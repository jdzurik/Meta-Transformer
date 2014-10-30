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
  public partial class AddFilterUC : UserControl
  {
    public TableSet ActiveTable;
    public TreeNode ActiveTreenode;
    public AddFilterUC()
    {
      InitializeComponent();
    }

    private void lblNewFilter_Click(object sender, EventArgs e)
    {

    }

    private void AddNewFilter()
    {
      if (txtNewFilterName.Text != "")
      {
        FilterSet newFilter = new FilterSet();
        newFilter.Name = Gen.NameFormat(txtNewFilterName.Text);
        newFilter.FilterName = Gen.NameFormat(txtNewFilterName.Text);
        newFilter.Parms = new List<FilterParameterSet>();
        newFilter.Where = new List<FilterWhereSet>();
        
        if (ActiveTable.Filters == null)
          ActiveTable.Filters = new List<FilterSet>();
        ActiveTable.Filters.Add(newFilter);

        TreeNode ctn = new TreeNode(newFilter.Name);
        ctn.Checked = false;
        ctn.Name = "Filter";
        ctn.Tag = newFilter;
        ActiveTreenode.Nodes.Add(ctn);
        ActiveTreenode.Expand();
        ActiveTreenode.TreeView.SelectedNode = ctn;
        //((Form1)ParentForm).LoadAsyncTree();
        
      }
      else
      {
        MessageBox.Show("Please Enter a new filter name.");
      }
    }

    private void btnAddNewFilter_Click(object sender, EventArgs e)
    {
      AddNewFilter();
    }

    private void txtNewFilterName_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == 13)
      {
        AddNewFilter();
      }
    }

    private void tplFilterHeader_Paint(object sender, PaintEventArgs e)
    {

    }

    private void txtNewFilterName_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
