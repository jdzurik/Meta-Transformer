namespace GenForm
{
  partial class ForeignKeyUC
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tlpFKItems = new System.Windows.Forms.TableLayoutPanel();
      this.chxIsOveride = new System.Windows.Forms.CheckBox();
      this.chxIsLazyLoaded = new System.Windows.Forms.CheckBox();
      this.label4 = new System.Windows.Forms.Label();
      this.lblfkname = new System.Windows.Forms.Label();
      this.txtKeyName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbxForeignTable = new System.Windows.Forms.ComboBox();
      this.lblRelated = new System.Windows.Forms.Label();
      this.cblRelatedColumns = new System.Windows.Forms.CheckedListBox();
      this.cbxForeignColumn = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cbxForeignFilter = new System.Windows.Forms.ComboBox();
      this.chxIsSingle = new System.Windows.Forms.CheckBox();
      this.chxIsStream = new System.Windows.Forms.CheckBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.btnDelete = new System.Windows.Forms.Button();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.extPropSetUC1 = new GenForm.ExtPropSetUC();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.label3 = new System.Windows.Forms.Label();
      this.rtbDesc = new System.Windows.Forms.RichTextBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tlpFKItems.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tlpFKItems);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
      this.splitContainer1.Size = new System.Drawing.Size(613, 324);
      this.splitContainer1.SplitterDistance = 390;
      this.splitContainer1.TabIndex = 0;
      // 
      // tlpFKItems
      // 
      this.tlpFKItems.ColumnCount = 2;
      this.tlpFKItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.tlpFKItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpFKItems.Controls.Add(this.chxIsOveride, 0, 6);
      this.tlpFKItems.Controls.Add(this.chxIsLazyLoaded, 1, 5);
      this.tlpFKItems.Controls.Add(this.label4, 0, 4);
      this.tlpFKItems.Controls.Add(this.lblfkname, 0, 0);
      this.tlpFKItems.Controls.Add(this.txtKeyName, 1, 0);
      this.tlpFKItems.Controls.Add(this.label1, 0, 2);
      this.tlpFKItems.Controls.Add(this.cbxForeignTable, 1, 2);
      this.tlpFKItems.Controls.Add(this.lblRelated, 0, 1);
      this.tlpFKItems.Controls.Add(this.cblRelatedColumns, 1, 1);
      this.tlpFKItems.Controls.Add(this.cbxForeignColumn, 1, 3);
      this.tlpFKItems.Controls.Add(this.label2, 0, 3);
      this.tlpFKItems.Controls.Add(this.cbxForeignFilter, 1, 4);
      this.tlpFKItems.Controls.Add(this.chxIsSingle, 0, 5);
      this.tlpFKItems.Controls.Add(this.chxIsStream, 1, 6);
      this.tlpFKItems.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tlpFKItems.Location = new System.Drawing.Point(0, 0);
      this.tlpFKItems.Name = "tlpFKItems";
      this.tlpFKItems.RowCount = 7;
      this.tlpFKItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tlpFKItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tlpFKItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tlpFKItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tlpFKItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tlpFKItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tlpFKItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tlpFKItems.Size = new System.Drawing.Size(390, 324);
      this.tlpFKItems.TabIndex = 0;
      // 
      // chxIsOveride
      // 
      this.chxIsOveride.AutoSize = true;
      this.chxIsOveride.Location = new System.Drawing.Point(3, 297);
      this.chxIsOveride.Name = "chxIsOveride";
      this.chxIsOveride.Size = new System.Drawing.Size(104, 17);
      this.chxIsOveride.TabIndex = 13;
      this.chxIsOveride.Text = "Override Column";
      this.chxIsOveride.UseVisualStyleBackColor = true;
      this.chxIsOveride.CheckedChanged += new System.EventHandler(this.chxIsOveride_CheckedChanged);
      // 
      // chxIsLazyLoaded
      // 
      this.chxIsLazyLoaded.AutoSize = true;
      this.chxIsLazyLoaded.Location = new System.Drawing.Point(123, 267);
      this.chxIsLazyLoaded.Name = "chxIsLazyLoaded";
      this.chxIsLazyLoaded.Size = new System.Drawing.Size(95, 17);
      this.chxIsLazyLoaded.TabIndex = 12;
      this.chxIsLazyLoaded.Text = "Is LazyLoaded";
      this.chxIsLazyLoaded.UseVisualStyleBackColor = true;
      this.chxIsLazyLoaded.CheckedChanged += new System.EventHandler(this.chxIsLazyLoaded_CheckedChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(3, 234);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(88, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Foreign Key Filter";
      // 
      // lblfkname
      // 
      this.lblfkname.AutoSize = true;
      this.lblfkname.Location = new System.Drawing.Point(3, 0);
      this.lblfkname.Name = "lblfkname";
      this.lblfkname.Size = new System.Drawing.Size(94, 13);
      this.lblfkname.TabIndex = 0;
      this.lblfkname.Text = "Foreign Key Name";
      // 
      // txtKeyName
      // 
      this.txtKeyName.Dock = System.Windows.Forms.DockStyle.Top;
      this.txtKeyName.Location = new System.Drawing.Point(123, 3);
      this.txtKeyName.Name = "txtKeyName";
      this.txtKeyName.Size = new System.Drawing.Size(264, 20);
      this.txtKeyName.TabIndex = 5;
      this.txtKeyName.TextChanged += new System.EventHandler(this.txtKeyName_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 174);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(93, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Foreign Key Table";
      // 
      // cbxForeignTable
      // 
      this.cbxForeignTable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbxForeignTable.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbxForeignTable.Dock = System.Windows.Forms.DockStyle.Top;
      this.cbxForeignTable.Enabled = false;
      this.cbxForeignTable.FormattingEnabled = true;
      this.cbxForeignTable.Items.AddRange(new object[] {
            "Select Table"});
      this.cbxForeignTable.Location = new System.Drawing.Point(123, 177);
      this.cbxForeignTable.Name = "cbxForeignTable";
      this.cbxForeignTable.Size = new System.Drawing.Size(264, 21);
      this.cbxForeignTable.TabIndex = 3;
      this.cbxForeignTable.SelectedIndexChanged += new System.EventHandler(this.cbxForeignTable_SelectedIndexChanged);
      // 
      // lblRelated
      // 
      this.lblRelated.AutoSize = true;
      this.lblRelated.Location = new System.Drawing.Point(3, 30);
      this.lblRelated.Name = "lblRelated";
      this.lblRelated.Size = new System.Drawing.Size(68, 13);
      this.lblRelated.TabIndex = 6;
      this.lblRelated.Text = "Key Columns";
      // 
      // cblRelatedColumns
      // 
      this.cblRelatedColumns.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cblRelatedColumns.FormattingEnabled = true;
      this.cblRelatedColumns.Location = new System.Drawing.Point(123, 33);
      this.cblRelatedColumns.Name = "cblRelatedColumns";
      this.cblRelatedColumns.Size = new System.Drawing.Size(264, 138);
      this.cblRelatedColumns.Sorted = true;
      this.cblRelatedColumns.TabIndex = 7;
      this.cblRelatedColumns.ThreeDCheckBoxes = true;
      this.cblRelatedColumns.SelectedIndexChanged += new System.EventHandler(this.cblRelatedColumns_SelectedIndexChanged);
      this.cblRelatedColumns.SelectedValueChanged += new System.EventHandler(this.cblRelatedColumns_SelectedValueChanged);
      // 
      // cbxForeignColumn
      // 
      this.cbxForeignColumn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbxForeignColumn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbxForeignColumn.Dock = System.Windows.Forms.DockStyle.Top;
      this.cbxForeignColumn.Enabled = false;
      this.cbxForeignColumn.FormattingEnabled = true;
      this.cbxForeignColumn.Items.AddRange(new object[] {
            "Select Column"});
      this.cbxForeignColumn.Location = new System.Drawing.Point(123, 207);
      this.cbxForeignColumn.Name = "cbxForeignColumn";
      this.cbxForeignColumn.Size = new System.Drawing.Size(264, 21);
      this.cbxForeignColumn.TabIndex = 4;
      this.cbxForeignColumn.SelectedIndexChanged += new System.EventHandler(this.cbxForeignColumn_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 204);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(101, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Foreign Key Column";
      // 
      // cbxForeignFilter
      // 
      this.cbxForeignFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbxForeignFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbxForeignFilter.Dock = System.Windows.Forms.DockStyle.Top;
      this.cbxForeignFilter.Enabled = false;
      this.cbxForeignFilter.FormattingEnabled = true;
      this.cbxForeignFilter.Items.AddRange(new object[] {
            "Select Filter"});
      this.cbxForeignFilter.Location = new System.Drawing.Point(123, 237);
      this.cbxForeignFilter.Name = "cbxForeignFilter";
      this.cbxForeignFilter.Size = new System.Drawing.Size(264, 21);
      this.cbxForeignFilter.TabIndex = 9;
      this.cbxForeignFilter.SelectedIndexChanged += new System.EventHandler(this.cbxForeignFilter_SelectedIndexChanged);
      // 
      // chxIsSingle
      // 
      this.chxIsSingle.AutoSize = true;
      this.chxIsSingle.Location = new System.Drawing.Point(3, 267);
      this.chxIsSingle.Name = "chxIsSingle";
      this.chxIsSingle.Size = new System.Drawing.Size(101, 17);
      this.chxIsSingle.TabIndex = 10;
      this.chxIsSingle.Text = "Is Single Return";
      this.chxIsSingle.UseVisualStyleBackColor = true;
      this.chxIsSingle.CheckedChanged += new System.EventHandler(this.chxIsSingle_CheckedChanged);
      // 
      // chxIsStream
      // 
      this.chxIsStream.AutoSize = true;
      this.chxIsStream.Location = new System.Drawing.Point(123, 297);
      this.chxIsStream.Name = "chxIsStream";
      this.chxIsStream.Size = new System.Drawing.Size(94, 17);
      this.chxIsStream.TabIndex = 11;
      this.chxIsStream.Text = "Stream Return";
      this.chxIsStream.UseVisualStyleBackColor = true;
      this.chxIsStream.CheckedChanged += new System.EventHandler(this.chxIsStream_CheckedChanged);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.btnDelete, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(219, 324);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // btnDelete
      // 
      this.btnDelete.Location = new System.Drawing.Point(3, 297);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(133, 23);
      this.btnDelete.TabIndex = 1;
      this.btnDelete.Text = "Delete Foreign Key ";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(3, 3);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.extPropSetUC1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel2);
      this.splitContainer2.Size = new System.Drawing.Size(213, 288);
      this.splitContainer2.SplitterDistance = 144;
      this.splitContainer2.TabIndex = 2;
      // 
      // extPropSetUC1
      // 
      this.extPropSetUC1.DisplayOnGroup = null;
      this.extPropSetUC1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.extPropSetUC1.ExtProp = null;
      this.extPropSetUC1.Location = new System.Drawing.Point(0, 0);
      this.extPropSetUC1.Name = "extPropSetUC1";
      this.extPropSetUC1.Size = new System.Drawing.Size(213, 144);
      this.extPropSetUC1.TabIndex = 1;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 1;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.rtbDesc, 0, 1);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 2;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(213, 140);
      this.tableLayoutPanel2.TabIndex = 10;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(60, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "Description";
      // 
      // rtbDesc
      // 
      this.rtbDesc.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtbDesc.Location = new System.Drawing.Point(3, 23);
      this.rtbDesc.Name = "rtbDesc";
      this.rtbDesc.Size = new System.Drawing.Size(207, 114);
      this.rtbDesc.TabIndex = 9;
      this.rtbDesc.Text = "";
      // 
      // ForeignKeyUC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Name = "ForeignKeyUC";
      this.Size = new System.Drawing.Size(613, 324);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tlpFKItems.ResumeLayout(false);
      this.tlpFKItems.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TableLayoutPanel tlpFKItems;
    private System.Windows.Forms.Label lblfkname;
    private System.Windows.Forms.TextBox txtKeyName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbxForeignColumn;
    private System.Windows.Forms.ComboBox cbxForeignTable;
    private System.Windows.Forms.Label lblRelated;
    private System.Windows.Forms.CheckedListBox cblRelatedColumns;
    private System.Windows.Forms.RichTextBox rtbDesc;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private ExtPropSetUC extPropSetUC1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cbxForeignFilter;
    private System.Windows.Forms.CheckBox chxIsSingle;
    private System.Windows.Forms.CheckBox chxIsStream;
    private System.Windows.Forms.CheckBox chxIsOveride;
    private System.Windows.Forms.CheckBox chxIsLazyLoaded;
  }
}
