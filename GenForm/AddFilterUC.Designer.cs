namespace GenForm
{
  partial class AddFilterUC
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.tplFilterHeader = new System.Windows.Forms.TableLayoutPanel();
      this.txtNewFilterName = new System.Windows.Forms.TextBox();
      this.lblNewFilter = new System.Windows.Forms.Label();
      this.btnAddNewFilter = new System.Windows.Forms.Button();
      this.tplFilterHeader.SuspendLayout();
      this.SuspendLayout();
      // 
      // tplFilterHeader
      // 
      this.tplFilterHeader.ColumnCount = 3;
      this.tplFilterHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tplFilterHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.85593F));
      this.tplFilterHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
      this.tplFilterHeader.Controls.Add(this.txtNewFilterName, 1, 0);
      this.tplFilterHeader.Controls.Add(this.lblNewFilter, 0, 0);
      this.tplFilterHeader.Controls.Add(this.btnAddNewFilter, 2, 0);
      this.tplFilterHeader.Location = new System.Drawing.Point(8, 3);
      this.tplFilterHeader.Name = "tplFilterHeader";
      this.tplFilterHeader.RowCount = 1;
      this.tplFilterHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tplFilterHeader.Size = new System.Drawing.Size(426, 37);
      this.tplFilterHeader.TabIndex = 6;
      this.tplFilterHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.tplFilterHeader_Paint);
      // 
      // txtNewFilterName
      // 
      this.txtNewFilterName.Dock = System.Windows.Forms.DockStyle.Top;
      this.txtNewFilterName.Location = new System.Drawing.Point(103, 3);
      this.txtNewFilterName.Name = "txtNewFilterName";
      this.txtNewFilterName.Size = new System.Drawing.Size(213, 20);
      this.txtNewFilterName.TabIndex = 5;
      this.txtNewFilterName.TextChanged += new System.EventHandler(this.txtNewFilterName_TextChanged);
      this.txtNewFilterName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewFilterName_KeyDown);
      // 
      // lblNewFilter
      // 
      this.lblNewFilter.AutoSize = true;
      this.lblNewFilter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblNewFilter.Location = new System.Drawing.Point(3, 3);
      this.lblNewFilter.Margin = new System.Windows.Forms.Padding(3);
      this.lblNewFilter.Name = "lblNewFilter";
      this.lblNewFilter.Size = new System.Drawing.Size(94, 31);
      this.lblNewFilter.TabIndex = 6;
      this.lblNewFilter.Text = "New Filter Name";
      this.lblNewFilter.TextAlign = System.Drawing.ContentAlignment.TopRight;
      this.lblNewFilter.Click += new System.EventHandler(this.lblNewFilter_Click);
      // 
      // btnAddNewFilter
      // 
      this.btnAddNewFilter.Location = new System.Drawing.Point(322, 3);
      this.btnAddNewFilter.Name = "btnAddNewFilter";
      this.btnAddNewFilter.Size = new System.Drawing.Size(92, 25);
      this.btnAddNewFilter.TabIndex = 4;
      this.btnAddNewFilter.Text = "Add New Filter";
      this.btnAddNewFilter.UseVisualStyleBackColor = true;
      this.btnAddNewFilter.Click += new System.EventHandler(this.btnAddNewFilter_Click);
      // 
      // AddFilterUC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tplFilterHeader);
      this.Name = "AddFilterUC";
      this.Size = new System.Drawing.Size(437, 49);
      this.tplFilterHeader.ResumeLayout(false);
      this.tplFilterHeader.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tplFilterHeader;
    private System.Windows.Forms.TextBox txtNewFilterName;
    private System.Windows.Forms.Label lblNewFilter;
    private System.Windows.Forms.Button btnAddNewFilter;
  }
}
