using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Structure;

namespace GenForm
{
  public partial class PopXsltEditor : Form
  {
    public XsltSet activeXslt { get; set; }
    public Gen activeSet { get; set; }

    public int MyProperty { get; set; }
    public PopXsltEditor()
    {
      InitializeComponent();
    }
    public void SetXslt()
    {
      xsltEditor1.activeSet = activeSet;
      xsltEditor1.activeXslt = activeXslt;
      xsltEditor1.setXslt();
    }
  }
}
