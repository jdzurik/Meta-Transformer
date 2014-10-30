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
  public partial class ExtPropSetUC : UserControl
  {
    public List<ExtPropSet> ExtProp { get; set; }
    public String DisplayOnGroup { get; set; }
    public String PropName;
    public String DelPropName;

    public delegate void UpdateExtProp(String Name, String Value, String OriginalName);
    public event UpdateExtProp extPropDel;


    public ExtPropSetUC()
    {
      InitializeComponent();
    }
    public void SetExtProps()
    {
      dasExtProp.DataSource = ExtProp;
    }



    private void dgvExtProp_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dgvExtProp_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (dgvExtProp.Rows[e.RowIndex].Cells[0].Value != null)
        PropName = dgvExtProp.Rows[e.RowIndex].Cells[0].Value.ToString();
    }


    private void dgvExtProp_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (dgvExtProp.Rows[e.RowIndex].Cells[0].Value != null)
        PropName = dgvExtProp.Rows[e.RowIndex].Cells[0].Value.ToString();
      else
        PropName = "";
    }



    private void dgvExtProp_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
      if (PropName != "")
      {
        extPropDel.Invoke(
            "",
            "",
            DelPropName);
      }
    }

    private void dgvExtProp_SelectionChanged(object sender, EventArgs e)
    {


    }

    private void dgvExtProp_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
      if (e.Row.Cells[0].Value != null)
        DelPropName = e.Row.Cells[0].Value.ToString();
      else
        DelPropName = "";
    }

    private void dgvExtProp_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
      if (e.Control is DataGridViewTextBoxEditingControl)
      {
        if (Properties.Settings.Default.ExtPropAutoComplete != null)
        {
        ExtPropAutoCompleteList ExtPropList = Properties.Settings.Default.ExtPropAutoComplete;
          if (ExtPropList != null)
          {
            DataGridViewTextBoxEditingControl te = (DataGridViewTextBoxEditingControl)e.Control;
            te.AutoCompleteMode = AutoCompleteMode.Suggest;
            te.AutoCompleteSource = AutoCompleteSource.CustomSource;
            if (dgvExtProp.CurrentCell.ColumnIndex == 0)
            {
              foreach (ExtPropAutoComplete epaci in ExtPropList)
              {
                if (epaci.DisplayOnGroup.ToLower() == "all" || epaci.DisplayOnGroup == DisplayOnGroup)
                  te.AutoCompleteCustomSource.Add(epaci.Name);
              }
            }
            if (dgvExtProp.CurrentCell.ColumnIndex == 1)
            {
              foreach (ExtPropAutoComplete epaci in ExtPropList)
              {
                if (dgvExtProp.CurrentRow.Cells[0].Value.ToString() == epaci.Name && epaci.Enabled)
                {
                  foreach (ExtPropAutoCompleteValue item in epaci.AvalibalValues)
                  {
                    te.AutoCompleteCustomSource.Add(item.Value);
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
