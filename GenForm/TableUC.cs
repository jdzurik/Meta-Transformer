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
  public partial class TableUC : UserControl
  {
    public TableSet ActiveTable { get; set; }
    public TableUC()
    {
      InitializeComponent();

     
    }

    public void SetForm() 
    {
      txtName.Text = ActiveTable.Name;
      lblTableName.Text = ActiveTable.TableName;
      lblSchema.Text = ActiveTable.SchemaName;

      if (ActiveTable.MetaData != null)
      {
        //lblSchema.Text = ActiveTable.MetaData.INI;
        if (!String.IsNullOrEmpty(ActiveTable.MetaData.Comment))
          txtDescrption.Text = ActiveTable.MetaData.Comment;
        else
          txtDescrption.Text = ActiveTable.MetaData.AltDescription;
        //rtbExcelDesc.Text = ActiveTable.MetaData.AltDescription; 
        rtbDesc.Text = ActiveTable.MetaData.Description;
      }
      extPropSetUC1.dasExtProp.DataSource = ActiveTable.ExtProp;
    }


    private void txtDescrption_TextChanged(object sender, EventArgs e)
    {
       
    }

    private void txtName_TextChanged(object sender, EventArgs e)
    {
       ActiveTable.Name = txtName.Text;
    }

    private void rtbDesc_TextChanged(object sender, EventArgs e)
    {
      ActiveTable.MetaData.Description = rtbDesc.Text;
    }

    


  }
}
