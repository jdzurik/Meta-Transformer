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
    public partial class ForeignKeyUC : UserControl
    {
        public TableSet ActiveTable;
        public ForeignKeySet ActiveFK;
        public TreeNode ActiveTreenode;
        public Gen ActiveSet;

        public ForeignKeyUC()
        {
            InitializeComponent();
        }

        public void SetForm()
        {
            cblRelatedColumns.Items.Clear();

            foreach (ColumnSet cs in ActiveTable.Columns)
            {
                //if (cs.Checked)
                cblRelatedColumns.Items.Add(cs);

            }
            cblRelatedColumns.DisplayMember = "Name";

            if (ActiveFK.Columns != null)
            {
                //foreach (Guid fkcs in ActiveFK.Columns)
                //{
                //    try
                //    {
                //        cblRelatedColumns.SetItemChecked(cblRelatedColumns.Items.IndexOf(ActiveTable.GetColumnByGUID(fkcs)), true);
                //    }
                //    catch (Exception ex)
                //    {

                //        MessageBox.Show("Error Selecting Column: " + ex.Message);
                //    }

                //}
            }
            cbxForeignTable.DataSource = ActiveSet.Tables;//.Where<TableSet>(x => x.Checked).OrderBy(x => x.Name).ToList<TableSet>();
            cbxForeignTable.DisplayMember = "Name";

            if (ActiveSet.GetTableByName(ActiveFK.ReferencedTable) != null)
            {
                //cbxForeignTable.Text = ActiveFK.ForeignTable.Name;
                TableSet ats = ActiveSet.GetTableByName(ActiveFK.ReferencedTable);
                cbxForeignTable.SelectedItem = ats;

                cbxForeignColumn.DataSource = ats.Columns;
                cbxForeignColumn.DisplayMember = "Name";

                cbxForeignFilter.DataSource = ats.Filters.OrderBy(x => x.Name).ToList<FilterSet>();
                cbxForeignFilter.DisplayMember = "Name";


                if (ActiveFK.ReferencedKey != "")
                {
                    //cbxForeignColumn.Text = ActiveFK.ForeignColumn.Name;
                    cbxForeignColumn.SelectedItem = ats.GetColumnByColumnName(ActiveFK.ReferencedKey);
                    cbxForeignColumn.Enabled = true;

                }
                //if (ActiveFK.ForeignKeyFilterID != Guid.Empty)
                //{

                //    cbxForeignFilter.SelectedItem = ats.GetFilterByGUID(ActiveFK.ForeignKeyFilterID);
                //    cbxForeignFilter.Enabled = true;

                //}

            }

            txtKeyName.Text = ActiveFK.Name;
            chxIsSingle.Checked = ActiveFK.IsSingleReturn;
            chxIsLazyLoaded.Checked = ActiveFK.IsLazyLoaded;
            chxIsOveride.Checked = ActiveFK.IsOverrideColumn;
            chxIsStream.Checked = ActiveFK.IsStreamReturn;

        }

        private void txtKeyName_TextChanged(object sender, EventArgs e)
        {
            ActiveFK.Name = txtKeyName.Text;
        }

        private void cblRelatedColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ActiveFK.Columns.Clear();
            //foreach (ColumnSet csi in cblRelatedColumns.CheckedItems)
            //{
            //    ActiveFK.Columns.Add(csi.ItemGuid);
            //}
        }

        private void cbxForeignTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbxForeignTable.Enabled)
            //{
            //    if (!((TableSet)cbxForeignTable.SelectedItem).Equals(ActiveTable))
            //    {
            //        ActiveFK.ForeignTableID = ((TableSet)cbxForeignTable.SelectedItem).ItemGuid;

            //        cbxForeignColumn.DataSource = GetChecked(((TableSet)cbxForeignTable.SelectedItem).Columns);
            //        cbxForeignColumn.DisplayMember = "Name";
            //        cbxForeignColumn.Enabled = true;


            //    }
            //    else
            //    {
            //        MessageBox.Show("Cant add reference to self.");
            //    }
            //}
            //else
            //{
            //    cbxForeignTable.Enabled = true;
            //}
        }

        private void cbxForeignColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbxForeignColumn.Enabled)
            //{
            //    ActiveFK.ForeignColumnID = ((ColumnSet)cbxForeignColumn.SelectedItem).ItemGuid;
            //}
        }

        private void cblRelatedColumns_SelectedValueChanged(object sender, EventArgs e)
        {
            //foreach (ColumnSet csi in cblRelatedColumns.CheckedItems)
            //{
            //    ActiveFK.Columns.Add(csi.ItemGuid);
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are You sure you would like to delete this Foreign Key?", "Confirm your desire to delete.", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                ActiveTable.ForeignKeys.Remove(ActiveFK);
                ActiveTreenode.Remove();

            }
        }

        private void cbxForeignFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbxForeignFilter.Enabled)
            //{
            //    ActiveFK.ForeignFilterID = ((FilterSet)cbxForeignFilter.SelectedItem).ItemGuid;
            //}
        }

        private List<ColumnSet> GetChecked(List<ColumnSet> cs)
        {
            List<ColumnSet> cso = new List<ColumnSet>();
            cso = cs.Where(x => x.Checked).OrderBy(x => x.Name).ToList<ColumnSet>();
            return cso;

        }

        private void chxIsSingle_CheckedChanged(object sender, EventArgs e)
        {
            ActiveFK.IsSingleReturn = chxIsSingle.Checked;
        }

        private void chxIsLazyLoaded_CheckedChanged(object sender, EventArgs e)
        {
            ActiveFK.IsLazyLoaded = chxIsLazyLoaded.Checked;
        }

        private void chxIsOveride_CheckedChanged(object sender, EventArgs e)
        {
            ActiveFK.IsOverrideColumn = chxIsOveride.Checked;
        }

        private void chxIsStream_CheckedChanged(object sender, EventArgs e)
        {
            ActiveFK.IsStreamReturn = chxIsStream.Checked;
        }





    }
}
