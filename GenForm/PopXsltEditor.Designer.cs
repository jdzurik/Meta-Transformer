namespace GenForm
{
  partial class PopXsltEditor
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.xsltEditor1 = new GenForm.XsltEditor();
      this.SuspendLayout();
      // 
      // xsltEditor1
      // 
      this.xsltEditor1.activeXslt = null;
      this.xsltEditor1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.xsltEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.xsltEditor1.Location = new System.Drawing.Point(0, 0);
      this.xsltEditor1.Name = "xsltEditor1";
      this.xsltEditor1.Size = new System.Drawing.Size(634, 585);
      this.xsltEditor1.TabIndex = 0;
      // 
      // PopXsltEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(634, 585);
      this.Controls.Add(this.xsltEditor1);
      this.Name = "PopXsltEditor";
      this.Text = "Xslt Editor";
      this.ResumeLayout(false);

    }

    #endregion

    private XsltEditor xsltEditor1;

  }
}