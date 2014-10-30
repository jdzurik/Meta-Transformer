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
  public partial class TemplateUC : UserControl
  {
    public XsltSet xsltData;
    public Gen activeSet;

    public TemplateUC()
    {
      InitializeComponent();
    }

    public void TeplateUCLoad()
    {
      if (xsltData.XsltInfo != null && xsltData.XsltInfo.Name != "")
      {
        gbxTemplate.Text = xsltData.XsltInfo.Name;
        if (xsltData.Use)
          gbxTemplate.BackColor = Color.White;
        else
          gbxTemplate.BackColor = Color.DarkGray;
      }
      else
      {
        gbxTemplate.Enabled = false;
      }
      txtXsltPath.Text = xsltData.XsltPath;
      chkUseXslt.Checked = xsltData.Use;
      txtOutputDir.Text = xsltData.OutputDirectory;
      cbxCreateFolder.Checked = xsltData.NameAsFolder;
      chkRunOnDB.Checked = xsltData.RunOnDatabase;
      txtExt.Text = xsltData.OutputFileExtention;
      cbxRunOnce.Checked = xsltData.CreateOneFile;

      txtExtPropFile.Text = xsltData.FileAsPropExtName;
      txtExtPropFile.AutoCompleteCustomSource.Add("Default to Table Name");
      foreach (ExtPropAutoComplete item in activeSet.AvailableExtProps)
      {
        txtExtPropFile.AutoCompleteCustomSource.Add(item.Name);
      }

      txtExtPropFolder.Text = xsltData.FolderAsPropExtName;
      txtExtPropFolder.AutoCompleteCustomSource.Add("Default to Table Name");
      foreach (ExtPropAutoComplete item in activeSet.AvailableExtProps)
      {
        txtExtPropFolder.AutoCompleteCustomSource.Add(item.Name);
      }


      txtOneFileName.Text = xsltData.OneFileName;
    }

    private void chkUseXslt_CheckedChanged(object sender, EventArgs e)
    {
      xsltData.Use = chkUseXslt.Checked;
    }

    private void chkRunOnDB_CheckedChanged(object sender, EventArgs e)
    {
      xsltData.RunOnDatabase = chkRunOnDB.Checked;
    }

    private void txtExt_TextChanged(object sender, EventArgs e)
    {
      xsltData.OutputFileExtention = txtExt.Text;
    }

    private void btnChangeOutput_Click(object sender, EventArgs e)
    {

      DialogResult dr = folderBrowserDialog1.ShowDialog();

      if (dr == DialogResult.OK)
      {
        xsltData.OutputDirectory = folderBrowserDialog1.SelectedPath;
        txtOutputDir.Text = xsltData.OutputDirectory;
      }
    }

    private void txtOutputDir_TextChanged(object sender, EventArgs e)
    {
      xsltData.OutputDirectory = txtOutputDir.Text;
    }

    private void cbxCreateFolder_CheckedChanged(object sender, EventArgs e)
    {
      xsltData.NameAsFolder = cbxCreateFolder.Checked;
    }

    private void cbxRunOnce_CheckedChanged(object sender, EventArgs e)
    {
      xsltData.CreateOneFile = cbxRunOnce.Checked;
    }

    private void txtXsltPath_TextChanged(object sender, EventArgs e)
    {
      xsltData.XsltPath = txtXsltPath.Text;
    }

    private void btnChangeXSLT_Click(object sender, EventArgs e)
    {
      openFileDialog1.DefaultExt = "xslt";
      openFileDialog1.Filter = "Extensible Stylesheet Language Transformations (*.xslt)|*.xslt";
      openFileDialog1.RestoreDirectory = true;

      DialogResult dr = openFileDialog1.ShowDialog();
      if (dr == DialogResult.OK)
      {
        xsltData.OutputDirectory = openFileDialog1.FileName;
        txtXsltPath.Text = xsltData.OutputDirectory;
      }
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      DialogResult dr = MessageBox.Show("Are you sure you would like to remove this XSLT?", "Remove XSLT", MessageBoxButtons.YesNo);
      if (dr == DialogResult.Yes)
      {
        activeSet.XsltFiles.Remove(xsltData);
        Parent.Controls.Remove(this);
      }

    }

    private void txtOneFileName_TextChanged(object sender, EventArgs e)
    {
      xsltData.OneFileName = txtOneFileName.Text;
    }

    private void txtExtPropFile_TextChanged(object sender, EventArgs e)
    {
      xsltData.FileAsPropExtName = txtExtPropFile.Text;
    }

    private void txtExtPropFolder_TextChanged(object sender, EventArgs e)
    {
      xsltData.FolderAsPropExtName = txtExtPropFolder.Text;
    }

    private void chkUseXslt_CheckedChanged_1(object sender, EventArgs e)
    {
      xsltData.Use = chkUseXslt.Checked;
      if (xsltData.Use)
        gbxTemplate.BackColor = Color.White;
      else
        gbxTemplate.BackColor = Color.DarkGray;
    }

    private void btnNewWindow_Click(object sender, EventArgs e)
    {
      PopXsltEditor dlg = new PopXsltEditor();
      dlg.activeXslt = xsltData;
      dlg.activeSet = activeSet;
      dlg.SetXslt();
      dlg.Show();
    }
  }
}
