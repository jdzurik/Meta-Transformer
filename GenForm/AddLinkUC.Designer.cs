namespace GenForm
{
  partial class AddLinkUC
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
      this.tplFilterHeader = new System.Windows.Forms.TableLayoutPanel();
      this.lblNewLink = new System.Windows.Forms.Label();
      this.btnAddNewLink = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.chxIsSingle = new System.Windows.Forms.CheckBox();
      this.cbxTable = new System.Windows.Forms.ComboBox();
      this.cbxColumn = new System.Windows.Forms.ComboBox();
      this.chxIsLazyLoaded = new System.Windows.Forms.CheckBox();
      this.chxOverride = new System.Windows.Forms.CheckBox();
      this.cbxLinkFilter = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.tplFilterHeader.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtNewFilterName
      // 
      this.txtNewFilterName.Dock = System.Windows.Forms.DockStyle.Top;
      this.txtNewFilterName.Location = new System.Drawing.Point(100, 3);
      this.txtNewFilterName.Name = "txtNewFilterName";
      this.txtNewFilterName.Size = new System.Drawing.Size(391, 20);
      this.txtNewFilterName.TabIndex = 5;
      // 
      // tplFilterHeader
      // 
      this.tplFilterHeader.ColumnCount = 2;
      this.tplFilterHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
      this.tplFilterHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tplFilterHeader.Controls.Add(this.label3, 0, 3);
      this.tplFilterHeader.Controls.Add(this.txtNewFilterName, 1, 0);
      this.tplFilterHeader.Controls.Add(this.lblNewLink, 0, 0);
      this.tplFilterHeader.Controls.Add(this.btnAddNewLink, 1, 7);
      this.tplFilterHeader.Controls.Add(this.label1, 0, 1);
      this.tplFilterHeader.Controls.Add(this.chxIsSingle, 1, 4);
      this.tplFilterHeader.Controls.Add(this.cbxTable, 1, 1);
      this.tplFilterHeader.Controls.Add(this.chxIsLazyLoaded, 1, 5);
      this.tplFilterHeader.Controls.Add(this.chxOverride, 1, 6);
      this.tplFilterHeader.Controls.Add(this.label2, 0, 2);
      this.tplFilterHeader.Controls.Add(this.cbxColumn, 1, 2);
      this.tplFilterHeader.Controls.Add(this.cbxLinkFilter, 1, 3);
      this.tplFilterHeader.Location = new System.Drawing.Point(3, 3);
      this.tplFilterHeader.Name = "tplFilterHeader";
      this.tplFilterHeader.RowCount = 8;
      this.tplFilterHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tplFilterHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tplFilterHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tplFilterHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tplFilterHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tplFilterHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tplFilterHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tplFilterHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tplFilterHeader.Size = new System.Drawing.Size(494, 243);
      this.tplFilterHeader.TabIndex = 8;
      // 
      // lblNewLink
      // 
      this.lblNewLink.AutoSize = true;
      this.lblNewLink.Location = new System.Drawing.Point(3, 3);
      this.lblNewLink.Margin = new System.Windows.Forms.Padding(3);
      this.lblNewLink.Name = "lblNewLink";
      this.lblNewLink.Size = new System.Drawing.Size(83, 13);
      this.lblNewLink.TabIndex = 6;
      this.lblNewLink.Text = "New Link Name";
      this.lblNewLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // btnAddNewLink
      // 
      this.btnAddNewLink.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnAddNewLink.Location = new System.Drawing.Point(349, 213);
      this.btnAddNewLink.Name = "btnAddNewLink";
      this.btnAddNewLink.Size = new System.Drawing.Size(142, 27);
      this.btnAddNewLink.TabIndex = 4;
      this.btnAddNewLink.Text = "Add New Link";
      this.btnAddNewLink.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 33);
      this.label1.Margin = new System.Windows.Forms.Padding(3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(57, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Link Table";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 63);
      this.label2.Margin = new System.Windows.Forms.Padding(3);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(65, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Link Column";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // chxIsSingle
      // 
      this.chxIsSingle.AutoSize = true;
      this.chxIsSingle.Location = new System.Drawing.Point(100, 123);
      this.chxIsSingle.Name = "chxIsSingle";
      this.chxIsSingle.Size = new System.Drawing.Size(125, 17);
      this.chxIsSingle.TabIndex = 9;
      this.chxIsSingle.Text = "Is Single Reference?";
      this.chxIsSingle.UseVisualStyleBackColor = true;
      // 
      // cbxTable
      // 
      this.cbxTable.FormattingEnabled = true;
      this.cbxTable.Location = new System.Drawing.Point(100, 33);
      this.cbxTable.Name = "cbxTable";
      this.cbxTable.Size = new System.Drawing.Size(288, 21);
      this.cbxTable.TabIndex = 10;
      // 
      // cbxColumn
      // 
      this.cbxColumn.Enabled = false;
      this.cbxColumn.FormattingEnabled = true;
      this.cbxColumn.Location = new System.Drawing.Point(100, 63);
      this.cbxColumn.Name = "cbxColumn";
      this.cbxColumn.Size = new System.Drawing.Size(288, 21);
      this.cbxColumn.TabIndex = 11;
      // 
      // chxIsLazyLoaded
      // 
      this.chxIsLazyLoaded.AutoSize = true;
      this.chxIsLazyLoaded.Location = new System.Drawing.Point(100, 153);
      this.chxIsLazyLoaded.Name = "chxIsLazyLoaded";
      this.chxIsLazyLoaded.Size = new System.Drawing.Size(104, 17);
      this.chxIsLazyLoaded.TabIndex = 12;
      this.chxIsLazyLoaded.Text = "Is Lazy Loaded?";
      this.chxIsLazyLoaded.UseVisualStyleBackColor = true;
      // 
      // chxOverride
      // 
      this.chxOverride.AutoSize = true;
      this.chxOverride.Location = new System.Drawing.Point(100, 183);
      this.chxOverride.Name = "chxOverride";
      this.chxOverride.Size = new System.Drawing.Size(137, 17);
      this.chxOverride.TabIndex = 13;
      this.chxOverride.Text = "Is Override of Property?";
      this.chxOverride.UseVisualStyleBackColor = true;
      // 
      // cbxLinkFilter
      // 
      this.cbxLinkFilter.FormattingEnabled = true;
      this.cbxLinkFilter.Location = new System.Drawing.Point(100, 93);
      this.cbxLinkFilter.Name = "cbxLinkFilter";
      this.cbxLinkFilter.Size = new System.Drawing.Size(288, 21);
      this.cbxLinkFilter.TabIndex = 14;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 93);
      this.label3.Margin = new System.Windows.Forms.Padding(3);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(52, 13);
      this.label3.TabIndex = 15;
      this.label3.Text = "Link Filter";
      this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // AddLinkUC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tplFilterHeader);
      this.Name = "AddLinkUC";
      this.Size = new System.Drawing.Size(558, 260);
      this.tplFilterHeader.ResumeLayout(false);
      this.tplFilterHeader.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtNewFilterName;
    private System.Windows.Forms.TableLayoutPanel tplFilterHeader;
    private System.Windows.Forms.Label lblNewLink;
    private System.Windows.Forms.Button btnAddNewLink;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox chxIsSingle;
    private System.Windows.Forms.ComboBox cbxTable;
    private System.Windows.Forms.ComboBox cbxColumn;
    private System.Windows.Forms.CheckBox chxIsLazyLoaded;
    private System.Windows.Forms.CheckBox chxOverride;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cbxLinkFilter;
  }
}
