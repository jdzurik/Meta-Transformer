namespace GenForm
{
  partial class AddForeignKeyUC
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
      this.txtNewFilterName = new System.Windows.Forms.TextBox();
      this.btnAddNewForeignKey = new System.Windows.Forms.Button();
      this.lblNewFK = new System.Windows.Forms.Label();
      this.tplFilterHeader = new System.Windows.Forms.TableLayoutPanel();
      this.tplFilterHeader.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtNewFilterName
      // 
      this.txtNewFilterName.Dock = System.Windows.Forms.DockStyle.Top;
      this.txtNewFilterName.Location = new System.Drawing.Point(132, 3);
      this.txtNewFilterName.Name = "txtNewFilterName";
      this.txtNewFilterName.Size = new System.Drawing.Size(211, 20);
      this.txtNewFilterName.TabIndex = 5;
      this.txtNewFilterName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewFilterName_KeyDown);
      // 
      // btnAddNewForeignKey
      // 
      this.btnAddNewForeignKey.Location = new System.Drawing.Point(349, 3);
      this.btnAddNewForeignKey.Name = "btnAddNewForeignKey";
      this.btnAddNewForeignKey.Size = new System.Drawing.Size(142, 26);
      this.btnAddNewForeignKey.TabIndex = 4;
      this.btnAddNewForeignKey.Text = "Add New Foreign Key";
      this.btnAddNewForeignKey.UseVisualStyleBackColor = true;
      this.btnAddNewForeignKey.Click += new System.EventHandler(this.btnAddNewForeignKey_Click);
      // 
      // lblNewFK
      // 
      this.lblNewFK.AutoSize = true;
      this.lblNewFK.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblNewFK.Location = new System.Drawing.Point(3, 3);
      this.lblNewFK.Margin = new System.Windows.Forms.Padding(3);
      this.lblNewFK.Name = "lblNewFK";
      this.lblNewFK.Size = new System.Drawing.Size(123, 26);
      this.lblNewFK.TabIndex = 6;
      this.lblNewFK.Text = "New Foreign Key Name";
      this.lblNewFK.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // tplFilterHeader
      // 
      this.tplFilterHeader.ColumnCount = 3;
      this.tplFilterHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
      this.tplFilterHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.85593F));
      this.tplFilterHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
      this.tplFilterHeader.Controls.Add(this.txtNewFilterName, 1, 0);
      this.tplFilterHeader.Controls.Add(this.lblNewFK, 0, 0);
      this.tplFilterHeader.Controls.Add(this.btnAddNewForeignKey, 2, 0);
      this.tplFilterHeader.Location = new System.Drawing.Point(3, 3);
      this.tplFilterHeader.Name = "tplFilterHeader";
      this.tplFilterHeader.RowCount = 1;
      this.tplFilterHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tplFilterHeader.Size = new System.Drawing.Size(494, 32);
      this.tplFilterHeader.TabIndex = 7;
      // 
      // AddForeignKeyUC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tplFilterHeader);
      this.Name = "AddForeignKeyUC";
      this.Size = new System.Drawing.Size(516, 46);
      this.tplFilterHeader.ResumeLayout(false);
      this.tplFilterHeader.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtNewFilterName;
    private System.Windows.Forms.Button btnAddNewForeignKey;
    private System.Windows.Forms.Label lblNewFK;
    private System.Windows.Forms.TableLayoutPanel tplFilterHeader;
  }
}
