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
  public partial class IndexUC : UserControl
  {
    public IndexSet ActiveIndex { get; set; }

    public IndexUC()
    {
      InitializeComponent();
    }

    public void SetForm()
    {
      richTextBox1.Text = ActiveIndex.IsUnique.ToString();
      foreach (IndexColumnSet item in ActiveIndex.Columns)
      {
        richTextBox1.Text += "\n " + item.ColumnName + "\n"; 
      }
    }
  }
}
